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
        public List<ScheduledEvent> ScheduledEvents { get { return _events; } }
       
        public event Action onAdd;
        public event Action onDelete;

        private static Notifiers.VisualNotifier visual = new Notifiers.VisualNotifier();
        private static Notifiers.SoundNotifier sound = new Notifiers.SoundNotifier();

        public void ProcessEvents()
        {
            DateTime dateNow = DateTime.Now;           

            foreach (var i in _events)
            {
                if (dateNow >= i.DateTime)
                {
                    if (i.Notifications.Contains(NotificationType.Sound))
                    {
                        sound.Notify();
                    }

                    if (i.Notifications.Contains(NotificationType.Visual))
                    {
                        visual.Notify(i);
                    }
                }
            }
        }

        public void AddEvent(ScheduledEvent ev)
        {
            IEnumerable<ScheduledEvent> res = _events.Where(x => x.Name == ev.Name);
            if (res.Count() > 0)
            {
                throw new ArgumentException("Name can be unique");
            }

            _events.Add(ev);
            _events =_events.OrderBy(x => x.DateTime).ToList();
            onAdd();
        }

        public void RemoveEvent(ScheduledEvent ev)
        {
            _events.Remove(ev);
            onDelete();
        }
    }
}
