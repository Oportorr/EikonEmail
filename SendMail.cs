namespace EIKON
{
    using System;
    using System.Collections.Generic;
    using System.Net.Mail;
    using System.Text;
    using System.IO;

        public class SendMail
        {

            public string name;
            public  SendMail()
            {
                name="Testing";
            }
                
            public string SendEikonEmail(string emailAddress, string fullName, string mailBody, string FileName,string _Subject)
            {
                try
                {
                    var myMessage = new MailMessage
                    {
                        Subject = _Subject,
                        Body = mailBody,
                        From = new MailAddress(emailAddress, fullName),
                        IsBodyHtml = true,
                        

                    };
                    //This is a testing Messages
                    //Another Testing Message
                    myMessage.Attachments.Add(new Attachment(FileName)); //Todo enviar archivo como parametro
                    myMessage.IsBodyHtml = true;  //HTML
                    myMessage.To.Add(new MailAddress(emailAddress));
                    var mySmtpClient = new SmtpClient();
                    mySmtpClient.Send(myMessage);

                    return "1|Sent";
                }
                catch (Exception ex)
                {
                    return "0|" + ex.Message;
                }
            }
        }
    }


