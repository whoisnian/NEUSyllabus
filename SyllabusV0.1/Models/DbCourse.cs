using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SyllabusV0._1.Models
{
    public class DbCourse
    {
        // 每一个表都需要primary key
        public int Id { get; set; }
        public String Name { get; set; }
        public String Teacher { get; set; }
        public List<DbLocTime> DbLocTimes { get; set; }
    }
}
