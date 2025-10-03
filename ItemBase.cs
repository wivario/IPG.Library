using System;

namespace IPG.Library
{
    /// <summary>
    /// فئة مجردة (abstract) تطبق IItem وتحتوي المنطق المشترك لكل العناصر.
    /// - الحقول private
    /// - خصائص مع التحقق (validation) عند التعيين
    /// - دالة مجردة Use() تُعاد تعريفها في الفئات الفرعية (مثلاً Book)
    /// </summary>
    public abstract class ItemBase : IItem
    {
        // حقول خاصة (private) — لا تُكشف مباشرة للخارج
        private readonly Guid _id;
        private string _title;
        private int _borrowCount;

        // قراءة فقط: معرف فريد (GUID) يُعطى عند الإنشاء
        public Guid Id => _id;

        // خاصية عامة مع التحقق عند التعيين
        public string Title
        {
            get => _title;
            set
            {
                if (!Validator.IsNonEmpty(value))
                    throw new ArgumentException("Title must not be empty.", nameof(Title));
                _title = value.Trim();
            }
        }

        // عدد مرات الاستعارة — قراءة فقط من الخارج
        public int BorrowCount => _borrowCount;

        // البناء: يعطينا Id جديد ويعين عنوان مع التحقق
        protected ItemBase(string title)
        {
            _id = Guid.NewGuid();
            Title = title;   // يستخدم setter (فيه التحقق)
            _borrowCount = 0;
        }

        /// <summary>
        /// دالة مجردة يجب أن تعيد تعريفها في الفئة الفرعية.
        /// المقصود: تنفيذ استخدام/استعارة العنصر بآلية النوع المحدد.
        /// الفئة الفرعية يجب أن ترفع _borrowCount بالطريقة المناسبة (مثلاً عن طريق استدعاء IncrementBorrowCount()).
        /// </summary>
        public abstract void Use();

        /// <summary>
        /// دالة افتراضية لعرض معلومات العنصر — يمكن للفئات الفرعية أن توسع/تعدل السلوك.
        /// </summary>
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"{GetType().Name} [{Id}]: {Title} (Borrows: {BorrowCount})");
        }

        /// <summary>
        /// طريقة محمية (protected) لزيادة عداد الاستعارات.
        /// متاحة للفئات الفرعية لكي يحدثوا العداد بطريقة محمية.
        /// لا نجعل الحقل عامّاً ولا نسمح بتعديله مباشرة من أي مكان آخر.
        /// </summary>
        protected void IncrementBorrowCount()
        {
            _borrowCount++;
        }
    }
}
