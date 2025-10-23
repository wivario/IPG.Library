using System;
using System.Collections.Generic;

namespace LibrarySystem
{
    /// <summary>
    /// فئة تمثل عميل/عضو المكتبة.
    /// تطبق IDisplayInfo و INotification لعرض البيانات واستقبال الإشعارات.
    /// </summary>
    public class Customer : Users, IDisplayInfo, INotification
    {
        // قوائم المعرفات للعناصر المستعارة والمحجوزة
        private readonly List<Guid> _borrowedItems = new();
        private readonly List<Guid> _reservedItems = new();

        private readonly DateTime _dateOfRegistration;

        public Customer(int id, string fullName, string email, string userName, string password, DateTime registrationDate)
            : base(id, fullName, email, userName, password)
        {
            _dateOfRegistration = registrationDate;
        }

        /// <summary>إضافة عنصر إلى سجل الاستعارات.</summary>
        public void AddBorrowed(Guid itemId)
        {
            if (!_borrowedItems.Contains(itemId))
                _borrowedItems.Add(itemId);
        }

        /// <summary>إزالة عنصر من سجل الاستعارات عند الإرجاع.</summary>
        public void RemoveBorrowed(Guid itemId) => _borrowedItems.Remove(itemId);

        /// <summary>إضافة عنصر إلى الحجوزات.</summary>
        public void AddReservation(Guid itemId)
        {
            if (!_reservedItems.Contains(itemId))
                _reservedItems.Add(itemId);
        }

        /// <summary>إزالة عنصر من الحجوزات.</summary>
        public void RemoveReservation(Guid itemId) => _reservedItems.Remove(itemId);

        /// <summary>عرض معلومات مفصّلة عن العميل (Detailed Format).</summary>
        public void DisplayInfo()
        {
            Console.WriteLine($"\nCustomer [{Id}]");
            Console.WriteLine($"  Name: {FullName}");
            Console.WriteLine($"  Email: {Email}");
            Console.WriteLine($"  Username: {UserName}");
            Console.WriteLine($"  Registered: {_dateOfRegistration.ToShortDateString()}");
            Console.WriteLine($"  Borrowed Items Count: {_borrowedItems.Count}");
            Console.WriteLine($"  Reserved Items Count: {_reservedItems.Count}");
        }

        /// <summary>استقبال إشعار نصي يُعرض على Console .</summary>
        public void SendNotification(string message)
        {
            Console.WriteLine($"[Notification to Customer {UserName}]: {message}");
        }
    }
}
