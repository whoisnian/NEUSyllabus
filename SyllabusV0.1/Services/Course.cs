using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyllabusV0._1.Services
{
    class Course
    {
        public String Name;
        public String Teacher;
        public List<LocTime>LocTimes;

        public Course(String InputName, String InputTeacher)
        {
            Name = InputName;
            Teacher = InputTeacher;
        }
    }
    
}
