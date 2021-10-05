using CsvHelper.Configuration;
using CsvPaginated.October.Models;

namespace CsvPaginated.October.Data
{
    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Map(x => x.FirstName).Name("first_name");
            Map(x => x.Id).Name("id");
            Map(x => x.LastName).Name("last_name");
            Map(x => x.BirthDate).Name("birth_date");
            Map(x => x.Salary).Name("salary").Convert(x =>
            {
                var field = x.Row.GetField("salary");
                field = field.Replace("$", "");
                return decimal.Parse(field);
            });
        }
    }
}
