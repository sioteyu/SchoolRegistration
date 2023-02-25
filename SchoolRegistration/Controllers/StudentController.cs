using Microsoft.AspNetCore.Mvc;
using SchoolRegistration.Model;

namespace SchoolRegistration.Controllers
{
    [Route("Student")]
    public class StudentController : ControllerBase
    {
        public StudentController(IConfiguration _configuration)
        {
            //Instantiate configuration
            ConfigurationHelper.Configuration = _configuration;
        }

        [HttpGet("GetStudent")]
        public List<Student> GetStudent(int StudentID, string StudentName)
        {
            Student student = new Student();
            //Get student by id or name or both
            return student.GetStudents(StudentID, StudentName);
        }

        [HttpPost("AddStudent")]
        public ResultMessage AddStudent(string FirstName, string LastName, DateTime Birthday, int Adviser, float OldGPA)
        {
            Student student = new Student()
            {
                FirstName = FirstName,
                LastName = LastName,
                Birthday = Birthday,
                Age = GetAge(Birthday),
                Adviser = Adviser,
                OldGPA = OldGPA,
                IsStarSection = OldGPA > 95
            };
            //Check if student fields are valid
            ResultMessage result = student.CheckIfValid(student);
            if (result.Success)
            {
                //Add student to database
                return student.AddStudent(student);
            }
            else
            {
                return result;
            }
        }

        [HttpPost("UpdateStudent")]
        public ResultMessage UpdateStudent(int StudentID, string FirstName, string LastName, DateTime Birthday, int Adviser, float OldGPA)
        {
            Student student = new Student()
            {
                StudentID = StudentID,
                FirstName = FirstName,
                LastName = LastName,
                Birthday = Birthday,
                Age = GetAge(Birthday),
                Adviser = Adviser,
                OldGPA = OldGPA,
                IsStarSection = OldGPA > 95
            };
            //Check if student fields are valid
            ResultMessage result = student.CheckIfValid(student, true);
            if (result.Success)
            {
                //Update student in database
                return student.UpdateStudent(student);
            }
            else
            {
                return result;
            }
        }

        [HttpDelete("DeleteStudent")]
        public ResultMessage DeleteStudent(int StudentID)
        {
            Student student = new Student()
            {
                StudentID = StudentID,
            };
            //Delete student in database
            return student.DeleteStudent(student);
        }

        //gets age based on birthday
        private Int32 GetAge(DateTime Birthday)
        {
            var today = DateTime.Today;
            int age = today.Year - Birthday.Year;
            if (today.Month < Birthday.Month)
            {
                age -= 1;
            }
            return age;
        }
    }
}
