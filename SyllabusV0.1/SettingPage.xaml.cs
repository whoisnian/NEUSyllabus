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
        public LocTime NowLocTime;

        public SettingPage()
        {
            InitializeComponent();
            ViewModel = SimpleIoc.Default.GetInstance<Courses>();
            NowLocTime = SimpleIoc.Default.GetInstance<LocTime>();
        }

        private async void EnterAccount_OnClick(object sender, RoutedEventArgs e)
        {
            await SimpleIoc.Default.GetInstance<GetCoursesService>().LoginAndGetCoursesAsync(NameBox.Text, PswBox.Password);
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
            int idx = (int) ((sender as GridView).Tag) - 1;
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

        private void DelCourse_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.Delete((sender as Button).Tag.ToString());
        }

        private void DelLoctime_OnClick(object sender, RoutedEventArgs e)
        {
            int LocTimeTag = (int)(sender as Button).Tag;
            ViewModel.DeleteLocTime(LocTimeTag);
        }

    }
}
