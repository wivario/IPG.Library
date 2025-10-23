using System;
using System.Collections.Generic;

namespace LibrarySystem
{
    /// <summary>
    /// خدمة مركزية لإدارة الإشعارات (Broadcast و Notify).
    /// تحفظ قوائم المشتركين من العملاء والموظفين وتبعث الرسائل إليهم.
    /// </summary>
    public class NotificationService
    {
        private readonly List<Customer> _customers = new();
        private readonly List<Employee> _employees = new();

        /// <summary>تسجيل عميل لتلقي الإشعارات.</summary>
        public void Subscribe(Customer customer)
        {
            if (!_customers.Contains(customer))
                _customers.Add(customer);
        }

        /// <summary>تسجيل موظف لتلقي الإشعارات.</summary>
        public void Subscribe(Employee employee)
        {
            if (!_employees.Contains(employee))
                _employees.Add(employee);
        }

        /// <summary>إرسال إشعار عام لجميع المشتركين.</summary>
        public void Broadcast(string message)
        {
            foreach (var c in _customers)
                c.SendNotification(message);

            foreach (var e in _employees)
                e.SendNotification(message);
        }

        /// <summary>إرسال إشعار لشخص محدد عبر واجهة INotification.</summary>
        public void NotifyPerson(INotification person, string message)
        {
            person.SendNotification(message);
        }
    }
}
