using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class SendMail
    {
        public void SendEmail( string Number,string HomeName,string CustomerName, string? HomeOwnerN=null)
        {

            string fromEmail = "homeland.az.service@gmail.com";
            string toEmail = Number;
            string subject = "HomeLand.az";
            string body;
            if (HomeOwnerN==null?true :false)
            {
                body = $"Hörmətli :  {HomeName}." + Environment.NewLine +
                $"Sizə yeni müştəri yönləndirdik."+ Environment.NewLine + 
                $" Tarix: {DateTime.Now.ToString("M/d/yyyy h:mm")}" + Environment.NewLine +
                $"Müştəri :  {CustomerName}." + Environment.NewLine +
                $" " + Environment.NewLine +
                $"  Hörmətlə : Homeland Company";
            }
            else
            {
                body = $"Hörmətli :  {CustomerName}." + Environment.NewLine +
                $"Sizin müraciə etdiyiniz ev sahibinin nömrəsi: {HomeOwnerN}."+ Environment.NewLine  +
                $" Tarix: {DateTime.Now.ToString("M/d/yyyy h:mm")}" + Environment.NewLine +
                $"Ev sahibi :  {HomeName}." + Environment.NewLine +
                $" " + Environment.NewLine +
                $"  Hörmətlə : Homeland Company";
            }
             
            try
            {
                using (System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com"))
                {
                    smtp.Port = 587;
                    smtp.Credentials = new System.Net.NetworkCredential("homeland.az.service@gmail.com", "dvmq vowb frps tsye");
                    smtp.EnableSsl = true;

                    using (System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage(fromEmail, toEmail, subject, body))
                    {
                        smtp.Send(mailMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Gmail is wrong");
            }

        }

    }
}
