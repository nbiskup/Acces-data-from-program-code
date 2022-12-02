using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPF_Project.ViewModels;

namespace WPF_Project
{
    public class ProfessorPage : Page
    {
        public ProfessorViewModel ProfessorViewModel { get; set; }
        public Frame Frame { get; set; }

        public ProfessorPage(ProfessorViewModel viewModel)
        {
            ProfessorViewModel = viewModel;
        }
    }
}
