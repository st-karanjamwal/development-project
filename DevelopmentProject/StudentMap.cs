using CsvHelper.Configuration;

namespace DevelopmentProject
{
    public sealed class StudentMap : ClassMap<Student>
    {
        public StudentMap()
        {
            Map(m => m.StudentName).Name(Constants.CsvHeaders.StudentName);
            Map(m => m.Grade).Name(Constants.CsvHeaders.Grade);
        }
    }
}
