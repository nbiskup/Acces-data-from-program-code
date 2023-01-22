/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.controller;

import hr.algebra.dao.RepositoryFactory;
import hr.algebra.viewmodel.ProfessorViewModel;
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
import javafx.scene.image.ImageView;

/**
 *
 * @author Nikola
 */
public class ProfessorController implements Initializable{

    private Map<TextField,Label> validationMap;
    private final ObservableList<ProfessorViewModel> professors = FXCollections.observableArrayList();
    private ProfessorViewModel selectedProfessorViewModel;
    private Tab previusTab;
    
    @FXML
    private TabPane tpContent;
    @FXML
    private Tab tabStudentList;
    @FXML
    private Tab tabEditProfessor;
    @FXML
    private Tab tabProfessorList;
    @FXML
    private TableView<ProfessorViewModel> tvProfessors;
    @FXML
    private TableColumn<ProfessorViewModel, String> tcFirstName;
    @FXML
    private TableColumn<ProfessorViewModel, String> tcLastName;
    @FXML
    private TableColumn<ProfessorViewModel, String> tcEmail;
    @FXML
    private ImageView ivProfessor;
    @FXML
    private TextField tfFirstNameProfessor;
    @FXML
    private Label lblFirstNameProfessorError;
    @FXML
    private TextField tfLastNameProfessor;
    @FXML
    private Label lblLastNameProfessorError;
    @FXML
    private TextField tfEmailProfessor;
    @FXML
    private Label lblEmailProfessorError;
    @FXML
    private Label lblIconProfessorError;
    @FXML
    private Tab tabCourseList;
    @FXML
    private Tab tabEditStudent11;

    @Override
    public void initialize(URL location, ResourceBundle resources) {
        initValidation();
        initProfessors();
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
    private void edit(ActionEvent event) {
        if (tvProfessors.getSelectionModel().getSelectedItem() != null) {
            bindProfessor(tvProfessors.getSelectionModel().getSelectedItem());
            tpContent.getSelectionModel().select(tabEditProfessor);
            previusTab = tabEditProfessor;
        }
    }

    @FXML
    private void delete(ActionEvent event) {
        if (tvProfessors.getSelectionModel().getSelectedItem() != null) {
                professors.remove(tvProfessors.getSelectionModel().getSelectedItem());
        }    
    }

    @FXML
    private void tabProfessors(Event event) {
    }

    @FXML
    private void upload(ActionEvent event) {
    }

    @FXML
    private void commit(ActionEvent event) {
        if (formValid()) {
            selectedProfessorViewModel.getProfessor().setFirstName(tfFirstNameProfessor.getText());
            selectedProfessorViewModel.getProfessor().setLastName(tfLastNameProfessor.getText());
            selectedProfessorViewModel.getProfessor().setEmail(tfEmailProfessor.getText());
            if (selectedProfessorViewModel.getProfessor().getId() == 0) {
                professors.add(selectedProfessorViewModel);
            } else{
                try {
                    RepositoryFactory.getRepository().updateProfessor(selectedProfessorViewModel.getProfessor());
                    tvProfessors.refresh();
                } catch (Exception ex) {
                    Logger.getLogger(StudentController.class.getName()).log(Level.SEVERE, null, ex);
                }
            }
            selectedProfessorViewModel = null;
            tpContent.getSelectionModel().select(tabStudentList);
            tvProfessors.refresh();
            resetForm();
        }
    
    }

    @FXML
    private void tabProfessor(Event event) {
    }

    @FXML
    private void tabCourse(Event event) {
    }

    private void bindProfessor(ProfessorViewModel pvm) {
        
        resetForm();
        
        selectedProfessorViewModel = pvm != null ? pvm : new ProfessorViewModel(null);
        tfFirstNameProfessor.setText(selectedProfessorViewModel.getFirstNameProperty().get());
        tfLastNameProfessor.setText(selectedProfessorViewModel.getLastNameProperty().get());
        tfEmailProfessor.setText(selectedProfessorViewModel.getEmailProperty().get());                
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
    
    private void initValidation() {
        validationMap = Stream.of(
                new AbstractMap.SimpleImmutableEntry<>(tfFirstNameProfessor, lblFirstNameProfessorError),
                new AbstractMap.SimpleImmutableEntry<>(tfLastNameProfessor, lblLastNameProfessorError),
                new AbstractMap.SimpleImmutableEntry<>(tfEmailProfessor, lblEmailProfessorError)
        ).collect(Collectors.toMap(Map.Entry::getKey, Map.Entry::getValue));                
    }
    
    private void initProfessors() {
        try {
            RepositoryFactory.getRepository().getProfessors().forEach(p -> professors.add(new ProfessorViewModel(p)));
        } catch (Exception ex) {
            Logger.getLogger(StudentController.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
    
     private void initTable() {
        tcFirstName.setCellValueFactory(s -> s.getValue().getFirstNameProperty());
        tcLastName.setCellValueFactory(s -> s.getValue().getLastNameProperty());
        tcEmail.setCellValueFactory(s -> s.getValue().getEmailProperty());
        tvProfessors.setItems(professors);
    }
     
    private void setupListeners() {
        tpContent.setOnMouseClicked( event -> {
            
            if (tpContent.getSelectionModel().getSelectedItem().equals(tabEditProfessor)
                    && !tabEditProfessor.equals(previusTab)
                    ) {
                  bindProfessor(null);
            }
            
            previusTab = tpContent.getSelectionModel().getSelectedItem();
        });
        
        professors.addListener(new ListChangeListener<ProfessorViewModel>(){
                @Override
            public void onChanged(ListChangeListener.Change<? extends ProfessorViewModel> change) {
                if (change.next()){
                    if (change.wasRemoved()) {
                        change.getRemoved().forEach(pvm -> {
                            try {
                                RepositoryFactory.getRepository().deleteProfessor(pvm.getProfessor());
                            } catch (Exception ex) {
                                Logger.getLogger(ProfessorController.class.getName()).log(Level.SEVERE, null, ex);
                            }
                        });
                    }
                     if (change.wasAdded()) {
                        change.getAddedSubList().forEach(pvm -> {
                            try {
                                int id = RepositoryFactory.getRepository().addProfessor(pvm.getProfessor());
                                pvm.getProfessor().setId(id);
                            } catch (Exception ex) {
                                Logger.getLogger(ProfessorController.class.getName()).log(Level.SEVERE, null, ex);
                            }
                        });
                    }
                }
            }
        });
    } 
    
}
