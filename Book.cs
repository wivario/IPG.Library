using System;

namespace LibrarySystem
{
    /// <summary>
    /// حالة الكتاب داخل المكتبة.
    /// </summary>
    public enum BookStatus
    {
        Available,
        Reserved,
        Borrowed,
        Sold
    }

    /// <summary>
    /// لغة الكتاب.
    /// </summary>
    public enum BookLanguage
    {
        Arabic,
        English,
        French,
        Other
    }

    /// <summary>
    /// نوع الكتاب أو تصنيفه.
    /// </summary>
    public enum BookType
    {
        Science,
        History,
        Novel,
        Educational,
        Other
    }

    /// <summary>
    /// فئة تمثل كتابًا. ترث ItemBase وتطبّق IDisplayInfo صراحة.
    /// </summary>
    public class Book : ItemBase, IDisplayInfo
    {
        // خصائص خاصة بالكتاب
        public string Author { get; private set; }
        public int PublicationYear { get; private set; }
        public string Publisher { get; private set; }
        public BookType Type { get; private set; }
        public BookStatus Status { get; private set; }
        public BookLanguage Language { get; private set; }

        /// <summary>الباني يتحقق من صحة المعطيات ثم يخزنها.</summary>
        public Book(string title, string author, int publicationYear, string publisher,
                    BookType type, BookLanguage language = BookLanguage.English,
                    BookStatus status = BookStatus.Available)
            : base(title)
        {
            Validator.ThrowIfNullOrEmpty(author, nameof(author));
            Validator.ThrowIfNullOrEmpty(publisher, nameof(publisher));
            if (publicationYear < 1500 || publicationYear > DateTime.Now.Year)
                throw new ArgumentException("Invalid publication year.", nameof(publicationYear));

            Author = author.Trim();
            PublicationYear = publicationYear;
            Publisher = publisher.Trim();
            Type = type;
            Language = language;
            Status = status;
        }

        /// <summary>
        /// عند استخدام الكتاب (استعارته) — نتحقق من الحالة ونزيد العداد ونحدّث الحالة.
        /// </summary>
        public override void Use()
        {
            if (Status == BookStatus.Available)
            {
                Status = BookStatus.Borrowed;
                IncrementBorrowCount();
                Console.WriteLine($"[Success]: Book \"{Title}\" has been borrowed.");
            }
            else
            {
                Console.WriteLine($"[Info]: Book \"{Title}\" cannot be borrowed. Current status: {Status}.");
            }
        }

        /// <summary>عرض معلومات مفصّلة عن الكتاب.</summary>
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"  Author: {Author}");
            Console.WriteLine($"  PublicationYear: {PublicationYear}");
            Console.WriteLine($"  Publisher: {Publisher}");
            Console.WriteLine($"  Type: {Type}");
            Console.WriteLine($"  Language: {Language}");
            Console.WriteLine($"  Status: {Status}");
        }

        public void MarkAvailable() => Status = BookStatus.Available;
        public void MarkReserved() => Status = BookStatus.Reserved;
        public void MarkSold() => Status = BookStatus.Sold;
    }
}
