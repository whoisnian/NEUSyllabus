using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyllabusV0._1.Models
{
    public class DbCourse
    {
        // 每一个表都需要primary key
        public int Id { get; set; }
        public String Name { get; set; }
        public String Teacher { get; set; }
        public List<DbLocTime> DbLocTimes { get; set; }

        // 直接使用int会出错，无法创建表
        public List<DbInt> WeekChoices { get; set; }
    }
}
