using System;

namespace LibrarySystem
{
    /// <summary>
    /// تصنيف المجلات.
    /// </summary>
    public enum MagazineCategory
    {
        News,
        Fashion,
        Sports,
        Science,
        Culture,
        Other
    }

    /// <summary>
    /// فئة تمثل مجلة في المكتبة.
    /// </summary>
    public class Magazine : ItemBase, IDisplayInfo
    {
        public int IssueNumber { get; private set; }
        public string Month { get; private set; }
        public MagazineCategory Category { get; private set; }

        public Magazine(string title, int issueNumber, string month, MagazineCategory category)
            : base(title)
        {
            if (issueNumber <= 0)
                throw new ArgumentException("Issue number must be positive.", nameof(issueNumber));
            Validator.ThrowIfNullOrEmpty(month, nameof(month));

            IssueNumber = issueNumber;
            Month = month.Trim();
            Category = category;
        }

        public override void Use()
        {
            IncrementBorrowCount();
            Console.WriteLine($"[Success]: Magazine \"{Title}\" (Issue {IssueNumber}) has been borrowed.");
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"  IssueNumber: {IssueNumber}");
            Console.WriteLine($"  Month: {Month}");
            Console.WriteLine($"  Category: {Category}");
        }
    }
}
