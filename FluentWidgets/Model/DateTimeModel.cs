using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace FluentWidgets.Model
{
    public class DateTimeModel
    {
        private static string[] _scopes = { CalendarService.Scope.CalendarReadonly };
        private CalendarService _service;

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
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets, _scopes, 
                    "user", CancellationToken.None, new FileDataStore(credPath, true)).Result;
            }

            // Create the Google Calendar API service
            _service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "FluentWidgets",
            });
        }

        public async Task<ObservableCollection<Event>> UpdateUpcomingEvents()
        {
            var eventsList = new ObservableCollection<Event>();

            // Get all the calendars that exist
            var calendarList = await _service.CalendarList.List().ExecuteAsync();

            foreach (var calendar in calendarList.Items)
            {
                // Request events in the next 7 days
                var request = _service.Events.List(calendar.Id);
                request.TimeMax = DateTime.Now + TimeSpan.FromDays(7);
                request.TimeMin = DateTime.Now;
                request.ShowDeleted = false;
                request.SingleEvents = true;
                request.MaxResults = 10;
                request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

                // Get the events and add them to the hashset
                var events = await request.ExecuteAsync();
                if (events.Items == null || events.Items.Count <= 0) continue;

                foreach (var eventItem in events.Items)
                {
                    eventsList.Add(eventItem);
                }
            }

            return eventsList;
        }
    }
}
