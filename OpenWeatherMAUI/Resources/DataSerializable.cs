using System.Text.Json;

namespace OpenWeatherMAUI.Resources
{
    class DataSerializable
    {
        public async Task Serializable(WeatherData json, string path)
        {
            using (FileStream fl = new FileStream(path, FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync<WeatherData>(fl,json);
            }
        }
        public async Task<WeatherData> Deserializable(string path)
        {
            using (FileStream fl = new FileStream(path, FileMode.Open))
            {
                return await JsonSerializer.DeserializeAsync<WeatherData>(fl) ?? new WeatherData();
            }
        }
    }
}
