using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Xml;

namespace WeatherAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ValuesController : ApiController
    {
        //method to get windchill calculation from smhi and return text
        public static string GetWindChill()
        {
            var url = "http://www8.tfe.umu.se/vadertjanst/service1.asmx/calculateWindChill?windSpeed=30&tempC=10";

            var syncClient = new WebClient();
            var content = syncClient.DownloadString(url);
            Console.WriteLine(content);
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(content);
            //get the return value from the xml
            XmlNodeList nodeList = xmlDoc.GetElementsByTagName("string");
            string windChill = string.Empty;
            foreach (XmlNode node in nodeList)
            {
                windChill = node.InnerText;
            }
            Console.WriteLine(windChill);
            return windChill;

        }
        
        // GET api/values/
        public string Get()
        {
            return GetWindChill();
        }
    }
}
