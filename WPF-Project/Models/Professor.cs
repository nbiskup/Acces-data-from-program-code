using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Project.Models
{
    public class Professor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<Course> TeachingCourses { get; set; }

        public override bool Equals(object obj) => obj is Professor proffesor && Id == proffesor.Id;

        public override int GetHashCode()=> 2108858624 + Id.GetHashCode();
        
    }
}
