using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace SchoolRegistration.Model.Repository
{
    public class StudentRepository
    {
        public List<Student> GetStudents(int SutdentID, string StudentName)
        {
            //Instantiate database connection
            using (var conn = new SqlConnection(ConfigurationHelper.Configuration.GetConnectionString("DBConn")))
            {
                try
                {
                    //open database connection
                    conn.Open();
                    //create parameters
                    var param = new DynamicParameters();
                    param.Add("@StudentID", SutdentID);
                    param.Add("@StudentName", StudentName);
                    //send parameters to stored procedure
                    var result = conn.Query<Student>("Student_Select", param, commandType: System.Data.CommandType.StoredProcedure);
                    //Close the connection
                    conn.Close();
                    //return results
                    return result.ToList();
                }
                catch (Exception ex)
                {
                    //return an empty list if error occurs
                    return new List<Student>();
                }
            }
        }

        public List<Student> GetStudentsByTeacherID(int TeacherID)
        {
            //Instantiate database connection
            using (var conn = new SqlConnection(ConfigurationHelper.Configuration.GetConnectionString("DBConn")))
            {
                try
                {
                    //open database connection
                    conn.Open();
                    //create parameters
                    var param = new DynamicParameters();
                    param.Add("@TeacherID", TeacherID);
                    //send parameters to stored procedure
                    var result = conn.Query<Student>("Student_SelectByAdviser", param, commandType: System.Data.CommandType.StoredProcedure);
                    //Close the connection
                    conn.Close();
                    //return results
                    return result.ToList();
                }
                catch (Exception ex)
                {
                    //return an empty list if error occurs
                    return new List<Student>();
                }
            }
        }

        public ResultMessage AddStudent(Student student)
        {
            //Instantiate database connection
            using (var conn = new SqlConnection(ConfigurationHelper.Configuration.GetConnectionString("DBConn")))
            {
                try
                {
                    //open database connection
                    conn.Open();
                    //create parameters
                    var param = new DynamicParameters();
                    param.Add("@FirstName", student.FirstName);
                    param.Add("@LastName", student.LastName);
                    param.Add("@Birthday", student.Birthday);
                    param.Add("@Age", student.Age);
                    param.Add("@Adviser", student.Adviser);
                    param.Add("@OldGPA", student.OldGPA);
                    param.Add("@IsStarSection", student.IsStarSection);
                    //send parameters to stored procedure
                    var result = conn.Execute("Student_Insert", param, commandType: System.Data.CommandType.StoredProcedure);
                    //Close the connection
                    conn.Close();
                    if (result >= 1)
                    {
                        //return success message
                        return new ResultMessage()
                        {
                            Success = true,
                            Message = "Student insert success"
                        };
                    }
                    else
                    {
                        //return error message when no rows where updated
                        return new ResultMessage()
                        {
                            Success = false,
                            Message = "Student insert failed"
                        };
                    }
                }
                catch (Exception ex)
                {
                    //return error message when exception occurs
                    return new ResultMessage()
                    {
                        Success = true,
                        Message = "Student insert failed"
                    };
                }
            }
        }
        public ResultMessage UpdateStudent(Student student)
        {
            //Instantiate database connection
            using (var conn = new SqlConnection(ConfigurationHelper.Configuration.GetConnectionString("DBConn")))
            {
                try
                {
                    //open database connection
                    conn.Open();
                    //create parameters
                    var param = new DynamicParameters();
                    param.Add("@StudentID", student.StudentID);
                    param.Add("@FirstName", student.FirstName);
                    param.Add("@LastName", student.LastName);
                    param.Add("@Birthday", student.Birthday);
                    param.Add("@Age", student.Age);
                    param.Add("@Adviser", student.Adviser);
                    param.Add("@OldGPA", student.OldGPA);
                    param.Add("@IsStarSection", student.IsStarSection);
                    //send parameters to stored procedure
                    var result = conn.Execute("Student_Update", param, commandType: System.Data.CommandType.StoredProcedure);
                    //Close the connection
                    conn.Close();
                    if (result >= 1)
                    {
                        //return success message
                        return new ResultMessage()
                        {
                            Success = true,
                            Message = "Student update success"
                        };
                    }
                    else
                    {
                        //return error message
                        return new ResultMessage()
                        {
                            Success = false,
                            Message = "Student insert failed"
                        };
                    }
                }
                catch (Exception ex)
                {
                    //return error message when exception occurs
                    return new ResultMessage()
                    {
                        Success = false,
                        Message = "Student insert failed"
                    };
                }
            }
        }

        public ResultMessage DeleteStudent(Student student)
        {
            //Instantiate database connection
            using (var conn = new SqlConnection(ConfigurationHelper.Configuration.GetConnectionString("DBConn")))
            {
                try
                {
                    //open database connection
                    conn.Open();
                    //create parameters
                    var param = new DynamicParameters();
                    param.Add("@StudentID", student.StudentID);
                    //send parameters to stored procedure
                    var result = conn.Execute("Student_Delete", param, commandType: System.Data.CommandType.StoredProcedure);
                    //Close the connection
                    conn.Close();
                    if (result >= 1)
                    {
                        //return success message
                        return new ResultMessage()
                        {
                            Success = true,
                            Message = "Student delete success"
                        };
                    }
                    else
                    {
                        //return error message
                        return new ResultMessage()
                        {
                            Success = false,
                            Message = "Student delete failed"
                        };
                    }
                }
                catch (Exception ex)
                {
                    //return error message
                    return new ResultMessage()
                    {
                        Success = false,
                        Message = "Student delete failed"
                    };
                }
            }
        }
    }
}
