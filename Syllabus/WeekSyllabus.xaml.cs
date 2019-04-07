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


    }
}
