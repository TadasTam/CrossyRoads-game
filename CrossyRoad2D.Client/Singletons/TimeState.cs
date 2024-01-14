using CrossyRoad2D.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Singletons
{
    public class TimeState
    {
        private static TimeState _instance;
        public static TimeState Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TimeState();
                }
                return _instance;
            }
        }

        public double TimeDeltaSeconds { get; set; } = 0.0;
        public double TimeSecondsSinceStart
        {
            get
            {
                return (DateTime.UtcNow - _dateTimeStart).TotalSeconds;
            }
        }

        private DateTime _lastDateTimeBeforeUpdate = DateTime.UtcNow;
        private DateTime _dateTimeStart = DateTime.UtcNow;

        private TimeState() { }

        public void UpdateBeforeProcessing()
        {
            TimeDeltaSeconds = (DateTime.UtcNow - _lastDateTimeBeforeUpdate).TotalSeconds;
            _lastDateTimeBeforeUpdate = DateTime.UtcNow;
        }
    }
}
