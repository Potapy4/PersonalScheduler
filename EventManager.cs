using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalScheduler
{
    public class EventManager
    {
        private List<ScheduledEvent> _events = new List<ScheduledEvent>();
        private static List<ScheduledEvent> _toRemove = new List<ScheduledEvent>();
        public List<ScheduledEvent> ScheduledEvents { get { return _events; } }

        public event Action onAdd;
        public event Action onDelete;

        public void ProcessEvents()
        {
            DateTime dateNow = DateTime.Now;
            _toRemove.Clear();

            foreach (ScheduledEvent scheduledEvent in _events)
            {
                if (dateNow >= scheduledEvent.DateTime)
                {
                    _toRemove.Add(scheduledEvent);

                    if (scheduledEvent.Notifications.Contains(NotificationType.Sound))
                    {
                        new Notifiers.SoundNotifier().Notify();
                    }

                    if (scheduledEvent.Notifications.Contains(NotificationType.Visual))
                    {
                        new Notifiers.VisualNotifier().Notify(scheduledEvent);
                    }

                    if (scheduledEvent.Notifications.Contains(NotificationType.Email))
                    {
                        try
                        {
                            new Notifiers.EmailNotifier("receiver@email").Notify(scheduledEvent);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }

            _toRemove.ForEach((x) => RemoveEvent(x));
        }

        public void AddEvent(ScheduledEvent ev)
        {
            IEnumerable<ScheduledEvent> res = _events.Where(x => x.Name == ev.Name);
            if (res.Count() > 0)
            {
                throw new ArgumentException("Name can be unique");
            }

            _events.Add(ev);
            _events = _events.OrderBy(x => x.DateTime).ToList();
            onAdd();
        }

        public void RemoveEvent(ScheduledEvent ev)
        {
            _events.Remove(ev);
            onDelete();
        }
    }
}
