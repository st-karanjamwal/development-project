using CsvHelper.Configuration.Attributes;

namespace DevelopmentProject
{
    public class Student
    {
        [Name(Constants.CsvHeaders.StudentName)]
        public string StudentName { get; set; }

        [Name(Constants.CsvHeaders.Grade)]
        public decimal Grade { get; set; }
    }
}
