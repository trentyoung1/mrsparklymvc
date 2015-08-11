using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MrSparklyMVC
{
    public class MyViewEngine : RazorViewEngine
    {
        public MyViewEngine()
        {
            var newLocationFormat = new[]
                                    {
                                        "~/Views/Shared/{1}/{0}.cshtml",
                                    };

            PartialViewLocationFormats = PartialViewLocationFormats.Union(newLocationFormat).ToArray();
        }
    }
}