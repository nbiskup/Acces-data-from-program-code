using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_Project.DAL;
using WPF_Project.Models;
using WPF_Project.Utils;
using WPF_Project.ViewModels;

namespace WPF_Project
{
    public partial class EditProfessorPage : ProfessorPage
    {
        private Professor _professor;
        public EditProfessorPage(ProfessorViewModel viewModel, Professor professor = null) : base(viewModel)
        {
            InitializeComponent();
            _professor = professor ?? new Professor();
            DataContext = _professor;
            GetCourses();
        }

        

        private void BtnAddCourse_Click(object sender, RoutedEventArgs e)
        {
            if (CbCourses.SelectedItem != null)
            {
                if (_professor.Id == 0)
                {
                    if (MessageBox.Show("Commit changes first!", "Alert") == MessageBoxResult.OK)
                    {
                        return;
                    }
                }
                RepositoryFactory.GetRepository().AddCourse(_professor, (Course)CbCourses.SelectedItem);
                GetCourses();
            }
        }

        private void BtnRemoveCourse_Click(object sender, RoutedEventArgs e)
        {
            if (LvCourses.SelectedItem != null)
            {
                RepositoryFactory.GetRepository().RemoveCourse(_professor, ((Course)LvCourses.SelectedItem).IDCourse);
                GetCourses();
            }
        }

        private void GetCourses()
        {
            var allCourses = RepositoryFactory.GetRepository().GetAllCourse();
            if (_professor.Id != 0)
            {
                var teachingCourses = RepositoryFactory.GetRepository().GetCourses(_professor);
                var otherCourses = allCourses.Except(teachingCourses);
                CbCourses.ItemsSource = otherCourses;
                LvCourses.ItemsSource = teachingCourses;
            }
            else
                CbCourses.ItemsSource = allCourses;

            CbCourses.SelectedIndex = -1;
        }

        private void BtnCommit_Click(object sender, RoutedEventArgs e)
        {
            if (FormValid())
            {
                _professor.FirstName = TbFirstName.Text.Trim();
                _professor.LastName = TbLastName.Text.Trim();
                _professor.Email = TbEmail.Text.Trim();
                if (_professor.Id == 0)
                {
                    ProfessorViewModel.Professors.Add(_professor);
                    Frame.Navigate(new ProfessorsListPage(new ProfessorViewModel()) { Frame = Frame });
                }
                else
                {
                    ProfessorViewModel.Update(_professor);
                    Frame.NavigationService.GoBack();
                }
            }
        }

        private bool FormValid()
        {
            bool valid = true;
            GridContainer.Children.OfType<TextBox>().ToList().ForEach(e =>
            {
                if (string.IsNullOrEmpty(e.Text.Trim()) || ("Email".Equals(e.Tag) && !ValidationUtils.isValidEmail(TbEmail.Text.Trim())))
                {
                    e.Background = Brushes.LightCoral;
                    valid = false;
                }
                else
                {
                    e.Background = Brushes.White;
                }
            });

            return valid;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.NavigationService.GoBack();
        }


    }
}
