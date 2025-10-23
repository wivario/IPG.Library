using System;

namespace LibrarySystem
{
    /// <summary>
    /// مهمات ممكن أن يقوم بها الموظف داخل المكتبة.
    /// </summary>
    public enum EmployeeTask
    {
        AddBook,
        EditBook,
        DeleteBook,
        ManageBorrowing,
        General
    }

    /// <summary>
    /// فئة تمثل موظف المكتبة. تُنفّذ IDisplayInfo وINotification.
    /// </summary>
    public class Employee : Users, IDisplayInfo, INotification
    {
        private readonly DateTime _employmentDate;
        private EmployeeTask _currentTask;

        public Employee(int id, string fullName, string email, string userName, string password, DateTime employmentDate, EmployeeTask task)
            : base(id, fullName, email, userName, password)
        {
            _employmentDate = employmentDate;
            _currentTask = task;
        }

        /// <summary>عرض معلومات الموظف بشكل مفصّل.</summary>
        public void DisplayInfo()
        {
            Console.WriteLine($"\nEmployee [{Id}]");
            Console.WriteLine($"  Name: {FullName}");
            Console.WriteLine($"  Email: {Email}");
            Console.WriteLine($"  Username: {UserName}");
            Console.WriteLine($"  Employment Date: {_employmentDate.ToShortDateString()}");
            Console.WriteLine($"  Current Task: {_currentTask}");
        }

        /// <summary>استقبال إشعار نصي يُعرض على Console .</summary>
        public void SendNotification(string message)
        {
            Console.WriteLine($"[Notification to Employee {UserName}]: {message}");
        }

        /// <summary>تحديث مهمة الموظف.</summary>
        public void SetTask(EmployeeTask task) => _currentTask = task;
    }
}
