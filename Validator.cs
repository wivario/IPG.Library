using System;
using System.Text.RegularExpressions;

namespace IPG.Library
{
    /// <summary>
    /// فئة ساكنة (static) لاحتواء دوال التحقق/الـ validation.
    /// الهدف: نجمع دوال التحقق مكان واحد ليستخدمها باقي الكلاسات.
    /// </summary>
    public static class Validator
    {
        public static bool IsNonEmpty(string value) => !string.IsNullOrWhiteSpace(value);

        public static bool IsPositive(int value) => value > 0;

        public static bool IsWithin(string value, int maxLength)
            => value != null && value.Length <= maxLength;

        public static bool IsEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            // تعبير بسيط لصيغة الإيميل، 
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public static void ThrowIfNullOrEmpty(string value, string paramName)
        {
            if (!IsNonEmpty(value))
                throw new ArgumentException($"{paramName} must not be empty.", paramName);
        }
    }
}
