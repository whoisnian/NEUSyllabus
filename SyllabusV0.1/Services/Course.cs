using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyllabusV0._1.Services
{
    class Course
    {
        public String Name;
        public String Teacher;
        public ObservableCollection<LocTime> LocTimes=new ObservableCollection<LocTime>();
        public ObservableCollection<int> WeekChoices=new ObservableCollection<int>();
        
        public Course(String InputName, String InputTeacher)
        {
            this.Name = InputName;
            this.Teacher = InputTeacher;
            this.LocTimes.Add(new LocTime("一号楼A104","111111110000000000",1,3,4));
            this.LocTimes.Add(new LocTime("一号楼A105", "111111110000000000",3,5, 6));
            for(int i=1;i<=20;++i)WeekChoices.Add(i);
        }

        public void AddLocTime(String InputLocation, String InputWeek, int InputWeekDay, int InputBeginTime, int InputEndTime)
        {
            this.LocTimes.Add(new LocTime(InputLocation,InputWeek,InputWeekDay,InputBeginTime,InputEndTime));
        }
    }
    
}
