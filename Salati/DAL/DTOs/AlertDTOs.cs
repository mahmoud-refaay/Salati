namespace DAL.DTOs;

/// <summary>إعدادات تنبيه لصلاة واحدة</summary>
public record AlertConfigDTO(
    byte Prayer,
    bool IsEnabled,
    int MinutesBefore,
    int SoundID,
    byte AlertType,
    bool AlertAtAdhanTime,
    int Volume,
    string? SoundName,
    string? SoundFileName
);
