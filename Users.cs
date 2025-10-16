using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    public abstract class Users
    {
        protected int Id { get; }
        protected string FullName { get; set; }
        protected string Email { get; set; }
        protected string UserName { get; set; }
        protected string Password { get; set; }
        public Users (int id , string fullname, string email , string username, string password)
        {
            this.Id = id;
            this.FullName = fullname;
            this.Email = email;
            this.UserName = username;
            this.Password = password;
        }
    }
}
