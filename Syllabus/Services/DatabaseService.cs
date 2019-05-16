using Microsoft.EntityFrameworkCore;
using Syllabus.Models;
using Syllabus.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syllabus.Services
{
    public class DatabaseService
    {
        /// <summary>
        /// 从数据库中获取所有课程
        /// </summary>
        /// <returns>所有课程</returns>
        public Courses GetAllCourses()
        {
            // 要返回的结果
            Courses AllCourses = new Courses();

            // 从本地数据库获取数据
            var Database = new DataContext();
            List<DbCourse> TempDbCourses = Database.DbCourses
                .Include(DbCourse => DbCourse.DbLocTimes)
                .ToList<DbCourse>();
            foreach(DbCourse TempDbCourse in TempDbCourses)
            {
                Course TempCourse = new Course();
                TempCourse.Name = TempDbCourse.Name;
                TempCourse.Teacher = TempDbCourse.Teacher;
                TempCourse.Notes = TempDbCourse.Notes;
                AllCourses.Add(TempCourse);
                if(TempDbCourse.DbLocTimes == null)
                {
                    continue;
                }
                foreach(DbLocTime TempDbLocTime in TempDbCourse.DbLocTimes)
                {
                    LocTime TempLocTime = new LocTime(
                        TempDbLocTime.Location,
                        TempDbLocTime.Week,
                        TempDbLocTime.WeekDay,
                        TempDbLocTime.BeginTime,
                        TempDbLocTime.EndTime,
                        0);
                    AllCourses.AddLocTime(TempDbCourse.Name, TempLocTime);
                }
            }
            
            return AllCourses;
        }

        /// <summary>
        /// 删除数据库内所有课程，并使用提供的新课程集合覆盖（性能差，尽量不要使用）（未测试使用）
        /// </summary>
        /// <param name="AllCourses">新的课程集合</param>
        public async void OverrideAllCourses(Courses AllCourses)
        {
            // 从本地数据库获取数据
            var Database = new DataContext();

            // 清空数据库课程和时间
            foreach (DbCourse TempDbCourse in Database.DbCourses)
            {
                Database.DbCourses.Remove(TempDbCourse);
            }
            foreach (DbLocTime TempDbLocTime in Database.DbLocTimes)
            {
                Database.DbLocTimes.Remove(TempDbLocTime);
            }

            // 添加新的课程
            foreach (Course TempCourse in AllCourses.CourseCollection)
            {
                DbCourse TempDbCourse = new DbCourse();
                TempDbCourse.Name = TempCourse.Name;
                TempDbCourse.Teacher = TempCourse.Teacher;
                TempDbCourse.Notes = TempCourse.Notes;
                TempDbCourse.DbLocTimes = new List<DbLocTime>();
                foreach (LocTime TempLocTime in TempCourse.LocTimes)
                {
                    TempDbCourse.DbLocTimes.Add(new DbLocTime
                    {
                        Location = TempLocTime.Location,
                        Week = TempLocTime.Week,
                        WeekDay = TempLocTime.WeekDay,
                        BeginTime = TempLocTime.BeginTime,
                        EndTime = TempLocTime.EndTime
                    });
                }
                Database.DbCourses.Add(TempDbCourse);
            }
            await Database.SaveChangesAsync();
        }

        /// <summary>
        /// 向数据库中添加课程，如果课程已存在则自动增加时间
        /// </summary>
        /// <param name="TempCourse">要添加的课程</param>
        public void AddCourse(Course TempCourse)
        {
            // 从本地数据库获取数据
            var Database = new DataContext();
            var TempDbCourseRes = Database.DbCourses
                .Include(DbCourse => DbCourse.DbLocTimes)
                .SingleOrDefault(a => a.Name.Equals(TempCourse.Name));
            if(TempDbCourseRes == null)
            {
                // 新建课程
                DbCourse TempDbCourse = new DbCourse();
                TempDbCourse.Name = TempCourse.Name;
                TempDbCourse.Teacher = TempCourse.Teacher;
                TempDbCourse.Notes = TempCourse.Notes;
                TempDbCourse.DbLocTimes = new List<DbLocTime>();
                foreach (LocTime TempLocTime in TempCourse.LocTimes)
                {
                    TempDbCourse.DbLocTimes.Add(new DbLocTime
                    {
                        Location = TempLocTime.Location,
                        Week = TempLocTime.Week,
                        WeekDay = TempLocTime.WeekDay,
                        BeginTime = TempLocTime.BeginTime,
                        EndTime = TempLocTime.EndTime
                    });
                }
                Database.DbCourses.Add(TempDbCourse);
            }
            else
            {
                // 为已有课程添加时间
                foreach (LocTime TempLocTime in TempCourse.LocTimes)
                {
                    if(TempDbCourseRes.DbLocTimes == null)
                    {
                        TempDbCourseRes.DbLocTimes = new List<DbLocTime>();
                    }
                    if (!TempDbCourseRes.DbLocTimes.Exists(t =>
                        t.Location.Equals(TempLocTime.Location)&&
                        t.Week.Equals(TempLocTime.Week)&&
                        t.WeekDay.Equals(TempLocTime.WeekDay)&&
                        t.BeginTime.Equals(TempLocTime.BeginTime)&&
                        t.EndTime.Equals(TempLocTime.EndTime)
                    ))
                    {
                        TempDbCourseRes.DbLocTimes.Add(new DbLocTime
                        {
                            Location = TempLocTime.Location,
                            Week = TempLocTime.Week,
                            WeekDay = TempLocTime.WeekDay,
                            BeginTime = TempLocTime.BeginTime,
                            EndTime = TempLocTime.EndTime
                        });
                    }
                }
            }
            Database.SaveChanges();
        }

        /// <summary>
        /// 从数据库中删除课程（自动删除所有关联时间）
        /// </summary>
        /// <param name="CourseName">要删除的课程名称</param>
        public void DelCourse(String CourseName)
        {
            // 从本地数据库获取数据
            var Database = new DataContext();
            var TempDbCourseRes = Database.DbCourses
                .Include(DbCourse => DbCourse.DbLocTimes)
                .SingleOrDefault(a => a.Name.Equals(CourseName));
            if (TempDbCourseRes != null)
            {
                Database.Remove(TempDbCourseRes);
            }
            Database.SaveChanges();
        }

        /// <summary>
        /// 从数据库中删除课程某个时间
        /// </summary>
        /// <param name="CourseName">要删除时间课程的名称</param>
        /// <param name="InputLocation">>要删除时间的教室</param>
        /// <param name="InputWeek">>要删除时间的周</param>
        /// <param name="InputWeekDay">要删除时间的周几</param>
        /// <param name="InputBeginTime">要删除时间的上课时间</param>
        /// <param name="InputEndTime">要删除时间的下课时间</param>
        public void DelLocTime(String CourseName, String InputLocation, String InputWeek, int InputWeekDay, int InputBeginTime, int InputEndTime)
        {
            // 从本地数据库获取数据
            var Database = new DataContext();
            var TempDbCourseRes = Database.DbCourses
                .Include(DbCourse => DbCourse.DbLocTimes)
                .SingleOrDefault(a => a.Name.Equals(CourseName));
            DbLocTime DeletingLocTime=new DbLocTime();
            if (TempDbCourseRes != null)
            {
                if(TempDbCourseRes.DbLocTimes != null)
                {
                    foreach (DbLocTime TempDbLocTime in TempDbCourseRes.DbLocTimes)
                    {
                        if(TempDbLocTime.Location.Equals(InputLocation)
                            &&TempDbLocTime.Week.Equals(InputWeek)
                            &&TempDbLocTime.WeekDay.Equals(InputWeekDay)
                            && TempDbLocTime.BeginTime.Equals(InputBeginTime)
                            &&TempDbLocTime.EndTime.Equals(InputEndTime))
                        {
                            DeletingLocTime = TempDbLocTime;
                            
                        }
                    }
                    TempDbCourseRes.DbLocTimes.Remove(DeletingLocTime);
                }
            }
            Database.SaveChanges();
        }

        /// <summary>
        /// 向数据库中添加一个新便签
        /// </summary>
        /// <param name="courseName">课程名称</param>
        /// <param name="Content">便签内容</param>
        public void AddNote(String courseName, String content)
        {
            // 从本地数据库获取数据
            var Database = new DataContext();

            var TempDbCourseRes = Database.DbCourses
                .SingleOrDefault(a => a.Name.Equals(courseName));
            if (TempDbCourseRes != null)
            {
                TempDbCourseRes.Notes = content;
            }
            Database.SaveChanges();
        }

        /// <summary>
        /// 删除数据库中某课程的便签
        /// </summary>
        /// <param name="courseName">课程名称</param>
        public void DelNote(String courseName)
        {
            // 从本地数据库获取数据
            var Database = new DataContext();

            var TempDbCourseRes = Database.DbCourses
                .SingleOrDefault(a => a.Name.Equals(courseName));
            if (TempDbCourseRes != null)
            {
                TempDbCourseRes.Notes = "";
            }
            Database.SaveChanges();
        }
    }
}
