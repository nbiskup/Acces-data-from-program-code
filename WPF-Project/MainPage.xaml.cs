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

namespace WPF_Project
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : FramedPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        /*
            POPRAVITI:

            ????? Kad se promjeni lista predmeta za odabranog studenta primjeni se promjena na svakog studenta

            ????? Brisanje predmeta sa profesorove liste predmeta

           
        
            Ispisivanje liste predmeta na CourseListPage - Greška u metodi ReadCourse(dr)



            Pri pokretanju aplikacije ekran se postavlja manje nego šta mu je zadano
            
        */

        private void BtnCourses_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new CourseListPage(new ViewModels.CourseViewModel()) { Frame = Frame });
        }

        private void BtnStudents_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new StudentListPage(new ViewModels.StudentViewModel()) { Frame = Frame });
        }

        private void BtnProfessors_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new ProfessorsListPage(new ViewModels.ProfessorViewModel()) { Frame = Frame });

        }


    }
}
