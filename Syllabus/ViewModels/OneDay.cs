using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syllabus.ViewModels
{
    /// <summary>
    /// 一节课所包含的信息
    /// 课程名字；课程地点；课程Note；
    /// </summary>
    public class OneClass
    {
        public string ClassName;
        public string ClassSite;
        public string ClassNote;

        public OneClass()
        {
            this.ClassName = "";
            this.ClassSite = "";
            this.ClassNote = "";
        }

        public OneClass(string newClassName, string newClassSite, string newClassNote)
        {
            ClassName = newClassName;
            ClassSite = newClassSite;
            ClassNote = newClassNote;
        }

        public string GetName
        {
            get
            {
                return this.ClassName;
            }
        }

        public string GetSite
        {
            get
            {
                return this.ClassSite;
            }
        }

        public string GetNote
        {
            get
            {
                 return this.ClassNote;
            }
        }

    }

    /// <summary>
    /// 一天的课程，共6节大课；
    /// </summary>
    public class OneDay
    {
        public OneClass[] Oneday = new[] { new OneClass(), new OneClass(), new OneClass(), new OneClass(), new OneClass(), new OneClass()};

    }
}
