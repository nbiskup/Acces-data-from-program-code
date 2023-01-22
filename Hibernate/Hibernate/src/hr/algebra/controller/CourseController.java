/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.controller;

import hr.algebra.dao.RepositoryFactory;
import hr.algebra.viewmodel.CourseViewModel;
import hr.algebra.viewmodel.StudentViewModel;
import java.io.ByteArrayInputStream;
import java.io.File;
import java.net.URL;
import java.util.AbstractMap;
import java.util.Map;
import java.util.ResourceBundle;
import java.util.concurrent.atomic.AtomicBoolean;
import java.util.logging.Level;
import java.util.logging.Logger;
import java.util.stream.Collectors;
import java.util.stream.Stream;
import javafx.collections.FXCollections;
import javafx.collections.ListChangeListener;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.event.Event;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Label;
import javafx.scene.control.Tab;
import javafx.scene.control.TabPane;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.TextField;
import javafx.scene.image.Image;

/**
 * FXML Controller class
 *
 * @author Nikola
 */
public class CourseController implements Initializable {

    private Map<TextField,Label> validationMap;
    private final ObservableList<CourseViewModel> courses = FXCollections.observableArrayList();
    private CourseViewModel selectedCourseViewModel;
    private Tab previusTab;
    
    @FXML
    private TabPane tpContent;
    @FXML
    private Tab tabStudentList;
    @FXML
    private Tab tabEditStudent;
    @FXML
    private Tab tabProfessorList;
    @FXML
    private Tab tabEditProfessor;
    @FXML
    private Tab tabCourseList;
    @FXML
    private TableView<CourseViewModel> tvCourses;
    @FXML
    private TableColumn<CourseViewModel, String> tcName;
    @FXML
    private TableColumn<CourseViewModel, String> tcECTS;
    @FXML
    private Tab tabEditCourse;
    @FXML
    private TextField tfName;
    @FXML
    private Label lblNameError;
    @FXML
    private TextField tfECTS;
    @FXML
    private Label lblECTSError;

    /**
     * Initializes the controller class.
     */
    @Override
    public void initialize(URL url, ResourceBundle rb) {
        initValidation();
        initCourses();
        initTable();         
        setupListeners();
    }    

    @FXML
    private void tabStudents(Event event) {
    }

    @FXML
    private void tabStudent(Event event) {
    }

    @FXML
    private void tabProfessors(Event event) {
    }

    @FXML
    private void tabProfessor(Event event) {
    }

    @FXML
    private void edit(ActionEvent event) {
        if (tvCourses.getSelectionModel().getSelectedItem() != null) {
            bindCourse(tvCourses.getSelectionModel().getSelectedItem());
            tpContent.getSelectionModel().select(tabEditCourse);
            previusTab = tabEditCourse;
        }    
    }

    @FXML
    private void delete(ActionEvent event) {
        if (tvCourses.getSelectionModel().getSelectedItem() != null) {
            courses.remove(tvCourses.getSelectionModel().getSelectedItem());
        }
    }

    @FXML
    private void tabCourses(Event event) {
    }

    @FXML
    private void commit(ActionEvent event) {
        if (formValid()) {
            selectedCourseViewModel.getCourse().setName(tfName.getText());
            selectedCourseViewModel.getCourse().setEcts(Integer.parseInt(tfECTS.getText()));
            if (selectedCourseViewModel.getCourse().getId() == 0) {
                courses.add(selectedCourseViewModel);
            } else{
                try {
                    RepositoryFactory.getRepository().updateCourse(selectedCourseViewModel.getCourse());
                    tvCourses.refresh();
                } catch (Exception ex) {
                    Logger.getLogger(StudentController.class.getName()).log(Level.SEVERE, null, ex);
                }
            }
            selectedCourseViewModel = null;
            tpContent.getSelectionModel().select(tabCourseList);
            tvCourses.refresh();            
            resetForm();
        }
    }

    @FXML
    private void tabCourse(Event event) {
    }
    
    private void initValidation() {
        validationMap = Stream.of(
                new AbstractMap.SimpleImmutableEntry<>(tfName, lblNameError),
                new AbstractMap.SimpleImmutableEntry<>(tfECTS, lblECTSError)
        ).collect(Collectors.toMap(Map.Entry::getKey, Map.Entry::getValue));
                
    }

    private void initCourses() {
        try {
            RepositoryFactory.getRepository().getCourses().forEach(s -> courses.add(new CourseViewModel(s)));
        } catch (Exception ex) {
            Logger.getLogger(CourseController.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
    
    private void initTable() {
        tcName.setCellValueFactory(c -> c.getValue().getNameProperty());
        
        //tcECTS.setCellValueFactory(c -> c.getValue().getEctsProperty());   
        
        tvCourses.setItems(courses);
    }
    
    private void setupListeners() {
        tpContent.setOnMouseClicked( event -> {
            
            if (tpContent.getSelectionModel().getSelectedItem().equals(tabEditCourse)
                    && !tabEditCourse.equals(previusTab)
                    ) {
                  bindCourse(null);
            }
            
            previusTab = tpContent.getSelectionModel().getSelectedItem();
        });
        
        courses.addListener(new ListChangeListener<CourseViewModel>(){
                @Override
            public void onChanged(ListChangeListener.Change<? extends CourseViewModel> change) {
                if (change.next()){
                    if (change.wasRemoved()) {
                        change.getRemoved().forEach(cvm -> {
                            try {
                                RepositoryFactory.getRepository().deleteCourse(cvm.getCourse());
                            } catch (Exception ex) {
                                Logger.getLogger(CourseController.class.getName()).log(Level.SEVERE, null, ex);
                            }
                        });
                    }
                     if (change.wasAdded()) {
                        change.getAddedSubList().forEach(cvm -> {
                            try {
                                int id = RepositoryFactory.getRepository().addCourse(cvm.getCourse());
                                cvm.getCourse().setId(id);
                            } catch (Exception ex) {
                                Logger.getLogger(CourseController.class.getName()).log(Level.SEVERE, null, ex);
                            }
                        });
                    }
                }
            }
        });
    }
    

    private void bindCourse(CourseViewModel cvm) {
        resetForm();
        
        selectedCourseViewModel = cvm != null ? cvm : new CourseViewModel(null);
        tfName.setText(selectedCourseViewModel.getNameProperty().get());
        tfECTS.setText(selectedCourseViewModel.getEctsProperty().get() + "");
    }

    private void resetForm() {
        validationMap.values().forEach(lb -> lb.setVisible(false));        
    }
    
    private boolean formValid() {
       
        resetForm();
        final AtomicBoolean ok = new AtomicBoolean(true);
        
        validationMap.forEach((tf, lb)-> {
            if (tf.getText().isEmpty()) {
                lb.setVisible(true);
                ok.set(false);
            }
        });
        
        return ok.get();
    }
    
}
