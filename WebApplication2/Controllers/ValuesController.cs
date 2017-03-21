using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using HtmlAgilityPack;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ValuesController : ApiController
    {


        #region XHTML Scrapper
        /// <summary>
        /// Scraps heading from Google.com with respect to a parameter
        /// </summary>
        /// <param name="start"></param>
        /// <param name="query">
        /// Here query is your respective search query of Google
        /// </param>
        /// <returns>
        /// List<string/> | Scrapped Headings
        /// </returns>
        internal static IEnumerable<ScrapModel> ScrapHtml(int start, string query)
        {
            string url = $"{EndPoints.BasicUrlGoogle}{query} &start={start}";
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var headingList = doc.DocumentNode.SelectNodes("//h3").Select(link => link.InnerText).ToList();
            var descriptionList = doc.DocumentNode.SelectNodes("//span[@class='st']").Select(link => link.InnerText).ToList();
            var linksList = doc.DocumentNode.SelectNodes("//h3//a").Select(link => link.Attributes["href"]).ToList();
            
            return descriptionList.Select((t, i) => new ScrapModel {Title = headingList.ElementAt(i), Description = descriptionList.ElementAt(i), Url = linksList.ElementAt(i).Value}).ToList();
        }
        #endregion
        // GET api/values
        /// <summary>
        /// GET request
        /// </summary>
        /// <param name="q">
        /// Google Search Query
        /// </param>
        /// <param name="start"></param>
        /// <returns>List of Items Scrapped</returns>
        public IEnumerable<ScrapModel> Get([FromUri] string q, [FromUri] int start)
        {
            return ScrapHtml(start, q);
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
