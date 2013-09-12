﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace leads_parser
{
    public interface ITemplateRenderer
    {
        string Parse<T>(string template, T model, bool isHtml = true);
    }
}
