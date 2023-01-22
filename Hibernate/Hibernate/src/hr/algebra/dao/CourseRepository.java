/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.dao;

import hr.algebra.model.Course;
import java.util.List;

/**
 *
 * @author Nikola
 */
public interface CourseRepository {
   int addCourse(Course data) throws Exception ;
   void updateCourse(Course data) throws Exception ;
   void deleteCourse(Course course) throws Exception ;
   Course getCourse(int id) throws Exception ;
   List<Course> getCourses() throws Exception;
}
