-- ═══════════════════════════════════════════════════════════════
-- Salati — Prayer Tracking Stored Procedures
-- تاريخ الإنشاء: 2026-04-16
-- الوصف: SPs لميزة تتبع الصلوات (Mark Prayer + Stats + Charts)
-- ═══════════════════════════════════════════════════════════════

USE SalatiDB;
GO

-- ═══════════════════════════════════════════════════════════════
-- SP #1: تعليم صلاة كـ "صليت" ✅
-- القواعد:
--   ❌ ميقدرش يعلّم على صلاة أمبارح
--   ❌ ميقدرش يعلّم على صلاة لسه ما أذّنتش
--   ✅ لو عدّى وقتها يقدر يعلّم (OnTime أو Late)
-- ═══════════════════════════════════════════════════════════════
DROP PROCEDURE IF EXISTS SP_MarkPrayer;
GO

CREATE PROCEDURE SP_MarkPrayer
    @Prayer     TINYINT,        -- أي صلاة (1=Fajr, 2=Dhuhr, 3=Asr, 4=Maghrib, 5=Isha)
    @Status     TINYINT,        -- 1=OnTime, 2=Late
    @Result     INT OUTPUT      -- 0=نجح, -1=الصلاة لسه, -2=مفيش مواعيد
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Today DATE = CAST(GETDATE() AS DATE);
    DECLARE @Now TIME(0) = CAST(GETDATE() AS TIME(0));
    DECLARE @PrayerTime TIME(0);

    -- 1️⃣ جيب وقت الصلاة من جدول المواعيد
    SELECT @PrayerTime = CASE @Prayer
        WHEN 1 THEN FajrTime
        WHEN 2 THEN DhuhrTime
        WHEN 3 THEN AsrTime
        WHEN 4 THEN MaghribTime
        WHEN 5 THEN IshaTime
    END
    FROM DailyPrayerTimes
    WHERE [Date] = @Today;

    -- 2️⃣ لو مفيش مواعيد لليوم
    IF @PrayerTime IS NULL
    BEGIN
        SET @Result = -2;
        RETURN;
    END

    -- 3️⃣ لو الصلاة لسه ما أذّنتش
    IF @PrayerTime > @Now
    BEGIN
        SET @Result = -1;
        RETURN;
    END

    -- 4️⃣ UPSERT: لو موجود حدّث، لو مش موجود أضف
    IF EXISTS (SELECT 1 FROM PrayerTracking 
               WHERE [Date] = @Today AND Prayer = @Prayer)
    BEGIN
        UPDATE PrayerTracking
        SET [Status] = @Status, 
            MarkedAt = GETDATE()
        WHERE [Date] = @Today AND Prayer = @Prayer;
    END
    ELSE
    BEGIN
        INSERT INTO PrayerTracking ([Date], Prayer, PrayerTime, [Status], MarkedAt)
        VALUES (@Today, @Prayer, @PrayerTime, @Status, GETDATE());
    END

    SET @Result = 0;
END
GO

-- ═══════════════════════════════════════════════════════════════
-- SP #2: إلغاء تعليم صلاة (Unmark)
-- ═══════════════════════════════════════════════════════════════
DROP PROCEDURE IF EXISTS SP_UnmarkPrayer;
GO

CREATE PROCEDURE SP_UnmarkPrayer
    @Prayer TINYINT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Today DATE = CAST(GETDATE() AS DATE);

    UPDATE PrayerTracking
    SET [Status] = 0,       -- NotMarked
        MarkedAt = NULL
    WHERE [Date] = @Today AND Prayer = @Prayer;
END
GO

-- ═══════════════════════════════════════════════════════════════
-- SP #3: جيب حالة صلوات اليوم (5 صلوات)
-- بيرجع كل الـ 5 صلوات مع حالتها
-- ═══════════════════════════════════════════════════════════════
DROP PROCEDURE IF EXISTS SP_GetTodayTracking;
GO

CREATE PROCEDURE SP_GetTodayTracking
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Today DATE = CAST(GETDATE() AS DATE);

    SELECT 
        Prayer, 
        PrayerTime, 
        [Status], 
        MarkedAt
    FROM PrayerTracking
    WHERE [Date] = @Today
    ORDER BY Prayer;
END
GO

-- ═══════════════════════════════════════════════════════════════
-- SP #4: ملء الأيام اللي مافتحش فيها البرنامج = Missed (3)
-- بيتنادى مرة واحدة عند فتح البرنامج
-- ═══════════════════════════════════════════════════════════════
DROP PROCEDURE IF EXISTS SP_FillMissedDays;
GO

CREATE PROCEDURE SP_FillMissedDays
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Today DATE = CAST(GETDATE() AS DATE);
    DECLARE @LastDate DATE;

    -- آخر يوم مسجّل في السجل
    SELECT @LastDate = MAX([Date]) FROM PrayerTracking;

    -- لو مفيش سجلات أصلاً — مش بنملا حاجة
    IF @LastDate IS NULL RETURN;

    -- لو آخر يوم = النهارده — مفيش أيام فاضية
    IF @LastDate >= @Today RETURN;

    -- املا كل يوم فاضي بـ Missed
    DECLARE @CurrentDate DATE = DATEADD(DAY, 1, @LastDate);
    DECLARE @P TINYINT;

    WHILE @CurrentDate < @Today
    BEGIN
        SET @P = 1;
        WHILE @P <= 5
        BEGIN
            IF NOT EXISTS (SELECT 1 FROM PrayerTracking 
                          WHERE [Date] = @CurrentDate AND Prayer = @P)
            BEGIN
                INSERT INTO PrayerTracking ([Date], Prayer, PrayerTime, [Status])
                VALUES (@CurrentDate, @P, '00:00:00', 3); -- 3 = Missed
            END
            SET @P = @P + 1;
        END

        SET @CurrentDate = DATEADD(DAY, 1, @CurrentDate);
    END
END
GO

-- ═══════════════════════════════════════════════════════════════
-- SP #5: إحصائيات الشهر (للرسم البياني)
-- بيرجع لكل يوم في الشهر: كام OnTime + Late + Missed
-- ═══════════════════════════════════════════════════════════════
DROP PROCEDURE IF EXISTS SP_GetMonthlyTrackingStats;
GO

CREATE PROCEDURE SP_GetMonthlyTrackingStats
    @Month INT,
    @Year  INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        [Date],
        SUM(CASE WHEN [Status] = 1 THEN 1 ELSE 0 END) AS OnTime,
        SUM(CASE WHEN [Status] = 2 THEN 1 ELSE 0 END) AS Late,
        SUM(CASE WHEN [Status] = 3 THEN 1 ELSE 0 END) AS Missed,
        SUM(CASE WHEN [Status] = 0 THEN 1 ELSE 0 END) AS NotMarked,
        COUNT(*) AS TotalPrayers
    FROM PrayerTracking
    WHERE MONTH([Date]) = @Month AND YEAR([Date]) = @Year
    GROUP BY [Date]
    ORDER BY [Date];
END
GO

-- ═══════════════════════════════════════════════════════════════
-- SP #6: إحصائيات أسبوعية (آخر 7 أيام)
-- ═══════════════════════════════════════════════════════════════
DROP PROCEDURE IF EXISTS SP_GetWeeklyTrackingStats;
GO

CREATE PROCEDURE SP_GetWeeklyTrackingStats
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Prayer,
        SUM(CASE WHEN [Status] = 1 THEN 1 ELSE 0 END) AS OnTime,
        SUM(CASE WHEN [Status] = 2 THEN 1 ELSE 0 END) AS Late,
        SUM(CASE WHEN [Status] = 3 THEN 1 ELSE 0 END) AS Missed,
        COUNT(*) AS TotalPrayers
    FROM PrayerTracking
    WHERE [Date] >= DATEADD(DAY, -7, CAST(GETDATE() AS DATE))
    GROUP BY Prayer
    ORDER BY Prayer;
END
GO

-- ═══════════════════════════════════════════════════════════════
-- SP #7: عدد أيام الالتزام المتتالية (Streak 🔥)
-- بيحسب كام يوم متتالي صلّى فيهم كل الـ 5 صلوات
-- ═══════════════════════════════════════════════════════════════
DROP PROCEDURE IF EXISTS SP_GetStreakCount;
GO

CREATE PROCEDURE SP_GetStreakCount
    @StreakDays INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    SET @StreakDays = 0;

    DECLARE @CheckDate DATE = DATEADD(DAY, -1, CAST(GETDATE() AS DATE));
    DECLARE @AllPrayed BIT;

    -- ابدأ من أمبارح وارجع لورا
    WHILE 1 = 1
    BEGIN
        -- هل صلّى كل الـ 5 صلوات في اليوم ده؟
        IF EXISTS (
            SELECT 1 FROM PrayerTracking
            WHERE [Date] = @CheckDate
            GROUP BY [Date]
            HAVING COUNT(*) = 5 
               AND MIN([Status]) >= 1 
               AND MAX([Status]) <= 2   -- كله OnTime أو Late
        )
        BEGIN
            SET @StreakDays = @StreakDays + 1;
            SET @CheckDate = DATEADD(DAY, -1, @CheckDate);
        END
        ELSE
            BREAK;  -- أول يوم مش كامل = وقّف
    END

    -- النهارده — لو لحد دلوقتي كل الصلوات اللي أذّنت اتعلّمت
    DECLARE @Today DATE = CAST(GETDATE() AS DATE);
    DECLARE @PrayedToday INT;
    DECLARE @TotalPassedToday INT;

    SELECT @PrayedToday = COUNT(*)
    FROM PrayerTracking
    WHERE [Date] = @Today AND [Status] IN (1, 2);

    -- عدد الصلوات اللي أذّنت لحد دلوقتي
    SELECT @TotalPassedToday = COUNT(*)
    FROM PrayerTracking PT
    JOIN DailyPrayerTimes DPT ON DPT.[Date] = @Today
    WHERE PT.[Date] = @Today
      AND PT.PrayerTime <= CAST(GETDATE() AS TIME(0));

    IF @TotalPassedToday > 0 AND @PrayedToday = @TotalPassedToday
        SET @StreakDays = @StreakDays + 1;
END
GO

-- ═══════════════════════════════════════════════════════════════
-- SP #8: نسبة الالتزام الكلية (Overall Percentage)
-- ═══════════════════════════════════════════════════════════════
DROP PROCEDURE IF EXISTS SP_GetOverallPercentage;
GO

CREATE PROCEDURE SP_GetOverallPercentage
    @Percentage DECIMAL(5,2) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Total INT;
    DECLARE @Prayed INT;

    SELECT @Total = COUNT(*),
           @Prayed = SUM(CASE WHEN [Status] IN (1, 2) THEN 1 ELSE 0 END)
    FROM PrayerTracking
    WHERE [Status] != 0;  -- بنستبعد NotMarked

    IF @Total = 0
        SET @Percentage = 0;
    ELSE
        SET @Percentage = CAST(@Prayed AS DECIMAL(5,2)) / @Total * 100;
END
GO

PRINT '✅ All Prayer Tracking SPs created successfully!';
GO


-- ╔═══════════════════════════════════════════════════════════════╗
-- ║                                                             ║
-- ║   📿 أذكار وأحاديث — Adhkar & Reminders                   ║
-- ║   تاريخ الإضافة: 2026-04-16                                ║
-- ║                                                             ║
-- ╚═══════════════════════════════════════════════════════════════╝


-- ═══════════════════════════════════════════════════════════════
-- TABLE: Adhkar — جدول الأذكار والأحاديث
-- Categories:
--   1 = حديث (Hadith)
--   2 = ذكر عام (Dhikr)
--   3 = صلاة على النبي (Salawat)
--   4 = دعاء (Dua)
--   5 = أذكار الصباح (Morning Adhkar)
--   6 = أذكار المساء (Evening Adhkar)
-- ═══════════════════════════════════════════════════════════════

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Adhkar')
BEGIN
    CREATE TABLE Adhkar (
        AdhkarID       INT IDENTITY(1,1) PRIMARY KEY,
        Category       TINYINT NOT NULL,
        TextAr         NVARCHAR(500) NOT NULL,
        TextEn         NVARCHAR(500) NULL,
        Source         NVARCHAR(150) NULL,
        RepeatCount    TINYINT DEFAULT 1,
        SortOrder      TINYINT DEFAULT 0,
        IsActive       BIT DEFAULT 1,
        CreatedDate    DATETIME DEFAULT GETDATE()
    );

    PRINT '✅ Table [Adhkar] created.';
END
ELSE
    PRINT '⚠️ Table [Adhkar] already exists — skipped.';
GO


-- ═══════════════════════════════════════════════════════════════
-- SP #9: جيب ذكر/حديث عشوائي (للإشعارات في الخلفية)
-- يستبعد أذكار الصباح/المساء (5, 6)
-- ═══════════════════════════════════════════════════════════════
DROP PROCEDURE IF EXISTS SP_GetRandomAdhkar;
GO

CREATE PROCEDURE SP_GetRandomAdhkar
    @Category TINYINT = NULL   -- NULL = كل الأصناف (1-4)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        AdhkarID,
        Category,
        TextAr,
        TextEn,
        Source,
        RepeatCount
    FROM Adhkar
    WHERE IsActive = 1
      AND Category NOT IN (5, 6)  -- مش أذكار الصباح/المساء
      AND (@Category IS NULL OR Category = @Category)
    ORDER BY NEWID();  -- عشوائي
END
GO

-- ═══════════════════════════════════════════════════════════════
-- SP #10: جيب أذكار حسب التصنيف (للعرض في شاشة كاملة)
-- ═══════════════════════════════════════════════════════════════
DROP PROCEDURE IF EXISTS SP_GetAdhkarByCategory;
GO

CREATE PROCEDURE SP_GetAdhkarByCategory
    @Category TINYINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        AdhkarID,
        Category,
        TextAr,
        TextEn,
        Source,
        RepeatCount,
        SortOrder
    FROM Adhkar
    WHERE IsActive = 1 AND Category = @Category
    ORDER BY SortOrder, AdhkarID;
END
GO

-- ═══════════════════════════════════════════════════════════════
-- SP #11: أذكار الصباح والمساء (مرتبة بالـ SortOrder)
-- @Type: 5 = صباح, 6 = مساء
-- ═══════════════════════════════════════════════════════════════
DROP PROCEDURE IF EXISTS SP_GetMorningEveningAdhkar;
GO

CREATE PROCEDURE SP_GetMorningEveningAdhkar
    @Type TINYINT   -- 5 = صباح, 6 = مساء
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        AdhkarID,
        TextAr,
        TextEn,
        Source,
        RepeatCount,
        SortOrder
    FROM Adhkar
    WHERE IsActive = 1 AND Category = @Type
    ORDER BY SortOrder;
END
GO


-- ═══════════════════════════════════════════════════════════════
-- 📿 SEED DATA — بيانات الأذكار والأحاديث
-- ═══════════════════════════════════════════════════════════════

-- تنظيف البيانات القديمة (لو موجودة)
DELETE FROM Adhkar;
DBCC CHECKIDENT ('Adhkar', RESEED, 0);
GO

-- ─────────────────────────────────────────
-- 📖 Category 1: أحاديث (Hadith)
-- ─────────────────────────────────────────
INSERT INTO Adhkar (Category, TextAr, TextEn, Source) VALUES
(1, N'إنما الأعمال بالنيات، وإنما لكل امرئ ما نوى', 'Actions are judged by intentions, and everyone will be rewarded according to what he intended.', N'متفق عليه'),
(1, N'من حُسن إسلام المرء تركه ما لا يعنيه', 'Part of the perfection of a person''s Islam is leaving that which does not concern him.', N'رواه الترمذي'),
(1, N'لا يؤمن أحدكم حتى يحب لأخيه ما يحب لنفسه', 'None of you truly believes until he loves for his brother what he loves for himself.', N'متفق عليه'),
(1, N'مَن كان يؤمن بالله واليوم الآخر فليقل خيرًا أو ليصمت', 'Whoever believes in Allah and the Last Day, let him speak good or remain silent.', N'متفق عليه'),
(1, N'الطُّهور شطر الإيمان', 'Cleanliness is half of faith.', N'رواه مسلم'),
(1, N'الدنيا سجن المؤمن وجنة الكافر', 'The world is a prison for the believer and a paradise for the disbeliever.', N'رواه مسلم'),
(1, N'المسلم من سلم المسلمون من لسانه ويده', 'A Muslim is one from whose tongue and hand other Muslims are safe.', N'متفق عليه'),
(1, N'اتقِ الله حيثما كنت، وأتبع السيئة الحسنة تمحها، وخالق الناس بخلق حسن', 'Fear Allah wherever you are, follow a bad deed with a good one to erase it, and treat people with good character.', N'رواه الترمذي'),
(1, N'إن الله لا ينظر إلى صوركم وأموالكم، ولكن ينظر إلى قلوبكم وأعمالكم', 'Allah does not look at your forms or wealth, but He looks at your hearts and deeds.', N'رواه مسلم'),
(1, N'ما ملأ آدميٌّ وعاءً شرًا من بطنه', 'No human ever filled a vessel worse than his stomach.', N'رواه الترمذي');
GO

-- ─────────────────────────────────────────
-- 📿 Category 2: أذكار عامة (Dhikr)
-- ─────────────────────────────────────────
INSERT INTO Adhkar (Category, TextAr, TextEn, Source) VALUES
(2, N'سبحان الله وبحمده، سبحان الله العظيم', 'Glory be to Allah and His is the praise, Glory be to Allah the Almighty.', N'متفق عليه'),
(2, N'لا حول ولا قوة إلا بالله', 'There is no power nor strength except with Allah.', N'متفق عليه'),
(2, N'سبحان الله والحمد لله ولا إله إلا الله والله أكبر', 'Glory be to Allah, praise be to Allah, there is no god but Allah, and Allah is the Greatest.', N'رواه مسلم'),
(2, N'لا إله إلا الله وحده لا شريك له، له الملك وله الحمد وهو على كل شيء قدير', 'There is no god but Allah alone, with no partner. His is the dominion and praise, and He is over all things capable.', N'متفق عليه'),
(2, N'أستغفر الله العظيم الذي لا إله إلا هو الحي القيوم وأتوب إليه', 'I seek forgiveness from Allah the Almighty, there is no god but He, the Living, the Sustainer, and I repent to Him.', N'رواه أبو داود'),
(2, N'حسبي الله لا إله إلا هو عليه توكلت وهو رب العرش العظيم', 'Allah is sufficient for me. There is no god but He. In Him I put my trust, and He is the Lord of the Mighty Throne.', N'رواه أبو داود'),
(2, N'يا حي يا قيوم برحمتك أستغيث', 'O Ever-Living, O Sustainer, in Your mercy I seek relief.', N'رواه الترمذي'),
(2, N'اللهم إنك عفو تحب العفو فاعف عني', 'O Allah, You are Pardoning and love to pardon, so pardon me.', N'رواه الترمذي');
GO

-- ─────────────────────────────────────────
-- 🤲 Category 3: صلاة على النبي (Salawat)
-- ─────────────────────────────────────────
INSERT INTO Adhkar (Category, TextAr, TextEn, Source) VALUES
(3, N'اللهم صلِّ وسلم على نبينا محمد ﷺ', 'O Allah, send blessings and peace upon our Prophet Muhammad ﷺ.', NULL),
(3, N'اللهم صلِّ على محمد وعلى آل محمد كما صليت على إبراهيم وعلى آل إبراهيم إنك حميد مجيد', 'O Allah, send blessings upon Muhammad and the family of Muhammad as You sent blessings upon Ibrahim and the family of Ibrahim. You are Praiseworthy, Glorious.', N'الصلاة الإبراهيمية'),
(3, N'مَن صلّى عليّ صلاة واحدة صلى الله عليه بها عشرًا', 'Whoever sends blessings upon me once, Allah will send blessings upon him tenfold.', N'رواه مسلم'),
(3, N'أولى الناس بي يوم القيامة أكثرهم عليّ صلاة', 'The closest people to me on the Day of Resurrection will be those who sent the most blessings upon me.', N'رواه الترمذي'),
(3, N'صلوا على النبي ﷺ — فإنها نور في القلب وبركة في الرزق', 'Send blessings upon the Prophet ﷺ — it is light in the heart and blessing in provision.', NULL);
GO

-- ─────────────────────────────────────────
-- 🤲 Category 4: أدعية (Dua)
-- ─────────────────────────────────────────
INSERT INTO Adhkar (Category, TextAr, TextEn, Source) VALUES
(4, N'ربنا آتنا في الدنيا حسنة وفي الآخرة حسنة وقنا عذاب النار', 'Our Lord, give us good in this world and good in the Hereafter, and protect us from the punishment of the Fire.', N'سورة البقرة: 201'),
(4, N'رب اشرح لي صدري ويسر لي أمري', 'My Lord, expand for me my chest and ease for me my task.', N'سورة طه: 25-26'),
(4, N'اللهم إني أسألك العفو والعافية في الدنيا والآخرة', 'O Allah, I ask You for pardon and well-being in this world and the Hereafter.', N'رواه ابن ماجه'),
(4, N'اللهم إني أعوذ بك من الهم والحزن والعجز والكسل', 'O Allah, I seek refuge in You from worry, grief, weakness, and laziness.', N'رواه البخاري'),
(4, N'اللهم اغفر لي ذنبي كله، دقه وجله، أوله وآخره', 'O Allah, forgive all my sins, small and great, first and last.', N'رواه مسلم'),
(4, N'ربنا لا تزغ قلوبنا بعد إذ هديتنا وهب لنا من لدنك رحمة', 'Our Lord, do not let our hearts deviate after You have guided us, and grant us mercy from Yourself.', N'سورة آل عمران: 8'),
(4, N'اللهم أعني على ذكرك وشكرك وحسن عبادتك', 'O Allah, help me to remember You, thank You, and worship You well.', N'رواه أبو داود');
GO

-- ─────────────────────────────────────────
-- 🌅 Category 5: أذكار الصباح (Morning Adhkar)
-- ─────────────────────────────────────────
INSERT INTO Adhkar (Category, TextAr, TextEn, Source, RepeatCount, SortOrder) VALUES
(5, N'أصبحنا وأصبح الملك لله، والحمد لله، لا إله إلا الله وحده لا شريك له', 'We have reached the morning and the dominion belongs to Allah. Praise is to Allah. There is no god but Allah alone.', N'رواه مسلم', 1, 1),
(5, N'اللهم بك أصبحنا وبك أمسينا وبك نحيا وبك نموت وإليك النشور', 'O Allah, by You we enter the morning and by You we enter the evening. By You we live and by You we die, and to You is the resurrection.', N'رواه الترمذي', 1, 2),
(5, N'اللهم ما أصبح بي من نعمة فمنك وحدك لا شريك لك، فلك الحمد ولك الشكر', 'O Allah, whatever blessing I have this morning is from You alone, with no partner. All praise and thanks are Yours.', N'رواه أبو داود', 1, 3),
(5, N'اللهم إني أصبحت أُشهدك وأشهد حملة عرشك وملائكتك أنك أنت الله لا إله إلا أنت', 'O Allah, I have entered the morning calling You to witness, and the bearers of Your Throne and Your angels, that You are Allah, there is no god but You.', N'رواه أبو داود', 4, 4),
(5, N'اللهم عافني في بدني، اللهم عافني في سمعي، اللهم عافني في بصري', 'O Allah, grant me well-being in my body, in my hearing, and in my sight.', N'رواه أبو داود', 3, 5),
(5, N'اللهم إني أعوذ بك من الكفر والفقر وعذاب القبر', 'O Allah, I seek refuge in You from disbelief, poverty, and the punishment of the grave.', N'رواه أبو داود', 3, 6),
(5, N'حسبي الله لا إله إلا هو عليه توكلت وهو رب العرش العظيم', 'Allah is sufficient for me. There is no god but He. In Him I put my trust.', N'رواه أبو داود', 7, 7),
(5, N'بسم الله الذي لا يضر مع اسمه شيء في الأرض ولا في السماء وهو السميع العليم', 'In the Name of Allah, with Whose Name nothing on earth or in heaven can cause harm, and He is the All-Hearing, All-Knowing.', N'رواه أبو داود', 3, 8),
(5, N'أعوذ بكلمات الله التامات من شر ما خلق', 'I seek refuge in the perfect words of Allah from the evil of what He has created.', N'رواه مسلم', 3, 9),
(5, N'سبحان الله وبحمده', 'Glory be to Allah and His is the praise.', N'متفق عليه', 100, 10);
GO

-- ─────────────────────────────────────────
-- 🌇 Category 6: أذكار المساء (Evening Adhkar)
-- ─────────────────────────────────────────
INSERT INTO Adhkar (Category, TextAr, TextEn, Source, RepeatCount, SortOrder) VALUES
(6, N'أمسينا وأمسى الملك لله، والحمد لله، لا إله إلا الله وحده لا شريك له', 'We have reached the evening and the dominion belongs to Allah. Praise is to Allah. There is no god but Allah alone.', N'رواه مسلم', 1, 1),
(6, N'اللهم بك أمسينا وبك أصبحنا وبك نحيا وبك نموت وإليك المصير', 'O Allah, by You we enter the evening and by You we enter the morning. By You we live and by You we die, and to You is the final return.', N'رواه الترمذي', 1, 2),
(6, N'اللهم ما أمسى بي من نعمة فمنك وحدك لا شريك لك، فلك الحمد ولك الشكر', 'O Allah, whatever blessing I have this evening is from You alone. All praise and thanks are Yours.', N'رواه أبو داود', 1, 3),
(6, N'اللهم إني أمسيت أُشهدك وأشهد حملة عرشك وملائكتك أنك أنت الله لا إله إلا أنت', 'O Allah, I have entered the evening calling You to witness that You are Allah, there is no god but You.', N'رواه أبو داود', 4, 4),
(6, N'اللهم عافني في بدني، اللهم عافني في سمعي، اللهم عافني في بصري', 'O Allah, grant me well-being in my body, in my hearing, and in my sight.', N'رواه أبو داود', 3, 5),
(6, N'اللهم إني أعوذ بك من الكفر والفقر وعذاب القبر', 'O Allah, I seek refuge in You from disbelief, poverty, and the punishment of the grave.', N'رواه أبو داود', 3, 6),
(6, N'حسبي الله لا إله إلا هو عليه توكلت وهو رب العرش العظيم', 'Allah is sufficient for me. There is no god but He.', N'رواه أبو داود', 7, 7),
(6, N'بسم الله الذي لا يضر مع اسمه شيء في الأرض ولا في السماء وهو السميع العليم', 'In the Name of Allah, with Whose Name nothing can cause harm.', N'رواه أبو داود', 3, 8),
(6, N'أعوذ بكلمات الله التامات من شر ما خلق', 'I seek refuge in the perfect words of Allah from the evil of what He has created.', N'رواه مسلم', 3, 9),
(6, N'سبحان الله وبحمده', 'Glory be to Allah and His is the praise.', N'متفق عليه', 100, 10);
GO

-- ═══════════════════════════════════════════════════════════════
-- ✅ VERIFY
-- ═══════════════════════════════════════════════════════════════
PRINT '';
PRINT '═══════════════════════════════════════════════════';
PRINT '✅ All SPs created successfully!';
PRINT '✅ Adhkar table + seed data inserted!';
PRINT '═══════════════════════════════════════════════════';
PRINT '';

SELECT Category, COUNT(*) AS [Count]
FROM Adhkar
GROUP BY Category
ORDER BY Category;
GO
