using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyllabusV0._1.Services
{
    public class Course
    {
        public String Name;
        public String Teacher;
        public ObservableCollection<LocTime> LocTimes=new ObservableCollection<LocTime>();
        public ObservableCollection<int> WeekChoices=new ObservableCollection<int>();
        private int TagGenerator;

        public Course() { TagGenerator = 0; }
        
        public Course(String InputName, String InputTeacher,int Tag)
        {
            Name = InputName;
            Teacher = InputTeacher;
            TagGenerator = Tag;
            for (int i=1;i<=20;++i)WeekChoices.Add(i);
            AddLocTime("一号楼A104","111111110000000000",1,3,4);
            AddLocTime("一号楼A105", "111111110011000000",3,5, 6);   
        }

        public void AddLocTime(LocTime InputLocTime)
        {
            LocTimes.Add(new LocTime(InputLocTime.Location, InputLocTime.Week, InputLocTime.WeekDay, InputLocTime.BeginTime, InputLocTime.EndTime, TagGenerator++));
        }

        public void AddLocTime(String InputLocation, String InputWeek, int InputWeekDay, int InputBeginTime, int InputEndTime)
        {
            LocTimes.Add(new LocTime(InputLocation,InputWeek,InputWeekDay,InputBeginTime,InputEndTime,TagGenerator++));
        }

        public void DeleteLocTime(int Tag)
        {
            LocTime DeletingLocTime = new LocTime();
            foreach (var loctime in LocTimes)
            {
                if (loctime.Tag == Tag)
                {
                    DeletingLocTime = loctime;
                }
            }

            LocTimes.Remove(DeletingLocTime);
        }

        

        
    }
    
}
