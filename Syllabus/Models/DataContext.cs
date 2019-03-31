using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syllabus.Models
{
    public class DataContext : DbContext
    {
        /// <summary>
        /// 课程数据库
        /// </summary>
        public DbSet<DbCourse> DbCourses { get; set; }

        /// <summary>
        /// 上课时间数据库
        /// </summary>
        public DbSet<DbLocTime> DbLocTimes { get; set; }

        /// <summary>
        /// 限定课程名称唯一
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbCourse>()
                .HasIndex(c => c.Name)
                .IsUnique();
        }

        /// <summary>
        /// 指定本地数据库
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=syllabus.db");
        }
    }
}
