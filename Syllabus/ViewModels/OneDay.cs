using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syllabus.ViewModels
{
    /// <summary>
    /// 一天的课程，共6节大课；
    /// </summary>
    public class OneDay
    {
        public Course[] Oneday = new[] { new Course(), new Course(), new Course(), new Course(), new Course(), new Course() };//改为使用Course以方便数据读取

        internal void AddEmptyLocTime()
        {
            foreach (var onecourse in Oneday)
            {
                if(onecourse.LocTimes.Count==0)
                    onecourse.LocTimes.Add(new LocTime());
            }
        }
    }
}
