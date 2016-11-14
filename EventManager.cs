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

        public void ProcessEvents()
		{
			
		}

		public void AddEvent(ScheduledEvent ev)
		{
            IEnumerable<ScheduledEvent> res = _events.Where(x => x.Name == ev.Name);
            if (res.Count() > 0)
            {
                throw new ArgumentException("Name can be unique");
            }

            _events.Add(ev);
            _events.OrderBy(x => x.DateTime);
		}

		public void RemoveEvent(ScheduledEvent ev)
		{
            if (ev != null)
            {
                _events.Remove(ev);
            }
		}
	}
}
