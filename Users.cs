using System;

namespace LibrarySystem
{
    /// <summary>
    /// فئة أساسية مجردة تمثل مستخدماً (عميل أو موظف).
    /// جعلنا Id خاصية عمومية (public) لتسهيل الوصول دون استخدام Reflection.
    /// </summary>
    public abstract class Users
    {
        /// <summary>معرّف المستخدم (قراءة فقط).</summary>
        public int Id { get; }

        /// <summary>الاسم الكامل للمستخدم.</summary>
        protected string FullName { get; set; }

        /// <summary>البريد الإلكتروني.</summary>
        protected string Email { get; set; }

        /// <summary>اسم المستخدم للدخول.</summary>
        protected string UserName { get; set; }

        /// <summary>كلمة المرور .</summary>
        protected string Password { get; set; }

        /// <summary>الباني مع تحقق أساسي للمدخلات.</summary>
        protected Users(int id, string fullName, string email, string userName, string password)
        {
            if (id <= 0) throw new ArgumentException("User ID must be positive.", nameof(id));
            if (!Validator.IsNonEmpty(fullName)) throw new ArgumentException("Full name cannot be empty.", nameof(fullName));
            if (!Validator.IsEmail(email)) throw new ArgumentException("Invalid email.", nameof(email));
            if (!Validator.IsNonEmpty(userName)) throw new ArgumentException("Username cannot be empty.", nameof(userName));
            if (password == null || password.Length < 6) throw new ArgumentException("Password must be at least 6 characters.", nameof(password));

            Id = id;
            FullName = fullName.Trim();
            Email = email.Trim();
            UserName = userName.Trim();
            Password = password;
        }
    }
}
