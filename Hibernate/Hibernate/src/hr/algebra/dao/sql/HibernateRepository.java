/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.dao.sql;

import hr.algebra.dao.Repository;
import hr.algebra.model.Course;
import hr.algebra.model.Professor;
import hr.algebra.model.ProfessorCourse;
import hr.algebra.model.Student;
import hr.algebra.model.StudentCourse;
import java.util.List;
import java.util.stream.Collectors;
import javax.persistence.EntityManager;


public class HibernateRepository implements Repository {

    @Override
    public void release() throws Exception {
        HibernateFactory.release(); 
    }    
    
    @Override
    public int addStudent(Student data) throws Exception {
        try(EntityManagerWrapper emw = HibernateFactory.getEntityManager()) {
            EntityManager em = emw.get();
            
            em.getTransaction();
            Student student = new Student(data);
            em.persist(student);            
            em.getTransaction().commit();
            
            return student.getId();
        } 
    }

    @Override
    public void updateStudent(Student student) throws Exception {
        try(EntityManagerWrapper emw = HibernateFactory.getEntityManager()) {
            EntityManager em = emw.get();
            em.getTransaction();
            em.find(Student.class, student.getId()).updateDetails(student);
            em.getTransaction().commit();
        } 
    }

    @Override
    public void deleteStudent(Student student) throws Exception {
        try(EntityManagerWrapper emw = HibernateFactory.getEntityManager()) {
            EntityManager em = emw.get();
            
            em.getTransaction();
            em.remove(em.contains(student) ? student : em.merge(student));            
            em.getTransaction().commit();                        
        } 
    }

    @Override
    public Student getStudent(int idStudent) throws Exception {
        try(EntityManagerWrapper emw = HibernateFactory.getEntityManager()) {
            EntityManager em = emw.get();
            
            return em.find(Student.class, idStudent);
        } 
    }

    @Override
    public List<Student> getStudents() throws Exception {
        try(EntityManagerWrapper emw = HibernateFactory.getEntityManager()) {
            EntityManager em = emw.get();
            
            return em.createNamedQuery(HibernateFactory.SELECT_STUDENTS).getResultList();
        } 
    }

    @Override
    public int addProfessor(Professor data) throws Exception {
       try(EntityManagerWrapper emw = HibernateFactory.getEntityManager()) {
            EntityManager em = emw.get();
            
            em.getTransaction();
            Professor professor = new Professor(data);
            em.persist(professor);            
            em.getTransaction().commit();
            
            return professor.getId();
        } 
    }

    @Override
    public void updateProfessor(Professor professor) throws Exception {
       try(EntityManagerWrapper emw = HibernateFactory.getEntityManager()) {
            EntityManager em = emw.get();
            em.getTransaction();
            em.find(Professor.class, professor.getId()).updateDetails(professor);
            em.getTransaction().commit();
        } 
    }

    @Override
    public void deleteProfessor(Professor professor) throws Exception {
       try(EntityManagerWrapper emw = HibernateFactory.getEntityManager()) {
            EntityManager em = emw.get();
            
            em.getTransaction();
            em.remove(em.contains(professor) ? professor : em.merge(professor));            
            em.getTransaction().commit();                        
        } 
    }

    @Override
    public Professor getProfessor(int idProfessor) throws Exception {
       try(EntityManagerWrapper emw = HibernateFactory.getEntityManager()) {
            EntityManager em = emw.get();
            
            return em.find(Professor.class, idProfessor);
        }
    }

    @Override
    public List<Professor> getProfessors() throws Exception {
        try(EntityManagerWrapper emw = HibernateFactory.getEntityManager()) {
            EntityManager em = emw.get();
            
            return em.createNamedQuery(HibernateFactory.SELECT_PROFESSORS).getResultList();
        } 
    }

    @Override
    public int addCourse(Course data) throws Exception {
        try(EntityManagerWrapper emw = HibernateFactory.getEntityManager()) {
            EntityManager em = emw.get();
            
            em.getTransaction();
            Course course = new Course(data);
            em.persist(course);            
            em.getTransaction().commit();
            
            return course.getId();
        } 
    }

    @Override
    public void updateCourse(Course course) throws Exception {
         try(EntityManagerWrapper emw = HibernateFactory.getEntityManager()) {
            EntityManager em = emw.get();
            em.getTransaction();
            em.find(Course.class, course.getId()).updateDetails(course);
            em.getTransaction().commit();
        } 
    }

    @Override
    public void deleteCourse(Course course) throws Exception {
        try(EntityManagerWrapper emw = HibernateFactory.getEntityManager()) {
            EntityManager em = emw.get();
            
            em.getTransaction();
            em.remove(em.contains(course) ? course : em.merge(course));            
            em.getTransaction().commit();                        
        } 
    }

    @Override
    public Course getCourse(int idCourse) throws Exception {
        try(EntityManagerWrapper emw = HibernateFactory.getEntityManager()) {
            EntityManager em = emw.get();
            
            return em.find(Course.class, idCourse);
        } 
    }

    @Override
    public List<Course> getCourses() throws Exception {
        try(EntityManagerWrapper emw = HibernateFactory.getEntityManager()) {
            EntityManager em = emw.get();
            
            return em.createNamedQuery(HibernateFactory.SELECT_COURSESS).getResultList();
        } 
    }

    @Override
    public List<ProfessorCourse> getProfessorCourses(int professorId) throws Exception {
        try (EntityManagerWrapper emw = HibernateFactory.getEntityManager()) {
            EntityManager em = emw.get();
            List<ProfessorCourse> ptc = em.createNamedQuery(HibernateFactory.SELECT_STUDENT_COURSES).getResultList();
            return ptc.stream().filter(pc -> pc.getProfessorId().getId() == professorId).collect(Collectors.toList());
        }
    }

    @Override
    public int createOrUpdateProfessorCourses(ProfessorCourse pc) throws Exception {
        if (pc.getId() != null) {
            try (EntityManagerWrapper emw = HibernateFactory.getEntityManager()) {
                EntityManager em = emw.get();
                em.getTransaction().begin();
                em.find(ProfessorCourse.class, pc.getId()).updateDetails(pc);
                em.getTransaction().commit();
                return pc.getId();
            }
        } else {
            try (EntityManagerWrapper emw = HibernateFactory.getEntityManager()) {
                EntityManager em = emw.get();
                em.getTransaction().begin();
                ProfessorCourse professorCourse = new ProfessorCourse(pc);
                em.persist(professorCourse);
                em.getTransaction().commit();
                return professorCourse.getId();
            }
        }
    }

    @Override
    public void deleteProfessorCourse(ProfessorCourse pc) throws Exception {
        try (EntityManagerWrapper emw = HibernateFactory.getEntityManager()) {
            EntityManager em = emw.get();
            em.getTransaction().begin();
            em.createNativeQuery("DELETE FROM ProfessorCourse WHERE Id = " + pc.getId()).executeUpdate();
            em.getTransaction().commit();
        }
    }

    @Override
    public List<StudentCourse> getStudentsCourses(int studentId) throws Exception {
        try (EntityManagerWrapper emw = HibernateFactory.getEntityManager()) {
            EntityManager em = emw.get();
            List<StudentCourse> stc = em.createNamedQuery(HibernateFactory.SELECT_STUDENT_COURSES).getResultList();
            return stc.stream().filter(sc -> sc.getStudentId().getId() == studentId).collect(Collectors.toList());
        }
    }

    @Override
    public int createOrUpdateStudentsCourse(StudentCourse sc) throws Exception {
        if (sc.getId() != null) {
            try (EntityManagerWrapper emw = HibernateFactory.getEntityManager()) {
                EntityManager em = emw.get();
                em.getTransaction().begin();
                em.find(StudentCourse.class, sc.getId()).updateDetails(sc);
                em.getTransaction().commit();
                return sc.getId();
            }
        } else {
            try (EntityManagerWrapper emw = HibernateFactory.getEntityManager()) {
                EntityManager em = emw.get();
                em.getTransaction().begin();
                StudentCourse studentcourse = new StudentCourse(sc);
                em.persist(studentcourse);
                em.getTransaction().commit();
                return studentcourse.getId();
            }
        }
    }

    @Override
    public void deleteStudentCourse(StudentCourse sc) throws Exception {
        try (EntityManagerWrapper emw = HibernateFactory.getEntityManager()) {
            EntityManager em = emw.get();
            em.getTransaction().begin();
            em.createNativeQuery("DELETE FROM StudentCourse WHERE Id = " + sc.getId()).executeUpdate();
            em.getTransaction().commit();
        }
    }
    
}
