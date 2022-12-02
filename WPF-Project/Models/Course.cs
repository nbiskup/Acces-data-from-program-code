using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Project.Models
{
    public class Course
    {
        public int IDCourse { get; set; }
        public string CourseName { get; set; }
        public int ECTS { get; set; }
        public override string ToString() => $"{IDCourse}: {CourseName}";
        public override bool Equals(object obj) => obj is Course course && IDCourse == course.IDCourse;
        public override int GetHashCode() => -1213315891 + IDCourse.GetHashCode();
    }
}
