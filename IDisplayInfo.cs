using System;

namespace LibrarySystem
{
    /// <summary>
    /// واجهة لعرض معلومات أي كائن بشكل مفصّل.
    /// تُستخدم لتوحيد طريقة الطباعة لكل العناصر والمستخدمين.
    /// </summary>
    public interface IDisplayInfo
    {
        /// <summary>
        /// يعرض معلومات الكائن على الـ Console
        /// </summary>
        void DisplayInfo();
    }
}
