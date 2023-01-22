/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.viewmodel;

import hr.algebra.model.Student;
import javafx.beans.property.IntegerProperty;
import javafx.beans.property.ObjectProperty;
import javafx.beans.property.SimpleIntegerProperty;
import javafx.beans.property.SimpleObjectProperty;
import javafx.beans.property.SimpleStringProperty;
import javafx.beans.property.StringProperty;

/**
 *
 * @author Nikola
 */
public class StudentViewModel {
    
    private final Student student;

    public StudentViewModel(Student student) {
        if (student==null) {
            student = new Student(0, "", "", "");
        }
        this.student = student;
    }

    public Student getStudent() {
        return student;
    }
    
    public IntegerProperty getIDStudentProperty(){    
        return new SimpleIntegerProperty(student.getId());
    }
    
    public StringProperty getFirstNameProperty(){
        return new SimpleStringProperty(student.getFirstName());
    }
    
    public StringProperty getLastNameProperty(){
        return new SimpleStringProperty(student.getLastName());
    }
    
    public StringProperty getJMBGProperty(){
        return new SimpleStringProperty(student.getJmbag());
    }
    
    public ObjectProperty<byte[]> getPictureProperty(){
        return new SimpleObjectProperty<>(student.getPicture());
    }

    @Override
    public String toString() {
        return student.getFirstName() + " " + student.getLastName();
    }
    
}
