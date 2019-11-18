using System;
using System.Net.Mail;
using System.Windows;

namespace MessagingPhone
{
    class Messaging
    {
        public Messaging()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("sethnorton06@gmail.com");
                mail.To.Add("4235796874@vtext.com");
                mail.Subject = "";
                mail.Body = "Your program is done";

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("sethnorton06@gmail.com", "INeverCheckEmail");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                //MessageBox.Show("mail Send");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
       
    }
}
