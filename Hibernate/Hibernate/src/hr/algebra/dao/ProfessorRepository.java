/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.dao;

import hr.algebra.model.Professor;
import hr.algebra.model.ProfessorCourse;
import java.util.List;

/**
 *
 * @author Nikola
 */
public interface ProfessorRepository {
    
   int addProfessor(Professor data) throws Exception ;
   void updateProfessor(Professor data) throws Exception ;
   void deleteProfessor(Professor professor) throws Exception ;
   Professor getProfessor(int id) throws Exception ;
   List<Professor> getProfessors() throws Exception;
   
   List<ProfessorCourse> getProfessorCourses(int professorId) throws Exception ;
   int createOrUpdateProfessorCourses(ProfessorCourse pc) throws Exception;
   void deleteProfessorCourse(ProfessorCourse pc) throws Exception ;
    
}
