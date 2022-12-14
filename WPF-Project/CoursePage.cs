using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPF_Project.ViewModels;

namespace WPF_Project
{
    public class CoursePage : Page
    {
        public CourseViewModel CourseViewModel { get; }
        public Frame Frame { get; set; }

        public CoursePage(CourseViewModel viewModel)
        {
            CourseViewModel = viewModel;
        }
    }
}
