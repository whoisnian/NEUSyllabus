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
using SyllabusV0._1.Services;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace SyllabusV0._1
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SettingPage : Page
    {
        private Courses ViewModel { get; set; }
        private LocTime NowLocTime { get; set; }

        public SettingPage()
        {
            this.InitializeComponent();
            this.ViewModel = SimpleIoc.Default.GetInstance<Courses>();
            this.NowLocTime = SimpleIoc.Default.GetInstance<LocTime>();
        }

        private void EnterAccount_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void NotEnterAccount_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void EnterCourse_OnClick(object sender, RoutedEventArgs e)
        {
            if (CourseNameBox.Text != "")
            {
                if (!ViewModel.CourseCollection.Contains(new Course(CourseNameBox.Text, TeacherNameBox.Text)))//没有用？？
                {
                    this.ViewModel.Add(CourseNameBox.Text, TeacherNameBox.Text);
                    //todo:关闭窗口
                }
                else
                {
                    //todo:通过某种手段弹出错误提示
                }
                
            }
            else
            {
                //todo:通过某种手段弹出错误提示
            }
        }

        private void NotEnterCourse_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ChooseWeekday_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ChooseTime_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void EnterTime_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void NotEnterTime_OnClick(object sender, RoutedEventArgs e)
        {
            //todo:关闭当前弹出窗口
        }
    }
}
