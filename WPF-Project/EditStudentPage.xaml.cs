using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
    /// <summary>
    /// Interaction logic for EditStudentPage.xaml
    /// </summary>
    public partial class EditStudentPage : StudentPage
    {
        private const string Filter = "All supported graphics|*.jpg;*.jpeg;*.png|JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|Portable Network Graphic (*.png)|*.png";
        private Student _student;
        private bool imageSelected = false;
        public EditStudentPage(StudentViewModel viewModel, Student student = null) : base(viewModel)
        {
            InitializeComponent();
            _student = student ?? new Student();
            DataContext = _student;
            GetCourses();
        }

        private void GetCourses()
        {
            var allCourses = RepositoryFactory.GetRepository().GetAllCourse();
            if (_student.Id != 0)
            {
                var studentCourses = RepositoryFactory.GetRepository().GetCourses(_student);
                var leftCourses = allCourses.Except(studentCourses);
                CbCourses.ItemsSource = leftCourses;
                LvCourses.ItemsSource = studentCourses;
            }
            else
            {
                CbCourses.ItemsSource = allCourses;
            }

            CbCourses.SelectedIndex = -1;
        }

        private void BtnUpload_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog()
            {
                Filter = Filter
            };
            if (dialog.ShowDialog() == true)
            {
                xPicture.Source = new BitmapImage(new Uri(dialog.FileName));
                imageSelected = true;
            }

        }

        private void BtnCommit_Click(object sender, RoutedEventArgs e)
        {
            if (FormValid())
            {
                _student.FirstName = TbFirstName.Text.Trim();
                _student.LastName = TbLastName.Text.Trim();
                _student.JMBG = TbJmbag.Text.Trim();
                if (imageSelected)
                    _student.Image = ImageUtils.BitmapImageToByteArray(xPicture.Source as BitmapImage);

                if (_student.Id == 0)
                {
                    StudentViewModel.Students.Add(_student);
                    Frame.Navigate(new StudentListPage(new StudentViewModel()) { Frame = Frame });
                }
                else
                {
                    StudentViewModel.Update(_student);
                    Frame.NavigationService.GoBack();
                }
            }
        }

        private bool FormValid()
        {
            bool valid = true;
            GridContainer.Children.OfType<System.Windows.Controls.TextBox>().ToList().ForEach(e =>
            {
                if (string.IsNullOrEmpty(e.Text.Trim()))
                {
                    e.Background = Brushes.LightCoral;
                    valid = false;
                }
                else
                {
                    e.Background = Brushes.White;
                }
            });
            if (xPicture.Source == null)
            {
                ImageBorder.BorderBrush = Brushes.LightCoral;
                valid = false;
            }
            else
            {
                ImageBorder.BorderBrush = Brushes.White;

            }

            return valid;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.NavigationService.GoBack();
        }

        private void BtnRemoveCourse_Click(object sender, RoutedEventArgs e)
        {
            if (LvCourses.SelectedItem != null)
            {
                RepositoryFactory.GetRepository().RemoveCourse(_student, ((Course)LvCourses.SelectedItem).IDCourse);
                GetCourses();
            }
        }

        private void BtnAddCourse_Click(object sender, RoutedEventArgs e)
        {
            if (CbCourses.SelectedItem != null)
            {
                if (_student.Id == 0)
                {
                    if (MessageBox.Show("Commit changes first!", "Alert") == MessageBoxResult.OK)
                    {
                        return;
                    }
                }
                RepositoryFactory.GetRepository().AddCourse(_student, (Course)CbCourses.SelectedItem);
                GetCourses();
            }
        }

    }
}
