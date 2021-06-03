using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FluentWidgets.Helpers.Objects
{
    public class Weather
    {
        [JsonProperty("icon")] private string _icon;
        [JsonProperty("description")] private string _description;

        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("main")] public string Main { get; set; }

        public string Description => _description[0].ToString().ToUpper() + _description.Substring(1);

        public string Icon => $"../Resources/Weather/{_icon}.png";
    }

    public class Current
    {
        [JsonProperty("dt")] public int Dt { get; set; }

        [JsonProperty("sunrise")] public int Sunrise { get; set; }

        [JsonProperty("sunset")] public int Sunset { get; set; }

        [JsonProperty("temp")] public double Temp { get; set; }

        [JsonProperty("feels_like")] public double FeelsLike { get; set; }

        [JsonProperty("pressure")] public int Pressure { get; set; }

        [JsonProperty("humidity")] public int Humidity { get; set; }

        [JsonProperty("dew_point")] public double DewPoint { get; set; }

        [JsonProperty("uvi")] public double Uvi { get; set; }

        [JsonProperty("clouds")] public int Clouds { get; set; }

        [JsonProperty("visibility")] public int Visibility { get; set; }

        [JsonProperty("wind_speed")] public double WindSpeed { get; set; }

        [JsonProperty("wind_deg")] public int WindDeg { get; set; }

        [JsonProperty("wind_gust")] public double WindGust { get; set; }

        [JsonProperty("weather")] public List<Weather> Weather { get; set; }
    }

    public class Rain
    {
        [JsonProperty("1h")] public double _1h { get; set; }
    }

    public class Hourly
    {
        [JsonProperty("dt")] public int Dt { get; set; }

        [JsonProperty("temp")] public double Temp { get; set; }

        [JsonProperty("feels_like")] public double FeelsLike { get; set; }

        [JsonProperty("pressure")] public int Pressure { get; set; }

        [JsonProperty("humidity")] public int Humidity { get; set; }

        [JsonProperty("dew_point")] public double DewPoint { get; set; }

        [JsonProperty("uvi")] public double Uvi { get; set; }

        [JsonProperty("clouds")] public int Clouds { get; set; }

        [JsonProperty("visibility")] public int Visibility { get; set; }

        [JsonProperty("wind_speed")] public double WindSpeed { get; set; }

        [JsonProperty("wind_deg")] public int WindDeg { get; set; }

        [JsonProperty("wind_gust")] public double WindGust { get; set; }

        [JsonProperty("weather")] public List<Weather> Weather { get; set; }

        [JsonProperty("pop")] private double _pop;
        public double Pop => Math.Round(_pop * 100);

        [JsonProperty("rain")] public Rain Rain { get; set; }
    }

    public class Temp
    {
        [JsonProperty("day")] public double Day { get; set; }

        [JsonProperty("min")] public double Min { get; set; }

        [JsonProperty("max")] public double Max { get; set; }

        [JsonProperty("night")] public double Night { get; set; }

        [JsonProperty("eve")] public double Eve { get; set; }

        [JsonProperty("morn")] public double Morn { get; set; }
    }

    public class FeelsLike
    {
        [JsonProperty("day")] public double Day { get; set; }

        [JsonProperty("night")] public double Night { get; set; }

        [JsonProperty("eve")] public double Eve { get; set; }

        [JsonProperty("morn")] public double Morn { get; set; }
    }

    public class Daily
    {
        [JsonProperty("dt")] public int Dt { get; set; }

        [JsonProperty("sunrise")] public int Sunrise { get; set; }

        [JsonProperty("sunset")] public int Sunset { get; set; }

        [JsonProperty("moonrise")] public int Moonrise { get; set; }

        [JsonProperty("moonset")] public int Moonset { get; set; }

        [JsonProperty("moon_phase")] public double MoonPhase { get; set; }

        [JsonProperty("temp")] public Temp Temp { get; set; }

        [JsonProperty("feels_like")] public FeelsLike FeelsLike { get; set; }

        [JsonProperty("pressure")] public int Pressure { get; set; }

        [JsonProperty("humidity")] public int Humidity { get; set; }

        [JsonProperty("dew_point")] public double DewPoint { get; set; }

        [JsonProperty("wind_speed")] public double WindSpeed { get; set; }

        [JsonProperty("wind_deg")] public int WindDeg { get; set; }

        [JsonProperty("wind_gust")] public double WindGust { get; set; }

        [JsonProperty("weather")] public List<Weather> Weather { get; set; }

        [JsonProperty("clouds")] public int Clouds { get; set; }

        [JsonProperty("pop")] private double _pop;
        public double Pop => Math.Round(_pop * 100);

        [JsonProperty("rain")] public double Rain { get; set; }

        [JsonProperty("uvi")] public double Uvi { get; set; }
    }

    public class OpenWeather
    {
        [JsonProperty("lat")] public double Lat { get; set; }

        [JsonProperty("lon")] public double Lon { get; set; }

        [JsonProperty("timezone")] public string Timezone { get; set; }

        [JsonProperty("timezone_offset")] public int TimezoneOffset { get; set; }

        [JsonProperty("current")] public Current Current { get; set; }

        [JsonProperty("hourly")] public List<Hourly> Hourly { get; set; }

        [JsonProperty("daily")] public List<Daily> Daily { get; set; }

        public string Name { get; set; }

        public TemperatureUnit TemperatureUnit { get; set; }

        public SpeedUnit SpeedUnit { get; set; }
    }

    public enum TemperatureUnit
    {
        Celsius,
        Fahrenheit
    }

    public enum SpeedUnit
    {
        MetresPerSecond,
        MilesPerHour,
        KilometersPerHour
    }
}