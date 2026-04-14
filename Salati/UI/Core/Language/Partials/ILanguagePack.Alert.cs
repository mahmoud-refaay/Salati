namespace UI.Core.Language
{
    // نصوص نافذة التنبيه
    public partial interface ILanguagePack
    {
        /// <summary>"حان وقت صلاة {0}" — {0} = اسم الصلاة</summary>
        string AlertTimeFor { get; }

        /// <summary>"تبقى {0} دقيقة على صلاة {1}"</summary>
        string AlertMinutesUntil { get; }

        /// <summary>"يختفي تلقائياً بعد {0} ثانية"</summary>
        string AlertAutoDismiss { get; }

        /// <summary>"حي على الصلاة حي على الفلاح"</summary>
        string AlertHayaAlaSalah { get; }

        /// <summary>زر التأكيد</summary>
        string AlertConfirm { get; }

        /// <summary>زر الإلغاء</summary>
        string AlertCancel { get; }

        /// <summary>زر موافق</summary>
        string AlertOk { get; }

        /// <summary>نعم</summary>
        string AlertYes { get; }

        /// <summary>لا</summary>
        string AlertNo { get; }
    }
}
