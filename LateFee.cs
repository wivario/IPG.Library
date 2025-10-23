using System;

namespace LibrarySystem
{
    /// <summary>
    /// فئة بسيطة لحساب غرامات التأخير بناءً على سعر يومي.
    /// </summary>
    public class LateFee
    {
        /// <summary>قيمة الغرامة اليومية.</summary>
        public double DailyRate { get; private set; }

        public LateFee(double dailyRate = 1.0)
        {
            if (dailyRate < 0) throw new ArgumentException("Daily rate cannot be negative.", nameof(dailyRate));
            DailyRate = dailyRate;
        }

        /// <summary>حساب الغرامة لعدد الأيام المتأخرة.</summary>
        public double CalculateFee(int daysLate)
        {
            if (daysLate <= 0) return 0;
            return daysLate * DailyRate;
        }
    }
}
