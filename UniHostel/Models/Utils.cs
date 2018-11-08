using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace UniHostel.Models
{
    public class Utils
    {

        public static string getRandomID()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssffff");
        }

        public static void SendingMailUsingMailKit(string contentMail, string email)
        {
            Task.Run(() =>
            {
                try
                {
                    var message = new MimeMessage();
                    MailboxAddress from = new MailboxAddress("UniHostel-RoomRentalManagement@admin.vn"
                                                        , "UniHostel-RoomRentalManagement@admin.vn");
                    MailboxAddress to = new MailboxAddress(email);
                    message.From.Add(from);
                    message.To.Add(to);
                    message.Subject = "UniHostel - Recover mail " + DateTime.Now.ToString();

                    message.Body = new TextPart("html")
                    {
                        Text = contentMail
                    };
                    using (var client = new MailKit.Net.Smtp.SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587);
                        client.Authenticate("dongphan24@gmail.com", "dongphan987654321");
                        client.Send(message);
                        client.Disconnect(true);
                    }
                }
                catch (Exception e)
                {
                    Log("Error in sending mail process " + e.ToString());
                }
            });
        }


        public static void Log(string msg)
        {
            Task.Run(() =>
            {
                string filename = AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt";
                StreamWriter writer = null;
                if (!File.Exists(filename))
                {
                    File.Create(filename);
                }
                using (writer = new StreamWriter(filename, true))
                {
                    writer.WriteLine(DateTime.Now.ToString() + " " + msg);
                    writer.Flush();
                }
            });

        }

    }
}