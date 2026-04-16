-- =============================================
-- Salati Database Creation Script (Safe to re-run)
-- Prayer Reminder Application
-- تطبيق تذكير الصلاة والتنبيه بالأذان
-- ⚠️ هذا السكربت آمن للتشغيل أكثر من مرة
-- =============================================

-- إنشاء قاعدة البيانات (فقط إذا لم تكن موجودة)
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'SalatiDB')
BEGIN
    CREATE DATABASE SalatiDB;
    PRINT N'✅ تم إنشاء قاعدة البيانات SalatiDB';
END
ELSE
    PRINT N'⚠️ قاعدة البيانات SalatiDB موجودة بالفعل - تم تخطيها';
GO

USE SalatiDB;
GO

-- =============================================
-- المستوى 1: الجداول الأساسية
-- =============================================

-- 1. جدول المواقع
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = N'Locations')
BEGIN
    CREATE TABLE Locations (
        LocationID         INT            IDENTITY(1,1) PRIMARY KEY,
        Country            NVARCHAR(100)  NOT NULL,
        City               NVARCHAR(100)  NOT NULL,
        CalculationMethod  INT            NOT NULL DEFAULT 5,
        Latitude           DECIMAL(9,6)   NULL,
        Longitude          DECIMAL(9,6)   NULL,
        IsDefault          BIT            NOT NULL DEFAULT 0,
        CreatedDate        DATETIME       NOT NULL DEFAULT GETDATE(),

        CONSTRAINT UQ_Locations_CountryCity
            UNIQUE (Country, City)
    );
    PRINT N'✅ تم إنشاء جدول Locations';
END
ELSE
    PRINT N'⚠️ جدول Locations موجود بالفعل - تم تخطيه';
GO

-- 2. جدول مواعيد الصلاة اليومية
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = N'DailyPrayerTimes')
BEGIN
    CREATE TABLE DailyPrayerTimes (
        DailyPrayerTimeID  INT            IDENTITY(1,1) PRIMARY KEY,
        LocationID         INT            NULL,
        [Date]             DATE           NOT NULL,
        FajrTime           TIME(0)        NOT NULL,
        SunriseTime        TIME(0)        NULL,
        DhuhrTime          TIME(0)        NOT NULL,
        AsrTime            TIME(0)        NOT NULL,
        MaghribTime        TIME(0)        NOT NULL,
        IshaTime           TIME(0)        NOT NULL,
        [Source]           TINYINT        NOT NULL,
        HijriDate          NVARCHAR(30)   NULL,
        FetchedAt          DATETIME       NOT NULL DEFAULT GETDATE(),

        CONSTRAINT FK_DailyPrayerTimes_Locations
            FOREIGN KEY (LocationID) REFERENCES Locations(LocationID)
            ON DELETE SET NULL,

        CONSTRAINT UQ_DailyPrayerTimes_LocationDate
            UNIQUE (LocationID, [Date]),

        CONSTRAINT CK_DailyPrayerTimes_Source
            CHECK ([Source] IN (1, 2))
    );
    PRINT N'✅ تم إنشاء جدول DailyPrayerTimes';
END
ELSE
    PRINT N'⚠️ جدول DailyPrayerTimes موجود بالفعل - تم تخطيه';
GO

-- 3. جدول الأصوات
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = N'Sounds')
BEGIN
    CREATE TABLE Sounds (
        SoundID            INT            IDENTITY(1,1) PRIMARY KEY,
        SoundName          NVARCHAR(100)  NOT NULL,
        [FileName]         NVARCHAR(255)  NOT NULL,
        FilePath           NVARCHAR(500)  NULL,
        DurationSeconds    INT            NULL,
        IsDefault          BIT            NOT NULL DEFAULT 0,
        IsBuiltIn          BIT            NOT NULL DEFAULT 1,
        CreatedDate        DATETIME       NOT NULL DEFAULT GETDATE()
    );
    PRINT N'✅ تم إنشاء جدول Sounds';
END
ELSE
    PRINT N'⚠️ جدول Sounds موجود بالفعل - تم تخطيه';
GO

-- 4. جدول إعدادات التنبيه
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = N'AlertConfigs')
BEGIN
    CREATE TABLE AlertConfigs (
        AlertConfigID      INT            IDENTITY(1,1) PRIMARY KEY,
        Prayer             TINYINT        NOT NULL UNIQUE,
        IsEnabled          BIT            NOT NULL DEFAULT 1,
        MinutesBefore      INT            NOT NULL DEFAULT 5,
        SoundID            INT            NOT NULL DEFAULT 1,
        AlertType          TINYINT        NOT NULL DEFAULT 1,
        AlertAtAdhanTime   BIT            NOT NULL DEFAULT 1,
        Volume             INT            NOT NULL DEFAULT 80,
        UpdatedDate        DATETIME       NOT NULL DEFAULT GETDATE(),

        CONSTRAINT FK_AlertConfigs_Sounds
            FOREIGN KEY (SoundID) REFERENCES Sounds(SoundID)
            ON DELETE SET DEFAULT,

        CONSTRAINT CK_AlertConfigs_Prayer
            CHECK (Prayer BETWEEN 1 AND 5),

        CONSTRAINT CK_AlertConfigs_MinutesBefore
            CHECK (MinutesBefore BETWEEN 0 AND 120),

        CONSTRAINT CK_AlertConfigs_AlertType
            CHECK (AlertType IN (1, 2, 3)),

        CONSTRAINT CK_AlertConfigs_Volume
            CHECK (Volume BETWEEN 0 AND 100)
    );
    PRINT N'✅ تم إنشاء جدول AlertConfigs';
END
ELSE
    PRINT N'⚠️ جدول AlertConfigs موجود بالفعل - تم تخطيه';
GO

-- 5. جدول سجل التنبيهات
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = N'AlertLog')
BEGIN
    CREATE TABLE AlertLog (
        AlertLogID         INT            IDENTITY(1,1) PRIMARY KEY,
        Prayer             TINYINT        NOT NULL,
        AlertDateTime      DATETIME       NOT NULL DEFAULT GETDATE(),
        PrayerTime         TIME(0)        NOT NULL,
        AlertType          TINYINT        NOT NULL,
        MinutesBefore      INT            NOT NULL,
        WasDismissed       BIT            NOT NULL DEFAULT 0,
        WasMuted           BIT            NOT NULL DEFAULT 0,
        AutoDismissed      BIT            NOT NULL DEFAULT 0,
        DismissedAt        DATETIME       NULL,

        CONSTRAINT CK_AlertLog_Prayer
            CHECK (Prayer BETWEEN 1 AND 5)
    );
    PRINT N'✅ تم إنشاء جدول AlertLog';
END
ELSE
    PRINT N'⚠️ جدول AlertLog موجود بالفعل - تم تخطيه';
GO

-- 6. جدول تتبع الصلاة
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = N'PrayerTracking')
BEGIN
    CREATE TABLE PrayerTracking (
        TrackingID         INT            IDENTITY(1,1) PRIMARY KEY,
        [Date]             DATE           NOT NULL,
        Prayer             TINYINT        NOT NULL,
        PrayerTime         TIME(0)        NOT NULL,
        [Status]           TINYINT        NOT NULL DEFAULT 0,
        MarkedAt           DATETIME       NULL,
        Notes              NVARCHAR(200)  NULL,

        CONSTRAINT UQ_PrayerTracking_DatePrayer
            UNIQUE ([Date], Prayer),

        CONSTRAINT CK_PrayerTracking_Prayer
            CHECK (Prayer BETWEEN 1 AND 5),

        CONSTRAINT CK_PrayerTracking_Status
            CHECK ([Status] IN (0, 1, 2, 3))
    );
    PRINT N'✅ تم إنشاء جدول PrayerTracking';
END
ELSE
    PRINT N'⚠️ جدول PrayerTracking موجود بالفعل - تم تخطيه';
GO

-- 7. جدول إعدادات التطبيق
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = N'AppSettings')
BEGIN
    CREATE TABLE AppSettings (
        SettingID          INT            IDENTITY(1,1) PRIMARY KEY,
        SettingKey         NVARCHAR(100)  NOT NULL UNIQUE,
        SettingValue       NVARCHAR(500)  NULL,
        DefaultValue       NVARCHAR(500)  NULL,
        Category           NVARCHAR(50)   NOT NULL,
        [Description]      NVARCHAR(200)  NULL,
        UpdatedDate        DATETIME       NOT NULL DEFAULT GETDATE()
    );
    PRINT N'✅ تم إنشاء جدول AppSettings';
END
ELSE
    PRINT N'⚠️ جدول AppSettings موجود بالفعل - تم تخطيه';
GO

-- =============================================
-- الفهارس والقيود الإضافية
-- =============================================

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'UX_Locations_DefaultOnly')
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX UX_Locations_DefaultOnly
        ON Locations (IsDefault)
        WHERE IsDefault = 1;
    PRINT N'✅ تم إنشاء فهرس UX_Locations_DefaultOnly';
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'UX_Sounds_DefaultOnly')
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX UX_Sounds_DefaultOnly
        ON Sounds (IsDefault)
        WHERE IsDefault = 1;
    PRINT N'✅ تم إنشاء فهرس UX_Sounds_DefaultOnly';
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_DailyPrayerTimes_Date_LocationID')
BEGIN
    CREATE NONCLUSTERED INDEX IX_DailyPrayerTimes_Date_LocationID
        ON DailyPrayerTimes ([Date], LocationID)
        INCLUDE (FajrTime, DhuhrTime, AsrTime, MaghribTime, IshaTime, [Source], FetchedAt);
    PRINT N'✅ تم إنشاء فهرس IX_DailyPrayerTimes_Date_LocationID';
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_AlertLog_AlertDateTime')
BEGIN
    CREATE NONCLUSTERED INDEX IX_AlertLog_AlertDateTime
        ON AlertLog (AlertDateTime DESC)
        INCLUDE (Prayer, AlertType, MinutesBefore, WasDismissed, WasMuted, AutoDismissed);
    PRINT N'✅ تم إنشاء فهرس IX_AlertLog_AlertDateTime';
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_PrayerTracking_Date_Prayer')
BEGIN
    CREATE NONCLUSTERED INDEX IX_PrayerTracking_Date_Prayer
        ON PrayerTracking ([Date], Prayer)
        INCLUDE ([Status], MarkedAt, PrayerTime);
    PRINT N'✅ تم إنشاء فهرس IX_PrayerTracking_Date_Prayer';
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_AppSettings_Category')
BEGIN
    CREATE NONCLUSTERED INDEX IX_AppSettings_Category
        ON AppSettings (Category, SettingKey)
        INCLUDE (SettingValue, DefaultValue);
    PRINT N'✅ تم إنشاء فهرس IX_AppSettings_Category';
END
GO

-- =============================================
-- البيانات الأساسية (Seed Data)
-- ⚠️ يتم إدراجها فقط إذا لم تكن موجودة
-- =============================================

IF NOT EXISTS (
    SELECT 1
    FROM Locations
    WHERE Country = N'Egypt' AND City = N'Cairo'
)
BEGIN
    INSERT INTO Locations (Country, City, CalculationMethod, IsDefault)
    VALUES (N'Egypt', N'Cairo', 5, 1);
    PRINT N'✅ تم إدراج الموقع الافتراضي Cairo';
END
ELSE
    PRINT N'⚠️ الموقع الافتراضي Cairo موجود بالفعل - تم تخطيه';
GO

IF NOT EXISTS (SELECT TOP 1 1 FROM Sounds)
BEGIN
    INSERT INTO Sounds (SoundName, [FileName], IsDefault, IsBuiltIn) VALUES
        (N'أذان الحرم المكي',     N'adhan_makkah.mp3', 1, 1),
        (N'أذان المدينة المنورة', N'adhan_madinah.mp3', 0, 1),
        (N'أذان مشاري العفاسي',  N'adhan_afasy.mp3', 0, 1),
        (N'تنبيه بسيط',          N'beep_simple.wav', 0, 1),
        (N'تنبيه مزدوج',         N'beep_double.wav', 0, 1);
    PRINT N'✅ تم إدراج الأصوات الافتراضية';
END
ELSE
    PRINT N'⚠️ جدول Sounds يحتوي على بيانات بالفعل - تم تخطي Seed Data';
GO

IF NOT EXISTS (SELECT TOP 1 1 FROM AlertConfigs)
BEGIN
    INSERT INTO AlertConfigs
        (Prayer, IsEnabled, MinutesBefore, SoundID, AlertType, AlertAtAdhanTime, Volume)
    VALUES
        (1, 1, 10, 1, 1, 1, 80),
        (2, 1, 5,  1, 1, 1, 80),
        (3, 1, 5,  1, 1, 1, 80),
        (4, 1, 10, 1, 1, 1, 80),
        (5, 1, 5,  1, 1, 1, 80);
    PRINT N'✅ تم إدراج إعدادات التنبيه الافتراضية';
END
ELSE
    PRINT N'⚠️ جدول AlertConfigs يحتوي على بيانات بالفعل - تم تخطي Seed Data';
GO

IF NOT EXISTS (SELECT TOP 1 1 FROM AppSettings)
BEGIN
    INSERT INTO AppSettings (SettingKey, SettingValue, DefaultValue, Category, [Description]) VALUES
        (N'PrayerSource',      N'API',               N'API',               N'Prayer',     N'مصدر المواعيد: API أو Manual'),
        (N'DefaultLocationID', N'1',                 N'1',                 N'Prayer',     N'الموقع الافتراضي'),
        (N'ActiveThemeName',   N'Midnight Serenity', N'Midnight Serenity', N'Appearance', N'الثيم الحالي'),
        (N'LanguageCode',      N'ar',                N'ar',                N'Appearance', N'لغة التطبيق'),
        (N'StartWithWindows',  N'true',              N'true',              N'General',    N'تشغيل التطبيق مع بداية الويندوز'),
        (N'MinimizeOnStart',   N'false',             N'false',             N'General',    N'تصغير عند بدء التشغيل'),
        (N'ShowInTray',        N'true',              N'true',              N'General',    N'إظهار التطبيق في شريط النظام'),
        (N'MinimizeToTray',    N'true',              N'true',              N'General',    N'التصغير إلى شريط النظام عند الإغلاق'),
        (N'GlobalVolume',      N'80',                N'80',                N'Sound',      N'مستوى الصوت العام'),
        (N'LastFetchDate',     NULL,                 NULL,                 N'System',     N'آخر تاريخ جلب للمواعيد من API');
    PRINT N'✅ تم إدراج إعدادات التطبيق الافتراضية';
END
ELSE
    PRINT N'⚠️ جدول AppSettings يحتوي على بيانات بالفعل - تم تخطي Seed Data';
GO

UPDATE AppSettings
SET SettingValue = CONVERT(NVARCHAR(500), DL.LocationID),
    DefaultValue = CONVERT(NVARCHAR(500), DL.LocationID),
    UpdatedDate = GETDATE()
FROM AppSettings A
CROSS APPLY (
    SELECT TOP 1 LocationID
    FROM Locations
    WHERE IsDefault = 1
    ORDER BY LocationID
) DL
WHERE A.SettingKey = N'DefaultLocationID';
GO

-- =============================================
-- Stored Procedures
-- =============================================

-- ═══════════════════════════════════════════
-- SP: Locations
-- ═══════════════════════════════════════════

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_GetAllLocations')
    DROP PROCEDURE SP_GetAllLocations;
GO
CREATE PROCEDURE SP_GetAllLocations
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        LocationID, Country, City, CalculationMethod,
        Latitude, Longitude, IsDefault, CreatedDate
    FROM Locations
    ORDER BY IsDefault DESC, Country, City;
END
GO
PRINT N'✅ SP_GetAllLocations';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_GetLocationByID')
    DROP PROCEDURE SP_GetLocationByID;
GO
CREATE PROCEDURE SP_GetLocationByID
    @LocationID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        LocationID, Country, City, CalculationMethod,
        Latitude, Longitude, IsDefault, CreatedDate
    FROM Locations
    WHERE LocationID = @LocationID;
END
GO
PRINT N'✅ SP_GetLocationByID';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_GetDefaultLocation')
    DROP PROCEDURE SP_GetDefaultLocation;
GO
CREATE PROCEDURE SP_GetDefaultLocation
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        LocationID, Country, City, CalculationMethod,
        Latitude, Longitude, IsDefault, CreatedDate
    FROM Locations
    WHERE IsDefault = 1
    ORDER BY LocationID;
END
GO
PRINT N'✅ SP_GetDefaultLocation';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_AddLocation')
    DROP PROCEDURE SP_AddLocation;
GO
CREATE PROCEDURE SP_AddLocation
    @Country            NVARCHAR(100),
    @City               NVARCHAR(100),
    @CalculationMethod  INT = 5,
    @Latitude           DECIMAL(9,6) = NULL,
    @Longitude          DECIMAL(9,6) = NULL,
    @IsDefault          BIT = 0,
    @NewLocationID      INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF @IsDefault = 1
        BEGIN
            UPDATE Locations
            SET IsDefault = 0
            WHERE IsDefault = 1;
        END

        INSERT INTO Locations
            (Country, City, CalculationMethod, Latitude, Longitude, IsDefault)
        VALUES
            (@Country, @City, @CalculationMethod, @Latitude, @Longitude, @IsDefault);

        SET @NewLocationID = SCOPE_IDENTITY();

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END
GO
PRINT N'✅ SP_AddLocation';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_UpdateLocation')
    DROP PROCEDURE SP_UpdateLocation;
GO
CREATE PROCEDURE SP_UpdateLocation
    @LocationID         INT,
    @Country            NVARCHAR(100),
    @City               NVARCHAR(100),
    @CalculationMethod  INT,
    @Latitude           DECIMAL(9,6) = NULL,
    @Longitude          DECIMAL(9,6) = NULL,
    @IsDefault          BIT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF @IsDefault = 1
        BEGIN
            UPDATE Locations
            SET IsDefault = 0
            WHERE LocationID <> @LocationID
              AND IsDefault = 1;
        END

        UPDATE Locations
        SET Country = @Country,
            City = @City,
            CalculationMethod = @CalculationMethod,
            Latitude = @Latitude,
            Longitude = @Longitude,
            IsDefault = @IsDefault
        WHERE LocationID = @LocationID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END
GO
PRINT N'✅ SP_UpdateLocation';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_DeleteLocation')
    DROP PROCEDURE SP_DeleteLocation;
GO
CREATE PROCEDURE SP_DeleteLocation
    @LocationID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Locations
    WHERE LocationID = @LocationID;
END
GO
PRINT N'✅ SP_DeleteLocation';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_SetDefaultLocation')
    DROP PROCEDURE SP_SetDefaultLocation;
GO
CREATE PROCEDURE SP_SetDefaultLocation
    @LocationID INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE Locations
        SET IsDefault = 0
        WHERE IsDefault = 1;

        UPDATE Locations
        SET IsDefault = 1
        WHERE LocationID = @LocationID;

        UPDATE AppSettings
        SET SettingValue = CONVERT(NVARCHAR(500), @LocationID),
            UpdatedDate = GETDATE()
        WHERE SettingKey = N'DefaultLocationID';

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END
GO
PRINT N'✅ SP_SetDefaultLocation';

-- ═══════════════════════════════════════════
-- SP: DailyPrayerTimes
-- ═══════════════════════════════════════════

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_GetPrayerTimesByDate')
    DROP PROCEDURE SP_GetPrayerTimesByDate;
GO
CREATE PROCEDURE SP_GetPrayerTimesByDate
    @Date       DATE,
    @LocationID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        DailyPrayerTimeID, LocationID, [Date],
        FajrTime, SunriseTime, DhuhrTime, AsrTime, MaghribTime, IshaTime,
        [Source], HijriDate, FetchedAt
    FROM DailyPrayerTimes
    WHERE [Date] = @Date
      AND (
            (LocationID = @LocationID)
            OR (LocationID IS NULL AND @LocationID IS NULL)
          )
    ORDER BY FetchedAt DESC;
END
GO
PRINT N'✅ SP_GetPrayerTimesByDate';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_GetPrayerTimesByDateRange')
    DROP PROCEDURE SP_GetPrayerTimesByDateRange;
GO
CREATE PROCEDURE SP_GetPrayerTimesByDateRange
    @FromDate   DATE,
    @ToDate     DATE,
    @LocationID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        DailyPrayerTimeID, LocationID, [Date],
        FajrTime, SunriseTime, DhuhrTime, AsrTime, MaghribTime, IshaTime,
        [Source], HijriDate, FetchedAt
    FROM DailyPrayerTimes
    WHERE [Date] BETWEEN @FromDate AND @ToDate
      AND (
            (LocationID = @LocationID)
            OR (LocationID IS NULL AND @LocationID IS NULL)
          )
    ORDER BY [Date];
END
GO
PRINT N'✅ SP_GetPrayerTimesByDateRange';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_GetTodayPrayerTimes')
    DROP PROCEDURE SP_GetTodayPrayerTimes;
GO
CREATE PROCEDURE SP_GetTodayPrayerTimes
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Today DATE = CAST(GETDATE() AS DATE);
    DECLARE @DefaultLocationID INT;

    SELECT TOP 1 @DefaultLocationID = LocationID
    FROM Locations
    WHERE IsDefault = 1
    ORDER BY LocationID;

    SELECT TOP 1
        DailyPrayerTimeID, LocationID, [Date],
        FajrTime, SunriseTime, DhuhrTime, AsrTime, MaghribTime, IshaTime,
        [Source], HijriDate, FetchedAt
    FROM DailyPrayerTimes
    WHERE [Date] = @Today
    ORDER BY
        CASE
            WHEN @DefaultLocationID IS NOT NULL AND LocationID = @DefaultLocationID THEN 0
            WHEN LocationID IS NULL THEN 1
            ELSE 2
        END,
        FetchedAt DESC;
END
GO
PRINT N'✅ SP_GetTodayPrayerTimes';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_InsertOrUpdatePrayerTimes')
    DROP PROCEDURE SP_InsertOrUpdatePrayerTimes;
GO
CREATE PROCEDURE SP_InsertOrUpdatePrayerTimes
    @LocationID    INT = NULL,
    @Date          DATE,
    @FajrTime      TIME(0),
    @SunriseTime   TIME(0) = NULL,
    @DhuhrTime     TIME(0),
    @AsrTime       TIME(0),
    @MaghribTime   TIME(0),
    @IshaTime      TIME(0),
    @Source        TINYINT,
    @HijriDate     NVARCHAR(30) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM DailyPrayerTimes
        WHERE [Date] = @Date
          AND (
                (LocationID = @LocationID)
                OR (LocationID IS NULL AND @LocationID IS NULL)
              )
    )
    BEGIN
        UPDATE DailyPrayerTimes
        SET FajrTime = @FajrTime,
            SunriseTime = @SunriseTime,
            DhuhrTime = @DhuhrTime,
            AsrTime = @AsrTime,
            MaghribTime = @MaghribTime,
            IshaTime = @IshaTime,
            [Source] = @Source,
            HijriDate = @HijriDate,
            FetchedAt = GETDATE()
        WHERE [Date] = @Date
          AND (
                (LocationID = @LocationID)
                OR (LocationID IS NULL AND @LocationID IS NULL)
              );
    END
    ELSE
    BEGIN
        INSERT INTO DailyPrayerTimes
            (LocationID, [Date], FajrTime, SunriseTime, DhuhrTime, AsrTime,
             MaghribTime, IshaTime, [Source], HijriDate)
        VALUES
            (@LocationID, @Date, @FajrTime, @SunriseTime, @DhuhrTime, @AsrTime,
             @MaghribTime, @IshaTime, @Source, @HijriDate);
    END

    IF @Source = 1
    BEGIN
        UPDATE AppSettings
        SET SettingValue = CONVERT(NVARCHAR(500), GETDATE(), 126),
            UpdatedDate = GETDATE()
        WHERE SettingKey = N'LastFetchDate';
    END
END
GO
PRINT N'✅ SP_InsertOrUpdatePrayerTimes';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_DeleteOldPrayerTimes')
    DROP PROCEDURE SP_DeleteOldPrayerTimes;
GO
CREATE PROCEDURE SP_DeleteOldPrayerTimes
    @DaysToKeep INT = 30
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM DailyPrayerTimes
    WHERE [Date] < DATEADD(DAY, -@DaysToKeep, CAST(GETDATE() AS DATE));
END
GO
PRINT N'✅ SP_DeleteOldPrayerTimes';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_HasPrayerTimesForToday')
    DROP PROCEDURE SP_HasPrayerTimesForToday;
GO
CREATE PROCEDURE SP_HasPrayerTimesForToday
    @LocationID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT CAST(
        CASE
            WHEN EXISTS (
                SELECT 1
                FROM DailyPrayerTimes
                WHERE [Date] = CAST(GETDATE() AS DATE)
                  AND (
                        (LocationID = @LocationID)
                        OR (LocationID IS NULL AND @LocationID IS NULL)
                      )
            ) THEN 1
            ELSE 0
        END
        AS BIT
    ) AS HasPrayerTimes;
END
GO
PRINT N'✅ SP_HasPrayerTimesForToday';

-- ═══════════════════════════════════════════
-- SP: Sounds
-- ═══════════════════════════════════════════

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_GetAllSounds')
    DROP PROCEDURE SP_GetAllSounds;
GO
CREATE PROCEDURE SP_GetAllSounds
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        SoundID, SoundName, [FileName], FilePath, DurationSeconds,
        IsDefault, IsBuiltIn, CreatedDate
    FROM Sounds
    ORDER BY IsDefault DESC, IsBuiltIn DESC, SoundName;
END
GO
PRINT N'✅ SP_GetAllSounds';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_GetSoundByID')
    DROP PROCEDURE SP_GetSoundByID;
GO
CREATE PROCEDURE SP_GetSoundByID
    @SoundID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        SoundID, SoundName, [FileName], FilePath, DurationSeconds,
        IsDefault, IsBuiltIn, CreatedDate
    FROM Sounds
    WHERE SoundID = @SoundID;
END
GO
PRINT N'✅ SP_GetSoundByID';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_GetDefaultSound')
    DROP PROCEDURE SP_GetDefaultSound;
GO
CREATE PROCEDURE SP_GetDefaultSound
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        SoundID, SoundName, [FileName], FilePath, DurationSeconds,
        IsDefault, IsBuiltIn, CreatedDate
    FROM Sounds
    WHERE IsDefault = 1
    ORDER BY SoundID;
END
GO
PRINT N'✅ SP_GetDefaultSound';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_AddCustomSound')
    DROP PROCEDURE SP_AddCustomSound;
GO
CREATE PROCEDURE SP_AddCustomSound
    @SoundName         NVARCHAR(100),
    @FileName          NVARCHAR(255),
    @FilePath          NVARCHAR(500) = NULL,
    @DurationSeconds   INT = NULL,
    @IsDefault         BIT = 0,
    @NewSoundID        INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF @IsDefault = 1
        BEGIN
            UPDATE Sounds
            SET IsDefault = 0
            WHERE IsDefault = 1;
        END

        INSERT INTO Sounds
            (SoundName, [FileName], FilePath, DurationSeconds, IsDefault, IsBuiltIn)
        VALUES
            (@SoundName, @FileName, @FilePath, @DurationSeconds, @IsDefault, 0);

        SET @NewSoundID = SCOPE_IDENTITY();

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END
GO
PRINT N'✅ SP_AddCustomSound';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_DeleteCustomSound')
    DROP PROCEDURE SP_DeleteCustomSound;
GO
CREATE PROCEDURE SP_DeleteCustomSound
    @SoundID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Sounds
    WHERE SoundID = @SoundID
      AND IsBuiltIn = 0;
END
GO
PRINT N'✅ SP_DeleteCustomSound';

-- ═══════════════════════════════════════════
-- SP: AlertConfigs
-- ═══════════════════════════════════════════

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_GetAllAlertConfigs')
    DROP PROCEDURE SP_GetAllAlertConfigs;
GO
CREATE PROCEDURE SP_GetAllAlertConfigs
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ac.AlertConfigID, ac.Prayer, ac.IsEnabled, ac.MinutesBefore,
        ac.SoundID, ac.AlertType, ac.AlertAtAdhanTime, ac.Volume, ac.UpdatedDate,
        s.SoundName, s.[FileName] AS SoundFileName
    FROM AlertConfigs ac
    INNER JOIN Sounds s ON ac.SoundID = s.SoundID
    ORDER BY ac.Prayer;
END
GO
PRINT N'✅ SP_GetAllAlertConfigs';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_GetAlertConfigByPrayer')
    DROP PROCEDURE SP_GetAlertConfigByPrayer;
GO
CREATE PROCEDURE SP_GetAlertConfigByPrayer
    @Prayer TINYINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ac.AlertConfigID, ac.Prayer, ac.IsEnabled, ac.MinutesBefore,
        ac.SoundID, ac.AlertType, ac.AlertAtAdhanTime, ac.Volume, ac.UpdatedDate,
        s.SoundName, s.[FileName] AS SoundFileName
    FROM AlertConfigs ac
    INNER JOIN Sounds s ON ac.SoundID = s.SoundID
    WHERE ac.Prayer = @Prayer;
END
GO
PRINT N'✅ SP_GetAlertConfigByPrayer';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_UpdateAlertConfig')
    DROP PROCEDURE SP_UpdateAlertConfig;
GO
CREATE PROCEDURE SP_UpdateAlertConfig
    @Prayer             TINYINT,
    @IsEnabled          BIT,
    @MinutesBefore      INT,
    @SoundID            INT,
    @AlertType          TINYINT,
    @AlertAtAdhanTime   BIT,
    @Volume             INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE AlertConfigs
    SET IsEnabled = @IsEnabled,
        MinutesBefore = @MinutesBefore,
        SoundID = @SoundID,
        AlertType = @AlertType,
        AlertAtAdhanTime = @AlertAtAdhanTime,
        Volume = @Volume,
        UpdatedDate = GETDATE()
    WHERE Prayer = @Prayer;
END
GO
PRINT N'✅ SP_UpdateAlertConfig';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_EnableAllAlerts')
    DROP PROCEDURE SP_EnableAllAlerts;
GO
CREATE PROCEDURE SP_EnableAllAlerts
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE AlertConfigs
    SET IsEnabled = 1,
        UpdatedDate = GETDATE();
END
GO
PRINT N'✅ SP_EnableAllAlerts';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_DisableAllAlerts')
    DROP PROCEDURE SP_DisableAllAlerts;
GO
CREATE PROCEDURE SP_DisableAllAlerts
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE AlertConfigs
    SET IsEnabled = 0,
        UpdatedDate = GETDATE();
END
GO
PRINT N'✅ SP_DisableAllAlerts';

-- ═══════════════════════════════════════════
-- SP: AlertLog
-- ═══════════════════════════════════════════

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_InsertAlertLog')
    DROP PROCEDURE SP_InsertAlertLog;
GO
CREATE PROCEDURE SP_InsertAlertLog
    @Prayer         TINYINT,
    @PrayerTime     TIME(0),
    @AlertType      TINYINT,
    @MinutesBefore  INT,
    @NewAlertLogID  INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AlertLog (Prayer, PrayerTime, AlertType, MinutesBefore)
    VALUES (@Prayer, @PrayerTime, @AlertType, @MinutesBefore);

    SET @NewAlertLogID = SCOPE_IDENTITY();
END
GO
PRINT N'✅ SP_InsertAlertLog';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_UpdateAlertDismissed')
    DROP PROCEDURE SP_UpdateAlertDismissed;
GO
CREATE PROCEDURE SP_UpdateAlertDismissed
    @AlertLogID      INT,
    @WasDismissed    BIT = 1,
    @WasMuted        BIT = 0,
    @AutoDismissed   BIT = 0
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE AlertLog
    SET WasDismissed = @WasDismissed,
        WasMuted = @WasMuted,
        AutoDismissed = @AutoDismissed,
        DismissedAt = CASE WHEN @WasDismissed = 1 OR @AutoDismissed = 1 THEN GETDATE() ELSE DismissedAt END
    WHERE AlertLogID = @AlertLogID;
END
GO
PRINT N'✅ SP_UpdateAlertDismissed';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_GetAlertLogByDate')
    DROP PROCEDURE SP_GetAlertLogByDate;
GO
CREATE PROCEDURE SP_GetAlertLogByDate
    @Date DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        AlertLogID, Prayer, AlertDateTime, PrayerTime,
        AlertType, MinutesBefore, WasDismissed,
        WasMuted, AutoDismissed, DismissedAt
    FROM AlertLog
    WHERE CAST(AlertDateTime AS DATE) = @Date
    ORDER BY AlertDateTime DESC;
END
GO
PRINT N'✅ SP_GetAlertLogByDate';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_GetAlertStats')
    DROP PROCEDURE SP_GetAlertStats;
GO
CREATE PROCEDURE SP_GetAlertStats
    @FromDate DATE = NULL,
    @ToDate   DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @FromDate IS NULL
        SET @FromDate = DATEADD(DAY, -30, CAST(GETDATE() AS DATE));

    IF @ToDate IS NULL
        SET @ToDate = CAST(GETDATE() AS DATE);

    SELECT
        Prayer,
        COUNT(*) AS TotalAlerts,
        SUM(CASE WHEN WasDismissed = 1 THEN 1 ELSE 0 END) AS DismissedCount,
        SUM(CASE WHEN WasMuted = 1 THEN 1 ELSE 0 END) AS MutedCount,
        SUM(CASE WHEN AutoDismissed = 1 THEN 1 ELSE 0 END) AS AutoDismissedCount
    FROM AlertLog
    WHERE CAST(AlertDateTime AS DATE) BETWEEN @FromDate AND @ToDate
    GROUP BY Prayer
    ORDER BY Prayer;
END
GO
PRINT N'✅ SP_GetAlertStats';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_CleanOldAlertLogs')
    DROP PROCEDURE SP_CleanOldAlertLogs;
GO
CREATE PROCEDURE SP_CleanOldAlertLogs
    @DaysToKeep INT = 90
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM AlertLog
    WHERE AlertDateTime < DATEADD(DAY, -@DaysToKeep, GETDATE());
END
GO
PRINT N'✅ SP_CleanOldAlertLogs';

-- ═══════════════════════════════════════════
-- SP: PrayerTracking
-- ═══════════════════════════════════════════

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_MarkPrayer')
    DROP PROCEDURE SP_MarkPrayer;
GO
CREATE PROCEDURE SP_MarkPrayer
    @Date        DATE,
    @Prayer      TINYINT,
    @PrayerTime  TIME(0),
    @Status      TINYINT,
    @Notes       NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM PrayerTracking
        WHERE [Date] = @Date
          AND Prayer = @Prayer
    )
    BEGIN
        UPDATE PrayerTracking
        SET PrayerTime = @PrayerTime,
            [Status] = @Status,
            Notes = @Notes,
            MarkedAt = GETDATE()
        WHERE [Date] = @Date
          AND Prayer = @Prayer;
    END
    ELSE
    BEGIN
        INSERT INTO PrayerTracking ([Date], Prayer, PrayerTime, [Status], MarkedAt, Notes)
        VALUES (@Date, @Prayer, @PrayerTime, @Status, GETDATE(), @Notes);
    END
END
GO
PRINT N'✅ SP_MarkPrayer';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_GetDailyTracking')
    DROP PROCEDURE SP_GetDailyTracking;
GO
CREATE PROCEDURE SP_GetDailyTracking
    @Date DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        TrackingID, [Date], Prayer, PrayerTime,
        [Status], MarkedAt, Notes
    FROM PrayerTracking
    WHERE [Date] = @Date
    ORDER BY Prayer;
END
GO
PRINT N'✅ SP_GetDailyTracking';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_GetWeeklyStats')
    DROP PROCEDURE SP_GetWeeklyStats;
GO
CREATE PROCEDURE SP_GetWeeklyStats
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Prayer,
        COUNT(*) AS TotalRecords,
        SUM(CASE WHEN [Status] = 1 THEN 1 ELSE 0 END) AS OnTimeCount,
        SUM(CASE WHEN [Status] = 2 THEN 1 ELSE 0 END) AS LateCount,
        SUM(CASE WHEN [Status] = 3 THEN 1 ELSE 0 END) AS MissedCount,
        SUM(CASE WHEN [Status] = 0 THEN 1 ELSE 0 END) AS NotMarkedCount
    FROM PrayerTracking
    WHERE [Date] >= DATEADD(DAY, -7, CAST(GETDATE() AS DATE))
    GROUP BY Prayer
    ORDER BY Prayer;
END
GO
PRINT N'✅ SP_GetWeeklyStats';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_GetMonthlyStats')
    DROP PROCEDURE SP_GetMonthlyStats;
GO
CREATE PROCEDURE SP_GetMonthlyStats
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Prayer,
        COUNT(*) AS TotalRecords,
        SUM(CASE WHEN [Status] = 1 THEN 1 ELSE 0 END) AS OnTimeCount,
        SUM(CASE WHEN [Status] = 2 THEN 1 ELSE 0 END) AS LateCount,
        SUM(CASE WHEN [Status] = 3 THEN 1 ELSE 0 END) AS MissedCount,
        SUM(CASE WHEN [Status] = 0 THEN 1 ELSE 0 END) AS NotMarkedCount
    FROM PrayerTracking
    WHERE [Date] >= DATEADD(DAY, -30, CAST(GETDATE() AS DATE))
    GROUP BY Prayer
    ORDER BY Prayer;
END
GO
PRINT N'✅ SP_GetMonthlyStats';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_GetStreakCount')
    DROP PROCEDURE SP_GetStreakCount;
GO
CREATE PROCEDURE SP_GetStreakCount
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @CurrentDate DATE = CAST(GETDATE() AS DATE);
    DECLARE @StreakCount INT = 0;

    WHILE 1 = 1
    BEGIN
        IF EXISTS (
            SELECT 1
            FROM PrayerTracking
            WHERE [Date] = @CurrentDate
            GROUP BY [Date]
            HAVING COUNT(*) = 5
               AND SUM(CASE WHEN [Status] IN (1, 2) THEN 1 ELSE 0 END) = 5
        )
        BEGIN
            SET @StreakCount = @StreakCount + 1;
            SET @CurrentDate = DATEADD(DAY, -1, @CurrentDate);
        END
        ELSE
            BREAK;
    END

    SELECT @StreakCount AS StreakCount;
END
GO
PRINT N'✅ SP_GetStreakCount';

-- ═══════════════════════════════════════════
-- SP: AppSettings
-- ═══════════════════════════════════════════

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_GetAllSettings')
    DROP PROCEDURE SP_GetAllSettings;
GO
CREATE PROCEDURE SP_GetAllSettings
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        SettingID, SettingKey, SettingValue,
        DefaultValue, Category, [Description], UpdatedDate
    FROM AppSettings
    ORDER BY Category, SettingKey;
END
GO
PRINT N'✅ SP_GetAllSettings';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_GetSettingByKey')
    DROP PROCEDURE SP_GetSettingByKey;
GO
CREATE PROCEDURE SP_GetSettingByKey
    @SettingKey NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        SettingID, SettingKey, SettingValue,
        DefaultValue, Category, [Description], UpdatedDate
    FROM AppSettings
    WHERE SettingKey = @SettingKey;
END
GO
PRINT N'✅ SP_GetSettingByKey';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_GetSettingsByCategory')
    DROP PROCEDURE SP_GetSettingsByCategory;
GO
CREATE PROCEDURE SP_GetSettingsByCategory
    @Category NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        SettingID, SettingKey, SettingValue,
        DefaultValue, Category, [Description], UpdatedDate
    FROM AppSettings
    WHERE Category = @Category
    ORDER BY SettingKey;
END
GO
PRINT N'✅ SP_GetSettingsByCategory';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_UpdateSetting')
    DROP PROCEDURE SP_UpdateSetting;
GO
CREATE PROCEDURE SP_UpdateSetting
    @SettingKey    NVARCHAR(100),
    @SettingValue  NVARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE AppSettings
    SET SettingValue = @SettingValue,
        UpdatedDate = GETDATE()
    WHERE SettingKey = @SettingKey;
END
GO
PRINT N'✅ SP_UpdateSetting';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = N'SP_ResetToDefaults')
    DROP PROCEDURE SP_ResetToDefaults;
GO
CREATE PROCEDURE SP_ResetToDefaults
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE AppSettings
    SET SettingValue = DefaultValue,
        UpdatedDate = GETDATE();
END
GO
PRINT N'✅ SP_ResetToDefaults';

PRINT N'';
PRINT N'🎉 =============================================';
PRINT N'   Salati Database Script completed successfully!';
PRINT N'   تم تنفيذ سكربت قاعدة بيانات Salati بنجاح';
PRINT N'=============================================';
PRINT N'';
PRINT N'📊 الجداول (7):';
PRINT N'   1. Locations        - المواقع';
PRINT N'   2. DailyPrayerTimes - مواعيد الصلاة اليومية';
PRINT N'   3. Sounds           - أصوات الأذان';
PRINT N'   4. AlertConfigs     - إعدادات التنبيه';
PRINT N'   5. AlertLog         - سجل التنبيهات';
PRINT N'   6. PrayerTracking   - تتبع الصلاة';
PRINT N'   7. AppSettings      - إعدادات التطبيق';
PRINT N'';
PRINT N'🔗 العلاقات:';
PRINT N'   Locations - 1:M - DailyPrayerTimes';
PRINT N'   Sounds    - 1:M - AlertConfigs';
PRINT N'';
PRINT N'⚙️ Stored Procedures: 38';
GO
