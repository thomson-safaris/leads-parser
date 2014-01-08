using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace leads_parser
{
    class Exporter
    {
        public Exporter()
        {
        }

        public bool AutoExport(DataTable table, string path, string filename)
        {
            StringBuilder sb = new StringBuilder();

            switch (filename)
            {
                case MainForm.KILI_US_FILE:
                    GetTableDataForTSV_Kili_US(sb, table);
                    break;
                case MainForm.KILI_INTL_FILE:
                    GetTableDataForTSV_Kili_ND(sb, table);
                    break;
                case MainForm.SAFARI_US_FILE:
                    GetTableDataForCSV(sb, table);
                    break;
                case MainForm.SAFARI_INTL_FILE:
                    GetTableDataForTSV(sb, table);
                    break;
                default:
                    break;
            }

            path += filename;
            if (write_without_dialog(path, sb)) return true;
            else return false;
        }

        private StringBuilder GetTableDataForCSV(StringBuilder sb, DataTable table)
        {
            foreach (DataRow r in table.Rows)
            {
                sb.Append("\"");
                sb.Append(comma_wrap_in_quotes(r["ip"].ToString()));
                sb.Append(comma_wrap_in_quotes(r["date_received"].ToString()));
                sb.Append(comma_wrap_in_quotes(r["First Name"].ToString()));
                sb.Append(comma_wrap_in_quotes(r["Last Name"].ToString()));
                sb.Append(comma_wrap_in_quotes(r["Address 1"].ToString()));
                sb.Append(comma_wrap_in_quotes(r["Address 2"].ToString()));
                sb.Append(comma_wrap_in_quotes(r["City"].ToString()));
                sb.Append(comma_wrap_in_quotes(r["State/Province"].ToString()));
                sb.Append(comma_wrap_in_quotes(r["Zip/Postal Code"].ToString()));
                sb.Append(comma_wrap_in_quotes(r["Country"].ToString()));
                sb.Append(comma_wrap_in_quotes(r["Phone"].ToString()));
                sb.Append(comma_wrap_in_quotes(r["Email"].ToString()));
                sb.Append(comma_wrap_in_quotes(r["Travel Date Month"].ToString()));
                sb.Append(comma_wrap_in_quotes(r["Travel Date Year"].ToString()));
                sb.Append(comma_wrap_in_quotes(r["My Group"].ToString()));
                sb.Append(comma_wrap_in_quotes(r["Group Size"].ToString()));
                sb.Append(comma_wrap_in_quotes(r["How did you first hear about us"].ToString()));
                sb.Append(comma_wrap_in_quotes(r["If other, please specify"].ToString()));
                sb.Append(comma_wrap_in_quotes(r["Ask a Question or Request a Call"].ToString()));
                sb.Append("\",\""); // blank column....
                sb.Append(comma_wrap_in_quotes(r["Interests"].ToString()));
                sb.Append(comma_wrap_in_quotes(r["Catalog Preference"].ToString()));
                // add UUID
                //sb.Append(comma_wrap_in_quotes(r["get_by"].ToString())); // This is the UUID
                sb.Append("\"\r\n");
            }
            return sb;
        }

        private StringBuilder GetTableDataForTSV_Kili_US(StringBuilder sb, DataTable table)
        {
            foreach (DataRow r in table.Rows)
            {
                /* Domestic Fields */
                sb.Append("");
                sb.Append(tab_wrap_in_quotes(r["First Name"].ToString()));              // 1. First Name
                sb.Append(tab_wrap_in_quotes(r["Last Name"].ToString()));               // 2. Last Name
                sb.Append(tab_wrap_in_quotes(r["Address 1"].ToString()));               // 3. Address
                sb.Append(tab_wrap_in_quotes(r["Day Phone"].ToString()));               // 4. Home phone
                sb.Append(tab_wrap_in_quotes(r["Address 2"].ToString()));               // 5. Address 2
                sb.Append(tab_wrap_in_quotes(r["City"].ToString()));                    // 6. City
                sb.Append(tab_wrap_in_quotes(r["State/Province"].ToString()));                   // 7. State
                sb.Append(tab_wrap_in_quotes(r["Zip/Postal Code"].ToString()));         // 8. ZIP
                sb.Append(tab_wrap_in_quotes(r["Country"].ToString()));                 // 9. Country
                sb.Append(tab_wrap_in_quotes(r["How did you first hear about us?"].ToString())); // 10. SourceCode
                sb.Append("\t");                                                    // 11. blank column - OptIn
                sb.Append(tab_wrap_in_quotes(r["Interests"].ToString()));               // 12. TourInfo
                sb.Append(tab_wrap_in_quotes(r["My Group"].ToString()));                // 13. ClientType
                //sb.Append(tab_wrap_in_quotes(r["Questions and Comments"].ToString()));  // 14. Comments
                sb.Append("\t");                                                    // 14. no longer separate comments area
                sb.Append(tab_wrap_in_quotes(r["Email"].ToString()));                   // 15. Email
                sb.Append(tab_wrap_in_quotes(r["Group Size"].ToString()));              // 16. Travelers
                sb.Append("\t");                                                    // 17. blank column - Travel Month
                sb.Append(tab_wrap_in_quotes(r["Travel Year"].ToString()));             // 18. Travel Year
                sb.Append(tab_wrap_in_quotes(r["Phone Consult Notes"].ToString()));     // 19. Consultation
                sb.Append("Kilimanjaro Treks");
                sb.Append("\n");
            }
            return sb;
        }

        private StringBuilder GetTableDataForTSV_Kili_ND(StringBuilder sb, DataTable table)
        {
            foreach (DataRow r in table.Rows)
            {
                sb.Append(r["First Name"].ToString() + "\t");
                sb.Append(r["Last Name"].ToString() + "\t");
                sb.Append(r["Email"].ToString() + "\t");
                sb.Append(r["Country"].ToString() + "\t");
                sb.Append("\t");                                        // blank column - Travel Month
                sb.Append(r["Travel Year"].ToString() + "\t");
                sb.Append(r["Group Size"].ToString() + "\t");
                sb.Append(r["Interests"].ToString() + "\t");
                sb.Append(r["My Group"].ToString() + "\t");                 // client type
                sb.Append(r["How did you first hear about us?"].ToString() + "\t"); // source code
                sb.Append(r["Phone Consult Notes"].ToString() + "\r\n"); // comments
            }
            return sb;
        }

        private string comma_wrap_in_quotes(string text_to_wrap)
        {
            return text_to_wrap + @""",""";
        }

        private string tab_wrap_in_quotes(string text_to_wrap)
        {
            // Just kidding don't wrap it in quotes
            return text_to_wrap + "\t";
        }

        private StringBuilder GetTableDataForTSV(StringBuilder sb, DataTable table)
        {
            foreach (DataRow r in table.Rows)
            {
                sb.Append(r["First Name"].ToString() + "\t");
                sb.Append(r["Last Name"].ToString() + "\t");
                sb.Append(r["Email"].ToString() + "\t");
                sb.Append(r["Country"].ToString() + "\t");
                sb.Append(r["Travel Date Month"].ToString() + "\t");
                sb.Append(r["Travel Date Year"].ToString() + "\t");
                sb.Append(r["Group Size"].ToString() + "\t");
                sb.Append(r["Interests"].ToString() + "\t");
                sb.Append(r["My Group"].ToString() + "\t");
                sb.Append(r["How did you first hear about us"].ToString() + "\t");
                sb.Append(r["Ask a Question or Request a Call"].ToString() + "\r\n");
            }
            return sb;
        }

        private bool write_without_dialog(string path, StringBuilder sb)
        {
            try
            {
            using (StreamWriter outfile = new StreamWriter(path))
            {
                outfile.Write(sb.ToString());
            }
            }
            catch (Exception crap)
            {
                Console.WriteLine("Export failed: " + crap);
                return false;
            }
            return true;
        }

        //  --------------------
        /// Unused methods below
        //  --------------------

        private bool WriteToMyFile(SaveFileDialog saveFileDialog1, StringBuilder sb)
        {
            Stream myStream;
            try
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        System.Text.ASCIIEncoding encoder = new ASCIIEncoding();
                        myStream.Write(encoder.GetBytes(sb.ToString()), 0, sb.Length);
                        myStream.Close();
                    }
                    else return false;
                }
                else return false;
            }
            catch (Exception crap)
            {
                Console.WriteLine("Export failed: " + crap);
                return false;
            }
            return true;
        }

        private StringBuilder GetColumnHeadersForCSV(StringBuilder sb, DataTable table)
        {
            bool firstCol = true;
            foreach (DataColumn c in table.Columns)
            {
                if (firstCol)
                {
                    firstCol = false;
                    sb.Append("\"" + c.ColumnName.ToString());
                }
                else
                {
                    sb.Append(@""",""" + c.ColumnName.ToString());
                }
            }
            sb.Append("\"\r\n");
            return sb;
        }
       
        private SaveFileDialog SaveFileAs(string fileType)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = String.Format("All files (*.*)|*.*|{0} files (*.{0})|*.{0}", fileType);
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            return saveFileDialog1;
        }

    }
}
