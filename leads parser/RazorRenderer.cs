﻿using System;
using System.Collections.Generic;
//using System.Dynamic;
using System.Linq;
using System.Text;
//using Xipton.Razor;

namespace leads_parser
{
    public class RazorRenderer : ITemplateRenderer
    {
        public RazorRenderer()
        {
        }

        public string Parse<T>(string template, T model, bool isHtml = true)
        {
            //var rm = new RazorMachine(htmlEncode: false);

            //var razorTemplate = rm.ExecuteContent(template, model, null, true);

            //return razorTemplate.Result;
            return "";
        }
    }
}
