using System.Windows.Controls;
using WPF_Project.ViewModels;

namespace WPF_Project
{
    public class StudentPage : Page
    {
        public StudentViewModel StudentViewModel { get; }
        public Frame Frame { get; set; }

        public StudentPage(StudentViewModel viewModel)
        {
            StudentViewModel = viewModel;
        }
    }
}
