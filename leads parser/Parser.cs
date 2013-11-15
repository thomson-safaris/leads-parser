using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                if (r["Ask a Question or Request a Call"].ToString() != "")
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
        
        public DataTable parse_international_kili(DataTable table)
        {
            string interests_concatenated;

            table.Columns.Add("Interests", typeof(String));

            foreach (DataRow r in table.Rows)
            {
                //assign interests
                interests_concatenated = "";
                first = true;
                if (r["Kilimanjaro Trek"].ToString() == "checked") { interests_concatenated += adder("Kili Treks"); }
                if (r["Kilimanjaro Trek and Safari"].ToString() == "checked") { interests_concatenated += adder("Kili Safaris"); }
                if (r["Private Trek"].ToString() == "checked") { interests_concatenated += adder("Private Kili"); }
                if (r["Private Trek and Safari"].ToString() == "checked") { interests_concatenated += adder("Private Kili"); }
                r["Interests"] = interests_concatenated;

                // clean text fields
                // DataRows do not enjoy being handled directly, hence the strange code below
                if (r["Questions and Comments"].ToString() != "")
                {
                    string temp_custom_response = r["Questions and Comments"].ToString();
                    temp_custom_response = text_field_cleaner(temp_custom_response);
                    r["Questions and Comments"] = temp_custom_response;
                }

                if (r["Phone Consult Notes"].ToString() != "")
                {
                    string temp_custom_response = r["Phone Consult Notes"].ToString();
                    temp_custom_response = text_field_cleaner(temp_custom_response);
                    r["Phone Consult Notes"] = temp_custom_response;
                }

                string temp_country = r["Country"].ToString();
                temp_country = CountryAbbrToFull(temp_country);
                r["Country"] = temp_country;

                // fix typos
                if (r["How Did You First Hear About Us?"].ToString() == "AMEX Centurio") { r["How Did You First Hear About Us?"] = "AMEX Centurion"; }
            }

            return table;
        }

        private string CountryAbbrToFull(string country)
        {
            switch (country)
            {
                case "AF":
	                return "Afghanistan";
                case "AL":
	                return "Albania";
                case "DZ":
	                return "Algeria";
                case "AS":
	                return "American Samoa";
                case "AD":
	                return "Andorra";
                case "AO":
	                return "Angola";
                case "AI":
	                return "Anguilla";
                case "AQ":
	                return "Antarctica";
                case "AG":
	                return "Antigua And Barbuda";
                case "AR":
	                return "Argentina";
                case "AM":
	                return "Armenia";
                case "AW":
	                return "Aruba";
                case "AU":
	                return "Australia";
                case "AT":
	                return "Austria";
                case "AZ":
	                return "Azerbaijan";
                case "BS":
	                return "Bahamas";
                case "BH":
	                return "Bahrain";
                case "BD":
	                return "Bangladesh";
                case "BB":
	                return "Barbados";
                case "BY":
	                return "Belarus";
                case "BE":
	                return "Belgium";
                case "BZ":
	                return "Belize";
                case "BJ":
	                return "Benin";
                case "BM":
	                return "Bermuda";
                case "BT":
	                return "Bhutan";
                case "BO":
	                return "Bolivia";
                case "BA":
	                return "Bosnia And Herzegowina";
                case "BW":
	                return "Botswana";
                case "BV":
	                return "Bouvet Island";
                case "BR":
	                return "Brazil";
                case "IO":
	                return "British Indian Ocean Territory";
                case "BN":
	                return "Brunei Darussalam";
                case "BG":
	                return "Bulgaria";
                case "BF":
	                return "Burkina Faso";
                case "BI":
	                return "Burundi";
                case "KH":
	                return "Cambodia";
                case "CM":
	                return "Cameroon";
                case "CA":
	                return "Canada";
                case "CV":
	                return "Cape Verde";
                case "KY":
	                return "Cayman Islands";
                case "CF":
	                return "Central African Republic";
                case "TD":
	                return "Chad";
                case "CL":
	                return "Chile";
                case "CN":
	                return "China";
                case "CX":
	                return "Christmas Island";
                case "CC":
	                return "Cocos (Keeling) Islands";
                case "CO":
	                return "Colombia";
                case "KM":
	                return "Comoros";
                case "CG":
	                return "Congo";
                case "CD":
	                return "Congo, The Democratic Republic Of The";
                case "CK":
	                return "Cook Islands";
                case "CR":
	                return "Costa Rica";
                case "CI":
	                return "Cote D'Ivoire";
                case "HR":
	                return "Croatia (Local Name: Hrvatska)";
                case "CU":
	                return "Cuba";
                case "CY":
	                return "Cyprus";
                case "CZ":
	                return "Czech Republic";
                case "DK":
	                return "Denmark";
                case "DJ":
	                return "Djibouti";
                case "DM":
	                return "Dominica";
                case "DO":
	                return "Dominican Republic";
                case "TL":
	                return "Timor-Leste (East Timor)";
                case "EC":
	                return "Ecuador";
                case "EG":
	                return "Egypt";
                case "SV":
	                return "El Salvador";
                case "GQ":
	                return "Equatorial Guinea";
                case "ER":
	                return "Eritrea";
                case "EE":
	                return "Estonia";
                case "ET":
	                return "Ethiopia";
                case "FK":
	                return "Falkland Islands (Malvinas)";
                case "FO":
	                return "Faroe Islands";
                case "FJ":
	                return "Fiji";
                case "FI":
	                return "Finland";
                case "FR":
	                return "France";
                case "FX":
	                return "France, Metropolitan";
                case "GF":
	                return "French Guiana";
                case "PF":
	                return "French Polynesia";
                case "TF":
	                return "French Southern Territories";
                case "GA":
	                return "Gabon";
                case "GM":
	                return "Gambia";
                case "GE":
	                return "Georgia";
                case "DE":
	                return "Germany";
                case "GH":
	                return "Ghana";
                case "GI":
	                return "Gibraltar";
                case "GR":
	                return "Greece";
                case "GL":
	                return "Greenland";
                case "GD":
	                return "Grenada";
                case "GP":
	                return "Guadeloupe";
                case "GU":
	                return "Guam";
                case "GT":
	                return "Guatemala";
                case "GN":
	                return "Guinea";
                case "GW":
	                return "Guinea-Bissau";
                case "GY":
	                return "Guyana";
                case "HT":
	                return "Haiti";
                case "HM":
	                return "Heard And Mc Donald Islands";
                case "VA":
	                return "Holy See (Vatican City State)";
                case "HN":
	                return "Honduras";
                case "HK":
	                return "Hong Kong";
                case "HU":
	                return "Hungary";
                case "IS":
	                return "Iceland";
                case "IN":
	                return "India";
                case "ID":
	                return "Indonesia";
                case "IR":
	                return "Iran (Islamic Republic Of)";
                case "IQ":
	                return "Iraq";
                case "IE":
	                return "Ireland";
                case "IL":
	                return "Israel";
                case "IT":
	                return "Italy";
                case "JM":
	                return "Jamaica";
                case "JP":
	                return "Japan";
                case "JO":
	                return "Jordan";
                case "KZ":
	                return "Kazakhstan";
                case "KE":
	                return "Kenya";
                case "KI":
	                return "Kiribati";
                case "KP":
	                return "Korea, Democratic People's Republic Of";
                case "KR":
	                return "Korea, Republic Of";
                case "KW":
	                return "Kuwait";
                case "KG":
	                return "Kyrgyzstan";
                case "LA":
	                return "Lao People's Democratic Republic";
                case "LV":
	                return "Latvia";
                case "LB":
	                return "Lebanon";
                case "LS":
	                return "Lesotho";
                case "LR":
	                return "Liberia";
                case "LY":
	                return "Libyan Arab Jamahiriya";
                case "LI":
	                return "Liechtenstein";
                case "LT":
	                return "Lithuania";
                case "LU":
	                return "Luxembourg";
                case "MO":
	                return "Macau";
                case "MK":
	                return "Macedonia, Former Yugoslav Republic Of";
                case "MG":
	                return "Madagascar";
                case "MW":
	                return "Malawi";
                case "MY":
	                return "Malaysia";
                case "MV":
	                return "Maldives";
                case "ML":
	                return "Mali";
                case "MT":
	                return "Malta";
                case "MH":
	                return "Marshall Islands";
                case "MQ":
	                return "Martinique";
                case "MR":
	                return "Mauritania";
                case "MU":
	                return "Mauritius";
                case "YT":
	                return "Mayotte";
                case "MX":
	                return "Mexico";
                case "FM":
	                return "Micronesia, Federated States Of";
                case "MD":
	                return "Moldova, Republic Of";
                case "MC":
	                return "Monaco";
                case "MN":
	                return "Mongolia";
                case "ME":
	                return "Montenegro";
                case "MS":
	                return "Montserrat";
                case "MA":
	                return "Morocco";
                case "MZ":
	                return "Mozambique";
                case "MM":
	                return "Myanmar";
                case "NA":
	                return "Namibia";
                case "NR":
	                return "Nauru";
                case "NP":
	                return "Nepal";
                case "NL":
	                return "Netherlands";
                case "AN":
	                return "Netherlands Antilles";
                case "NC":
	                return "New Caledonia";
                case "NZ":
	                return "New Zealand";
                case "NI":
	                return "Nicaragua";
                case "NE":
	                return "Niger";
                case "NG":
	                return "Nigeria";
                case "NU":
	                return "Niue";
                case "NF":
	                return "Norfolk Island";
                case "MP":
	                return "Northern Mariana Islands";
                case "NO":
	                return "Norway";
                case "OM":
	                return "Oman";
                case "PK":
	                return "Pakistan";
                case "PW":
	                return "Palau";
                case "PA":
	                return "Panama";
                case "PG":
	                return "Papua New Guinea";
                case "PY":
	                return "Paraguay";
                case "PE":
	                return "Peru";
                case "PH":
	                return "Philippines";
                case "PN":
	                return "Pitcairn";
                case "PL":
	                return "Poland";
                case "PT":
	                return "Portugal";
                case "PR":
	                return "Puerto Rico";
                case "QA":
	                return "Qatar";
                case "RE":
	                return "Reunion";
                case "RO":
	                return "Romania";
                case "RU":
	                return "Russian Federation";
                case "RW":
	                return "Rwanda";
                case "KN":
	                return "Saint Kitts And Nevis";
                case "LC":
	                return "Saint Lucia";
                case "VC":
	                return "Saint Vincent And The Grenadines";
                case "WS":
	                return "Samoa";
                case "SM":
	                return "San Marino";
                case "ST":
	                return "Sao Tome And Principe";
                case "SA":
	                return "Saudi Arabia";
                case "SN":
	                return "Senegal";
                case "SR":
	                return "Serbia";
                case "SC":
	                return "Seychelles";
                case "SL":
	                return "Sierra Leone";
                case "SG":
	                return "Singapore";
                case "SK":
	                return "Slovakia (Slovak Republic)";
                case "SI":
	                return "Slovenia";
                case "SB":
	                return "Solomon Islands";
                case "SO":
	                return "Somalia";
                case "ZA":
	                return "South Africa";
                case "GS":
	                return "South Georgia, South Sandwich Islands";
                case "ES":
	                return "Spain";
                case "LK":
	                return "Sri Lanka";
                case "SH":
	                return "St. Helena";
                case "PM":
	                return "St. Pierre And Miquelon";
                case "SD":
	                return "Sudan";
                case "SJ":
	                return "Svalbard And Jan Mayen Islands";
                case "SZ":
	                return "Swaziland";
                case "SE":
	                return "Sweden";
                case "CH":
	                return "Switzerland";
                case "SY":
	                return "Syrian Arab Republic";
                case "TW":
	                return "Taiwan";
                case "TJ":
	                return "Tajikistan";
                case "TZ":
	                return "Tanzania, United Republic Of";
                case "TH":
	                return "Thailand";
                case "TG":
	                return "Togo";
                case "TK":
	                return "Tokelau";
                case "TO":
	                return "Tonga";
                case "TT":
	                return "Trinidad And Tobago";
                case "TN":
	                return "Tunisia";
                case "TR":
	                return "Turkey";
                case "TM":
	                return "Turkmenistan";
                case "TC":
	                return "Turks And Caicos Islands";
                case "TV":
	                return "Tuvalu";
                case "UG":
	                return "Uganda";
                case "UA":
	                return "Ukraine";
                case "AE":
	                return "United Arab Emirates";
                case "GB":
	                return "United Kingdom";
                case "US":
                    return "United States";
                case "UM":
	                return "United States Minor Outlying Islands";
                case "UY":
	                return "Uruguay";
                case "UZ":
	                return "Uzbekistan";
                case "VU":
	                return "Vanuatu";
                case "VE":
	                return "Venezuela";
                case "VN":
	                return "Viet Nam";
                case "VG":
	                return "Virgin Islands (British)";
                case "VI":
	                return "Virgin Islands (U.S.)";
                case "WF":
	                return "Wallis And Futuna Islands";
                case "EH":
	                return "Western Sahara";
                case "YE":
	                return "Yemen";
                case "YU":
	                return "Yugoslavia";
                case "ZM":
	                return "Zambia";
                case "ZW":
	                return "Zimbabwe";
                default:
                    return "";
            }
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


            return text_to_parse;
        }

        private string text_field_cleaner_US(string text_to_parse)
        {
            text_to_parse = text_to_parse.Replace("\r", "");
            text_to_parse = text_to_parse.Replace("\n", "");


            return text_to_parse;
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

        public DataTable parse_north_american_kili(DataTable table, string interests_divider)
        {
            string interests_concatenated;

            table.Columns.Add("Interests", typeof(String));

            foreach (DataRow r in table.Rows)
            {
                //assign interests
                interests_concatenated = "";
                first = true;
                if (r["Kilimanjaro Trek"].ToString() == "checked") { interests_concatenated += adder("Kili Treks"); }
                if (r["Kilimanjaro Trek and Safari"].ToString() == "checked") { interests_concatenated += adder("Kili Safaris"); }
                if (r["Private Trek"].ToString() == "checked") { interests_concatenated += adder("Private Kili"); }
                if (r["Private Trek and Safari"].ToString() == "checked") { interests_concatenated += adder("Private Kili"); }
                r["Interests"] = interests_concatenated;

                //interests_concatenated = "";
                //if (r["Kilimanjaro Trek"].ToString() == "checked") { interests_concatenated += "Kili Treks" + interests_divider; } else interests_concatenated += interests_divider;
                //if (r["Kilimanjaro Trek and Safari"].ToString() == "checked") { interests_concatenated += "Kili Safaris" + interests_divider; } else interests_concatenated += interests_divider;
                //if (r["Private Trek"].ToString() == "checked") { interests_concatenated += "Private Kili" + interests_divider; } else interests_concatenated += interests_divider;
                //if (r["Private Trek and Safari"].ToString() == "checked") { interests_concatenated += "Private Kili"; }
                //r["Interests"] = interests_concatenated;

                if (r["Country"].ToString() == "United States") { r["Country"] = ""; }

                // clear empty content
                if (r["Address 1"].ToString() == "Address 1") { r["Address 1"] = ""; }
                if (r["Address 2"].ToString() == "Address 2") { r["Address 2"] = ""; }
                if (r["City"].ToString() == "City") { r["City"] = ""; }
                if (r["Zip / Post Code"].ToString() == "Zip / Post Code") { r["Zip / Post Code"] = ""; }
                if (r["Day Phone"].ToString() == "Day Phone") { r["Day Phone"] = ""; }

                // clean text fields
                if (r["Questions and Comments"].ToString() != "")
                {
                    string temp_custom_response = r["Questions and Comments"].ToString();
                    temp_custom_response = text_field_cleaner_US(temp_custom_response);
                    r["Questions and Comments"] = temp_custom_response;
                }

                if (r["Phone Consult Notes"].ToString() != "")
                {
                    string temp_custom_response = r["Phone Consult Notes"].ToString();
                    temp_custom_response = text_field_cleaner_US(temp_custom_response);
                    r["Phone Consult Notes"] = temp_custom_response;
                }

                // fix typos
                if (r["How Did You First Hear About Us?"].ToString() == "AMEX Centurio") { r["How Did You First Hear About Us?"] = "AMEX Centurion"; }
            }

            return table;
        }



    }
}
