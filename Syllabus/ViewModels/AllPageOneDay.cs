using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syllabus.ViewModels
{
    /// <summary>
    /// 用于AllPage；
    /// 相当于一天有6节课，所以有6个AllPageOneCourse
    /// </summary>
    public class AllPageOneDay
    {
        public AllPageOneCourse[] AllPageOneday = new[]
        {
            new AllPageOneCourse(), new AllPageOneCourse(), new AllPageOneCourse(), new AllPageOneCourse(),
            new AllPageOneCourse(), new AllPageOneCourse()
        };
    }
}
