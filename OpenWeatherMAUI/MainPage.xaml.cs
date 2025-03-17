using OpenWeatherMAUI.Resources;

namespace OpenWeatherMAUI
{
    public partial class MainPage : ContentPage
    {
        DataSerializable dataSerializable = new DataSerializable();
        RESTApi rest = new RESTApi();
        WeatherData itemWeather = new WeatherData();
        string dataFile = "data.json";
        string path = string.Empty;
        public MainPage()
        {
            InitializeComponent();
        }
        public async Task Init()
        {
            path = Path.Combine(dataFile);
            if (File.Exists(path))
            {
                await DisplayAlert("Успешное сохранение", "Загрузка последнего запроса", "Выйти");
                await dataSerializable.Deserializable(path);
            }
            else
            {
                await DisplayAlert("Не успешное сохранение", "Загрузка последнего запроса не произошла", "Выйти");
                await dataSerializable.Serializable(itemWeather, path);
            }
        }
        public async void GetWeather(string sity)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sity)) return;
                if (string.IsNullOrWhiteSpace(sity)) return;

                activityIndicator.IsRunning = true;
                activityIndicator.IsVisible = true;

                itemWeather = await rest.GetWeathers(sity);
                currentSity.Text = itemWeather.name;
                temperatur.Text = itemWeather.main.temp.ToString("f2");
                currentdata.Text = DateTime.Now.ToString("dd.MM.yyyy");
                clouds.Text = $"{itemWeather.clouds.all.ToString("d0")}%";
                visibility.Text = $"{itemWeather.visibility.ToString()}M";
                pressure.Text = itemWeather.main.pressure.ToString();
                snowstorm.Text = $"{itemWeather.wind.speed} m/s";


                var imageSource = await rest.GetIcon(itemWeather.weather[0].icon);
                weatherIcon.Source = imageSource;

                activityIndicator.IsRunning = false;
                activityIndicator.IsVisible = false;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка",ex.Message,"Ок");
            }
        }
        private void GetWeatherButton_Clicked(object sender, EventArgs e)
        {
            string sity = entrySity.Text;
            GetWeather(sity);
        }
    }
}
