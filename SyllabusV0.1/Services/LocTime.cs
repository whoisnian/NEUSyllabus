using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Networking.Sockets;

namespace SyllabusV0._1.Services
{
    public class LocTime
    {
        public String Location;
        public int WeekDay,BeginTime, EndTime;
        public String Week;
        public String WeekForShow,TimeForShow;
        public int Tag;

        /// <summary>
        /// exp:new LocTime("one building A104","10000101011","3","3',"4"
        /// </summary>
        ///
        String ToHanzi(int day)
        {
            if (day == 1) return "周一";
            if (day == 2) return "周二";
            if (day == 3) return "周三";
            if (day == 4) return "周四";
            if (day == 5) return "周五";
            if (day == 6) return "周六";
            if (day == 7) return "周日";
            return "周几";
        }
        public LocTime()
        {
            Week = "00000000000000000000";
        }

        public LocTime(String InputLocation, String InputWeek,int InputWeekDay,int InputBeginTime, int InputEndTime,int InputTag)
        {
            Location = InputLocation;
            Week = InputWeek;
            WeekDay = InputWeekDay;
            BeginTime = InputBeginTime;
            EndTime = InputEndTime;
            WeekForShow = "";
            char pre = '0';
            int preidx = 1;
            bool first = true;
            for (int i = 0; i < Week.Length; ++i)
            {
                if (Week[i] == '1' && pre == '0')
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        WeekForShow += ", ";
                    }
                    WeekForShow += (i + 1).ToString();
                    preidx = i;

                }

                if (Week[i] == '0' && pre == '1')
                {
                    if (preidx != i - 1)
                    {
                        WeekForShow += "-"+i.ToString();
                    }
                    else
                    {
                        WeekForShow += i.ToString();
                    }                    
                }

                pre = Week[i];
            }

            WeekForShow += "周";

            TimeForShow = ToHanzi(WeekDay)+" "+BeginTime+"-"+EndTime+"节";//todo
            Tag = InputTag;
        }

    }

    
}
