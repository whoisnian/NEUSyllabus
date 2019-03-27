using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyllabusV0._1.Models
{
    public class DbLocTime
    {
        // 每一个表都需要primary key
        public int Id { get; set; }
        public String Location { get; set; }
        public int WeekDay { get; set; }
        public int BeginTime { get; set; }
        public int EndTime { get; set; }
        public String Week { get; set; }
    }
}
