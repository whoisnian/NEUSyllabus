using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments.AppointmentsProvider;

namespace SyllabusV0._1.Services
{
    class Courses
    {
        public ObservableCollection<Course> CourseCollection = new ObservableCollection<Course>();
        public Courses()
        {
            this.CourseCollection.Add(new Course("高等数学", "王老师"));
            this.CourseCollection.Add(new Course("线性代数", "渡边梨加"));
            this.CourseCollection.Add(new Course("数理统计", "斋藤飞鸟"));
        }
        
        public void Add(String InputName, String InputTeacher)
        {
            this.CourseCollection.Add(new Course(InputName,InputTeacher));
        }

        public bool Contains(String InputName,String InputTeacher)
        {
            return this.CourseCollection.Contains(new Course(InputName,InputTeacher));
        }

        
    }
}
