/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.dao;

import hr.algebra.model.Student;
import hr.algebra.model.StudentCourse;
import java.util.List;

/**
 *
 * @author Nikola
 */
public interface StudentRepository {
   int addStudent(Student data) throws Exception ;
   void updateStudent(Student data) throws Exception ;
   void deleteStudent(Student student) throws Exception ;
   Student getStudent(int id) throws Exception ;
   List<Student> getStudents() throws Exception;
   
   List<StudentCourse> getStudentsCourses(int studentId) throws Exception ;
   int createOrUpdateStudentsCourse(StudentCourse sc) throws Exception;
   void deleteStudentCourse(StudentCourse sc) throws Exception ;
}
