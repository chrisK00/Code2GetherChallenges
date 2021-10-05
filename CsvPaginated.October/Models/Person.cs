using System;
using System.Text;

namespace CsvPaginated.October.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public decimal Salary { get; set; }

        public string FullName => $"{FirstName}-{LastName}";

        public override string ToString()
        {
            var sb = new StringBuilder()
                .Append($"{Id}: ")
                .Append($"{FullName}; ")
                .Append($"\t{Salary:c}")
                .Append($"\t{BirthDate}").AppendLine();

            return sb.ToString();
        }
    }
}