using System;

namespace IPG.Library
{
    /// <summary>
    /// واجهة تمثل العقد العام لأي عنصر بالمكتبة (كتاب، مجلة، DVD، ...).
    /// تحدد الخصائص والوظائف الأساسية التي يفترض أن توفرها أي فئة ترث/تطبقها.
    /// </summary>
    public interface IItem
    {
        Guid Id { get; }            // معرّف فريد للعنصر (قراءة فقط)
        string Title { get; set; }  // عنوان العنصر
        int BorrowCount { get; }    // عدد مرات الاستعارة (قراءة فقط)
        void DisplayInfo();         // دالة لعرض معلومات العنصر (يمكن override)
    }
}
