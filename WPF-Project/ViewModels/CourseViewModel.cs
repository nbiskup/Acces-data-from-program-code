using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Project.DAL;
using WPF_Project.Models;

namespace WPF_Project.ViewModels
{
    public class CourseViewModel
    {
        public ObservableCollection<Course> Courses { get; }

        public CourseViewModel()
        {
            Courses = new ObservableCollection<Course>(RepositoryFactory.GetRepository().GetAllCourse());
            Courses.CollectionChanged += Courses_CollectionChanged;
        }

        private void Courses_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    RepositoryFactory.GetRepository().AddCourse(Courses[e.NewStartingIndex]);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    RepositoryFactory.GetRepository().DeleteCourse(e.OldItems.OfType<Course>().ToList()[0]);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    RepositoryFactory.GetRepository().UpdateCourse(e.NewItems.OfType<Course>().ToList()[0]);
                    break;
            }
        }
        public void Update(Course course) => Courses[Courses.IndexOf(course)] = course;

    }
}
