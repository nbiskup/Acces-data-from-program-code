/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.controller;


import com.sun.imageio.plugins.common.ImageUtil;
import hr.algebra.dao.Repository;
import hr.algebra.dao.RepositoryFactory;
import hr.algebra.utilities.FileUtils;
import hr.algebra.utilities.ImageUtils;
import hr.algebra.utilities.ValidationUtils;
import hr.algebra.viewmodel.StudentViewModel;
import java.io.ByteArrayInputStream;
import java.io.File;
import java.io.IOException;
import java.net.URL;
import java.util.AbstractMap;
import java.util.HashMap;
import java.util.Map;
import java.util.Observable;
import java.util.ResourceBundle;
import java.util.concurrent.atomic.AtomicBoolean;
import java.util.function.BinaryOperator;
import java.util.function.UnaryOperator;
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
import javafx.scene.control.TextFormatter;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.util.converter.IntegerStringConverter;
import org.hibernate.event.spi.PersistEvent;

/**
 * FXML Controller class
 *
 * @author Nikola
 */
public class StudentController implements Initializable {

    private Map<TextField,Label> validationMap;
    private final ObservableList<StudentViewModel> students = FXCollections.observableArrayList();
    private StudentViewModel selectedStudentViewModel;
    private Tab previusTab;
    
    @FXML
    private TabPane tpContent;
    @FXML
    private TableView<StudentViewModel> tvStudents;
    @FXML
    private TableColumn<StudentViewModel, String> tcFirstName;
    @FXML
    private TableColumn<StudentViewModel, String> tcLastName;
    @FXML
    private TableColumn<StudentViewModel, String> tcJMBG;
    @FXML
    private ImageView ivStudent;
    @FXML
    private TextField tfFirstName;
    @FXML
    private Label lblFirstNameError;
    @FXML
    private TextField tfLastName;
    @FXML
    private Label lblLastNameError;
    @FXML
    private TextField tfJMBG;
    @FXML
    private Label lblJMBGError;
    @FXML
    private Label lblIconError;
    @FXML
    private Tab tabStudentList;    
    @FXML
    private Tab tabEditStudent;
           

    /**
     * Initializes the controller class.
     */
    @Override
    public void initialize(URL url, ResourceBundle rb) {
        initValidation();
        initStudents();
        initTable();         
        setupListeners();
        //addIntegerMask(neki tf int);
    }    

    @FXML
    private void tabStudent(Event event) {
    }

    @FXML
    private void upload(ActionEvent event) {
        File file = FileUtils.uploadFileDialog(tfFirstName.getScene().getWindow(), "jpg","png","jpeg");
        if (file != null) {
            Image image = new Image(file.toURI().toString());
            ivStudent.setImage(image);
            String ext = file.getName().substring(file.getName().lastIndexOf(".")+1);
            
            try {
                selectedStudentViewModel.getStudent().setPicture(ImageUtils.imageToByteArray(image, ext));
            } catch (IOException ex) {
                Logger.getLogger(StudentController.class.getName()).log(Level.SEVERE, null, ex);
            }
        }
    }
    
    @FXML
    private void edit(ActionEvent event) {
        if (tvStudents.getSelectionModel().getSelectedItem() != null) {
            bindStudent(tvStudents.getSelectionModel().getSelectedItem());
            tpContent.getSelectionModel().select(tabEditStudent);
            previusTab = tabEditStudent;
        }
    }
    
    @FXML
    private void delete(ActionEvent event) {
        if (tvStudents.getSelectionModel().getSelectedItem() != null) {
            students.remove(tvStudents.getSelectionModel().getSelectedItem());
        }
    }

    @FXML
    private void commit(ActionEvent event) {
        if (formValid()) {
            selectedStudentViewModel.getStudent().setFirstName(tfFirstName.getText());
            selectedStudentViewModel.getStudent().setLastName(tfLastName.getText());
            selectedStudentViewModel.getStudent().setJmbag(tfJMBG.getText());
            if (selectedStudentViewModel.getStudent().getId() == 0) {
                students.add(selectedStudentViewModel);
            } else{
                try {
                    RepositoryFactory.getRepository().updateStudent(selectedStudentViewModel.getStudent());
                    tvStudents.refresh();
                } catch (Exception ex) {
                    Logger.getLogger(StudentController.class.getName()).log(Level.SEVERE, null, ex);
                }
            }
            selectedStudentViewModel = null;
            tpContent.getSelectionModel().select(tabStudentList);
            tvStudents.refresh();
            resetForm();
        }
    }

    @FXML
    private void tabProfessor(Event event) {
    }

    @FXML
    private void tabCourse(Event event) {
    }

    
    private void initValidation() {
        validationMap = Stream.of(
                new AbstractMap.SimpleImmutableEntry<>(tfFirstName, lblFirstNameError),
                new AbstractMap.SimpleImmutableEntry<>(tfLastName, lblLastNameError),
                new AbstractMap.SimpleImmutableEntry<>(tfJMBG, lblJMBGError)
        ).collect(Collectors.toMap(Map.Entry::getKey, Map.Entry::getValue));
                
    }

    private void initStudents() {
        try {
            RepositoryFactory.getRepository().getStudents().forEach(s -> students.add(new StudentViewModel(s)));
        } catch (Exception ex) {
            Logger.getLogger(StudentController.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    private void initTable() {
        tcFirstName.setCellValueFactory(s -> s.getValue().getFirstNameProperty());
        tcLastName.setCellValueFactory(s -> s.getValue().getLastNameProperty());
        tcJMBG.setCellValueFactory(s -> s.getValue().getJMBGProperty());
        tvStudents.setItems(students);
    }

    private void setupListeners() {
        tpContent.setOnMouseClicked( event -> {
            
            if (tpContent.getSelectionModel().getSelectedItem().equals(tabEditStudent)
                    && !tabEditStudent.equals(previusTab)
                    ) {
                  bindStudent(null);
            }
            
            previusTab = tpContent.getSelectionModel().getSelectedItem();
        });
        
        students.addListener(new ListChangeListener<StudentViewModel>(){
                @Override
            public void onChanged(ListChangeListener.Change<? extends StudentViewModel> change) {
                if (change.next()){
                    if (change.wasRemoved()) {
                        change.getRemoved().forEach(svm -> {
                            try {
                                RepositoryFactory.getRepository().deleteStudent(svm.getStudent());
                            } catch (Exception ex) {
                                Logger.getLogger(StudentController.class.getName()).log(Level.SEVERE, null, ex);
                            }
                        });
                    }
                     if (change.wasAdded()) {
                        change.getAddedSubList().forEach(svm -> {
                            try {
                                int id = RepositoryFactory.getRepository().addStudent(svm.getStudent());
                                svm.getStudent().setId(id);
                            } catch (Exception ex) {
                                Logger.getLogger(StudentController.class.getName()).log(Level.SEVERE, null, ex);
                            }
                        });
                    }
                }
            }
        });
    }
    

    private void addIntegerMask(TextField tf) {
        UnaryOperator<TextFormatter.Change> filter = change -> {
            if (change.getText().matches("\\d*")) {
                return change;
            }
            
            return null;
        };
        tf.setTextFormatter(new TextFormatter<>(new IntegerStringConverter(), 0, filter));
    }

    private void bindStudent(StudentViewModel svm) {
        resetForm();
        
        selectedStudentViewModel = svm != null ? svm : new StudentViewModel(null);
        tfFirstName.setText(selectedStudentViewModel.getFirstNameProperty().get());
        tfLastName.setText(selectedStudentViewModel.getLastNameProperty().get());
        tfJMBG.setText(selectedStudentViewModel.getJMBGProperty().get());
        //tfJMBG.setText(selectedStudentViewModel.getJMBGProperty().get() + ""); za integer
        
        ivStudent.setImage(
                selectedStudentViewModel.getPictureProperty().get() != null 
                ? new Image(new ByteArrayInputStream(
                        selectedStudentViewModel.getPictureProperty().get()))
                : new Image(new File("src/assets/no_image.png").toURI().toString()));
        
    }

    private void resetForm() {
        validationMap.values().forEach(lb -> lb.setVisible(false));
        lblIconError.setVisible(false);
        
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
        if (selectedStudentViewModel.getPictureProperty().get() == null) {
            lblIconError.setVisible(true);
            ok.set(false);
        }
        
        return ok.get();
    }
    
    
    
}
