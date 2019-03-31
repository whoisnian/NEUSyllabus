using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syllabus.Models
{
    public class DbLocTime
    {
        public int DbLocTimeId { get; set; }

        /// <summary>
        /// 上课地点
        /// </summary>
        public String Location { get; set; }

        /// <summary>
        /// 周几上课
        /// </summary>
        public int WeekDay { get; set; }

        /// <summary>
        /// 第几节开始
        /// </summary>
        public int BeginTime { get; set; }

        /// <summary>
        /// 第几节结束
        /// </summary>
        public int EndTime { get; set; }

        /// <summary>
        /// 教学周（53个0和1组成的字符串）
        /// </summary>
        public String Week { get; set; }

        // 关联到对应的课程
        public int DbCourseId { get; set; }
        public DbCourse DbCourse { get; set; }
    }
}
