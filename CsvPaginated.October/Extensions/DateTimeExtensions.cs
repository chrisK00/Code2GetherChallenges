using System;

namespace CsvPaginated.October.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateTime dob)
        {
            var today = DateTime.Today; 
            var age = today.Year - dob.Year;
            if (dob.Date > today.AddYears(-age))
            {
                //they have not had their birthday yet this year
                age--;
            }

            return age;
        }
    }
}
