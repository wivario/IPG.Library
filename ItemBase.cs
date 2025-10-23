using System;

namespace LibrarySystem
{
    /// <summary>
    /// الفئة الأساسية المجردة لكل عناصر المكتبة.
    /// تطبق IDisplayInfo وIItem، وتحتوي المنطق المشترك (Id, Title, BorrowCount, Events).
    /// </summary>
    public abstract class ItemBase : IItem, IDisplayInfo
    {
        // حقل خاص لتخزين المعرف الفريد
        private readonly Guid _id;

        // حقل خاص لتخزين العنوان
        private string _title;

        // حقل خاص لعداد مرات الاستعارة
        private int _borrowCount;

        // حدث يُطلق عند تغيّر مهم (مثلاً: الوصول إلى شعبية معينة)
        public event EventHandler<ItemEventArgs> StatusChanged;

        /// <summary>قراءة المعرف الفريد (GUID).</summary>
        public Guid Id => _id;

        /// <summary>العنوان مع تحقق عند التعيين.</summary>
        public string Title
        {
            get => _title;
            set
            {
                Validator.ThrowIfNullOrEmpty(value, nameof(Title));
                _title = value.Trim();
            }
        }

        /// <summary>قراءة عدد الاستعارات من الخارج.</summary>
        public int BorrowCount => _borrowCount;

        /// <summary>عتبة افتراضية للاعتبار "شعبي". يمكن إعادة تعريفها في الفئات الفرعية.</summary>
        protected virtual int PopularThreshold => 10;

        /// <summary>الباني: يعين Id جديدًا ويستخدم الـ setter للعنوان (فيتفقده).</summary>
        protected ItemBase(string title)
        {
            _id = Guid.NewGuid();
            Title = title;
            _borrowCount = 0;
        }

        /// <summary>
        /// طريقة محمية لزيادة عداد الاستعارات.
        /// الفئات الفرعية تستدعيها عند حدوث "Use" فعلي.
        /// وتطلق حدثًا عند الوصول للعتبة.
        /// </summary>
        protected void IncrementBorrowCount()
        {
            _borrowCount++;
            if (_borrowCount == PopularThreshold)
            {
                OnStatusChanged(new ItemEventArgs(this, $"Item '{Title}' reached popularity threshold ({PopularThreshold})."));
            }
        }

        /// <summary>يسمح بإطلاق الحدث للمشتركين.</summary>
        protected void OnStatusChanged(ItemEventArgs e)
        {
            StatusChanged?.Invoke(this, e);
        }

        /// <summary>عرض المعلومات بطريقة مفصّلة (Detailed Format).</summary>
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"\n{GetType().Name} [{Id}]");
            Console.WriteLine($"  Title: {Title}");
            Console.WriteLine($"  BorrowCount: {BorrowCount}");
        }

        /// <summary>دالة مجردة تصف "استخدام" العنصر — يجب على الفئات الفرعية تنفيذها.</summary>
        public abstract void Use();
    }
}
