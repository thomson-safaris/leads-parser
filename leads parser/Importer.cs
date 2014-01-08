using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Data;
using System.IO;
using GenericParsing;
using System.Windows.Forms;

namespace leads_parser
{
    class Importer
    {
        /// <summary>
        ///     Default constructor
        /// </summary>
        public Importer()
        {

        }

        /// <summary>
        ///     Function to get the data that was downloaded from the Newfangled site
        /// </summary>
        /// <returns></returns>
        public DataTable getCSVImport()
        {
            DataTable table = new DataTable();
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Please select a .csv file to import";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (ofd.FileName.Substring(ofd.FileName.Length - 4, 4) == ".csv")
                    {
                        using (GenericParserAdapter parser = new GenericParserAdapter(ofd.FileName))
                        {
                            parser.ColumnDelimiter = ",".ToCharArray()[0];
                            parser.FirstRowHasHeader = true;

                            table = parser.GetDataTable();

                            // send the original data file to me for troubleshooting
                            if (!MainForm.debug)
                            {
                                Emailer em = new Emailer();
                                em.send_original_data_file(ofd.FileName);
                            }
                            MainForm.importFileSelected = true;
                        }
                    }
                }
            }

            return table;
        }


        /// <summary>
        /// Method to get the recipient list
        /// </summary>
        /// <param name="file_name"></param>
        /// <returns></returns>
        public string get_recipient_list(string file_name)
        {
            string path = Directory.GetCurrentDirectory() + "\\" + file_name;
            string recipients = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    recipients = sr.ReadToEnd();
                }
            }
            catch (Exception crap)
            {
                MessageBox.Show("Recipient lists missing or improperly configured.  Use semicolons to separate the emails.\nDo not enter anything except the email addresses and semicolons.  See the developer if you have questions.\n\nError details: " + crap);
                return "";
            }


            return recipients;
        }


    }
}
