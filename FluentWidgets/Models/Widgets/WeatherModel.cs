using System;
using System.Configuration;
using System.Device.Location;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using FluentWidgets.Helpers;
using FluentWidgets.Helpers.Objects;
using Newtonsoft.Json;

namespace FluentWidgets.Models.Widgets
{
    public class WeatherModel
    {
        private GeoCoordinate GetGeoCoordinate()
        {
            var geoWatcher = new GeoCoordinateWatcher();
            geoWatcher.TryStart(false, TimeSpan.FromSeconds(1));

            while (true)
                if (!geoWatcher.Position.Location.IsUnknown)
                    return geoWatcher.Position.Location;
        }

        private async Task<string> GetLocationName(GeoCoordinate coord)
        {
            MapService.ServiceToken = ConfigurationManager.AppSettings["BING_MAPS"];
            var queryPoint = new BasicGeoposition {Latitude = coord.Latitude, Longitude = coord.Longitude};
            MapLocationFinderResult result =
                await MapLocationFinder.FindLocationsAtAsync(new Geopoint(queryPoint));

            if (result.Status == MapLocationFinderStatus.Success)
                return result.Locations[0].Address.Town + ", " + result.Locations[0].Address.Region;

            return "Unknown";
        }

        public async Task<OpenWeather> GetWeatherForecast()
        {
            var owmKey = ConfigurationManager.AppSettings["OPEN_WEATHER_API"];
            var coord = GetGeoCoordinate();

            Debug.WriteLine($"https://api.openweathermap.org/data/2.5/onecall?lat={coord.Latitude}&lon={coord.Longitude}&exclude=minutely&appid={owmKey}");
            using var wc = new WebClient();
            var owmJson = await wc.DownloadStringTaskAsync(
                $"https://api.openweathermap.org/data/2.5/onecall?lat={coord.Latitude}&lon={coord.Longitude}&exclude=minutely&appid={owmKey}");

            var owmObj = JsonConvert.DeserializeObject<OpenWeather>(owmJson);

            owmObj.Name = (await GetLocationName(coord)).ToUpper();

            //TODO: Implement functionality to store set values, for now use these defaults
            owmObj.TemperatureUnit = TemperatureUnit.Celsius;
            owmObj.SpeedUnit = SpeedUnit.MilesPerHour;

            return owmObj;
        }
    }
}