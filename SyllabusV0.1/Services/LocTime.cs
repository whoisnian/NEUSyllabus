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

        /// <summary>
        /// exp:new LocTime("one building A104","10000101011","3","3',"4"
        /// </summary>
        ///
        public LocTime()
        {

        }

        public LocTime(String InputLocation, String InputWeek,int InputWeekDay,int InputBeginTime, int InputEndTime)
        {
            this.Location = InputLocation;
            this.Week = InputWeek;
            this.WeekDay = InputWeekDay;
            this.BeginTime = InputBeginTime;
            this.EndTime = InputEndTime;
            this.WeekForShow = "1-8，9-19周";//todo
            this.TimeForShow = "周三 1-5节";//todo
        }
    }

    
}
