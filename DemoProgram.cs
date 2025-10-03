using System;
using IPG.Library;
// ملف توضيحي للإختبار
namespace IPG.Demo
{
    // فئة مؤقتة فقط لنعطي مثال عملي على كيفية اشتقاق ItemBase
    internal class DemoItem : ItemBase
    {
        public DemoItem(string title) : base(title) { }

        public override void Use()
        {
            // سلوك مبسط: عند الاستخدام نزيد عداد الاستعارات
            IncrementBorrowCount();
            Console.WriteLine($"(Demo) Item '{Title}' was used.");
        }
    }

    class Program
    {
        static void Main()
        {
            var it = new DemoItem("Intro to OOP");
            it.DisplayInfo();          // يطبع معلومات أولية
            it.Use(); it.Use();        // نستخدمه مرتين
            it.DisplayInfo();          // تشوف BorrowCount = 2

            try
            {
                it.Title = "";         // تجربة تحقق: لازم يرمى استثناء
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Validation error caught: " + ex.Message);
            }
        }
    }
}
