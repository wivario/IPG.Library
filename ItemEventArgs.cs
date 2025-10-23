using System;

namespace LibrarySystem
{
    /// <summary>
    /// فئة بيانات تُمرر مع الأحداث الخاصة بالعناصر.
    /// تحتوي على المرجع للعنصر ورسالة وصفية.
    /// </summary>
    public class ItemEventArgs : EventArgs
    {
        /// <summary>عنصر المكتبة الذي أطلق الحدث.</summary>
        public IItem Item { get; }

        /// <summary>رسالة وصفية حول الحدث .</summary>
        public string Message { get; }

        /// <summary>باني يستقبل العنصر والنص المراد إرساله للمستمعين.</summary>
        public ItemEventArgs(IItem item, string message)
        {
            Item = item;
            Message = message;
        }
    }
}
