using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalScheduler
{
    class RegularEvent : ScheduledEvent
    {
        private TimeSpan _interval;
        public RegularEvent(string Name, DateTime Date, string Description, string Place, List<NotificationType> Notifications, RepeatType repeatType, int interval) : base(Name, Date, Description, Place, Notifications)
        {
            if (Notifications.Contains(NotificationType.Email) && interval < 5)
            {
                throw new ArgumentException("Interval must be more than 5 minutes for Email notification");
            }

            switch (repeatType)
            {
                case RepeatType.Minutes:
                    _interval = new TimeSpan(0, interval, 0);
                    break;
                case RepeatType.Hours:
                    _interval = new TimeSpan(interval, 0, 0);
                    break;
                case RepeatType.Days:
                    _interval = new TimeSpan(interval, 0, 0, 0);
                    break;
            }            
        }

        public void Update()
        {
            _datetime = _datetime.Add(_interval);
        }
    }
}
