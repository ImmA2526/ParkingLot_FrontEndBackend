using Experimental.System.Messaging;
using System;
namespace PMSMQ
{
        
    public class Sender
    {
        /// <summary>
        /// Send Mail.
        /// </summary>

        public void MailSender()
        {
            var forgotPassword = "Reset Password link for Parking Model:- This is the Link of Your Forgot Password ";
            MessageQueue msgQueue = new MessageQueue();
            if (MessageQueue.Exists(@".\Private$\MyQueue"))
            {
                msgQueue = new MessageQueue(@".\Private$\MyQueue");
            }
            else
            {
                msgQueue = MessageQueue.Create(@".\Private$\MyQueue");
            }
            Message message = new Message
            {
                Formatter = new BinaryMessageFormatter(),
                Body = forgotPassword
            };
            msgQueue.Label = "E-Mails";
            msgQueue.Send(message);
        }
    }
}
