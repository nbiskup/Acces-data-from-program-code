using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Project.DAL;
using WPF_Project.Models;

namespace WPF_Project.ViewModels
{
    public class ProfessorViewModel
    {
        public ObservableCollection<Professor> Professors { get; }
        public ObservableCollection<Course> Courses { get; }

        public ProfessorViewModel()
        {
            Professors = new ObservableCollection<Professor>(RepositoryFactory.GetRepository().GetAllProfessor());
            Professors.CollectionChanged += Professors_CollectionChanged;
        }

        private void Professors_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    RepositoryFactory.GetRepository().AddProfessor(Professors[e.NewStartingIndex]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    RepositoryFactory.GetRepository().DeleteProfessor(e.OldItems.OfType<Professor>().ToList()[0]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    RepositoryFactory.GetRepository().UpdateProfessor(e.NewItems.OfType<Professor>().ToList()[0]);
                    break;

            }
        }
        public void Update(Professor professor) => Professors[Professors.IndexOf(professor)] = professor;
    }
}
