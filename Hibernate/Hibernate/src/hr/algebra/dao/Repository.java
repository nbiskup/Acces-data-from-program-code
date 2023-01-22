/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.dao;

/**
 *
 * @author Nikola
 */
public interface Repository extends ProfessorRepository, StudentRepository, CourseRepository{
   
    default void release() throws Exception {}
    
}
