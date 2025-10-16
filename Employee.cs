using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    public enum HandledEmployee
    {
        AddBook,
        EditBook,
        DeleteBook,
    }
        internal class Employee : Users, NotificationInterface, DisplayInfoInterface
        {
            private readonly int id;
            private DateTime DateOfEmployment;
            private HandledEmployee HandLed;

            public Employee(int id, string fullname, string email, DateTime dateofemployment, string username,
                string password, HandledEmployee handled) : base(id,fullname,email,username,password)
            {
                this.DateOfEmployment = dateofemployment;
                this.HandLed = handled;
            }
            public int EmployeeID
            {
                get { return id; }
            }
            public string _FullName
            {
                get { return FullName; }
                set
                {
                    if (string.IsNullOrEmpty(value) || value.Length <= 2)
                        throw new ArgumentException("The FullName should not " +
                                "be empty or less than three characters");
                    FullName = value;
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
                Console.WriteLine(this.id + this.FullName + this.Email + this.DateOfEmployment + this.UserName +
                    this.Password + this.HandLed );
            }
            public string _UserName
            {
                get { return UserName; }
                set
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
            public DateTime _DateOfEmployment
            {
                get
                {
                    return DateOfEmployment;
                }
                private set
                {
                    DateOfEmployment = value;
                }
            }
            public void _HandledEmployee()
            {
                if (HandLed == HandledEmployee.AddBook)
                {
                    Console.WriteLine("AddBook");
                }
                if (HandLed == HandledEmployee.EditBook)
                {
                    Console.WriteLine("EditBook");
                }
                else
                    Console.WriteLine("DeleteBook");
            }
            public void SendNotification(string message)
            {
                Console.WriteLine($"{UserName}: {Email}\nNew message for you:" +
                                  $"{message}");
            }
        }
    }
