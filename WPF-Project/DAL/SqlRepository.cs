using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using WPF_Project.Models;
using WPF_Project.Utils;

namespace WPF_Project.DAL
{
    class SqlRepository : IRepository
    {
        private static readonly string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;  

        public void AddProfessor(Professor proffesor)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(Professor.FirstName), proffesor.FirstName);
                    cmd.Parameters.AddWithValue(nameof(Professor.LastName), proffesor.LastName);
                    cmd.Parameters.AddWithValue(nameof(Professor.Email), proffesor.Email);

                    SqlParameter id = new SqlParameter(nameof(Professor.Id), System.Data.SqlDbType.Int)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    cmd.Parameters.Add(id);
                    cmd.ExecuteNonQuery();
                    proffesor.Id = (int)id.Value;
                }
            }
        }

        public void DeleteProfessor(Professor proffesor)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(Professor.Id), proffesor.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateProfessor(Professor proffesor)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name + nameof(Professor).ToString();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(Professor.Id), proffesor.Id);
                    cmd.Parameters.AddWithValue(nameof(Professor.FirstName), proffesor.FirstName);
                    cmd.Parameters.AddWithValue(nameof(Professor.LastName), proffesor.LastName);
                    cmd.Parameters.AddWithValue(nameof(Professor.Email), proffesor.Email);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Professor GetProfessor(int idProffesor)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name + nameof(Professor);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(Professor.Id), idProffesor);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            ReadProfessor(dr);
                        }
                    }
                }
            }
            throw new Exception("Wrong id");
        }

        public IEnumerable<Professor> GetAllProfessor()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "GetAll" + nameof(Professor);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            yield return ReadProfessor(dr);
                        }
                    }
                }
            }
        }



        public void AddStudent(Student student)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(Student.FirstName), student.FirstName);
                    cmd.Parameters.AddWithValue(nameof(Student.LastName), student.LastName);
                    cmd.Parameters.AddWithValue(nameof(Student.JMBG), student.JMBG);

                    cmd.Parameters.Add(new SqlParameter(nameof(Student.Image),
                        System.Data.SqlDbType.Binary, student.Image.Length)
                    {
                        Value = student.Image
                    });

                    SqlParameter id = new SqlParameter(nameof(Student.Id), System.Data.SqlDbType.Int)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    cmd.Parameters.Add(id);
                    cmd.ExecuteNonQuery();
                    student.Id = (int)id.Value;
                }
            }
        }

        public void DeleteStudent(Student student)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(Student.Id), student.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateStudent(Student student)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(Student.Id), student.Id);
                    cmd.Parameters.AddWithValue(nameof(Student.FirstName), student.FirstName);
                    cmd.Parameters.AddWithValue(nameof(Student.LastName), student.LastName);
                    cmd.Parameters.AddWithValue(nameof(Student.JMBG), student.JMBG);
                    cmd.Parameters.Add(new SqlParameter(
                        nameof(Student.Image),
                        System.Data.SqlDbType.Binary,
                        student.Image.Length)
                    {
                        Value = student.Image
                    });

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Student GetStudent(int idStudent)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name + nameof(Student);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(Student.Id), idStudent);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            ReadStudent(dr);
                        }
                    }
                }
            }
            throw new Exception("Wrong id");
        }

        public IEnumerable<Student> GetAllStudent()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(GetAllStudent);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            yield return ReadStudent(dr);
                        }
                    }
                }
            }
        }



        public void AddCourse(Course course)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(Course.CourseName), course.CourseName);
                    cmd.Parameters.AddWithValue(nameof(Course.ECTS), course.ECTS);

                    SqlParameter id = new SqlParameter(nameof(Course.IDCourse), System.Data.SqlDbType.Int)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    cmd.Parameters.Add(id);

                    cmd.ExecuteNonQuery();
                    course.IDCourse = (int)id.Value;
                }
            }
        }

        public void DeleteCourse(Course course)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(Course.IDCourse), course.IDCourse);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateCourse(Course course)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(Course.IDCourse), course.IDCourse);
                    cmd.Parameters.AddWithValue(nameof(Course.CourseName), course.CourseName);
                    cmd.Parameters.AddWithValue(nameof(Course.ECTS), course.ECTS);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Course GetCourse(int idCourse)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(Course.IDCourse), idCourse);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            ReadCourse(dr);
                        }
                    }
                }
            }
            throw new Exception("Wrong id");
        }

        public IEnumerable<Course> GetAll(Professor professor)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(GetAll) + nameof(Professor) + nameof(Course);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(Professor.Id), professor.Id);
                    IList<Course> courses = new List<Course>();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            courses.Add(ReadCourse(dr));
                        }
                        return courses;
                    }
                }
            }
        }

        public IEnumerable<Course> GetCourses(Student student)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "GetAll" + nameof(Student) + nameof(Course);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(Student.Id), student.Id);
                    IList<Course> courses = new List<Course>();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            courses.Add(ReadCourse(dr));
                        }
                        return courses;
                    }
                }
            }
        }

        public IEnumerable<Course> GetAllCourse()
        {
            IList<Course> courses = new List<Course>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "GetAllCourse";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            courses.Add(ReadCourse(dr));
                        }
                        return courses;
                    }
                }
            }
        }

        public void AddCourse(Student student, Course course)
        {
            if (course.IDCourse == 0 || course == null)
            {
                throw new Exception("Course doesn't exits");
            }
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(Student) + MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(Student) + nameof(Student.Id), student.Id);
                    cmd.Parameters.AddWithValue(nameof(Course.IDCourse), course.IDCourse);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddCourse(Professor professor, Course course)
        {
            if (course.IDCourse == 0 || course == null)
            {
                throw new Exception("Course doesn't exits!");
            }
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(Professor) + MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(Professor) + nameof(Professor.Id), professor.Id);
                    cmd.Parameters.AddWithValue(nameof(Course.IDCourse), course.IDCourse);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RemoveCourse(Professor professor, int courseId)
        {
            if (courseId == 0)
            {
                throw new Exception("Course doesn't exits!");
            }
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(Professor) + MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(Professor) + nameof(Professor.Id), professor.Id);
                    cmd.Parameters.AddWithValue(nameof(Course.IDCourse), courseId);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        private Course ReadCourse(SqlDataReader dr)
        {

            Course course = new Course();
            course.IDCourse = (int)dr["Id"];
            course.CourseName = dr["Name"].ToString();
            course.ECTS = (int)dr["ECTS"];
            return course;
        }

        private Student ReadStudent(SqlDataReader dr) => new Student
        {
            Id = (int)dr[nameof(Student.Id)],
            FirstName = dr[nameof(Student.FirstName)].ToString(),
            LastName = dr[nameof(Student.LastName)].ToString(),
            JMBG = dr["JMBAG"].ToString(),
            Image = ImageUtils.ByteArrayFromSqlDataReader(dr, 4)
        };
        private Professor ReadProfessor(SqlDataReader dr) => new Professor()
        {
            Id = (int)dr[nameof(Professor.Id)],
            FirstName = dr[nameof(Professor.FirstName)].ToString(),
            LastName = dr[nameof(Professor.LastName)].ToString(),
            Email = dr[nameof(Professor.Email)].ToString()
        };

        

        public IEnumerable<Course> GetCourses(Professor professor)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "GetAll" + nameof(Professor) + nameof(Course);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(Professor.Id), professor.Id);
                    List<Course> courses = new List<Course>();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            courses.Add(ReadCourse(dr));
                        }
                        return courses;
                    }
                }
            }
        }

        public void RemoveCourse(Student student, int courseId)
        {
            if (courseId == 0)
            {
                throw new Exception("Course doesn't exits!");
            }
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(Student) + MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(Student) + nameof(Student.Id), student.Id);
                    cmd.Parameters.AddWithValue(nameof(Course.IDCourse), courseId);

                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}
