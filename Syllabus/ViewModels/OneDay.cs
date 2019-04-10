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
        public string ClassName{get;set;}
        public string ClassSite{get;set;}
        public string ClassNote{get;set;}

        /// <summary>
        /// 颜色可以使用
        /// SpringGreen
        /// Aqua
        /// HotPink
        /// LightGoldenrodYellow
        /// 这样的英文字母
        /// 参考网址：https://docs.microsoft.com/en-us/uwp/api/windows.ui.colors
        /// 也可以是
        /// #000000
        /// 这样的字符
        /// </summary>
        public string ClassColor;  //表示该课程的背景颜色


        public OneClass()
        {
            this.ClassName = "";
            this.ClassSite = "";
            this.ClassNote = "";
            this.ClassColor = "#FFFFFF";
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
