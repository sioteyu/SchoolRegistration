using SchoolRegistration.Model.Repository;

namespace SchoolRegistration.Model
{
    public class Teacher
    {
        public int TeacherID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public int Age { get; set; }
        public bool IsStarSectionAdviser { get; set; }
        public List<Student> HandledStudents { get; set; }

        private readonly TeacherRepository repo = new TeacherRepository();

        public List<Teacher> GetTeachers(int TeacherID, string TeacherName)
        {
            //Access repository to get teachers
            List<Teacher> teachers = repo.GetTeachers(TeacherID, TeacherName);
            foreach(var teacher in teachers)
            {
                //Get the students that are handled by the teacher
                teacher.HandledStudents = new Student().GetStudentsByTeacherID(teacher.TeacherID);
            }
            return teachers;
        }

        public ResultMessage AddTeacher(Teacher teacher)
        {
            //Access repository to insert teacher
            return repo.AddTeacher  (teacher);
        }
        public ResultMessage UpdateTeacher(Teacher teacher)
        {
            //Access repository to update teacher
            return repo.UpdateTeacher(teacher);
        }

        public ResultMessage CheckIfValid(Teacher teacher, bool isUpdate = false)
        {
            //Check if teacher count is 5 only when inserting
            if (GetTeachers(0, "").Count >= 5 && !isUpdate)
            {
                return new ResultMessage()
                {
                    Success = false,
                    Message = "Teacher Limit Reached"
                };
            }
            //Check if first name is empty
            else if (teacher.FirstName == "" || teacher.FirstName == null)
            {
                return new ResultMessage()
                {
                    Success = false,
                    Message = "First name is empty or null"
                };
            }
            //check if last name is empty
            else if (teacher.LastName == "" || teacher.LastName == null)
            {
                return new ResultMessage()
                {
                    Success = false,
                    Message = "Last name is empty or null"
                };
            }
            else
            {
                //return success message if there is no issue
                return new ResultMessage()
                {
                    Success = true,
                    Message = "Operation Success"
                };
            }
        }
    }
}
