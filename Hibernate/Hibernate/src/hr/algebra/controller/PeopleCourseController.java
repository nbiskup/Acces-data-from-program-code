/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.controller;

import hr.algebra.dao.RepositoryFactory;
import hr.algebra.model.Course;
import hr.algebra.model.Student;
import hr.algebra.model.StudentCourse;
import hr.algebra.utilities.FileUtils;
import hr.algebra.utilities.ImageUtils;
import hr.algebra.viewmodel.CourseViewModel;
import hr.algebra.viewmodel.StudentViewModel;
import java.io.ByteArrayInputStream;
import java.io.File;
import java.io.IOException;
import java.net.URL;
import java.util.AbstractMap;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import java.util.ResourceBundle;
import java.util.Set;
import java.util.concurrent.atomic.AtomicBoolean;
import java.util.logging.Level;
import java.util.logging.Logger;
import java.util.stream.Collectors;
import java.util.stream.Stream;
import javafx.collections.FXCollections;
import javafx.collections.ListChangeListener;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Alert;
import javafx.scene.control.Button;
import javafx.scene.control.ComboBox;
import javafx.scene.control.Label;
import javafx.scene.control.Tab;
import javafx.scene.control.TabPane;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.TextField;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;

/**
 * FXML Controller class
 *
 * @author Nikola
 */
public class PeopleCourseController implements Initializable {
    private Map<TextField, Label> validationStudentMap;
    private Map<TextField,Label> validationMap;
    private final ObservableList<StudentViewModel> students = FXCollections.observableArrayList();
    private final ObservableList<CourseViewModel> courses = FXCollections.observableArrayList();

    private StudentViewModel selectedStudentViewModel;

    private Tab previousTab;

    @FXML
    private TabPane tpContent;
    @FXML
    private Tab tabList;
    @FXML
    private Tab tabEdit;
    @FXML
    private TextField tfFirstName;
    @FXML
    private Label lbFirstNameError;
    @FXML
    private TextField tfLastName;
    @FXML
    private Label lbLastNameError;
    @FXML
    private Label lbIconError;
    @FXML
    private Button btnStudentCourses;
    @FXML
    private Label lbJmbagError;
    @FXML
    private Tab tabStudentCourses;
    @FXML
    private ComboBox<StudentViewModel> cbStudent;
    @FXML
    private TableView<CourseViewModel> tvStudentCourses;
    @FXML
    private TableColumn<CourseViewModel, String> tcCourseStudentName;
    @FXML
    private TableColumn<CourseViewModel, Number> tcCourseStudentECTS;
    @FXML
    private ComboBox<CourseViewModel> cbCourseStudent;
    @FXML
    private TableColumn<StudentViewModel, String> tcStudentFirstName;
    @FXML
    private TableColumn<StudentViewModel, String> tcStudentLastName;
    @FXML
    private TableColumn<StudentViewModel, String> tcStudentJmbag;
    @FXML
    private TableView<StudentViewModel> tvStudents;
    @FXML
    private TextField tfJmbag;
    @FXML
    private ImageView ivStudent;
    @FXML
    private Button btnAddCourse;
    @FXML
    private Button btnRemoveCourse;

    /**
     * Initializes the controller class.
     */
    @Override
    public void initialize(URL url, ResourceBundle rb) {
        initValidation();
        initStudents();
        initTables();
        setupListeners();
    }

    @FXML
    private void upload(ActionEvent event) {
        File file = FileUtils.uploadFileDialog(tfFirstName.getScene().getWindow(), "jpg", "jpeg", "png");
        if (file != null) {        
            Image image = new Image(file.toURI().toString());
            ivStudent.setImage(image); 
            String ext = file.getName().substring(file.getName().lastIndexOf('.') + 1);            
            
            try {                
                selectedStudentViewModel.getStudent().setPicture(ImageUtils.imageToByteArray(image, ext));                
            } catch (IOException ex) {
                Logger.getLogger(PeopleCourseController.class.getName()).log(Level.SEVERE, null, ex);
            }
        }
    }

    @FXML
    private void commit(ActionEvent event) {
        if (studentFormValid()) {
            selectedStudentViewModel.getStudent().setFirstName(tfFirstName.getText().trim());
            selectedStudentViewModel.getStudent().setLastName(tfLastName.getText().trim());
            selectedStudentViewModel.getStudent().setJmbag(tfJmbag.getText().trim());
            if (selectedStudentViewModel.getStudent().getId() == 0) {
                students.add(selectedStudentViewModel);
            } else {
                try {
                    RepositoryFactory.getRepository().updateStudent(selectedStudentViewModel.getStudent());
                    tvStudents.refresh();
                } catch (Exception ex) {
                    Logger.getLogger(PeopleCourseController.class.getName()).log(Level.SEVERE, null, ex);
                }

            }
        }
        selectedStudentViewModel = null;
        tpContent.getSelectionModel().select(tabList);
        tvStudents.refresh();
    }

    private void resetForm() {
        validationMap.values().forEach(lb -> lb.setVisible(false));
        lbIconError.setVisible(false);
        
    }
    
    @FXML
    private void editStudentCourses(ActionEvent event) {
        if (tvStudents.getSelectionModel().getSelectedItem() != null) {
            try {
                bindStudentCourses(tvStudents.getSelectionModel().getSelectedItem());
            } catch (Exception ex) {
                Logger.getLogger(PeopleCourseController.class.getName()).log(Level.SEVERE, null, ex);
            }
            tpContent.getSelectionModel().select(tabStudentCourses);
            previousTab = tabStudentCourses;
        }
    }

    @FXML
    private void editStudent(ActionEvent event) {
        if (tvStudents.getSelectionModel().getSelectedItem() != null) {
            bindStudent(tvStudents.getSelectionModel().getSelectedItem());
            tpContent.getSelectionModel().select(tabEdit);
            previousTab = tabEdit;
        }
    }

    @FXML
    private void deleteStudent(ActionEvent event) {        
        if(tvStudents.getSelectionModel().getSelectedItem() != null){
           students.remove(tvStudents.getSelectionModel().getSelectedItem());
        }        
    }

    private void initValidation() {
        validationStudentMap = Stream.of(
                new AbstractMap.SimpleImmutableEntry<>(tfFirstName, lbFirstNameError),
                new AbstractMap.SimpleImmutableEntry<>(tfLastName, lbLastNameError),
                new AbstractMap.SimpleImmutableEntry<>(tfJmbag, lbJmbagError))
                .collect(Collectors.toMap(Map.Entry::getKey, Map.Entry::getValue));
    }

    private void initStudents() {
        try {
            RepositoryFactory.getRepository().getStudents().forEach(s -> students.add(new StudentViewModel(s)));
        } catch (Exception ex) {
            Logger.getLogger(PeopleCourseController.class.getName()).log(Level.SEVERE, null, ex);
        }
    }    

    private void initTables() {
        initStudentsTable();

    }

    private void setupListeners() {
        tpContent.setOnMouseClicked(event -> {
            
            if (tpContent.getSelectionModel().getSelectedItem().equals(tabEdit) && !tabEdit.equals(previousTab)) {
                bindStudent(null);
            } else if (tpContent.getSelectionModel().getSelectedItem().equals(tabStudentCourses) && !tabStudentCourses.equals(previousTab)) {
                try {
                    bindStudentCourses(null);
                } catch (Exception ex) {
                    Logger.getLogger(PeopleCourseController.class.getName()).log(Level.SEVERE, null, ex);
                }
            }
            previousTab = tpContent.getSelectionModel().getSelectedItem();

        });
        students.addListener((ListChangeListener.Change<? extends StudentViewModel> change) -> {
            if (change.next()) {
                if (change.wasRemoved()) {
                    change.getRemoved().forEach(pvm -> {
                        try {
                            RepositoryFactory.getRepository().deleteStudent(pvm.getStudent());
                        } catch (Exception ex) {
                            Logger.getLogger(PeopleCourseController.class.getName()).log(Level.SEVERE, null, ex);
                        }
                    });
                } else if (change.wasAdded()) {
                    change.getAddedSubList().forEach(pvm -> {
                        try {
                            int idStudent = RepositoryFactory.getRepository().addStudent(pvm.getStudent());
                            pvm.getStudent().setId(idStudent);
                        } catch (Exception ex) {
                            Logger.getLogger(PeopleCourseController.class.getName()).log(Level.SEVERE, null, ex);
                        }
                    });
                }
            }
        });
    }

    private void initStudentsTable() {
        tcStudentFirstName.setCellValueFactory(s -> s.getValue().getFirstNameProperty());
        tcStudentLastName.setCellValueFactory(s -> s.getValue().getLastNameProperty());
        tcStudentJmbag.setCellValueFactory(s -> s.getValue().getJMBGProperty());
        tvStudents.setItems(students);
    }

    private void initStudentsCourseTable() {
        tcCourseStudentName.setCellValueFactory(s -> s.getValue().getNameProperty());
        tcCourseStudentECTS.setCellValueFactory(s -> s.getValue().getEctsProperty());
        tvStudentCourses.setItems(courses);
    }

    private void bindStudent(StudentViewModel svm) {
                
        selectedStudentViewModel = svm != null ? svm : new StudentViewModel(null);
        tfFirstName.setText(selectedStudentViewModel.getFirstNameProperty().get());
        tfLastName.setText(selectedStudentViewModel.getLastNameProperty().get());
        tfJmbag.setText(selectedStudentViewModel.getJMBGProperty().get());

        ivStudent.setImage(selectedStudentViewModel.getPictureProperty().get() != null ? new Image(new ByteArrayInputStream(selectedStudentViewModel.getPictureProperty().get())) : new Image(new File("src/assets/no_image.png").toURI().toString()));
    }

    private boolean studentFormValid() {
        
        final AtomicBoolean ok = new AtomicBoolean(true);
        
        validationStudentMap.keySet().forEach(tf -> {
            if (tf.getText().trim().isEmpty()) {
                validationStudentMap.get(tf).setVisible(true);
                ok.set(false);
            } else {
                validationStudentMap.get(tf).setVisible(false);
            }
        });
        
        if (selectedStudentViewModel.getPictureProperty().get() == null) {
            lbIconError.setVisible(true);
            ok.set(false);
        }else {
            lbIconError.setVisible(false);

        }

        return ok.get();
    }

    private void bindStudentCourses(StudentViewModel svm) throws Exception {
        cbStudent.setItems(students);
        courses.clear();
        List<Course> allCourses = new ArrayList<>();
        RepositoryFactory.getRepository().getCourses().forEach(c -> allCourses.add((c)));
        ObservableList<CourseViewModel> leftCourses = FXCollections.observableArrayList();
        if (svm != null) {
            cbStudent.getSelectionModel().select(svm);
            RepositoryFactory.getRepository().getStudentsCourses(svm.getStudent().getId()).forEach(sc -> courses.add(new CourseViewModel(sc.getCourseId())));
            initStudentsCourseTable();

            for (CourseViewModel c : courses) {
                if (allCourses.contains(c.getCourse())) {
                    allCourses.remove(c.getCourse());
                }
            }
        }

        for (Course c : allCourses) {
            leftCourses.add(new CourseViewModel(c));
        }
        cbCourseStudent.setItems(leftCourses);
        cbCourseStudent.getSelectionModel().select(0);
    }

    @FXML
    private void addCourse(ActionEvent event) {
        if(cbCourseStudent.getSelectionModel().getSelectedItem() != null && cbStudent.getSelectionModel().getSelectedItem() != null){
            StudentCourse sc = new StudentCourse();
            
            sc.setCourseId(cbCourseStudent.getSelectionModel().getSelectedItem().getCourse());
            sc.setStudentId(cbStudent.getSelectionModel().getSelectedItem().getStudent());
            
            
                      
            
            try {
                RepositoryFactory.getRepository().createOrUpdateStudentsCourse(sc);
                courses.add(cbCourseStudent.getSelectionModel().getSelectedItem());
                tvStudentCourses.refresh();
                cbCourseStudent.getItems().remove(cbCourseStudent.getSelectionModel().getSelectedItem());
            } catch (Exception ex) {
                Logger.getLogger(PeopleCourseController.class.getName()).log(Level.SEVERE, null, ex);
            }
        }
    }

    @FXML
    private void removeCourse(ActionEvent event) {
        if(tvStudentCourses.getSelectionModel().getSelectedItem() != null){
            
            Course course = tvStudentCourses.getSelectionModel().getSelectedItem().getCourse();
            try {
                StudentCourse sc = RepositoryFactory.getRepository().getStudentsCourses(cbStudent.getSelectionModel().getSelectedItem().getStudent().getId()).stream().filter(c -> c.getCourseId().getId() == course.getId()).findFirst().orElse(null);
                RepositoryFactory.getRepository().deleteStudentCourse(sc);
                courses.remove(cbCourseStudent.getSelectionModel().getSelectedItem());
                bindStudentCourses(selectedStudentViewModel);
                tvStudentCourses.refresh();
                
            } catch (Exception ex) {
                Logger.getLogger(PeopleCourseController.class.getName()).log(Level.SEVERE, null, ex);
            }
        }
    }

    @FXML
    private void cbStudentChanged(ActionEvent event) {
        try {
            bindStudentCourses(cbStudent.getSelectionModel().getSelectedItem());
        } catch (Exception ex) {
            Logger.getLogger(PeopleCourseController.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
    
}
