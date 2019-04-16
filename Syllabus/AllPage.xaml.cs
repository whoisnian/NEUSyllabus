using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Ioc;
using Syllabus.Services;
using Syllabus.ViewModels;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Syllabus
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class AllPage : Page
    {
        //全部课程数据
        private Courses DataModel;

        public AllPageOneDay[] AllPageOneWeek =
        {
            new AllPageOneDay(), new AllPageOneDay(), new AllPageOneDay(), new AllPageOneDay(), new AllPageOneDay(),
            new AllPageOneDay(), new AllPageOneDay()
        };

        public AllPage()
        {
            this.InitializeComponent();
            DataModel = SimpleIoc.Default.GetInstance<DatabaseService>().GetAllCourses();
            foreach (var onecourse in DataModel.CourseCollection)//显示第一周课程
            {
                foreach (var oneloctime in onecourse.LocTimes)
                {
                    for (int k = oneloctime.BeginTime / 2; k < oneloctime.EndTime / 2; k++)
                    {
                        if (AllPageOneWeek[oneloctime.WeekDay - 1].AllPageOneday[k].Blocks[0] == " ")
                        {
                            AllPageOneWeek[oneloctime.WeekDay - 1].AllPageOneday[k].Blocks[0] =
                                onecourse.Name + "(" + onecourse.Teacher + ")\n"
                                 + "(" + oneloctime.WeekForShow + ")" + oneloctime.Location;
                        }
                        else if (AllPageOneWeek[oneloctime.WeekDay - 1].AllPageOneday[k].Blocks[1] == " ")
                        {
                            AllPageOneWeek[oneloctime.WeekDay - 1].AllPageOneday[k].Blocks[1] =
                                onecourse.Name + "(" + onecourse.Teacher + ")\n"
                                 + "(" + oneloctime.WeekForShow + ")" + oneloctime.Location;
                        }
                        else
                        {
                            AllPageOneWeek[oneloctime.WeekDay - 1].AllPageOneday[k].Blocks[1] = "\n"+
                                onecourse.Name + "(" + onecourse.Teacher + ")\n"
                                 + "(" + oneloctime.WeekForShow + ")" + oneloctime.Location;
                        }
                    }
                }
            }
        }
    }
}
