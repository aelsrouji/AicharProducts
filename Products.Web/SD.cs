﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.Web
{
    public static class SD
    {
        public static string ProductAPIBase { get; set; }
        public enum ApiType
        {
            Get,
            Post,
            Put,
            Delete
        }
    }
}
