using System.Collections.Generic;
using WPF_Project.Models;

namespace WPF_Project.DAL
{
    public interface IRepository
    {

        void AddProfessor(Professor proffesor);
        void DeleteProfessor(Professor proffesor);
        void UpdateProfessor(Professor proffesor);
        Professor GetProfessor(int idProffesor);
        IEnumerable<Professor> GetAllProfessor();
        IEnumerable<Course> GetCourses(Professor professor);
        void AddCourse(Professor professor, Course course);
        void RemoveCourse(Professor professor, int courseId);
        IEnumerable<Course> GetAll(Professor professor);


        void AddStudent(Student Student);
        void DeleteStudent(Student Student);
        void UpdateStudent(Student Student);
        Student GetStudent(int idStudent);
        IEnumerable<Student> GetAllStudent();
        IEnumerable<Course> GetCourses(Student student);
        void AddCourse(Student student, Course course);
        void RemoveCourse(Student student, int courseId);


        void AddCourse(Course course);
        void DeleteCourse(Course course);
        void UpdateCourse(Course course);
        Course GetCourse(int idCourse);
        IEnumerable<Course> GetAllCourse();
    }
}
