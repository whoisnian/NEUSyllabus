using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Syllabus.ViewModels
{
    class OneWeek
    {
        public OneDay[] Oneweek = new[] { new OneDay(), new OneDay(), new OneDay(), new OneDay(), new OneDay(), new OneDay(), new OneDay() };

        internal void AddEmptyLocTime()
        {
            foreach (var oneday in Oneweek)
            {
                oneday.AddEmptyLocTime();
            }
        }

        internal void Clear()
        {
            Oneweek = null;
            Oneweek = new[] { new OneDay(), new OneDay(), new OneDay(), new OneDay(), new OneDay(), new OneDay(), new OneDay() };
        }
    }
}
