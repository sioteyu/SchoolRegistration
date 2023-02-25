using Dapper;
using System.Data.SqlClient;

namespace SchoolRegistration.Model.Repository
{
    public class TeacherRepository
    {
        public List<Teacher> GetTeachers(int TeacherID, string TeacherName)
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
                    param.Add("@TeacherName", TeacherName);
                    //send parameters to stored procedure
                    var result = conn.Query<Teacher>("Teacher_Select", param, commandType: System.Data.CommandType.StoredProcedure);
                    //Close the connection
                    conn.Close();
                    //return results
                    return result.ToList();
                }
                catch (Exception ex)
                {
                    //return an empty list if error occurs
                    return new List<Teacher>();
                }
            }
        }

        public ResultMessage AddTeacher(Teacher teacher)
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
                    param.Add("@FirstName", teacher.FirstName);
                    param.Add("@LastName", teacher.LastName);
                    param.Add("@Birthday", teacher.Birthday);
                    param.Add("@Age", teacher.Age);
                    param.Add("@IsStarSectionAdviser", teacher.IsStarSectionAdviser);
                    //send parameters to stored procedure
                    var result = conn.Execute("Teacher_Insert", param, commandType: System.Data.CommandType.StoredProcedure);
                    //Close the connection
                    conn.Close();
                    if (result >= 1)
                    {
                        //return success message
                        return new ResultMessage()
                        {
                            Success = true,
                            Message = "Teacher insert success"
                        };
                    }
                    else
                    {
                        //return error message when no rows where updated
                        return new ResultMessage()
                        {
                            Success = false,
                            Message = "Teacher insert failed"
                        };
                    }
                }
                catch (Exception ex)
                {
                    //return error message when exception occurs
                    return new ResultMessage()
                    {
                        Success = false,
                        Message = "Teacher insert failed"
                    };
                }
            }
        }
        public ResultMessage UpdateTeacher(Teacher teacher)
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
                    param.Add("@TeacherID", teacher.TeacherID);
                    param.Add("@FirstName", teacher.FirstName);
                    param.Add("@LastName", teacher.LastName);
                    param.Add("@Birthday", teacher.Birthday);
                    param.Add("@Age", teacher.Age);
                    param.Add("@IsStarSectionAdviser", teacher.IsStarSectionAdviser);
                    //send parameters to stored procedure
                    var result = conn.Execute("Teacher_Update", param, commandType: System.Data.CommandType.StoredProcedure);
                    //Close the connection
                    conn.Close();
                    if (result >= 1)
                    {
                        //return success message
                        return new ResultMessage()
                        {
                            Success = true,
                            Message = "Teacher insert success"
                        };
                    }
                    else
                    {
                        //return error message when no rows where updated
                        return new ResultMessage()
                        {
                            Success = false,
                            Message = "Teacher update failed"
                        };
                    }
                }
                catch (Exception ex)
                {
                    //return error message when exception occurs
                    return new ResultMessage()
                    {
                        Success = false,
                        Message = "Teacher update failed"
                    };
                }
            }
        }
        public ResultMessage DeleteTeacher(Teacher teacher)
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
                    param.Add("@TeacherID", teacher.TeacherID);
                    //send parameters to stored procedure
                    var result = conn.Execute("Teacher_Delete", param, commandType: System.Data.CommandType.StoredProcedure);
                    //Close the connection
                    conn.Close();
                    if (result >= 1)
                    {
                        //return success message
                        return new ResultMessage()
                        {
                            Success = true,
                            Message = "Teacher delete success"
                        };
                    }
                    else
                    {
                        //return error message when no rows where updated
                        return new ResultMessage()
                        {
                            Success = false,
                            Message = "Teacher delete failed"
                        };
                    }
                }
                catch (Exception ex)
                {
                    //return error message when exception occurs
                    return new ResultMessage()
                    {
                        Success = false,
                        Message = "Teacher delete failed"
                    };
                }
            }
        }
    }
}
