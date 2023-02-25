using Microsoft.AspNetCore.Mvc;
using SchoolRegistration.Model;

namespace SchoolRegistration.Controllers
{
    [Route("Teacher")]
    public class TeacherController : ControllerBase
    {
        public TeacherController(IConfiguration _configuration)
        {
            //Instantiate configuration
            ConfigurationHelper.Configuration = _configuration;
        }

        [HttpGet("GetTeacher")]
        public List<Teacher> GetTeacher(int TeacherID, string TeacherName)
        {
            Teacher teacher = new Teacher();
            //Retrieve teachers from database by id or name or both
            return teacher.GetTeachers(TeacherID, TeacherName);
        }

        [HttpPost("AddTeacher")]
        public ResultMessage AddTeacher(string FirstName, string LastName, DateTime Birthday, bool IsStarSectionAdviser)
        {
            Teacher teacher = new Teacher()
            {
                FirstName = FirstName,
                LastName = LastName,
                Birthday = Birthday,
                Age = GetAge(Birthday),
                IsStarSectionAdviser = IsStarSectionAdviser
            };
            //Check if teacher fields are valid
            ResultMessage result = teacher.CheckIfValid(teacher);
            if (result.Success)
            {
                //Add teacher to database
                return teacher.AddTeacher(teacher);
            }
            else
            {
                //return error message
                return result;
            }
        }

        [HttpPost("UpdateTeacher")]
        public ResultMessage UpdateTeacher(int TeacherID, string FirstName, string LastName, DateTime Birthday, bool IsStarSectionAdviser)
        {
            Teacher teacher = new Teacher()
            {
                TeacherID = TeacherID,
                FirstName = FirstName,
                LastName = LastName,
                Birthday = Birthday,
                Age = GetAge(Birthday),
                IsStarSectionAdviser = IsStarSectionAdviser
            };
            //Check if teacher fields are valid
            ResultMessage result = teacher.CheckIfValid(teacher, true);
            if (result.Success)
            {
                //Update teacher in database
                return teacher.UpdateTeacher(teacher);
            }
            else
            {
                //return error message
                return result;
            }
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
