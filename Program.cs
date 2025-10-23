using System;
using System.Globalization;
using System.Linq;

namespace LibrarySystem
{
    /// <summary>
    /// الواجهة الرئيسية التفاعلية (Console Menu).
    /// تسمح للمستخدم بأداء جميع العمليات: إضافة/عرض/استعارة/إرجاع/حجز/إشعارات/غرامات.
    /// </summary>
    internal class Program
    {
        // كائنات نظام
        private static LibraryManager _manager = new LibraryManager();
        private static NotificationService _notifier = new NotificationService();
        private static LateFee _lateFee = new LateFee(2.5);

        static void Main(string[] args)
        {
            //  يمكننا الإستغناء عن القسم التالي إذا أردنا أن يكون النظام نظيف بدون بيانات افتراضية
            SeedDemoData(); // يضيف بعض البيانات الأولية لتجربة الواجهة بسرعة

            // حلقة القائمة الرئيسية
            while (true)
            {
                ShowMainMenu();
                var choice = ReadInt("Select option (number): ");

                switch (choice)
                {
                    case 1: AddItemMenu(); break;
                    case 2: AddCustomerMenu(); break;
                    case 3: AddEmployeeMenu(); break;
                    case 4: ListItems(); break;
                    case 5: ListCustomers(); break;
                    case 6: ListEmployees(); break;
                    case 7: BorrowItemMenu(); break;
                    case 8: ReturnItemMenu(); break;
                    case 9: ReserveItemMenu(); break;
                    case 10: ShowSummary(); break;
                    case 11: SendNotificationMenu(); break;
                    case 12: CalculateLateFeeMenu(); break;
                    case 0: Console.WriteLine("Exiting..."); return;
                    default: Console.WriteLine("Unknown option. Try again."); break;
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        #region Menu Helpers (Arabic comments, English prompts)

        /// <summary>عرض الخيارات الرئيسية للمستخدم.</summary>
        private static void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Library Management System (Menu) ===\n");
            Console.WriteLine("1) Add Item (Book / Magazine / DVD)");
            Console.WriteLine("2) Add Customer");
            Console.WriteLine("3) Add Employee");
            Console.WriteLine("4) List Items");
            Console.WriteLine("5) List Customers");
            Console.WriteLine("6) List Employees");
            Console.WriteLine("7) Borrow Item");
            Console.WriteLine("8) Return Item");
            Console.WriteLine("9) Reserve Item");
            Console.WriteLine("10) Show Library Summary");
            Console.WriteLine("11) Send Notification");
            Console.WriteLine("12) Calculate Late Fee");
            Console.WriteLine("0) Exit");
            Console.WriteLine();
        }

        /// <summary>قراءة عدد صحيح من المستخدم مع رسالة تكرارية حتى يقدّم قيمة صحيحة.</summary>
        private static int ReadInt(string prompt)
        {
            int val;
            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out val))
            {
                Console.Write("Invalid input. Enter an integer: ");
            }
            return val;
        }

        /// <summary>قراءة قيمة مزدوجة (double) آمنة.</summary>
        private static double ReadDouble(string prompt)
        {
            double val;
            Console.Write(prompt);
            while (!double.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out val))
            {
                Console.Write("Invalid input. Enter a number: ");
            }
            return val;
        }

        /// <summary>قراءة نص غير فارغ.</summary>
        private static string ReadNonEmpty(string prompt)
        {
            string s;
            Console.Write(prompt);
            while (string.IsNullOrWhiteSpace(s = Console.ReadLine()))
            {
                Console.Write("Input cannot be empty. Try again: ");
            }
            return s.Trim();
        }

        #endregion

        #region Seed Demo Data

        /// <summary>يضيف بيانات مبدئية لتسهيل التجربة.</summary>
        private static void SeedDemoData()
        {
            // موظف وعميل افتراضيان
            var emp = new Employee(1, "Ahmed Khalil", "ahmad@mail.com", "ahmadk", "pass123", DateTime.Now.AddYears(-2), EmployeeTask.ManageBorrowing);
            var cust = new Customer(1, "Layla Abdullah", "layla@mail.com", "layla", "layla789", DateTime.Now.AddMonths(-6));
            _manager.AddEmployee(emp);
            _manager.AddCustomer(cust);
            _notifier.Subscribe(emp);
            _notifier.Subscribe(cust);

            // عناصر افتراضية
            var book = new Book("Introduction to Programming", "Sami Hassan", 2021, "Arabic Press", BookType.Educational, BookLanguage.English);
            var mag = new Magazine("Modern Science", 25, "March", MagazineCategory.Science);
            var dvd = new DVD("Learn C#", "Marwan Issa", 1.5, DvdGenre.Educational);
            _manager.AddItem(book);
            _manager.AddItem(mag);
            _manager.AddItem(dvd);
        }

        #endregion

        #region Menu Actions (Add / List / Borrow / Return / Reserve / Notify / Fee)

        /// <summary>قائمة لإضافة عنصر جديد — يسأل النوع ثم الحقول الخاصة بكل نوع.</summary>
        private static void AddItemMenu()
        {
            Console.WriteLine("\n-- Add Item --");
            Console.WriteLine("Select type: 1) Book  2) Magazine  3) DVD");
            var t = ReadInt("Type: ");
            try
            {
                if (t == 1)
                {
                    var title = ReadNonEmpty("Title: ");
                    var author = ReadNonEmpty("Author: ");
                    var year = ReadInt("Publication Year: ");
                    var publisher = ReadNonEmpty("Publisher: ");
                    Console.WriteLine("Select Book Type: 1) Science 2) History 3) Novel 4) Educational 5) Other");
                    var bt = ReadInt("Choice: ");
                    var bookType = bt switch
                    {
                        1 => BookType.Science,
                        2 => BookType.History,
                        3 => BookType.Novel,
                        4 => BookType.Educational,
                        _ => BookType.Other
                    };
                    Console.WriteLine("Select Language: 1) Arabic 2) English 3) French 4) Other");
                    var bl = ReadInt("Choice: ");
                    var lang = bl switch
                    {
                        1 => BookLanguage.Arabic,
                        2 => BookLanguage.English,
                        3 => BookLanguage.French,
                        _ => BookLanguage.Other
                    };
                    var book = new Book(title, author, year, publisher, bookType, lang);
                    _manager.AddItem(book);
                    Console.WriteLine("[Success]: Book added.");
                }
                else if (t == 2)
                {
                    var title = ReadNonEmpty("Title: ");
                    var issue = ReadInt("Issue number: ");
                    var month = ReadNonEmpty("Month: ");
                    Console.WriteLine("Select Category: 1) News 2) Fashion 3) Sports 4) Science 5) Culture 6) Other");
                    var catChoice = ReadInt("Choice: ");
                    var category = catChoice switch
                    {
                        1 => MagazineCategory.News,
                        2 => MagazineCategory.Fashion,
                        3 => MagazineCategory.Sports,
                        4 => MagazineCategory.Science,
                        5 => MagazineCategory.Culture,
                        _ => MagazineCategory.Other
                    };
                    var mag = new Magazine(title, issue, month, category);
                    _manager.AddItem(mag);
                    Console.WriteLine("[Success]: Magazine added.");
                }
                else if (t == 3)
                {
                    var title = ReadNonEmpty("Title: ");
                    var director = ReadNonEmpty("Director: ");
                    var duration = ReadDouble("Duration (hours): ");
                    Console.WriteLine("Select Genre: 1) Action 2) Comedy 3) Drama 4) Documentary 5) Educational 6) Other");
                    var g = ReadInt("Choice: ");
                    var genre = g switch
                    {
                        1 => DvdGenre.Action,
                        2 => DvdGenre.Comedy,
                        3 => DvdGenre.Drama,
                        4 => DvdGenre.Documentary,
                        5 => DvdGenre.Educational,
                        _ => DvdGenre.Other
                    };
                    var dvd = new DVD(title, director, duration, genre);
                    _manager.AddItem(dvd);
                    Console.WriteLine("[Success]: DVD added.");
                }
                else
                {
                    Console.WriteLine("Invalid type selection.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error]: {ex.Message}");
            }
        }

        /// <summary>قائمة لإضافة عميل جديد.</summary>
        private static void AddCustomerMenu()
        {
            Console.WriteLine("\n-- Add Customer --");
            try
            {
                var id = ReadInt("Customer ID (integer): ");
                var name = ReadNonEmpty("Full Name: ");
                var email = ReadNonEmpty("Email: ");
                var username = ReadNonEmpty("Username: ");
                var password = ReadNonEmpty("Password (min 6 chars): ");
                var regDate = DateTime.Now;
                var customer = new Customer(id, name, email, username, password, regDate);
                _manager.AddCustomer(customer);
                _notifier.Subscribe(customer);
                Console.WriteLine("[Success]: Customer added and subscribed to notifications.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error]: {ex.Message}");
            }
        }

        /// <summary>قائمة لإضافة موظف جديد.</summary>
        private static void AddEmployeeMenu()
        {
            Console.WriteLine("\n-- Add Employee --");
            try
            {
                var id = ReadInt("Employee ID (integer): ");
                var name = ReadNonEmpty("Full Name: ");
                var email = ReadNonEmpty("Email: ");
                var username = ReadNonEmpty("Username: ");
                var password = ReadNonEmpty("Password (min 6 chars): ");
                var empDate = DateTime.Now;
                Console.WriteLine("Select Task: 1) AddBook 2) EditBook 3) DeleteBook 4) ManageBorrowing 5) General");
                var t = ReadInt("Choice: ");
                var task = t switch
                {
                    1 => EmployeeTask.AddBook,
                    2 => EmployeeTask.EditBook,
                    3 => EmployeeTask.DeleteBook,
                    4 => EmployeeTask.ManageBorrowing,
                    _ => EmployeeTask.General
                };
                var emp = new Employee(id, name, email, username, password, empDate, task);
                _manager.AddEmployee(emp);
                _notifier.Subscribe(emp);
                Console.WriteLine("[Success]: Employee added and subscribed to notifications.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error]: {ex.Message}");
            }
        }

        /// <summary>عرض العناصر مع أرقام الصفوف لتمكين الاختيار.</summary>
        private static void ListItems()
        {
            var items = _manager.GetAllItems();
            Console.WriteLine("\n=== Items List ===");
            if (items.Count == 0) { Console.WriteLine("No items."); return; }
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"[{i}] {items[i].GetType().Name} - {items[i].Title} (BorrowCount: {items[i].BorrowCount}) - Id: {items[i].Id}");
            }
        }

        /// <summary>عرض العملاء مفصلاً.</summary>
        private static void ListCustomers()
        {
            var customers = _manager.GetAllCustomers();
            Console.WriteLine("\n=== Customers List ===");
            if (customers.Count == 0) { Console.WriteLine("No customers."); return; }
            foreach (var c in customers)
                c.DisplayInfo();
        }

        /// <summary>عرض الموظفين مفصلاً.</summary>
        private static void ListEmployees()
        {
            var employees = _manager.GetAllEmployees();
            Console.WriteLine("\n=== Employees List ===");
            if (employees.Count == 0) { Console.WriteLine("No employees."); return; }
            foreach (var e in employees)
                e.DisplayInfo();
        }

        /// <summary>قائمة استعارة: نعرض العملاء ثم العناصر ليختار المستخدم.</summary>
        private static void BorrowItemMenu()
        {
            Console.WriteLine("\n-- Borrow Item --");
            try
            {
                var customerId = ReadInt("Customer ID: ");
                var items = _manager.GetAllItems();
                if (items.Count == 0) { Console.WriteLine("No items available."); return; }
                ListItems();
                var idx = ReadInt("Enter item index to borrow: ");
                if (idx < 0 || idx >= items.Count) { Console.WriteLine("Invalid index."); return; }
                var itemId = items[idx].Id;
                _manager.BorrowItem(customerId, itemId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error]: {ex.Message}");
            }
        }

        /// <summary>قائمة إرجاع: يطلب Id العميل ثم يطلب اختيار عنصر من قائمة العناصر.</summary>
        private static void ReturnItemMenu()
        {
            Console.WriteLine("\n-- Return Item --");
            try
            {
                var customerId = ReadInt("Customer ID: ");
                ListItems();
                var idx = ReadInt("Enter item index to return: ");
                var items = _manager.GetAllItems();
                if (idx < 0 || idx >= items.Count) { Console.WriteLine("Invalid index."); return; }
                var itemId = items[idx].Id;
                _manager.ReturnItem(customerId, itemId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error]: {ex.Message}");
            }
        }

        /// <summary>قائمة حجز عنصر.</summary>
        private static void ReserveItemMenu()
        {
            Console.WriteLine("\n-- Reserve Item --");
            try
            {
                var customerId = ReadInt("Customer ID: ");
                ListItems();
                var idx = ReadInt("Enter item index to reserve: ");
                var items = _manager.GetAllItems();
                if (idx < 0 || idx >= items.Count) { Console.WriteLine("Invalid index."); return; }
                var itemId = items[idx].Id;
                _manager.ReserveItem(customerId, itemId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error]: {ex.Message}");
            }
        }

        /// <summary>عرض ملخص المكتبة.</summary>
        private static void ShowSummary()
        {
            _manager.PrintSummary();
        }

        /// <summary>قائمة إرسال إشعار: يمكن الإرسال كـ Broadcast أو لشخص محدد عبر Id.</summary>
        private static void SendNotificationMenu()
        {
            Console.WriteLine("\n-- Send Notification --");
            Console.WriteLine("1) Broadcast to all subscribers");
            Console.WriteLine("2) Notify specific customer by ID");
            var ch = ReadInt("Choice: ");
            if (ch == 1)
            {
                var msg = ReadNonEmpty("Message: ");
                _notifier.Broadcast(msg);
                Console.WriteLine("[Success]: Broadcast sent.");
            }
            else if (ch == 2)
            {
                var id = ReadInt("Customer ID: ");
                var cust = _manager.FindCustomerById(id);
                if (cust == null) { Console.WriteLine("Customer not found."); return; }
                var msg = ReadNonEmpty("Message: ");
                _notifier.NotifyPerson(cust, msg);
                Console.WriteLine("[Success]: Message sent to customer.");
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }

        /// <summary>حساب غرامة التأخير بواسطة LateFee.</summary>
        private static void CalculateLateFeeMenu()
        {
            Console.WriteLine("\n-- Calculate Late Fee --");
            var days = ReadInt("Days late: ");
            var fee = _lateFee.CalculateFee(days);
            Console.WriteLine($"Late fee for {days} day(s) = {fee}");
        }

        #endregion
    }
}
