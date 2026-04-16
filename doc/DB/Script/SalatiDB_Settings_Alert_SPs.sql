-- ═══════════════════════════════════════════════════════════════
-- Salati — AppSettings + Alert SPs
-- الوصف: SPs لإعدادات التطبيق والتنبيهات
-- ═══════════════════════════════════════════════════════════════

USE SalatiDB;
GO

-- ═══════════════════════════════════════════
-- SP: AppSettings — قراءة/كتابة الإعدادات
-- ═══════════════════════════════════════════

DROP PROCEDURE IF EXISTS SP_GetSettingValue;
GO
CREATE PROCEDURE SP_GetSettingValue
    @SettingKey NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SettingValue
    FROM AppSettings
    WHERE SettingKey = @SettingKey;
END
GO
PRINT N'✅ SP_GetSettingValue';

DROP PROCEDURE IF EXISTS SP_UpdateSettingValue;
GO
CREATE PROCEDURE SP_UpdateSettingValue
    @SettingKey   NVARCHAR(100),
    @SettingValue NVARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE AppSettings
    SET SettingValue = @SettingValue,
        UpdatedDate = GETDATE()
    WHERE SettingKey = @SettingKey;
END
GO
PRINT N'✅ SP_UpdateSettingValue';

DROP PROCEDURE IF EXISTS SP_GetSettingsByCategory;
GO
CREATE PROCEDURE SP_GetSettingsByCategory
    @Category NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SettingKey AS [Key], SettingValue AS Value
    FROM AppSettings
    WHERE Category = @Category
    ORDER BY SettingKey;
END
GO
PRINT N'✅ SP_GetSettingsByCategory';

DROP PROCEDURE IF EXISTS SP_ResetSettingsToDefaults;
GO
CREATE PROCEDURE SP_ResetSettingsToDefaults
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE AppSettings
    SET SettingValue = DefaultValue,
        UpdatedDate = GETDATE();
END
GO
PRINT N'✅ SP_ResetSettingsToDefaults';

-- ═══════════════════════════════════════════
-- SP: AlertConfigs — إعدادات التنبيهات
-- ═══════════════════════════════════════════

DROP PROCEDURE IF EXISTS SP_GetAllAlertConfigs;
GO
CREATE PROCEDURE SP_GetAllAlertConfigs
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        AC.Prayer, AC.IsEnabled, AC.MinutesBefore,
        AC.SoundID, AC.AlertType, AC.AlertAtAdhanTime,
        AC.Volume, S.SoundName, S.[FileName] AS SoundFileName
    FROM AlertConfigs AC
    LEFT JOIN Sounds S ON AC.SoundID = S.SoundID
    ORDER BY AC.Prayer;
END
GO
PRINT N'✅ SP_GetAllAlertConfigs';

DROP PROCEDURE IF EXISTS SP_UpdateAlertConfig;
GO
CREATE PROCEDURE SP_UpdateAlertConfig
    @Prayer          TINYINT,
    @IsEnabled       BIT,
    @MinutesBefore   INT,
    @SoundID         INT,
    @AlertType       TINYINT,
    @AlertAtAdhanTime BIT,
    @Volume          INT
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

-- ═══════════════════════════════════════════
-- SP: AlertLog — سجل التنبيهات
-- ═══════════════════════════════════════════

DROP PROCEDURE IF EXISTS SP_InsertAlertLog;
GO
CREATE PROCEDURE SP_InsertAlertLog
    @Prayer        TINYINT,
    @PrayerTime    TIME(0),
    @AlertType     TINYINT,
    @MinutesBefore INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO AlertLog (Prayer, PrayerTime, AlertType, MinutesBefore)
    VALUES (@Prayer, @PrayerTime, @AlertType, @MinutesBefore);
END
GO
PRINT N'✅ SP_InsertAlertLog';

PRINT N'';
PRINT N'══════════════════════════════════════════';
PRINT N'✅ All AppSettings + Alert SPs created!';
PRINT N'══════════════════════════════════════════';
