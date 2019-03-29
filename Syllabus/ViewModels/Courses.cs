using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syllabus.ViewModels
{
    public class Courses
    {
        /// <summary>
        /// Courses are specified by its "course.Name", so two courses can not have one same Name.
        /// Use method "Contains" to avoid that.
        /// </summary>
        public ObservableCollection<Course> CourseCollection = new ObservableCollection<Course>();

        public int TagGenerator;

        public Courses()
        {
            TagGenerator = 0;
            this.CourseCollection.Add(new Course("高等数学", "王老师", 100000 * TagGenerator++));
            this.CourseCollection.Add(new Course("线性代数", "渡边梨加", 100000 * TagGenerator++));
            this.CourseCollection.Add(new Course("数理统计", "斋藤飞鸟", 100000 * TagGenerator++));
        }

        public void Add(String InputName, String InputTeacher)
        {
            this.CourseCollection.Add(new Course(InputName, InputTeacher, 100000 * TagGenerator++));
        }

        public bool Contains(String InputName)
        {
            foreach (var course in CourseCollection)
            {
                if (course.Name.Equals(InputName))
                    return true;
            }
            return false;
        }

        public void Delete(String InputName)
        {
            Course DeletingCourse = new Course();
            foreach (var course in CourseCollection)
            {
                if (course.Name.Equals(InputName))
                    DeletingCourse = course;
            }
            CourseCollection.Remove(DeletingCourse);
        }

        public void AddLocTime(string InputName, LocTime InputLocTime)
        {
            foreach (var course in CourseCollection)
            {
                if (course.Name.Equals(InputName))
                    course.AddLocTime(InputLocTime);
            }
        }

        public void DeleteLocTime(int Tag)
        {
            bool find = false;
            foreach (var course in CourseCollection)
            {
                foreach (var loctime in course.LocTimes)
                {
                    if (loctime.Tag.Equals(Tag))
                    {
                        find = true;
                    }
                }

                if (find)
                {
                    course.DeleteLocTime(Tag);
                    find = false;
                }
            }
        }

        public void Clear()
        {
            CourseCollection.Clear();
        }
    }
}
