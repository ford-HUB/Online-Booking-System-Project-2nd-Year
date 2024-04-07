using System;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Login_page.Resources.MAIN_PAGE
{
    public partial class Weather : Form
    {
        public Weather()
        {
            InitializeComponent();

            //Removing hover action button
            button_monday.FlatAppearance.MouseOverBackColor = button_monday.BackColor;
            button_Tue.FlatAppearance.MouseOverBackColor = button_Tue.BackColor;
            button_Wed.FlatAppearance.MouseOverBackColor = button_Wed.BackColor;
            button_Thu.FlatAppearance.MouseOverBackColor = button_Thu.BackColor;
            button_Fri.FlatAppearance.MouseOverBackColor = button_Fri.BackColor;
            button_Sat.FlatAppearance.MouseOverBackColor = button_Sat.BackColor;
            button_Sun.FlatAppearance.MouseOverBackColor = button_Sun.BackColor;

            // Display time
            Active.Start();
        }

        private void Weather_Load(object sender, EventArgs e)
        {
            DisplayWeatherData();
        }

        private void DisplayWeatherData()
        {
            try
            {
                string json = GetWeatherJson();
                WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(json);

                // Convert temp from kelvin to celsius
                double temperatureCelsius = weatherData.Main.Temp - 273.15;

                // Display current weather information
                label_condition.Text = $"Condition: {weatherData.Weather[0].Main}";
                label_temp.Text = $"Temperature: {temperatureCelsius}°C";
                label_Humidity.Text = $"Humidity: {weatherData.Main.Humidity}%";
                label_WS.Text = $"Wind Speed: {weatherData.Wind.Speed} km/h";


                Temp_Mon.Text = "31°C";
                Temp_Tue.Text = "28°C";
                Temp_Wed.Text = "24°C";
                Temp_Thu.Text = "25°C";
                Temp_Fri.Text = "Current";
                Temp_Sat.Text = "19°C";
                Temp_Sun.Text = "29°C";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error displaying weather data: " + ex.Message);
            }
        }

        private string GetWeatherJson()
        {
            string apiKey = Properties.Settings.Default.API;
            string city = Properties.Settings.Default.City;

            string url = $"https://api.openweathermap.org/data/2.5/weather?lat=10.288273&lon=123.965285&appid=fb410627afcb1e60ffd2a68dffe4bc2a";

            using (WebClient client = new WebClient())
            {
                try
                {
                    return client.DownloadString(url);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error fetching weather data: " + ex.Message);
                }
            }
        }

        private void Active_Tick_1(object sender, EventArgs e)
        {
            CurrentTime.Text = DateTime.Now.ToLongTimeString();
            CurrentDate.Text = DateTime.Now.ToLongDateString();
        }

    }
}
