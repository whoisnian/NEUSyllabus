using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Syllabus.ViewModels
{
    public class Course
    {
        public String Name;
        public String Teacher;
        public String Notes;
        public String Color;
        public ObservableCollection<LocTime> LocTimes = new ObservableCollection<LocTime>();
        public ObservableCollection<int> WeekChoices = new ObservableCollection<int>();
        private int TagGenerator;

        public Course()
        {
            TagGenerator = 0;
            Color = "#FFFFFF";
            Name = "";
            Teacher = "";
            Notes = "";
        }

        public Course(String InputName, String InputTeacher, int Tag=0)
        {
            Name = InputName;
            Teacher = InputTeacher;
            TagGenerator = Tag;
            for (int i = 1; i <= 20; ++i) WeekChoices.Add(i);
            //AddLocTime("一号楼A104", "111111110000000000", 1, 3, 4);
            //AddLocTime("一号楼A105", "111111110011000000", 3, 5, 6);
            Color = "#FFFFFF";
            Notes = "";
        }

        public Course(Course oldcourse)
        {
            Name = oldcourse.Name;
            Teacher = oldcourse.Teacher;
            Notes = oldcourse.Notes;
            Color = oldcourse.Color;
            LocTimes = oldcourse.LocTimes;
            WeekChoices = oldcourse.WeekChoices;
            TagGenerator = oldcourse.TagGenerator;
        }

        public void AddLocTime(LocTime InputLocTime)
        {
            LocTimes.Add(new LocTime(InputLocTime.Location, InputLocTime.Week, InputLocTime.WeekDay, InputLocTime.BeginTime, InputLocTime.EndTime, TagGenerator++));
        }

        public void AddLocTime(String InputLocation, String InputWeek, int InputWeekDay, int InputBeginTime, int InputEndTime)
        {
            LocTimes.Add(new LocTime(InputLocation, InputWeek, InputWeekDay, InputBeginTime, InputEndTime, TagGenerator++));
        }

        public void SetTag(int InputTag)
        {
            TagGenerator = InputTag;
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

        public void ClearLocTime()
        {
            LocTimes.Clear();
        }

        public Course DeepCopy()
        {
            object retval;
            using (MemoryStream ms = new MemoryStream())
            {
                XmlSerializer xml = new XmlSerializer(typeof(Course));
                xml.Serialize(ms, this);
                ms.Seek(0, SeekOrigin.Begin);
                retval = xml.Deserialize(ms);
                ms.Close();
            }
            return (Course)retval;
        }
    }
}
