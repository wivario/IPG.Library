using System;

namespace LibrarySystem
{
    /// <summary>
    /// واجهة لإرسال الإشعارات إلى كائن.
    /// تُطبَّق على الفئات التي يمكن أن تستقبل إشعارات (Customer, Employee).
    /// </summary>
    public interface INotification
    {
        /// <summary>
        /// يستقبل الكائن رسالة إشعار نصية.
        /// الرسائل تظهر على Console 
        /// </summary>
        /// <param name="message">نص الإشعار</param>
        void SendNotification(string message);
    }
}
