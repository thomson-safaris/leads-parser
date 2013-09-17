using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;
using System.Net.Mime;
using System.IO;

namespace leads_parser
{
    class Emailer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Emailer()
        {

        }

        public void send_emails(string path, string file, string recipient_list, string site_name)
        {
            string file_contents = "";
            try
            {
                using (StreamReader sr = new StreamReader(path + file))
                {
                    file_contents = sr.ReadToEnd();
                }
            }
            catch (Exception crap)
            {
                Console.WriteLine("Reading from file failed: " + crap);
            }

            
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

            client.Credentials = new NetworkCredential("tom.thomson.safaris@gmail.com", "THOpass12");


            if (file_contents == "")
            {
                var email = Email
                    .From("tom.thomson.safaris@gmail.com")
                    .UseSSL()
                    .UsingClient(client)
                    .To(recipient_list)
                    .Subject("Leads from " + site_name + " - no leads today")
                    .Body("There were no leads from the " + site_name + " today - if this is unexpected, please contact the site administrator.")
                    .Send();
            }
            else
            {
                Attachment leads = new Attachment(path + file, MediaTypeNames.Application.Octet);

                var email = Email
                    .From("tom.thomson.safaris@gmail.com")
                    .UseSSL()
                    .UsingClient(client)
                    .To(recipient_list)
                    .Subject(file + " from " + site_name)
                    .Body(file_contents)
                    .Attach(leads)
                    .Send();
            }
        }

        public void send_original_data_file(string file_name)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

            client.Credentials = new NetworkCredential("tom.thomson.safaris@gmail.com", "THOpass12");
            Attachment leads = new Attachment(file_name, MediaTypeNames.Application.Octet);

            var email = Email
                .From("tom.thomson.safaris@gmail.com")
                .UseSSL()
                .UsingClient(client)
                .To("tom@thomsonsafaris.com")
                .Subject("Raw leads data file")
                .Body("See attached")
                .Attach(leads)
                .Send();
        }

    }
}
