using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;

namespace WeatherApp.ViewModel.Helpers
{
    public class AccuWeatherHelper
    {
        public const string BASE_URL = "http://dataservice.accuweather.com/";
        public const string AUTO_COMPLETE_ENDPOINT = "http://dataservice.accuweather.com/locations/v1/cities/autocomplete?apikey={0}&q={1}";
        public const string CURRENT_CONDITION_ENPOINT = "currentconditions/v1/{0}?apikey={1}";
        public const string API_KEY = "LAFbK0Gxg0AR05qAv8OwiEAhnOAmSU7f";

        public async static Task<List<DetailsCity>> GetDetailsCities(string query)
        {
            List<DetailsCity> cities = new List<DetailsCity>();

            string url = BASE_URL + string.Format(AUTO_COMPLETE_ENDPOINT,API_KEY,query);

            using (HttpClient client = new HttpClient())
            {
                var responses = await client.GetAsync(url);
                string json = await responses.Content.ReadAsStringAsync();

                cities = JsonConvert.DeserializeObject<List<DetailsCity>>(json);
            }

            return cities;
        }

        public static async Task<DetailsWeather> GetCurrentConditions(string cityKey)
        {
            DetailsWeather currentConditions = new DetailsWeather();
            string url = BASE_URL + string.Format(CURRENT_CONDITION_ENPOINT, cityKey, API_KEY);

            using (HttpClient client = new HttpClient())
            {
                var responses = await client.GetAsync(url);
                string json = await responses.Content.ReadAsStringAsync();

                currentConditions = (JsonConvert.DeserializeObject<List<DetailsWeather>>(json)).FirstOrDefault();
            }

            return currentConditions;
        }

    }
}
