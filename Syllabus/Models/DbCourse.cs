using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syllabus.Models
{
    public class DbCourse
    {
        public int DbCourseId { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 教师名称
        /// </summary>
        public String Teacher { get; set; }

        /// <summary>
        /// 上课地点时间安排
        /// </summary>
        public List<DbLocTime> DbLocTimes { get; set; }
    }
}
