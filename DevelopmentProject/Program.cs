using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DevelopmentProject
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // xls files were malformed and I simply changed the extensions of the files to csv
                // because I could see the data comma separated when I opened them in excel 2019

                var resultFilePath = "Files/result.txt";
                var classResults = new List<ClassResult>();

                var studentService = new StudentService();

                Console.WriteLine(@"Please find the result file at DevelopmentProject\bin\Debug\netcoreapp3.1\Files\result.txt\n\n");

                var files = new List<string> { "ClassA", "ClassB", "ClassC" };

                foreach (var file in files)
                {
                    var fileName = $"Files/{file}.csv";

                    var students = studentService.ReadCsvFileToStudentListModel(fileName);

                    // student scores with a decimal should be truncated to the nearest whole number  
                    // before calculating class average 
                    students = students 
                        .Select(x => new Student
                        {
                            StudentName = x.StudentName,
                            Grade = Math.Floor(x.Grade)
                        }).ToList();

                    // calculate average for a class
                    classResults.Add(new ClassResult
                    {
                        ClassName = file,
                        // student scores of 0 should be ignored during the calculation
                        Average = students.Where(x => x.Grade != 0).Average(x => x.Grade),
                        NoOfStudentsUsedInCalculation = students.Count(x => x.Grade != 0),
                        NoOfStudentsDiscarded = students.Count(x => x.Grade == 0),
                        TotalNoOfStudents = students.Count()
                    }); 
                }

                var highestPerformingClass = classResults.OrderByDescending(x => x.Average).FirstOrDefault();

                var avgScoreForAllStudents = classResults.Average(x => x.Average);

                var result = new StringBuilder($"Result written to file at {DateTime.Now.ToLongTimeString()}");

                result.AppendLine($"\nThe highest performing class  : {highestPerformingClass.ClassName} with average: {highestPerformingClass.Average } ");

                result.AppendLine($"The average score for all students regardless of class {avgScoreForAllStudents:f1}");

                foreach (var classResult in classResults)
                {
                    result.AppendLine($"Class Name: {classResult.ClassName}");
                    result.AppendLine($"\tAverage score for the class is {classResult.Average:f1}");
                    result.AppendLine($"\tTotal number of students within the class are {classResult.TotalNoOfStudents}");
                    result.AppendLine($"\t\tThe number of students used to calculate the class average {classResult.NoOfStudentsUsedInCalculation}");
                    result.AppendLine($"\t\tThe names of any students who were discarded from  {classResult.NoOfStudentsDiscarded}"); 
                }

                File.WriteAllText(resultFilePath, result.ToString());

                Console.WriteLine("Press any key to continue.");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occured.");
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Press any key to continue.");
                Console.ReadLine();
            }
        } 
    }
}
