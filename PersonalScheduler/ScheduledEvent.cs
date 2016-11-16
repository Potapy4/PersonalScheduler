using System;
using System.Collections.Generic;

namespace PersonalScheduler
{
    public enum NotificationType
    {
        Email,
        Sound,
        Visual
    }

    public enum RepeatType
    {
        Minutes,
        Hours,
        Days
    }

    public class ScheduledEvent
    {
        protected readonly string _name;
        protected DateTime _datetime;
        protected readonly string _description;
        protected readonly string _place;
        protected readonly List<NotificationType> _notifications;

        public string Name { get { return _name; } }
        public DateTime DateTime { get { return _datetime; } private set { _datetime = value; } }
        public string Description { get { return _description; } }
        public string Place { get { return _place; } }
        public List<NotificationType> Notifications { get { return _notifications; } }

        public ScheduledEvent(string Name, DateTime Date, string Description, string Place, List<NotificationType> Notifications)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                if (Name == null)
                {
                    throw new ArgumentNullException("Name cannot be NULL");
                }
                else
                {
                    throw new ArgumentException("Name cannot be empty");
                }
            }

            if (Date < DateTime.Now)
            {
                throw new ArgumentOutOfRangeException("Date cannot be earlier than the current moment");
            }

            if (Notifications.Count == 0)
            {
                throw new ArgumentException("At least one notification has to be specified for an event");
            }

            _name = Name;
            _datetime = Date;
            _description = Description;
            _place = Place;
            _notifications = new List<NotificationType>(Notifications);            
        }
    }
}
