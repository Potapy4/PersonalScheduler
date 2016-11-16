namespace PersonalScheduler.Notifiers
{
    public interface INotifier
    {
        void Notify(ScheduledEvent obj);        
    }
}
