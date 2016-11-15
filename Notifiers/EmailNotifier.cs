using System;
using System.Net;
using System.Net.Mail;

namespace PersonalScheduler.Notifiers
{
    class EmailNotifier : INotifier
    {
        private readonly string _receiver;
        private SmtpClient smtpClient;

        public EmailNotifier(string receiverEmail)
        {
            _receiver = receiverEmail;
        }

        public void Notify(ScheduledEvent obj)
        {
            try
            {
                NetworkCredential nc = new NetworkCredential("login", "password");

                MailMessage msg = new MailMessage("login", _receiver);
                msg.Subject = "PersonalScheduler Notificaton";
                msg.Body = string.Format("Notification: {0}\n\nDescription: {1}\n\nPlace: {2}",obj.Name, obj.Description, obj.Place);

                smtpClient.Host = "smtp.server";
                smtpClient.Port = 123;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = nc;

                smtpClient.Send(msg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
