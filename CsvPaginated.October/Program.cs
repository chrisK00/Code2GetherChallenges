using System;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvPaginated.October.Data;
using CsvPaginated.October.Extensions;
using CsvPaginated.October.Models;

namespace CsvPaginated.October
{
    internal class Program
    {
        private static void Main()
        {
            const int minAge = 18;
            const decimal minSalary = 2000;

            using var streamReader = new StreamReader("Data/targets.csv");
            using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            csvReader.Context.RegisterClassMap<PersonMap>();

            var people = csvReader.GetRecords<Person>()
                .Where(x => x.BirthDate != null
                && x.BirthDate.Value < DateTime.Now
                && x.BirthDate.Value.CalculateAge() > minAge
                && x.Salary > minSalary)
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .ToList();

            var menu = new PaginatedMenu<Person>(people);
            menu.Show();
        }
    }
}