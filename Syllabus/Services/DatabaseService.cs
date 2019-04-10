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
                System.Diagnostics.Debug.WriteLine("Course: " + TempDbCourse.Name);
                AllCourses.Add(TempDbCourse.Name, TempDbCourse.Teacher);
                if(TempDbCourse.DbLocTimes == null)
                {
                    continue;
                }
                foreach(DbLocTime TempDbLocTime in TempDbCourse.DbLocTimes)
                {
                    System.Diagnostics.Debug.WriteLine("    Time: " + TempDbLocTime.Week);
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
                .SingleOrDefault(a => a.Name == TempCourse.Name);
            if(TempDbCourseRes == null)
            {
                // 新建课程
                DbCourse TempDbCourse = new DbCourse();
                TempDbCourse.Name = TempCourse.Name;
                TempDbCourse.Teacher = TempCourse.Teacher;
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
            Database.SaveChanges();
        }

        /// <summary>
        /// 获取所有便签
        /// </summary>
        /// <returns>DbNote的List列表</returns>
        public List<DbNote> GetAllNotes()
        {
            List<DbNote> dbNotes = new List<DbNote>();

            // 从本地数据库获取数据
            var Database = new DataContext();
            dbNotes = Database.DbNotes
                .ToList<DbNote>();

            return dbNotes;
        }

        /// <summary>
        /// 向数据库中添加一个新便签
        /// </summary>
        /// <param name="Content">便签内容</param>
        /// <param name="Week">第几周</param>
        /// <param name="WeekDay">周几</param>
        /// <param name="Time">第几节课</param>
        public void AddNote(String content, int week, int weekDay, int time)
        {
            // 从本地数据库获取数据
            var Database = new DataContext();

            DbNote TempDbNote = new DbNote()
            {
                Content = content,
                Week = week,
                WeekDay = weekDay,
                Time = time
            };

            Database.DbNotes.Add(TempDbNote);
            Database.SaveChanges();
        }
    }
}
