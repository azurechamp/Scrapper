using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using HtmlAgilityPack;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class TechCrunchController : ApiController
    {

        #region XHTML Scrapper

        /// <summary>
        /// Scraps heading from Google.com with respect to a parameter
        /// </summary>
        /// <returns>
        /// List<string/> | Scrapped Headings
        /// </returns>
        internal static IEnumerable<ScrapModel> ScrapHtml()
        {
            const string url = "http://www.theverge.com/tech";
            var web = new HtmlWeb();
            var doc = web.Load(url);
            var headingList = doc.DocumentNode.SelectNodes("//h2[@class='c-entry-box--compact__title']//a").Select(link => link.InnerText).ToList();
            var linksList = doc.DocumentNode.SelectNodes("//h2[@class='c-entry-box--compact__title']//a").Select(link => link.Attributes["href"]).ToList();

            return headingList.Select((t, i) => new ScrapModel { Title = headingList.ElementAt(i), Url = linksList.ElementAt(i).Value }).ToList();
        }
        #endregion
        // GET api/values
        /// <summary>
        /// GET request
        /// </summary>
        /// <returns>List of Items Scrapped</returns>
        public IEnumerable<ScrapModel> Get()
        {
            return ScrapHtml();
        }

    }
}
