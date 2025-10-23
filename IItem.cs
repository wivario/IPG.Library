using System;

namespace LibrarySystem
{
    /// <summary>
    /// واجهة تحدد العقد العام لأي عنصر في المكتبة (Book/Magazine/DVD).
    /// </summary>
    public interface IItem
    {
        /// <summary>معرّف فريد للعنصر (قراءة فقط).</summary>
        Guid Id { get; }

        /// <summary>عنوان العنصر (قراءة/كتابة).</summary>
        string Title { get; set; }

        /// <summary>عدد مرات الاستعارة (قراءة فقط).</summary>
        int BorrowCount { get; }

        /// <summary>عرض معلومات العنصر.</summary>
        void DisplayInfo();
    }
}
