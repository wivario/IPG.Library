using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    internal class LibraryManager
    {
        private List<Book> _Book = new List<Book>();
        private List<Employee> _Employee = new List<Employee>();
        private List<Customer> _Customer = new List<Customer>();
        private int _nextBookId = 1;
        private int _nextEmployeeId = 1;
        private int _nextCustomerId = 1;
        public void AddEmployee(string fullname, string email, DateTime dateofemployment, string username, string password,
                 HandledEmployee handled )
        {
            try
            {
                var newEmployee = new Employee(_nextEmployeeId++, fullname, email, dateofemployment, username, password,
                  handled );
                _Employee.Add(newEmployee);
                Console.WriteLine(newEmployee._UserName + newEmployee.EmployeeID);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"{ex.Message.ToString()}");
            }
        }
        public void DisplayEmployee()
        {
            foreach (var E in _Employee)
            {
                E.SendNotification(E._UserName);
                E.DisplayInfo();
            }
        }
        public Employee GetEmployeeById(int id)
        {
            return _Employee.FirstOrDefault(E => E.EmployeeID == id);
        }
        public bool UpdateEmployee(int id, string newfullname, string newusername)
        {
            var EmployeeToUpdate = GetEmployeeById(id);

            if (EmployeeToUpdate != null)
            {
                try
                {
                    EmployeeToUpdate._FullName = newfullname;
                    EmployeeToUpdate._UserName = newusername;
                    Console.WriteLine($"Completed Successfully");
                    return true;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"{ex.Message.ToString()}");
                    return false;
                }
            }
            Console.WriteLine($"It can't be empty");
            return false;
        }
        public bool DeleteEmployee(int id)
        {
            var EmployeeToRemove = GetEmployeeById(id);

            if (EmployeeToRemove != null)
            {
                _Employee.Remove(EmployeeToRemove);
                _Employee.RemoveAll(e => e.EmployeeID == id);

                Console.WriteLine("Removed Successfully");
                return true;
            }
            Console.WriteLine("Removal was unsuccessful");
            return false;
        }
    }
}
