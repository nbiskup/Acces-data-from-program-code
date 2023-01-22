/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.model;

import java.io.Serializable;
import javax.persistence.Basic;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.Table;
import javax.xml.bind.annotation.XmlRootElement;

/**
 *
 * @author Nikola
 */
@Entity
@Table(name = "ProfessorCourse")
@XmlRootElement
@NamedQueries({
    @NamedQuery(name = "ProfessorCourse.findAll", query = "SELECT p FROM ProfessorCourse p")
    , @NamedQuery(name = "ProfessorCourse.findById", query = "SELECT p FROM ProfessorCourse p WHERE p.id = :id")})
public class ProfessorCourse implements Serializable {

    private static final long serialVersionUID = 1L;
    @Id
    @Basic(optional = false)
    @Column(name = "Id")
    private Integer id;
    @JoinColumn(name = "CourseId", referencedColumnName = "Id")
    @ManyToOne(optional = false)
    private Course courseId;
    @JoinColumn(name = "ProfessorId", referencedColumnName = "Id")
    @ManyToOne(optional = false)
    private Professor professorId;

    public ProfessorCourse() {
    }

    public ProfessorCourse(Integer id) {
        this.id = id;
    }
    
    public ProfessorCourse(ProfessorCourse pc){
        updateDetails(pc);
    }

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public Course getCourseId() {
        return courseId;
    }

    public void setCourseId(Course courseId) {
        this.courseId = courseId;
    }

    public Professor getProfessorId() {
        return professorId;
    }

    public void setProfessorId(Professor professorId) {
        this.professorId = professorId;
    }

    @Override
    public int hashCode() {
        int hash = 0;
        hash += (id != null ? id.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof ProfessorCourse)) {
            return false;
        }
        ProfessorCourse other = (ProfessorCourse) object;
        if ((this.id == null && other.id != null) || (this.id != null && !this.id.equals(other.id))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "hr.algebra.model.ProfessorCourse[ id=" + id + " ]";
    }

    public void updateDetails(ProfessorCourse pc) {
        this.professorId = pc.getProfessorId();
        this.courseId = pc.getCourseId();
    }
    
}
