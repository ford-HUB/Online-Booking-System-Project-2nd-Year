using System.Collections.Generic;

namespace Login_page.Resources.MAIN_PAGE
{
    public class WeatherData
    {
        public List<Weather> Weather { get; set; }
        public Main Main { get; set; }
        public Wind Wind { get; set; }
    }

    public partial class Weather
    {
        public string Main { get; set; }
        public string Description { get; set; }
        public new string Icon { get; set; } 
    }

    public class Main
    {
        public double Temp { get; set; }
        public int Humidity { get; set; }
    }

    public class Wind
    {
        public double Speed { get; set; }
    }
}
