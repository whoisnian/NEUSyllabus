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

        //两列颜色；
        //用于周一周三周五
        private string[] FirstColor = { "HotPink" , "LightGoldenrodYellow", "HotPink", "LightGoldenrodYellow", "HotPink", "LightGoldenrodYellow"};
        //用于周日周二周四周六
        private string[] SecondColor = {"SpringGreen", "Aqua", "SpringGreen", "Aqua", "SpringGreen", "Aqua"};

        private int[] FirstNum = {0, 2, 4};
        private int[] SecondNum = {1, 3, 5, 6};

        public AllPageOneDay[] AllPageOneWeek =
        {
            new AllPageOneDay(), new AllPageOneDay(), new AllPageOneDay(), new AllPageOneDay(), new AllPageOneDay(),
            new AllPageOneDay(), new AllPageOneDay()
        };

        public AllPage()
        {
            this.InitializeComponent();
            DataModel = SimpleIoc.Default.GetInstance<DatabaseService>().GetAllCourses();
            foreach (var onecourse in DataModel.CourseCollection)
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

            foreach (int i in FirstNum)
            {
                for (int k = 0; k < 6; k++)
                {
                    if (AllPageOneWeek[i].AllPageOneday[k].Blocks[0] != " ")
                        AllPageOneWeek[i].AllPageOneday[k].Color = FirstColor[k];
                }
            }

            foreach (int i in SecondNum)
            {
                for (int k = 0; k < 6; k++)
                {
                    if (AllPageOneWeek[i].AllPageOneday[k].Blocks[0] != " ")
                        AllPageOneWeek[i].AllPageOneday[k].Color = SecondColor[k];
                }
            }

        }
    }
}
