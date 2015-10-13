using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MrSparklyMVC
{
    /// <summary>
    /// An attribute to be used to mark methods as pages
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class MVCUrlAttribute: ActionFilterAttribute
    {
        public string Url { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="url"></param>
        public MVCUrlAttribute(string url)
        {
            this.Url = url;
        }
    }
}