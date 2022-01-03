using System;
using System.Collections.Generic;

namespace FactoryMethod
{
    class Program
    {

        public abstract class Messenger
        {
            public abstract Alert CreateAlert(string title, string msg, string recipient);
            public void SendAlert(Alert alert)
            {
                alert.Send();
            }
        }

        public abstract class Alert
        {
            public abstract void Send();
        }

        public class SMSAlert : Alert
        {
            private string number;
            private string text;

            public SMSAlert(string number, string text)
            {
                this.number = number;
                this.text = text;
            }

            public override void Send()
            {
                Console.WriteLine($"Invio di SMS al numero: {number}");
            }
        }

        public class EmailAlert : Alert
        {
            private string address;
            private string body;
            private string subject;
            private bool isHtml;

            public EmailAlert(string address, string body, string subject, bool isHtml)
            {
                this.address = address;
                this.body = body;
                this.subject = subject;
                this.isHtml = isHtml;
            }

            public override void Send()
            {
                Console.WriteLine($"Invio di email a: {address}");
            }
        }

        public class EmailMessenger : Messenger
        {
            public override Alert CreateAlert(string title, string msg, string recipient)
            {
                EmailAlert alert = new EmailAlert(recipient, title, msg, true);
                return alert;
            }
        }
        
        public class SMSMessenger : Messenger
        {
            public override Alert CreateAlert(string title, string msg, string recipient)
            {
                SMSAlert alert = new SMSAlert(recipient, msg);
                return alert;
            }
        }
        static void Main(string[] args)
        {
            string text = "messaggio di avviso";
            string title = "Warning";
            string recipient = "antonio@dominio.com";

            Messenger messenger = null;
            if (recipient.Contains("@"))
            {
                messenger = new EmailMessenger();
            }
            else
            {
                messenger = new SMSMessenger();
            }

            Alert alert = messenger.CreateAlert(title, text, recipient);
            messenger.SendAlert(alert);
        }
    }

    
}
