using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    public enum BookStatus
    {
        Available,
        Sold,
        Borrowed,
        Reserved
    }
    public enum BookLanguage
    {
        English,
        Arabic
        //....
    }
    public enum BookType
    {
        Ditective,
        Romance,
        Horror,
        Science
        //....
    }
    internal class Book : DisplayInfoInterface
    {
        private readonly int id;
        private string BookTitle;
        private string Author;
        private int PublicationYear;
        private string PublicationHouse;
        private BookType Type;
        private BookStatus Status;
        private BookLanguage Language;

        public Book(string title, string author, int publicationyear, string publicationhouse,
            BookType type, BookStatus status, BookLanguage language)
        {
            this.BookTitle = title;
            this.Author = author;
            this.PublicationYear = publicationyear;
            this.PublicationHouse = publicationhouse;
            this.Type = type;
            this.Status = status;
            this.Language = language;
        }
        public int BookID
        {
            get { return id; }
        }
        public string _Title
        {
            get { return BookTitle; }
            private set
            {
                if (string.IsNullOrEmpty(value) || value.Length <= 2)
                    throw new ArgumentException("The title of the book should not " +
                            "be empty or less than three characters");
                BookTitle = value;
            }
        }
        public string _Author
        {
            get
            {
                return Author;
            }
            private set
            {
                Author = value;
            }
        }
        public int _PublicationYear
        {
            get => PublicationYear;
            private set
            {
                PublicationYear = value;
            }
        }
        public string _PublicationHouse
        {
            get => PublicationHouse;
            private set
            {
                PublicationHouse = value;
            }
        }
        public BookLanguage _Language
        {
            get => Language;
            private set
            {
                Language = value;
            }
        }
        public BookType _Type
        {
            get => Type;
            private set
            {
                Type = value;
            }
        }
        public BookStatus _Status
        {
            get => Status;
            set
            {
                Status = value;
            }
        }
        public virtual void DisplayInfo()
        {
            Console.WriteLine(this.id + this.BookTitle + this.Author + this.PublicationHouse +
                this.Type + this.Status + this.Language);
        }
        public void UpdateTitle(string newTitle)
        {
            if (string.IsNullOrEmpty(newTitle) || newTitle.Length <= 2)
                throw new ArgumentException("The title of the book should not " +
                        "be empty or less than three characters");
            this._Title = newTitle;
        }
        public void UpdateAuthor(string newAuthor)
        {
            if (string.IsNullOrEmpty(newAuthor) || newAuthor.Length <= 2)
                throw new ArgumentException("The title of the book should not " +
                        "be empty or less than three characters");
            this._Author = newAuthor;
        }
        public void Available()
        {
            if (Status != BookStatus.Available)
            {
                Status = BookStatus.Reserved;
                Console.WriteLine("This book is reserved");
            }
            else if (Status != BookStatus.Available)
            {
                Status = BookStatus.Sold;
                Console.WriteLine("This book is sold");
            }
            else
                Status = BookStatus.Borrowed;
            Console.WriteLine("this book is borrowed");
        }
        public void Reserved()
        {
            if (Status != BookStatus.Reserved)
            {
                Status = BookStatus.Sold;
                Console.WriteLine("This book is sold");
            }
            else if (Status != BookStatus.Reserved)
            {
                Status = BookStatus.Available;
                Console.WriteLine("This book is available");
            }
            else
                Status = BookStatus.Borrowed;
            Console.WriteLine("this book is borrowed");
        }
        public void Borrowed()
        {
            if (Status != BookStatus.Borrowed)
            {
                Status = BookStatus.Available;
                Console.WriteLine("this book is available");
            }
            else
                Status = BookStatus.Sold;
            Console.WriteLine("This book is sold");
        }
    }
}