﻿using GalaSoft.MvvmLight.Ioc;
using Syllabus.Services;
using Syllabus.ViewModels;
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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Syllabus
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SettingPage : Page
    {
        private Courses ViewModel { get; set; }
        public LocTime NowLocTime;

        public SettingPage()
        {
            InitializeComponent();
            //ViewModel = SimpleIoc.Default.GetInstance<Courses>();
            ViewModel = SimpleIoc.Default.GetInstance<DatabaseService>().GetAllCourses();
            NowLocTime = SimpleIoc.Default.GetInstance<LocTime>();
        }

        private async void EnterAccount_OnClick(object sender, RoutedEventArgs e)
        {
            List<Course> AllCourses = await SimpleIoc.Default.GetInstance<GetCoursesService>().LoginAndGetCoursesAsync(NameBox.Text, PswBox.Password);
            foreach(var TempCourse in AllCourses)
            {
                SimpleIoc.Default.GetInstance<DatabaseService>().AddCourse(TempCourse);
            }
            ViewModel = SimpleIoc.Default.GetInstance<DatabaseService>().GetAllCourses();
        }

        private async void EnterCourse_OnClick(object sender, RoutedEventArgs e)
        {
            if (CourseNameBox.Text != "")
            {
                if (!ViewModel.Contains(CourseNameBox.Text))//没有用？？
                {
                    this.ViewModel.Add(CourseNameBox.Text, TeacherNameBox.Text);
                }
                else
                {
                    ContentDialog InvalidInputDialog = new ContentDialog()
                    {
                        Title = "课程名称重复",
                        Content = "课程名称重复，请在已有课程中添加时间",
                        CloseButtonText = "Ok"
                    };
                    await InvalidInputDialog.ShowAsync();
                }

            }
            else
            {
                ContentDialog InvalidInputDialog = new ContentDialog()
                {
                    Title = "课程名称空",
                    Content = "课程名称空，请填写课程名称",
                    CloseButtonText = "Ok"
                };
                await InvalidInputDialog.ShowAsync();
            }
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NowLocTime.SolveWeek((sender as GridView).SelectedItems);
        }

        private void Control_OnFocusDisengaged(Control sender, FocusDisengagedEventArgs args)
        {

        }

        private void ChooseWeekday_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NowLocTime.WeekDay = (sender as ComboBox).SelectedIndex + 1;
        }

        private void ChooseTime_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String midstring = (String)(sender as ComboBox).SelectedItem;
            int state = 0;
            NowLocTime.BeginTime = 0;
            NowLocTime.EndTime = 0;
            foreach (var ch in midstring)
            {
                if (ch <= '9' && ch >= '0')
                {
                    if (state == 0)
                    {
                        NowLocTime.BeginTime = NowLocTime.BeginTime * 10 + ch - '0';
                    }
                    else
                    {
                        NowLocTime.EndTime = NowLocTime.EndTime * 10 + ch - '0';
                    }
                }
                else
                {
                    state = 1;
                }
            }
        }

        private void ChooseLocation_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            NowLocTime.Location = (sender as TextBox).Text;
        }

        private void EnterTime_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.AddLocTime((sender as Button).Tag.ToString(), NowLocTime);
        }

        private void DelCourse_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.Delete((sender as Button).Tag.ToString());
        }

        private void DelLoctime_OnClick(object sender, RoutedEventArgs e)
        {
            int LocTimeTag = (int)(sender as Button).Tag;
            ViewModel.DeleteLocTime(LocTimeTag);
        }


        private void FlyoutBase_OnOpened(object sender, object e)
        {
            NowLocTime = new LocTime();
        }
    }
}