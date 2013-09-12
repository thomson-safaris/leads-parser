using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace leads_parser
{
    class Exporter
    {
        public Exporter()
        {
            // nothing to see here
        }

        /// <summary>
        ///     Export the provided table to a tsv file of the user's choosing
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool toTSV(DataTable table)
        {
            StringBuilder sb = new StringBuilder();
            SaveFileDialog fileName = new SaveFileDialog();
            string fileType;
            bool successfulExport;

            GetTableDataForTSV(sb, table);
            fileType = "tsv";
            fileName = SaveFileAs(fileType);

            successfulExport = WriteToMyFile(fileName, sb);

            if (successfulExport) return true;
            else return false;
        }

        /// <summary>
        ///     Export the provided table to a csv file of the user's choosing
        /// </summary>
        /// <param name="path"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool toCSV(DataTable table)
        {
            StringBuilder sb = new StringBuilder();
            SaveFileDialog fileName = new SaveFileDialog();
            string fileType;
            bool successfulExport;

            GetColumnHeadersForCSV(sb, table);
            GetTableDataForCSV(sb, table);
            fileType = "csv";
            fileName = SaveFileAs(fileType);
            successfulExport = WriteToMyFile(fileName, sb);

            if (successfulExport) return true;
            else return false;
        }

        /// <summary>
        /// Write to a tsv file without prompt
        /// Specify file to write to in path
        /// </summary>
        /// <param name="table"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool toTSV_auto(DataTable table, string path)
        {
            StringBuilder sb = new StringBuilder();
            bool successfulExport;

            GetTableDataForTSV(sb, table);
            successfulExport = write_without_dialog(path, sb);

            if (successfulExport) return true;
            else return false;
        }


        /// <summary>
        /// Write to a csv file without prompt
        /// Specify file to write to in path
        /// </summary>
        /// <param name="table"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool toCSV_auto(DataTable table, string path)
        {
            StringBuilder sb = new StringBuilder();
            bool successfulExport;

            /// Chris does not want to use column headers for the import into FMP (as of 2/20/13)
            //GetColumnHeadersForCSV(sb, table);
            GetTableDataForCSV(sb, table);
            successfulExport = write_without_dialog(path, sb);

            if (successfulExport) return true;
            else return false;
        }

        /// <summary>
        ///     GET COLUMN HEADERS FOR CSV    
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="table"></param>
        /// <returns>
        ///     Returns the column headers from the provided table in the provided stringbuilder
        ///     Returns the data in CSV format
        /// </returns>
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

        /// <summary>
        ///     GET TABLE DATA FOR CSV
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="table"></param>
        /// <returns>
        ///     Returns the table data from the provided table in the provided stringbuilder
        ///     Returns the data in CSV format
        /// </returns>
        private StringBuilder GetTableDataForCSV(StringBuilder sb, DataTable table)
        {
            foreach (DataRow r in table.Rows)
            {
                sb.Append("\"");
                sb.Append(wrap_in_quotes(r["ip"].ToString()));
                sb.Append(wrap_in_quotes(r["date_received"].ToString()));
                sb.Append(wrap_in_quotes(r["First Name"].ToString()));
                sb.Append(wrap_in_quotes(r["Last Name"].ToString()));
                sb.Append(wrap_in_quotes(r["Address 1"].ToString()));
                sb.Append(wrap_in_quotes(r["Address 2"].ToString()));
                sb.Append(wrap_in_quotes(r["City"].ToString()));
                sb.Append(wrap_in_quotes(r["State/Province"].ToString()));
                sb.Append(wrap_in_quotes(r["Zip/Postal Code"].ToString()));
                sb.Append(wrap_in_quotes(r["Country"].ToString()));
                sb.Append(wrap_in_quotes(r["Phone"].ToString()));
                sb.Append(wrap_in_quotes(r["Email"].ToString()));
                //sb.Append(wrap_in_quotes(r["Confirm Email"].ToString()));
                sb.Append(wrap_in_quotes(r["Travel Date Month"].ToString()));
                sb.Append(wrap_in_quotes(r["Travel Date Year"].ToString()));
                sb.Append(wrap_in_quotes(r["My Group"].ToString()));
                sb.Append(wrap_in_quotes(r["Group Size"].ToString()));
                sb.Append(wrap_in_quotes(r["How did you first hear about us"].ToString()));
                sb.Append(wrap_in_quotes(r["If other, please specify"].ToString()));
                sb.Append(wrap_in_quotes(r["Ask a Question or Request a Call"].ToString()));
                sb.Append("\",\""); // blank column....
                sb.Append(wrap_in_quotes(r["Interests"].ToString()));
                sb.Append(wrap_in_quotes(r["Catalog Preference"].ToString()));

                /*
                 * Have to comment this out because Chris doesn't use labeled columns... changing columns is insane
                 * 
                 * 
                bool firstColumn = true;
                foreach (DataColumn c in table.Columns)
                {
                    if (firstColumn)
                    {
                        firstColumn = false;
                        sb.Append("\"" + r[c].ToString());
                    }
                    else
                    {
                        sb.Append(@""",""" + r[c].ToString());
                    }
                }
                 * 
                 */
                sb.Append("\"\r\n");
            }
            return sb;
        }

        /// <summary>
        /// wrap it
        /// </summary>
        private string wrap_in_quotes(string text_to_wrap)
        {
            return text_to_wrap + @""",""";
        }

        /// <summary>
        ///     GET TABLE DATA FOR TSV
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="table"></param>
        /// <returns>
        ///     Returns the table data from the provided table in the provided stringbuilder
        ///     Returns the data in TSV format
        /// </returns>
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

        /// <summary>
        ///     SAVE FILE AS
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns>
        ///     Returns a SaveFileDialog with the selected filename to save as
        /// </returns>
        private SaveFileDialog SaveFileAs(string fileType)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = String.Format("All files (*.*)|*.*|{0} files (*.{0})|*.{0}", fileType);
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            return saveFileDialog1;
        }

        /// <summary>
        /// Write to a file without opening a dialog box
        /// Must specify full path to write to
        /// </summary>
        /// <param name="path"></param>
        /// <param name="sb"></param>
        /// <returns></returns>
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


        /// <summary>
        ///     WRITE TO MY FILE
        /// </summary>
        /// <param name="saveFileDialog1"></param>
        /// <param name="sb"></param>
        /// <returns>
        ///     Returns nothing, only writes a stringbuilder to a file
        /// </returns>
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

    }
}
