namespace PersonalScheduler.Notifiers
{
    class VisualNotifier : INotifier
    {
        public void Notify(ScheduledEvent ev)
        {
            VisualNotificationWindow window = new VisualNotificationWindow(ev);
            window.Show();
        }
    }
}