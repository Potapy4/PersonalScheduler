﻿using System;
using System.Collections.Generic;
using System.Linq;

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

                    if (scheduledEvent is RegularEvent)
                    {
                        RegularEvent regularEvent = scheduledEvent as RegularEvent;
                        regularEvent.Update();
                        onAdd(); // to update time in listbox for repeat interval
                    }
                    else
                    {
                        _toRemove.Add(scheduledEvent);
                    }
                }
            }

            _toRemove.ForEach((x) => RemoveEvent(x));
        }

        public void AddEvent(ScheduledEvent ev)
        {
            var isUniqueName = _events.FirstOrDefault(x => x.Name == ev.Name);
            if (isUniqueName != null)
            {
                throw new ArgumentException("Name must be be unique");
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
