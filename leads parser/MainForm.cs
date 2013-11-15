using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GenericParsing;
using System.IO;

namespace leads_parser
{
    public partial class MainForm : Form
    {
        private Exporter exporter = new Exporter();
        private Parser parser = new Parser();
        private Importer importer = new Importer();
        private Emailer emailer = new Emailer();
        private string path = Directory.GetCurrentDirectory() + "\\";
        private string us_filename;
        private string nd_filename;
        public const string SAFARI_INTL_FILE = "nd_leads_from_thomson_safaris.txt";
        public const string SAFARI_US_FILE = "north_american_leads.csv";
        public const string KILI_INTL_FILE = "kili_nd_leads.txt";
        public const string KILI_US_FILE = "kili_north_american_leads.tab";
        private string international_recipient_list = ""; // specified in external file
        private string north_american_recipient_list = ""; // specified in external file
        private DataTable table = new DataTable();
        private DataTable international_table = new DataTable();
        private DataTable north_american_table = new DataTable();

        /// Debug flag for sending emails! Set to false for final testing & production.
        public static bool debug = false;

        public MainForm()
        {
            InitializeComponent();
            InitializeTheAwesome();

            safarisLeadsButton.Click += new System.EventHandler(parse_safari_leads);
            treksLeadsButton.Click += new System.EventHandler(parse_treks_leads);

        }   /// End of main section

        private void InitializeTheAwesome()
        {
            international_recipient_list = importer.get_recipient_list("international recipients.txt");
            north_american_recipient_list = importer.get_recipient_list("us-canada recipients.txt");
        }

        private bool CheckRecipientLists()
        {
            if ((international_recipient_list == "") || (north_american_recipient_list == ""))
            {
                parser_completion_status_box.Text = "Recipient list missing or improperly configured.";
                return false;
            }
            return true;
        }

        private bool ImportLeads()
        {
            table = importer.getCSVImport();
            if (table.Rows.Count == 0)
            {
                parser_completion_status_box.Text = "No file selected.";
                return false;
            }
            international_table = table.Clone();
            north_american_table = table.Clone();
            return true;
        }

        private void SplitBasedOnCountry()
        {
            foreach (DataRow r in table.Rows)
            {
                if ((r["Country"].ToString() != "US") && (r["Country"].ToString() != "United States") && (r["Country"].ToString() != "Canada") && (r["Country"].ToString() != "CA"))
                {
                    international_table.ImportRow(r);
                    // note that this could be any text at all
                    // ie this is not checking data validity
                    Console.WriteLine("imported international");
                }
                else if ((r["Country"].ToString() == "US") || (r["Country"].ToString() == "United States") || (r["Country"].ToString() == "Canada") || (r["Country"].ToString() == "CA"))
                {
                    if (r["Country"].ToString() == "US")
                    {
                        r["Country"] = "United States";
                    }
                    if (r["Country"].ToString() == "CA")
                    {
                        r["Country"] = "Canada";
                    }
                    north_american_table.ImportRow(r);
                    Console.WriteLine("imported north american");
                }
            }
        }

        private void ParseLeads(string lead_type)
        {
            string site_name;

            if (!CheckRecipientLists()) return;
            if (!ImportLeads()) return;

            SplitBasedOnCountry();

            switch (lead_type)
            {
                case "safari":
                    site_name = "thomsonsafaris.com";
                    us_filename = SAFARI_US_FILE;
                    nd_filename = SAFARI_INTL_FILE;
                    international_table = parser.parse_international(international_table);
                    north_american_table = parser.parse_north_american(north_american_table, "|");
                    break;

                case "kili":
                    site_name = "thomsontreks.com";
                    us_filename = KILI_US_FILE;
                    nd_filename = KILI_INTL_FILE;
                    international_table = parser.parse_international_kili(international_table);
                    north_american_table = parser.parse_north_american_kili(north_american_table, "|");
                    break;

                default:
                    Console.WriteLine("You dun messed up");
                    return;
            }

            try
            {
                exporter.AutoExport(north_american_table, path, us_filename);
                exporter.AutoExport(international_table, path, nd_filename);
            }
            catch (Exception crap) { MessageBox.Show("Error while exporting: " + crap); }
                        
            if (!debug)
            {
                emailer.send_emails(path, nd_filename, international_recipient_list, site_name);
                emailer.send_emails(path, us_filename, north_american_recipient_list, site_name);
            }
            parser_completion_status_box.Text = "Parsing complete!";
        }

        private void parse_safari_leads(object sender, System.EventArgs e)
        {
            ParseLeads("safari");
        }

        private void parse_treks_leads(object sender, System.EventArgs e)
        {
            ParseLeads("kili");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }


    }
}
