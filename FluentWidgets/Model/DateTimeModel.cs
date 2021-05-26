using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using FluentWidgets.Helpers;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace FluentWidgets.Model
{
    public class DateTimeModel
    {
        private static readonly string[] _scopes = {CalendarService.Scope.CalendarReadonly};
        private readonly CalendarService _service;

        public DateTimeModel()
        {
            // Read the credentials file
            UserCredential credential;
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith("google.json"));

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                const string credPath = "token.json";
                try
                {
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets,
                        _scopes,
                        "user", CancellationToken.None, new FileDataStore(credPath, true)).Result;
                }
                catch (Exception ex)
                {
                    // Sometimes initial credential verification fails, so just try again
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets,
                        _scopes,
                        "user", CancellationToken.None, new FileDataStore(credPath, true)).Result;
                }
            }

            // Create the Google Calendar API service
            _service = new CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "FluentWidgets"
            });
        }

        public async Task<ObservableCollection<Event>> UpdateUpcomingEvents()
        {
            if (!await NetConnect.IsOnline()) return new ObservableCollection<Event>();

            var eventsList = new List<Event>();

            // Get all the calendars that exist
            var calendarList = await _service.CalendarList.List().ExecuteAsync();

            foreach (var calendar in calendarList.Items)
            {
                var color = calendar.BackgroundColor;

                // Request events up to 30 days ahead, but only selecting the first 7
                var request = _service.Events.List(calendar.Id);
                request.TimeMax = DateTime.Now.Date.AddDays(30).AddTicks(-1);
                request.TimeMin = DateTime.Now;
                request.ShowDeleted = false;
                request.SingleEvents = true;
                request.MaxResults = 7;

                // Get the events and add them to the hashset
                var events = await request.ExecuteAsync();
                if (events.Items == null || events.Items.Count <= 0) continue;

                foreach (var eventItem in events.Items)
                {
                    eventItem.ColorId = color;
                    eventsList.Add(eventItem);
                }
            }

            // Sort the list in ascending date and time
            eventsList.Sort((e1, e2) =>
            {
                if (e1.Start.DateTime is null && e2.Start.DateTime is null)
                {
                    e1.Start.DateTime = DateTime.Parse(e1.Start.Date);
                    e2.Start.DateTime = DateTime.Parse(e2.Start.Date);
                }

                if (e1.Start.DateTime is null) e1.Start.DateTime = DateTime.Parse(e1.Start.Date);

                if (e2.Start.DateTime is null) e2.Start.DateTime = DateTime.Parse(e2.Start.Date);

                return ((DateTime) e1.Start.DateTime).CompareTo(e2.Start.DateTime);
            });

            return new ObservableCollection<Event>(eventsList);
        }
    }
}