using System;
using System.Text.RegularExpressions;

namespace LibrarySystem
{
    /// <summary>
    /// فئة ساكنة تضم دوال التحقق من المدخلات (Validation).
    /// الغاية: عدم تكرار منطق التحقق في عدة أماكن.
    /// </summary>
    public static class Validator
    {
        /// <summary>يتحقق أن السلسلة ليست فارغة أو مسافات فقط.</summary>
        public static bool IsNonEmpty(string value) => !string.IsNullOrWhiteSpace(value);

        /// <summary>يتحقق أن العدد موجب (>0).</summary>
        public static bool IsPositive(int value) => value > 0;

        /// <summary>تحقق بسيط لصيغة البريد الإلكتروني .</summary>
        public static bool IsEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        /// <summary>يرمي استثناء إذا كانت السلسلة فارغة؛ يستخدم في setters وconstructors.</summary>
        public static void ThrowIfNullOrEmpty(string value, string paramName)
        {
            if (!IsNonEmpty(value))
                throw new ArgumentException($"{paramName} must not be empty.", paramName);
        }
    }
}
