using MvcApplication1.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Linq;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index()
        {
            return View();
        }



        public ActionResult JsonSerialization()
        {
            string url = "http://api.openweathermap.org/data/2.5/weather?q=London&APPID=201fb22425c00d3f2ebc54bdf9a5c6bc";
            using (HttpClient client = new HttpClient())
            {

                var response = client.GetAsync(url).Result;
                var json = response.Content.ReadAsStringAsync().Result;
                OpenWeatherResponse lWeather = new JavaScriptSerializer().Deserialize<OpenWeatherResponse>(json);
                
                StringBuilder sb = new StringBuilder();
                sb.Append("Coordinates: Latitude = ");
                sb.Append(lWeather.coord.lat);
                sb.Append(", Longitude=");
                sb.Append(lWeather.coord.lon);
                sb.Append("<br />");
                sb.Append("Weather: Id =");
                sb.Append(lWeather.weather[0].id);
                sb.Append(", Main =");
                sb.Append(lWeather.weather[0].main);
                sb.Append(", Description =");
                sb.Append(lWeather.weather[0].description);
                sb.Append(", Icons =");
                sb.Append(lWeather.weather[0].icon);
                sb.Append("<br />");

                sb.Append("Main: temp =");
                sb.Append(lWeather.main.temp);
                sb.Append(", feels_like =");
                sb.Append(lWeather.main.feels_like);
                sb.Append(", temp_min =");
                sb.Append(lWeather.main.temp_min);
                sb.Append(", temp_max =");
                sb.Append(lWeather.main.temp_max);
                sb.Append(", pressure =");
                sb.Append(lWeather.main.pressure);
                sb.Append(", humidity =");
                sb.Append(lWeather.main.humidity);
                sb.Append("<br />");

                sb.Append("visibility =");
                sb.Append(lWeather.visibility);
                sb.Append("<br />");

                sb.Append("Wind: speed =");
                sb.Append(lWeather.wind.speed);
                sb.Append(", deg =");
                sb.Append(lWeather.wind.deg);
                sb.Append("<br />");


                sb.Append("Clouds: all =");
                sb.Append(lWeather.clouds.all);
                sb.Append("<br />");

                sb.Append("dt:  =");
                sb.Append(lWeather.dt);
                sb.Append("<br />");

                sb.Append("sys: type =");
                sb.Append(lWeather.sys.type);
                sb.Append(", id =");
                sb.Append(lWeather.sys.id);
                sb.Append(", country =");
                sb.Append(lWeather.sys.country);
                sb.Append(", sunrise =");
                sb.Append(lWeather.sys.sunrise);
                sb.Append(", sunset =");
                sb.Append(lWeather.sys.sunset);
                sb.Append("<br />");

                sb.Append("timezone:  =");
                sb.Append(lWeather.timezone);
                sb.Append("<br />");

                sb.Append("id:  =");
                sb.Append(lWeather.id);
                sb.Append("<br />");


                sb.Append("name:  =");
                sb.Append(lWeather.name);
                sb.Append("<br />");

                sb.Append("cod:  =");
                sb.Append(lWeather.cod);
                sb.Append("<br />");


                return Content(sb.ToString());


            }
        }



        public ActionResult xmlSerialization()
        {


            string url = "http://api.openweathermap.org/data/2.5/weather?q=London&mode=xml&APPID=201fb22425c00d3f2ebc54bdf9a5c6bc";
            StringBuilder sb = new StringBuilder();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
                HttpResponseMessage response = client.GetAsync(url).Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    XDocument xdoc = XDocument.Parse(response.Content.ReadAsStringAsync().Result);

                    string xml = xdoc.ToString();

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xml);
                    var json = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.None, true);
                    json = json.Replace("\"@", "\"");
                    json = json.Replace("\"#text\"", "\"text\"");

                    WeatherResponsexml lWeather = new JavaScriptSerializer().Deserialize<WeatherResponsexml>(json);

                    
                    sb.Append("City: Id = ");
                    sb.Append(lWeather.city.id);
                    sb.Append(", Name=");
                    sb.Append(lWeather.city.name);
                    sb.Append("<br />");

                    sb.Append("Coordinates: Latitude = ");
                    sb.Append(lWeather.city.coord.lat);
                    sb.Append(", Longitude=");
                    sb.Append(lWeather.city.coord.lon);
                    sb.Append("<br />");

                    sb.Append("Country:");
                    sb.Append(lWeather.city.country);
                    sb.Append(", timezone=");
                    sb.Append(lWeather.city.timezone);
                    sb.Append("<br />");

                    sb.Append("sun:rises");
                    sb.Append(lWeather.city.sun.rise);
                    sb.Append(", set=");
                    sb.Append(lWeather.city.sun.set);
                    sb.Append("<br />");

                    sb.Append("temperature: value = ");
                    sb.Append(lWeather.temperature.value);
                    sb.Append(", min=");
                    sb.Append(lWeather.temperature.min);
                    sb.Append(", max=");
                    sb.Append(lWeather.temperature.max);
                    sb.Append(", unit=");
                    sb.Append(lWeather.temperature.unit);
                    sb.Append("<br />");

                    sb.Append("feels_like: value = ");
                    sb.Append(lWeather.feels_like.value);
                    sb.Append(", unit=");
                    sb.Append(lWeather.feels_like.unit);
                    sb.Append("<br />");

                    sb.Append("humidity: value = ");
                    sb.Append(lWeather.humidity.value);
                    sb.Append(", unit=");
                    sb.Append(lWeather.humidity.unit);
                    sb.Append("<br />");


                    sb.Append("Pressure: value = ");
                    sb.Append(lWeather.pressure.value);
                    sb.Append(", unit=");
                    sb.Append(lWeather.pressure.unit);
                    sb.Append("<br />");

                }

            }

            return Content(sb.ToString());
        }




    }
}
