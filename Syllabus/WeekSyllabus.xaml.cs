using Syllabus.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Syllabus
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class WeekSyllabus : Page
    {
        
        private OneWeek ViewModel { get; set; }
        private Courses DataModel;

        public string BoxText;

        public WeekSyllabus()
        {
            this.InitializeComponent();
            ViewModel = SimpleIoc.Default.GetInstance<OneWeek>();
            DataModel = SimpleIoc.Default.GetInstance<DatabaseService>().GetAllCourses();
            foreach (var onecourse in DataModel.CourseCollection)//显示第一周课程
            {
                foreach (var oneloctime in onecourse.LocTimes)
                {
                    if (oneloctime.Week[1] == '1')
                    {
                        for (int k = oneloctime.BeginTime / 2; k < oneloctime.EndTime / 2; k++)
                        {
                            ViewModel.Oneweek[oneloctime.WeekDay - 1].Oneday[k] = onecourse.DeepCopy();
                            ViewModel.Oneweek[oneloctime.WeekDay - 1].Oneday[k].SetColor("#AADDFF");
                            ViewModel.Oneweek[oneloctime.WeekDay - 1].Oneday[k].ClearLocTime();
                            ViewModel.Oneweek[oneloctime.WeekDay - 1].Oneday[k].AddLocTime(oneloctime);
                        }
                    }
                }
            }

            ViewModel.AddEmptyLocTime();
            //Week选择
            List<ViewModelWeekList> WeekList = new List<ViewModelWeekList>();
            for (int count = 1; count <= 28; count++)
            {
                WeekList.Add(new ViewModelWeekList { Week = count.ToString() });
            }
            ChooseWeek.ItemsSource = WeekList;

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }


        //添加便签
        private void EnterNote_Click(object sender, RoutedEventArgs e)
        {
            //得到要改变的是哪节课
            var Bianhao = (sender as Button).Tag.ToString();
            int Day = ((int)Bianhao[0]) - 48;
            int Class = (int)Bianhao[1] - 48;
 
           //不对BoxText设置判断是否为空；
           //这样为空的时候直接就是删除了；
           //就不需要再写一个删除的Button了
            this.ViewModel.Oneweek[Day].Oneday[Class].Notes = BoxText;
            Bindings.Update();  //在Note的Layout上写完数据后要通过这句话刷新一下才可以；

                //把东西写入数据库；
                //下边这两句不正确。
                //楼上说不正确是因为调用错方法了！
            SimpleIoc.Default.GetInstance<DatabaseService>().AddNote(ViewModel.Oneweek[Day].Oneday[Class].Name, BoxText);
               


        }



        //选择的Week;
        private void ChooseWeek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ChooseWeek.SelectedItem != null)
            {
                //可以得到被选择的数据；
                ViewModelWeekList SelectedWeek = ChooseWeek.SelectedItem as ViewModelWeekList;
                Text.Text = SelectedWeek.Week;
                int newweek;
                int.TryParse(SelectedWeek.Week, out newweek);
                ViewModel.Clear();
                foreach (var onecourse in DataModel.CourseCollection)//显示第一周课程
                {
                    foreach (var oneloctime in onecourse.LocTimes)
                    {
                        if (oneloctime.Week[newweek] == '1')
                        {
                            for (int k = oneloctime.BeginTime / 2; k < oneloctime.EndTime / 2; k++)
                            {
                                ViewModel.Oneweek[oneloctime.WeekDay - 1].Oneday[k] = onecourse.DeepCopy();
                                ViewModel.Oneweek[oneloctime.WeekDay - 1].Oneday[k].ClearLocTime();
                                ViewModel.Oneweek[oneloctime.WeekDay - 1].Oneday[k].AddLocTime(oneloctime);
                            }
                        }
                    }
                }
                ViewModel.AddEmptyLocTime();
                Bindings.Update();
            }
        }

        
    }
}
