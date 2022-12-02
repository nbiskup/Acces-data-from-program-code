using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPF_Project.Models;
using WPF_Project.ViewModels;

namespace WPF_Project
{
    /// <summary>
    /// Interaction logic for EditCoursePage.xaml
    /// </summary>
    public partial class EditCoursePage : CoursePage
    {
        private readonly Course _course;
        public EditCoursePage(CourseViewModel viewModel, Course course = null) : base(viewModel)
        {
            InitializeComponent();
            _course = course ?? new Course();
            DataContext = _course;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.NavigationService.GoBack();
        }

        private void BtnCommit_Click(object sender, RoutedEventArgs e)
        {
            if (FormValid())
            {
                _course.CourseName = TbName.Text.Trim();
                _course.ECTS = int.Parse(TbECTS.Text.Trim());
                if (_course.IDCourse == 0)
                {
                    CourseViewModel.Courses.Add(_course);
                    Frame.Navigate(new CourseListPage(CourseViewModel) { Frame = Frame });
                }
                else
                {
                    CourseViewModel.Update(_course);
                    Frame.NavigationService.GoBack();
                }
            }
        }

        private bool FormValid()
        {
            bool valid = true;
            GridContainer.Children.OfType<TextBox>().ToList().ForEach(e =>
            {
                if (string.IsNullOrEmpty(e.Text.Trim()) || "Int".Equals(e.Tag) && !int.TryParse(e.Text.Trim(), out int r))
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



    }
}
