using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    internal class Customer : Users, DisplayInfoInterface, NotificationInterface
    {
        private readonly int id;
        private DateTime DateOfRegestration;
        private Book BorrowedBooks;

        public Customer(int id ,string fullname, string email, string username, string password, DateTime dateofregestration,
            Book borrowedBooks) : base(id, fullname, email, username, password)
        {
            this.DateOfRegestration = dateofregestration;
            this.BorrowedBooks = borrowedBooks;
        }
        public int CustomerID
        {
            get { return id; }
        }
        public string _FullName
        {
            get { return FullName; }
            private set
            {
                if (string.IsNullOrEmpty(value) || value.Length <= 2)
                    throw new ArgumentException("The FullName should not " +
                            "be empty or less than three characters");
                FullName = value;
            }
        }
        public string _UserName
        {
            get { return UserName; }
            private set
            {
                if (string.IsNullOrEmpty(value) || value.Length <= 2)
                    throw new ArgumentException("The UserName should not " +
                            "be empty or less than three characters");
                UserName = value;
            }
        }
        public string _Password
        {
            get { return Password; }
            private set
            {
                if (string.IsNullOrEmpty(value) || value.Length <= 5)
                    throw new ArgumentException("The Password should not " +
                            "be empty or less than three characters");
                Password = value;
            }
        }
        public void UpdateEmail(string newEmail)
        {
            if (!string.IsNullOrWhiteSpace(Email) && Email.Contains("@") && Email.Contains("."))
            {
                Console.WriteLine("The email is correct");
            }
            else
            {
                Console.WriteLine("Invalid or empty email");
            }
        }
        public virtual void DisplayInfo()
        {
            Console.WriteLine(this.id + this.FullName + this.Email + this.UserName + this.Password +
                this.DateOfRegestration + this.BorrowedBooks);
        }
        public DateTime _DateOfRegestration
        {
            get
            {
                return DateOfRegestration;
            }
            private set
            {
                DateOfRegestration = value;
            }
        }
        public Book _BorrowedBooks
        {
            get
            {
                return BorrowedBooks;
            }
            private set
            {
                BorrowedBooks = value;
            }
        }
        public void SendNotification(string message)
        {
            Console.WriteLine($"{UserName}: {Email}\nNew message for you:" +
                              $"{message}");
        }
    }
}
