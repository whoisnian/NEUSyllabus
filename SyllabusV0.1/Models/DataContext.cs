using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyllabusV0._1.Models
{
    public class DataContext : DbContext
    {
        public DbSet<DbCourse> DbCourses { get; set; }

        // 限定课程名称唯一
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbCourse>()
                .HasIndex(c => c.Name)
                .IsUnique();
        }

        // 指定本地数据库
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=syllabus.db");
        }
    }
}
