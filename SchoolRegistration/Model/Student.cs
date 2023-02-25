using Dapper;
using Microsoft.Extensions.Configuration;
using SchoolRegistration.Model.Repository;
using System.Data.SqlClient;

namespace SchoolRegistration.Model
{
    public class Student
    {
        public int StudentID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime Birthday { get; set; }
        public int Age { get; set; }
        public int Adviser { get; set; }
        public float OldGPA { get; set; }
        public bool IsStarSection { get; set; }
        private readonly StudentRepository repo = new StudentRepository();

        public List<Student> GetStudents(int SutdentID, string StudentName)
        {
            //Access repository to get students
            return repo.GetStudents(StudentID, StudentName);
        }

        public List<Student> GetStudentsByTeacherID(int TeacherID)
        {
            //Access repository to get students
            return repo.GetStudentsByTeacherID(TeacherID);
        }

        public ResultMessage AddStudent(Student student)
        {
            Teacher teacher = new Teacher();
            List<Teacher> teachers = teacher.GetTeachers(0, "");
            //check if teacher has less than 5 students to take in another student
            if(teachers.Find(x => x.TeacherID == student.Adviser).HandledStudents.Count >= 5)
            {
                //if teacher has 5 students find another teacher 
                Teacher sub = teachers.Find(x => x.HandledStudents.Count < 5);
                if(sub == null)
                {
                    //return an error message if all teachers have 5 students
                    return new ResultMessage()
                    {
                        Success = false,
                        Message = "All teachers are full"
                    };
                }
                else
                {
                    //replace teacher id
                    student.Adviser = sub.TeacherID;
                }
                
            }
            //Access repository to insert student
            return repo.AddStudent(student);
        }
        public ResultMessage UpdateStudent(Student student)
        {
            //Access repository to update student
            return repo.UpdateStudent(student);
        }

        public ResultMessage DeleteStudent(Student student)
        {
            //Access repository to delete student
            return repo.DeleteStudent(student);
        }

        public ResultMessage CheckIfValid(Student student, bool isUpdate = false)
        {
            //Check if student count is 25 only when inserting
            if (GetStudents(0, "").Count >= 25 && !isUpdate)
            {
                return new ResultMessage()
                {
                    Success = false,
                    Message = "Student Limit Reached"
                };
            }
            //check if first name is empty
            else if (student.FirstName == "" || student.FirstName == null)
            {
                return new ResultMessage()
                {
                    Success = false,
                    Message = "First name is empty or null"
                };
            }
            //Check if last naem is empty 
            else if (student.LastName == "" || student.LastName == null)
            {
                return new ResultMessage()
                {
                    Success = false,
                    Message = "Last name is empty or null"
                };
            }
            //validate is star section placement
            else if (student.IsStarSection && student.OldGPA <= 95)
            {
                return new ResultMessage()
                {
                    Success = false,
                    Message = "OldGPA is too low to be in star section"
                };
            }
            //Check if teacher id is empty
            else if (student.Adviser == 0)
            {
                return new ResultMessage()
                {
                    Success = false,
                    Message = "Invalid adviser id"
                };
            }
            else if (student.Adviser != 0)
            {
                Teacher teacher = new Teacher();
                List<Teacher> teachers = teacher.GetTeachers(0, "");
                teacher = teachers.Find(x => x.TeacherID == student.Adviser);
                //Check if teacher exists
                if (teacher == null)
                {
                    return new ResultMessage()
                    {
                        Success = false,
                        Message = "Invalid adviser id"
                    };
                }
                //Check if the student is suitable to be placed under the teacher
                else if (teacher.IsStarSectionAdviser && !student.IsStarSection)
                {
                    return new ResultMessage()
                    {
                        Success = false,
                        Message = "Student cannot be placed in a star section"
                    };
                }
                //return success message
                else
                {
                    return new ResultMessage()
                    {
                        Success = true,
                        Message = "Operation Success"
                    };
                }
            }
            else
            {
                //return success message
                return new ResultMessage()
                {
                    Success = true,
                    Message = "Operation Success"
                };
            }
        }
    }
}
