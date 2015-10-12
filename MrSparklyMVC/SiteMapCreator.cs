using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace MrSparklyMVC
{
    /// <summary>
    /// This class is used to generate an XML Site Map
    /// </summary>
    public class SiteMapCreator
    {
        List<string> urls;

        //constructor
        public SiteMapCreator()
        {
            urls = new List<string>();
        }

        /// <summary>
        /// creates a new xml sitemap
        /// </summary>
        /// <param name="urlsList">a list of relative urls to be included in the sitemap</param>
        /// <param name="fqdn">the base absolute uri of the site</param>
        /// <returns>XDocument SiteMap</returns>
        public XDocument createSiteMap(List<string> urlsList, string fqdn)
        {
            urls = urlsList;
            string baseurl = fqdn + "{0}";
            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";

            //create the xml sitemap document
            var siteMap = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(ns + "urlset",
                    from u in urls
                    select
                    new XElement(ns + "url",
                        new XElement(ns + "loc", string.Format(baseurl, u)),
                        new XElement(ns + "lastmod", String.Format("{0:yyyy-MM-dd}", DateTime.Now)),
                        new XElement(ns + "changefreq", "always"),
                        new XElement(ns + "priority", "0.5")
                        )));            

            return siteMap;
        }
    }
}