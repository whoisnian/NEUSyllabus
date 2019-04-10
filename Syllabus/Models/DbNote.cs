using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syllabus.Models
{
    public class DbNote
    {
        public int DbNoteId { get; set; }

        /// <summary>
        /// 便签内容
        /// </summary>
        public String Content { get; set; }

        /// <summary>
        /// 第几周
        /// </summary>
        public int Week { get; set; }

        /// <summary>
        /// 周几
        /// </summary>
        public int WeekDay { get; set; }

        /// <summary>
        /// 第几节课
        /// </summary>
        public int Time { get; set; }
    }
}
