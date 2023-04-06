using System;
using System.Collections.Generic;
using System.Linq;

namespace days
{
    class Program
    {
        static void Main(string[] args)
        {
            List<DateTime> holidays = new List<DateTime>();
            holidays.Add(new DateTime(2023, 4, 7));
            holidays.Add(new DateTime(2023, 4, 10));
            holidays.Add(new DateTime(2023, 4, 14));
            holidays.Add(new DateTime(2023, 4, 17));
            DateTime startDate = new DateTime(2023,4,6);
            (int days, DateTime start, DateTime end)= CalculateNumberOfLeaveDays(startDate, 5 , holidays);
            Console.WriteLine("Number of days is " + days);
            Console.WriteLine("Leave start date is " + start);
            Console.WriteLine("Leave end date is " + end);
        }

        public static int CalculateNumberOfLeaveDays(DateTime leaveStartDate, DateTime leaveEndDate)
        {
            return Convert.ToInt32(leaveEndDate.AddDays(1).Subtract(leaveStartDate).TotalDays);
        }

        public static (int, DateTime, DateTime) CalculateNumberOfLeaveDays(DateTime leaveStartDate, int requestedLeaveDays, List<DateTime> holidays)
        {
            if (holidays == null) holidays = new List<DateTime>();
            // Skip non-working days and holidays
            while (leaveStartDate.DayOfWeek == DayOfWeek.Saturday || leaveStartDate.DayOfWeek == DayOfWeek.Sunday || holidays.Contains(leaveStartDate))
            {
                leaveStartDate = leaveStartDate.AddDays(1);
            }

            DateTime leaveEndDate = leaveStartDate;
            int actualLeaveDays = 0;

            // Calculate actual leave days including holidays
            while (actualLeaveDays < requestedLeaveDays)
            {
                if (leaveEndDate.DayOfWeek != DayOfWeek.Saturday && leaveEndDate.DayOfWeek != DayOfWeek.Sunday && !holidays.Contains(leaveEndDate))
                {
                    actualLeaveDays++;
                }
                leaveEndDate = leaveEndDate.AddDays(1);
            }

            // Adjust end date to maintain requested leave days
            while (actualLeaveDays > requestedLeaveDays)
            {
                leaveEndDate = leaveEndDate.AddDays(-1);
                if (leaveEndDate.DayOfWeek != DayOfWeek.Saturday && leaveEndDate.DayOfWeek != DayOfWeek.Sunday && !holidays.Contains(leaveEndDate))
                {
                    actualLeaveDays--;
                }
            }

            return (requestedLeaveDays, leaveStartDate, leaveEndDate.AddDays(-1));
        }


    }
}
