using Google.Apis.Calendar.v3.Data;

namespace Application.Helpers
{
        public class GoogleMeetEvent
        {
            public string Summary { get; set; }
            public string Description { get; set; }
            public EventDateTime Start { get; set; }
            public EventDateTime End { get; set; }
            public ConferenceData? ConferenceData { get; set; }

            public GoogleMeetEvent()
            {
                this.Start = new EventDateTime()
                {
                    TimeZone = "America/Sao_Paulo"
                };
                this.End = new EventDateTime()
                {
                    TimeZone = "America/Sao_Paulo"
                };
            }
        }

        public class EventDateTime
        {
            public DateTime DateTime { get; set; }
            public string TimeZone { get; set; }
        }
}
