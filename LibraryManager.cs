using System;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystem
{
    /// <summary>
    /// فئة لإدارة المكتبة: عناصر، عملاء، وموظفين؛ وتدير العمليات الأساسية (Borrow, Return, Reserve).
    /// أضفنا دوال للوصول إلى القوائم (Read-only) لدعم الواجهة التفاعلية.
    /// </summary>
    public class LibraryManager
    {
        // قوائم داخلية
        private readonly List<ItemBase> _items = new();
        private readonly List<Customer> _customers = new();
        private readonly List<Employee> _employees = new();

        // عداد للعناصر المضافة
        public static int TotalItemsAdded { get; private set; } = 0;

        #region Add / Find

        /// <summary>
        /// إضافة عنصر للمكتبة والاشتراك بحدث حالته.
        /// </summary>
        public void AddItem(ItemBase item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            _items.Add(item);
            TotalItemsAdded++;
            item.StatusChanged += OnItemStatusChanged;
        }

        /// <summary>إضافة عميل.</summary>
        public void AddCustomer(Customer customer)
        {
            if (customer == null) throw new ArgumentNullException(nameof(customer));
            _customers.Add(customer);
        }

        /// <summary>إضافة موظف.</summary>
        public void AddEmployee(Employee employee)
        {
            if (employee == null) throw new ArgumentNullException(nameof(employee));
            _employees.Add(employee);
        }

        /// <summary>البحث عن عنصر بواسطة المعرف (GUID).</summary>
        public ItemBase FindItemById(Guid id) => _items.FirstOrDefault(i => i.Id == id);

        /// <summary>البحث عن عميل بواسطة Id (المعرف الرقمي).</summary>
        public Customer FindCustomerById(int id) => _customers.FirstOrDefault(c => c.Id == id);

        /// <summary>البحث عن موظف بواسطة Id.</summary>
        public Employee FindEmployeeById(int id) => _employees.FirstOrDefault(e => e.Id == id);

        #endregion

        #region ReadOnly Accessors for UI

        /// <summary>
        /// إرجاع لائحة العناصر (قراءة فقط) لتمكين الواجهة من العرض والاختيار.
        /// </summary>
        public IReadOnlyList<ItemBase> GetAllItems() => _items.AsReadOnly();

        /// <summary>إرجاع لائحة العملاء.</summary>
        public IReadOnlyList<Customer> GetAllCustomers() => _customers.AsReadOnly();

        /// <summary>إرجاع لائحة الموظفين.</summary>
        public IReadOnlyList<Employee> GetAllEmployees() => _employees.AsReadOnly();

        #endregion

        #region Operations

        /// <summary>تنفيذ استعارة: يستدعي Use على العنصر ويحدّث سجل العميل.</summary>
        public bool BorrowItem(int customerId, Guid itemId)
        {
            var customer = FindCustomerById(customerId);
            var item = FindItemById(itemId);

            if (customer == null || item == null)
            {
                Console.WriteLine("[Error]: Borrow failed. Customer or item not found.");
                return false;
            }

            // هنا Use() يتضمن التحقق في الكلاسات الفرعية (مثلاً Book يتحقق من الحالة)
            item.Use();
            customer.AddBorrowed(item.Id);

            Console.WriteLine($"[Success]: Customer {customerId} borrowed \"{item.Title}\".");
            return true;
        }

        /// <summary>تنفيذ إرجاع العنصر وتحديث حالة العنصر وسجل العميل.</summary>
        public bool ReturnItem(int customerId, Guid itemId)
        {
            var customer = FindCustomerById(customerId);
            var item = FindItemById(itemId);

            if (customer == null || item == null)
            {
                Console.WriteLine("[Error]: Return failed. Customer or item not found.");
                return false;
            }

            customer.RemoveBorrowed(itemId);

            if (item is Book book)
                book.MarkAvailable();

            Console.WriteLine($"[Success]: Item \"{item.Title}\" returned by customer {customerId}.");
            return true;
        }

        /// <summary>تنفيذ حجز عنصر (يسجل في العميل ويغيّر حالة الكتاب إن كان كتاباً).</summary>
        public bool ReserveItem(int customerId, Guid itemId)
        {
            var customer = FindCustomerById(customerId);
            var item = FindItemById(itemId);

            if (customer == null || item == null)
            {
                Console.WriteLine("[Error]: Reservation failed. Customer or item not found.");
                return false;
            }

            customer.AddReservation(itemId);

            if (item is Book book)
                book.MarkReserved();

            Console.WriteLine($"[Success]: Item \"{item.Title}\" reserved by customer {customerId}.");
            return true;
        }

        #endregion

        #region Print / Events

        /// <summary>مستجيب لحدث حالة العنصر (مثل الوصول للشعبية) — يطبع رسالة على Console.</summary>
        private void OnItemStatusChanged(object sender, ItemEventArgs e)
        {
            Console.WriteLine($"[System Event]: {e.Message}");
        }

        /// <summary>طباعة ملخص المكتبة.</summary>
        public void PrintSummary()
        {
            Console.WriteLine("\n=== Library Summary ===");
            Console.WriteLine($"  Total Items: {_items.Count}");
            Console.WriteLine($"  Total Customers: {_customers.Count}");
            Console.WriteLine($"  Total Employees: {_employees.Count}");
            Console.WriteLine($"  Total Items Added: {TotalItemsAdded}");
        }

        /// <summary>طباعة كل العناصر بالتفصيل.</summary>
        public void PrintAllItems()
        {
            Console.WriteLine("\n=== Library Items ===");
            foreach (var item in _items)
                item.DisplayInfo();
        }

        #endregion
    }
}
