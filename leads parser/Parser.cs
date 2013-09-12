using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Data;


namespace leads_parser
{
    class Parser
    {
        bool first = true; 
        /// <summary>
        ///     Default constructor
        /// </summary>
        public Parser()
        {

        }

        /// <summary>
        ///     Method to remove the unused columns from the datafile
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public DataTable remove_columns(DataTable table)
        {
            table.Columns.Remove("Adult Safari");
            table.Columns.Remove("Family Safari");
            table.Columns.Remove("Private Safari");
            table.Columns.Remove("Kilimanjaro Trek");
            table.Columns.Remove("Kilimanjaro Trek and Safari");
            table.Columns.Remove("Private Trek");
            table.Columns.Remove("Receive Our Catalog by Mail");
            table.Columns.Remove("Receive Our Catalog Online");
            //table.Columns.Remove("Column1");
            table.Columns.Remove("popupform");
            table.Columns.Remove("form_id");
            table.Columns.Remove("form_type");
            table.Columns.Remove("status");
            table.Columns.Remove("status_date");
            table.Columns.Remove("session_id");
            table.Columns.Remove("related_content_id");

            return table;
        }

        /// <summary>
        /// Parses the international leads
        /// </summary>
        /// <param name="table"></param>
        /// <returns>
        /// </returns>
        public DataTable parse_international(DataTable table)
        {
            string interests_concatenated;

            table.Columns.Add("Interests", typeof(String));



            foreach (DataRow r in table.Rows)
            {
                //assign interests
                interests_concatenated = "";
                first = true;
                if (r["Adult Safari"].ToString() == "on") { interests_concatenated += adder( "Adult Safaris"); } 
                if (r["Family Safari"].ToString() == "on") { interests_concatenated += adder( "Family Safaris"); } 
                if (r["Private Safari"].ToString() == "on") { interests_concatenated += adder( "Private Safaris"); } 
                if (r["Kilimanjaro Trek"].ToString() == "on") { interests_concatenated += adder( "Kili Treks"); } 
                if (r["Kilimanjaro Trek and Safari"].ToString() == "on") { interests_concatenated += adder( "Kili Safaris"); } 
                if (r["Private Trek"].ToString() == "on") { interests_concatenated += adder( "Private Kili"); }
                r["Interests"] = interests_concatenated;

                // clean text fields
                if (r["Ask a Question or Request a Call"].ToString() == "")
                {
                    string temp_custom_response = r["Ask a Question or Request a Call"].ToString();
                    temp_custom_response = text_field_cleaner(temp_custom_response);
                    r["Ask a Question or Request a Call"] = temp_custom_response;
                }

                // fix typos
                if (r["How did you first hear about us"].ToString() == "AMEX Centurio") { r["How did you first hear about us"] = "AMEX Centurion"; }
            }

            return table;
        }

        /// <summary>
        /// Break the interest selections via vertical tabs 
        /// 
        /// for Nature Discovery (non-north-american) leads only!
        /// </summary>
        /// <param name="add_this"></param>
        /// <returns></returns>
        private string adder(string add_this)
        {
            if (!first)
            {
                return "\v" + add_this;
            }
            else
            {
                first = false;
                return add_this;
            }
        }

        /// <summary>
        /// Get rid of carriage returns and nils in text fields so that they don't disturb the data
        /// </summary>
        /// <param name="text_to_parse"></param>
        /// <returns></returns>
        private string text_field_cleaner(string text_to_parse)
        {
            text_to_parse.Replace("\r", "\v");
            text_to_parse.Replace("\n", "");


            return "";
        }

        /// <summary>
        /// Parses the north american leads
        /// </summary>
        /// <param name="table"></param>
        /// <param name="interests_divider"></param>
        /// <returns></returns>
        public DataTable parse_north_american(DataTable table, string interests_divider)
        {
            string interests_concatenated;
            
            table.Columns.Add("Interests", typeof(String));
            table.Columns.Add("Catalog Preference", typeof(String));
            table.Columns.Add("Confirm Email", typeof(String));


            foreach (DataRow r in table.Rows)
            {
                //assign interests
                interests_concatenated = "";
                if (r["Adult Safari"].ToString() == "on") { interests_concatenated += "Adult Safaris" + interests_divider; } else interests_concatenated += interests_divider;
                if (r["Family Safari"].ToString() == "on") { interests_concatenated += "Family Safaris" + interests_divider; } else interests_concatenated += interests_divider;
                if (r["Private Safari"].ToString() == "on") { interests_concatenated += "Private Safaris" + interests_divider; } else interests_concatenated += interests_divider;
                if (r["Kilimanjaro Trek"].ToString() == "on") { interests_concatenated += "Kili Treks" + interests_divider; } else interests_concatenated += interests_divider;
                if (r["Kilimanjaro Trek and Safari"].ToString() == "on") { interests_concatenated += "Kili Safaris" + interests_divider; } else interests_concatenated += interests_divider;
                if (r["Private Trek"].ToString() == "on") { interests_concatenated += "Private Kili"; }
                r["Interests"] = interests_concatenated;

                if ((r["Receive Our Catalog by Mail"].ToString() == "on") && (r["Receive Our Catalog Online"].ToString() == "on"))
                {
                    r["Catalog Preference"] = "By Mail|Online";
                }
                else if (r["Receive Our Catalog by Mail"].ToString() == "on")
                {
                    r["Catalog Preference"] = "By Mail|";
                }
                else if (r["Receive Our Catalog Online"].ToString() == "on")
                {
                    r["Catalog Preference"] = "|Online";
                }
                else
                {
                    r["Catalog Preference"] = "|";
                }

                if (r["Country"].ToString() == "United States") { r["Country"] = ""; }

                r["Confirm Email"] = r["Email"].ToString(); // duplicating this :(

                // clean text fields
                if (r["If other, please specify"].ToString() == "If other, please specify") { r["If other, please specify"] = ""; }

                // clear empty content
                if (r["Address 1"].ToString() == "Address 1") { r["Address 1"] = ""; }
                if (r["Address 2"].ToString() == "Address 2") { r["Address 2"] = ""; }
                if (r["City"].ToString() == "City") { r["City"] = ""; }
                if (r["Zip/Postal Code"].ToString() == "Zip/Postal Code") { r["Zip/Postal Code"] = ""; }
                if (r["Phone"].ToString() == "Phone") { r["Phone"] = ""; }

                // fix typos
                if (r["How did you first hear about us"].ToString() == "AMEX Centurio") { r["How did you first hear about us"] = "AMEX Centurion"; }
            }


            remove_columns(table);


            return table;
        }




    }
}
