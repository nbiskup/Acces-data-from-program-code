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
    public class StudentViewModel
    {
        public ObservableCollection<Student> Students { get; }

        public StudentViewModel()
        {
            Students = new ObservableCollection<Student>(RepositoryFactory.GetRepository().GetAllStudent());
            Students.CollectionChanged += Students_CollectionChanged;
        }

        private void Students_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    RepositoryFactory.GetRepository().AddStudent(Students[e.NewStartingIndex]);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    RepositoryFactory.GetRepository().DeleteStudent(e.OldItems.OfType<Student>().ToList()[0]);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    RepositoryFactory.GetRepository().UpdateStudent(e.NewItems.OfType<Student>().ToList()[0]);
                    break;
            }
        }
        public void Update(Student student) => Students[Students.IndexOf(student)] = student;
    }
}
