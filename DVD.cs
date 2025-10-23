using System;

namespace LibrarySystem
{
    /// <summary>
    /// تصنيف/نوع محتوى أقراص DVD.
    /// </summary>
    public enum DvdGenre
    {
        Action,
        Comedy,
        Drama,
        Documentary,
        Educational,
        Other
    }

    /// <summary>
    /// فئة تمثل قرص DVD في المكتبة.
    /// </summary>
    public class DVD : ItemBase, IDisplayInfo
    {
        public string Director { get; private set; }
        public double DurationHours { get; private set; }
        public DvdGenre Genre { get; private set; }

        public DVD(string title, string director, double durationHours, DvdGenre genre)
            : base(title)
        {
            Validator.ThrowIfNullOrEmpty(director, nameof(director));
            if (durationHours <= 0)
                throw new ArgumentException("Duration must be greater than zero.", nameof(durationHours));

            Director = director.Trim();
            DurationHours = durationHours;
            Genre = genre;
        }

        public override void Use()
        {
            IncrementBorrowCount();
            Console.WriteLine($"[Success]: DVD \"{Title}\" directed by {Director} has been borrowed.");
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"  Director: {Director}");
            Console.WriteLine($"  DurationHours: {DurationHours}");
            Console.WriteLine($"  Genre: {Genre}");
        }
    }
}
