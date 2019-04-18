using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syllabus.ViewModels
{

    /// <summary>
    /// 用于AllPage；
    /// 一节课就是一个AllPageOneCourse;
    /// 为2个string
    /// </summary>
    public class AllPageOneCourse
    {
        public string[] Blocks = new string[2] { " ", " " };
        public string Color { get; set; }

        
        public AllPageOneCourse()
        {
            Color = "White";
        }
    }
}
