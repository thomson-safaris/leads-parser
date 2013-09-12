using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using GenericParsing;
using System.IO;

namespace leads_parser
{
    public partial class MainForm : Form
    {
        private Exporter exportThis = new Exporter();
        private Parser p = new Parser();
        private Importer importer = new Importer();
        private Emailer emailer = new Emailer();
        private string path = Directory.GetCurrentDirectory() + "\\";
        private string international_file_name = "nd_leads_from_thomson_safaris.txt";
        private string north_american_file_name = "north_american_leads.csv";
        private string international_recipient_list = ""; // specified in external file
        private string north_american_recipient_list = ""; // specified in external file

        public MainForm()
        {
            InitializeComponent();
            
            run_everything();

            /// End of main section
        }
        

        private void run_everything()
        {
            /// Import recipient lists
            international_recipient_list = importer.get_recipient_list("international recipients.txt");
            north_american_recipient_list = importer.get_recipient_list("us-canada recipients.txt");
            if ((international_recipient_list == "") || (north_american_recipient_list == ""))
            {
                parser_completion_status_box.Text = "Recipient list missing or improperly configured.";
                return;
            }

            /// Import leads data
            DataTable table = importer.getCSVImport();
            if (table.Rows.Count == 0)
            {
                parser_completion_status_box.Text = "No file selected.";
                return; 
            }
            DataTable international_table = new DataTable();
            DataTable north_american_table = new DataTable();


            /// Set up the tables
            international_table = table.Clone();
            north_american_table = table.Clone();

            foreach (DataRow r in table.Rows)
            {
                if ((r["Country"].ToString() != "United States") && (r["Country"].ToString() != "Canada"))
                {
                    international_table.ImportRow(r);
                    Console.WriteLine("imported international");
                }
                else if ((r["Country"].ToString() == "United States") || (r["Country"].ToString() == "Canada"))
                {
                    north_american_table.ImportRow(r);
                    Console.WriteLine("imported north american");
                }
                else
                {
                    Console.WriteLine("This does not have a correct country assignment");
                }
            }


            /// Parse the tables
            international_table = p.parse_international(international_table);
            north_american_table = p.parse_north_american(north_american_table, "|");


            /// Export 
            try
            {
                exportThis.toTSV_auto(international_table, path + international_file_name);
                exportThis.toCSV_auto(north_american_table, path + north_american_file_name);
            }
            catch (Exception crap) { MessageBox.Show("Error while exporting: " + crap); }


            /// Send email notifications
            emailer.send_emails(path, international_file_name, international_recipient_list);
            emailer.send_emails(path, north_american_file_name, north_american_recipient_list);

            parser_completion_status_box.Text = "Parsing complete!";
        }

        

    }
}
