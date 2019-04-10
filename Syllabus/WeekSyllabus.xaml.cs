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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Syllabus
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class WeekSyllabus : Page
    {
        public WeekSyllabus()
        {
            this.InitializeComponent();
            this.Sunday = new OneDay();
            this.Monday = new OneDay();
            this.Tuesday = new OneDay();
            this.Wednesday = new OneDay();
            this.Thursday = new OneDay();
            this.Friday = new OneDay();
            this.Saturday = new OneDay();


            //Week选择
            List<ViewModelWeekList> WeekList = new List<ViewModelWeekList>();
            for (int count = 1; count < 54; count++)
            {
                WeekList.Add(new ViewModelWeekList { Week = count.ToString()});
            }
            ChooseWeek.ItemsSource = WeekList;

            


        }
        

        public OneDay Sunday
        {
            get;
            set;
        }

        public OneDay Monday
        {
            get;
            set;
        }

        public OneDay Tuesday
        {
            get;
            set;
        }

        public OneDay Wednesday
        {
            get;
            set;
        }

        public OneDay Thursday
        {
            get;
            set;
        }

        public OneDay Friday
        {
            get;
            set;
        }

        public OneDay Saturday
        {
            get;
            set;
        }

        public int WhichWeek;
        public string MyColor;


        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EnterNote_Click(object sender, RoutedEventArgs e)
        {
            if (NoteBox.Text != "")
            {
                //应该存到数据库里去；
               //目前只是把它显示了出来；但是切换页面后就没有了；
                this.Sunday.Oneday[0].ClassNote = NoteBox.Text;
                
                Bindings.Update();  //在Note的Layout上写完数据后要通过这句话刷新一下才可以；
            }
        }


        //选择的Week;
        private void ChooseWeek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ChooseWeek.SelectedItem != null)
            {
                //可以得到被选择的数据；
                ViewModelWeekList SelectedWeek = ChooseWeek.SelectedItem as ViewModelWeekList;
                Text.Text = SelectedWeek.Week;
            }
        }

       

    }
}
