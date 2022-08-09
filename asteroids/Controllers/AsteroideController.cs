using AsteroidsAPI.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace AsteroidsAPI.Controllers
{
    [Route("asteroids")]
    [ApiController]
    public class AsteroideController : Controller
    {
        [HttpGet]
        public IActionResult GetAsteroides([FromQuery] AsteroideParameters asteroideParameters)
        {
            if (!asteroideParameters.ValidDays)
            {
                return BadRequest("El número de días debe de estar comprendido entre 1 y 7");
            }

            //start_date=2021-12-09  today
            string start_date = DateTime.Today.ToString("yyyy-MM-dd");
            //end day = today + days
            string end_date = DateTime.Today.AddDays(asteroideParameters.days).ToString("yyyy-MM-dd");

            var url = "https://api.nasa.gov/neo/rest/v1/feed?start_date=@STARTDATE@&end_date=@ENDDATE@&api_key=zdUP8ElJv1cehFM0rsZVSQN7uBVxlDnu4diHlLSb";
            url = url.Replace("@STARTDATE@", start_date).Replace("@ENDDATE@", end_date);

            string stringResult;

            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);

                httpRequest.Accept = "application/json";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
               
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    stringResult = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error obteniendo información desde API Nasa: " + ex.Message);
            }
            

            var result = JsonConvert.DeserializeObject<NasaResult>(stringResult);
            List<AsteroideReturn> lReturn = new List<AsteroideReturn>();

            foreach (KeyValuePair<string, DateResults[]> dict in result.near_earth_objects)
            {
                foreach (DateResults dateResults in dict.Value)
                {
                    if (dateResults.is_potentially_hazardous_asteroid)
                    {
                        AsteroideReturn asteroide = new AsteroideReturn();
                        asteroide.Nombre = dateResults.name;
                        asteroide.Fecha = dateResults.close_approach_data[0].close_approach_date;
                        asteroide.Diametro = ((Convert.ToDecimal(dateResults.estimated_diameter.kilometers.estimated_diameter_max) +
                            Convert.ToDecimal(dateResults.estimated_diameter.kilometers.estimated_diameter_min)) / 2).ToString();
                        asteroide.Velocidad = dateResults.close_approach_data[0].relative_velocity.kilometers_per_hour;
                        asteroide.Planeta = dateResults.close_approach_data[0].orbiting_body;

                        lReturn.Add(asteroide);
                    }
                }
            }

            return Ok(JsonConvert.SerializeObject(lReturn));

        }

    }
}