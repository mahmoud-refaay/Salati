-- ═══════════════════════════════════════════════════════════════
-- Salati — إضافة بيانات أذكار شاملة + إعداد فترة الإشعارات
-- ═══════════════════════════════════════════════════════════════
-- ⚠️ هذا الملف يُشغّل مرة واحدة بعد SalatiDB_PrayerTracking_SPs.sql
-- يضيف أذكار جديدة على اللي موجود + إعداد الفترة

USE SalatiDB;
GO

-- ═══════════════════════════════════════════
-- 1️⃣ إضافة إعداد فترة الإشعارات (لو مش موجود)
-- ═══════════════════════════════════════════

IF NOT EXISTS (SELECT 1 FROM AppSettings WHERE SettingKey = N'AdhkarIntervalMinutes')
BEGIN
    INSERT INTO AppSettings (SettingKey, SettingValue, DefaultValue, Category, [Description])
    VALUES (N'AdhkarIntervalMinutes', N'30', N'30', N'Notifications',
            N'فترة إشعارات الأذكار بالدقائق (10, 15, 20, 30, 60)');
    PRINT N'✅ Added AdhkarIntervalMinutes setting';
END
GO

IF NOT EXISTS (SELECT 1 FROM AppSettings WHERE SettingKey = N'AdhkarNotificationsEnabled')
BEGIN
    INSERT INTO AppSettings (SettingKey, SettingValue, DefaultValue, Category, [Description])
    VALUES (N'AdhkarNotificationsEnabled', N'true', N'true', N'Notifications',
            N'تفعيل/إيقاف إشعارات الأذكار العشوائية');
    PRINT N'✅ Added AdhkarNotificationsEnabled setting';
END
GO

-- ═══════════════════════════════════════════
-- 2️⃣ إضافة أذكار عامة جديدة (Category 2)
--    هذه هي اللي تظهر كإشعارات عشوائية
-- ═══════════════════════════════════════════

-- تأكد مش بنضيف مكرر
IF NOT EXISTS (SELECT 1 FROM Adhkar WHERE Category = 2 AND TextAr LIKE N'%لا إله إلا الله والله أكبر%')
BEGIN
    INSERT INTO Adhkar (Category, TextAr, TextEn, Source) VALUES
    (2, N'لا إله إلا الله والله أكبر ولا حول ولا قوة إلا بالله',
        'There is no god but Allah, and Allah is the Greatest. There is no power except with Allah.',
        N'رواه البخاري'),
    (2, N'سبحان الله عدد خلقه، سبحان الله رضا نفسه، سبحان الله زنة عرشه، سبحان الله مداد كلماته',
        'Glory be to Allah by the number of His creation, by His pleasure, by the weight of His throne, and by the extent of His words.',
        N'رواه مسلم'),
    (2, N'اللهم اغفر للمسلمين والمسلمات والمؤمنين والمؤمنات الأحياء منهم والأموات',
        'O Allah, forgive the Muslim men and women, the believing men and women, the living and the dead.',
        N'رواه مسلم'),
    (2, N'رب اغفر لي وتب عليّ إنك أنت التواب الرحيم',
        'My Lord, forgive me and accept my repentance. You are the Accepting of repentance, the Merciful.',
        N'رواه أبو داود'),
    (2, N'سبحان الله والحمد لله ولا إله إلا الله والله أكبر ولا حول ولا قوة إلا بالله العلي العظيم',
        'Glory be to Allah, praise be to Allah, there is no god but Allah, Allah is the Greatest, and there is no power except with Allah the Most High, the Almighty.',
        N'رواه مسلم'),
    (2, N'اللهم صلِّ وسلم وبارك على سيدنا محمد',
        'O Allah, send Your blessings, peace and grace upon our master Muhammad.',
        NULL),
    (2, N'لا إله إلا أنت سبحانك إني كنت من الظالمين',
        'There is no god but You, glory be to You, I have been among the wrongdoers.',
        N'سورة الأنبياء: 87'),
    (2, N'اللهم إني أسألك الجنة وأعوذ بك من النار',
        'O Allah, I ask You for Paradise and seek refuge in You from the Fire.',
        N'رواه أبو داود'),
    (2, N'اللهم أنت ربي لا إله إلا أنت، خلقتني وأنا عبدك',
        'O Allah, You are my Lord, there is no god but You. You created me and I am Your servant.',
        N'سيد الاستغفار — رواه البخاري'),
    (2, N'اللهم إني أعوذ بك من زوال نعمتك، وتحوّل عافيتك، وفجاءة نقمتك، وجميع سخطك',
        'O Allah, I seek refuge in You from the loss of Your blessings, the decline of Your protection, the suddenness of Your punishment, and all Your displeasure.',
        N'رواه مسلم');
    PRINT N'✅ Added 10 new general Adhkar (Category 2)';
END
GO

-- ═══════════════════════════════════════════
-- 3️⃣ إضافة أحاديث جديدة (Category 1)
-- ═══════════════════════════════════════════

IF NOT EXISTS (SELECT 1 FROM Adhkar WHERE Category = 1 AND TextAr LIKE N'%الحياء لا يأتي إلا بخير%')
BEGIN
    INSERT INTO Adhkar (Category, TextAr, TextEn, Source) VALUES
    (1, N'الحياء لا يأتي إلا بخير',
        'Modesty only brings good.',
        N'متفق عليه'),
    (1, N'مثل المؤمنين في توادّهم وتراحمهم كمثل الجسد الواحد',
        'The believers in their mutual kindness and compassion are like one body.',
        N'متفق عليه'),
    (1, N'خيركم أحسنكم أخلاقًا',
        'The best among you are those with the best character.',
        N'رواه البخاري'),
    (1, N'الكلمة الطيبة صدقة',
        'A good word is charity.',
        N'متفق عليه'),
    (1, N'إذا مات ابن آدم انقطع عمله إلا من ثلاث: صدقة جارية أو علم ينتفع به أو ولد صالح يدعو له',
        'When a person dies, their deeds end except for three: ongoing charity, beneficial knowledge, or a righteous child who prays for them.',
        N'رواه مسلم'),
    (1, N'لا تحقرنّ من المعروف شيئًا ولو أن تلقى أخاك بوجه طلق',
        'Do not belittle any good deed, even if it is meeting your brother with a cheerful face.',
        N'رواه مسلم'),
    (1, N'من سلك طريقًا يلتمس فيه علمًا سهّل الله له طريقًا إلى الجنة',
        'Whoever treads a path in search of knowledge, Allah will make easy for him a path to Paradise.',
        N'رواه مسلم'),
    (1, N'إن الله يحب إذا عمل أحدكم عملاً أن يتقنه',
        'Allah loves when one of you does a task that he does it with excellence.',
        N'رواه الطبراني'),
    (1, N'المؤمن القوي خير وأحب إلى الله من المؤمن الضعيف',
        'The strong believer is better and more beloved to Allah than the weak believer.',
        N'رواه مسلم'),
    (1, N'ما نقص مال من صدقة',
        'Charity does not decrease wealth.',
        N'رواه مسلم');
    PRINT N'✅ Added 10 new Hadith (Category 1)';
END
GO

-- ═══════════════════════════════════════════
-- 4️⃣ إضافة أدعية جديدة (Category 4)
-- ═══════════════════════════════════════════

IF NOT EXISTS (SELECT 1 FROM Adhkar WHERE Category = 4 AND TextAr LIKE N'%ربنا هب لنا من أزواجنا%')
BEGIN
    INSERT INTO Adhkar (Category, TextAr, TextEn, Source) VALUES
    (4, N'ربنا هب لنا من أزواجنا وذرياتنا قرة أعين واجعلنا للمتقين إمامًا',
        'Our Lord, grant us from among our spouses and offspring comfort to our eyes, and make us an example for the righteous.',
        N'سورة الفرقان: 74'),
    (4, N'رب أوزعني أن أشكر نعمتك التي أنعمت عليّ وعلى والديّ',
        'My Lord, enable me to be grateful for Your favor which You have bestowed upon me and upon my parents.',
        N'سورة النمل: 19'),
    (4, N'اللهم إني أسألك الهدى والتقى والعفاف والغنى',
        'O Allah, I ask You for guidance, piety, chastity, and self-sufficiency.',
        N'رواه مسلم'),
    (4, N'اللهم اهدني وسددني',
        'O Allah, guide me and direct me.',
        N'رواه مسلم'),
    (4, N'اللهم إني أسألك علمًا نافعًا ورزقًا طيبًا وعملًا متقبّلاً',
        'O Allah, I ask You for beneficial knowledge, wholesome provision, and accepted deeds.',
        N'رواه ابن ماجه');
    PRINT N'✅ Added 5 new Dua (Category 4)';
END
GO

-- ═══════════════════════════════════════════
-- 5️⃣ إضافة أذكار صباح إضافية (Category 5)
-- ═══════════════════════════════════════════

IF NOT EXISTS (SELECT 1 FROM Adhkar WHERE Category = 5 AND TextAr LIKE N'%أصبحنا على فطرة الإسلام%')
BEGIN
    INSERT INTO Adhkar (Category, TextAr, TextEn, Source, RepeatCount, SortOrder) VALUES
    (5, N'أصبحنا على فطرة الإسلام وكلمة الإخلاص وعلى دين نبينا محمد ﷺ وعلى ملة أبينا إبراهيم',
        'We have entered the morning upon the natural way of Islam, upon the word of pure faith, upon the religion of our Prophet Muhammad ﷺ, and upon the way of our father Ibrahim.',
        N'رواه أحمد', 1, 11),
    (5, N'اللهم إني أسألك العفو والعافية في ديني ودنياي وأهلي ومالي',
        'O Allah, I ask You for forgiveness and well-being in my religion, worldly affairs, family, and wealth.',
        N'رواه أبو داود', 1, 12),
    (5, N'اللهم استر عوراتي وآمن روعاتي',
        'O Allah, conceal my faults and calm my fears.',
        N'رواه أبو داود', 1, 13),
    (5, N'لا إله إلا الله وحده لا شريك له، له الملك وله الحمد وهو على كل شيء قدير',
        'There is no god but Allah alone, with no partner. His is the dominion and praise, and He is over all things capable.',
        N'متفق عليه', 10, 14),
    (5, N'رضيت بالله ربًا وبالإسلام دينًا وبمحمد ﷺ نبيًا',
        'I am pleased with Allah as Lord, with Islam as religion, and with Muhammad ﷺ as Prophet.',
        N'رواه أبو داود', 3, 15);
    PRINT N'✅ Added 5 new Morning Adhkar (Category 5)';
END
GO

-- ═══════════════════════════════════════════
-- 6️⃣ إضافة أذكار مساء إضافية (Category 6)
-- ═══════════════════════════════════════════

IF NOT EXISTS (SELECT 1 FROM Adhkar WHERE Category = 6 AND TextAr LIKE N'%أمسينا على فطرة الإسلام%')
BEGIN
    INSERT INTO Adhkar (Category, TextAr, TextEn, Source, RepeatCount, SortOrder) VALUES
    (6, N'أمسينا على فطرة الإسلام وكلمة الإخلاص وعلى دين نبينا محمد ﷺ وعلى ملة أبينا إبراهيم',
        'We have entered the evening upon the natural way of Islam, upon the word of pure faith, upon the religion of our Prophet Muhammad ﷺ.',
        N'رواه أحمد', 1, 11),
    (6, N'اللهم إني أسألك العفو والعافية في ديني ودنياي وأهلي ومالي',
        'O Allah, I ask You for forgiveness and well-being in my religion, worldly affairs, family, and wealth.',
        N'رواه أبو داود', 1, 12),
    (6, N'اللهم استر عوراتي وآمن روعاتي',
        'O Allah, conceal my faults and calm my fears.',
        N'رواه أبو داود', 1, 13),
    (6, N'لا إله إلا الله وحده لا شريك له، له الملك وله الحمد وهو على كل شيء قدير',
        'There is no god but Allah alone, with no partner.',
        N'متفق عليه', 10, 14),
    (6, N'رضيت بالله ربًا وبالإسلام دينًا وبمحمد ﷺ نبيًا',
        'I am pleased with Allah as Lord, with Islam as religion, and with Muhammad ﷺ as Prophet.',
        N'رواه أبو داود', 3, 15),
    (6, N'يا حي يا قيوم برحمتك أستغيث، أصلح لي شأني كله ولا تكلني إلى نفسي طرفة عين',
        'O Living, O Sustaining, in Your mercy I seek relief. Rectify all my affairs and do not leave me to myself for the blink of an eye.',
        N'رواه الحاكم', 1, 16);
    PRINT N'✅ Added 6 new Evening Adhkar (Category 6)';
END
GO

-- ═══════════════════════════════════════════
-- ✅ VERIFY
-- ═══════════════════════════════════════════

PRINT N'';
PRINT N'═══════════════════════════════════════';
PRINT N'✅ Enhanced Adhkar data added!';
PRINT N'═══════════════════════════════════════';

SELECT
    CASE Category
        WHEN 1 THEN N'📖 أحاديث (Hadith)'
        WHEN 2 THEN N'📿 أذكار عامة (Dhikr) — إشعارات'
        WHEN 3 THEN N'🤲 صلاة على النبي (Salawat)'
        WHEN 4 THEN N'🕌 أدعية (Dua)'
        WHEN 5 THEN N'🌅 أذكار الصباح (Morning)'
        WHEN 6 THEN N'🌇 أذكار المساء (Evening)'
    END AS [التصنيف],
    COUNT(*) AS [العدد]
FROM Adhkar
GROUP BY Category
ORDER BY Category;
GO
