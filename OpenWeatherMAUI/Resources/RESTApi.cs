using Newtonsoft.Json;

namespace OpenWeatherMAUI.Resources
{
    class RESTApi
    {
        private HttpClient httpClient = new HttpClient();
        private const string apiKey = "25dc40ef46d017a1eb9826fcb17b9590";
        private const string url = "https://api.openweathermap.org/data/2.5/weather";

        public async Task<WeatherData> GetWeathers(string query)
        {
            string path = Path.Combine(url, $"?q={query}&lang=ru&units=metric&appid={apiKey}");
            WeatherData weatherData = new WeatherData();
            using (var response = await httpClient.GetAsync(path))
            {
                if (response.IsSuccessStatusCode) 
                {
                    string content = await response.Content.ReadAsStringAsync();
                    weatherData = JsonConvert.DeserializeObject<WeatherData>(content) ?? new WeatherData();
                }
            }
            return weatherData;
        }
        public async Task<ImageSource> GetIcon(string query) 
        {
            using (var responce = await httpClient.GetAsync($"https://openweathermap.org/img/wn/{query}@4x.png"))
            {
                byte[] result = await responce.Content.ReadAsByteArrayAsync();
                MemoryStream memoryStream = new MemoryStream(result);
                ImageSource imageSource = ImageSource.FromStream(() => memoryStream);
                return imageSource;
            }
        }
    }
}
