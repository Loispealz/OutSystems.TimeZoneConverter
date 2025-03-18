using System;
using System.Collections.Generic;
using OutSystems.ExternalLibraries.SDK;

namespace TimeZone
{
    /// <summary>
    /// The ITimeZone interface defines the methods (exposed as server actions)
    /// for the TimeZone functionality.
    /// </summary>
    [OSInterface(Description = "Utils to support multiple time zones. in OutSystems Developer Cloud (ODC) apps.", IconResourceName = "TimeZone.resources.timezone.png")]
    public interface ITimeZone
    {
        string ConvertTimeZoneToIANA(string Timezone, string Country);
        public TimeZoneRecord GetIANATimeZone(string ssIANATimezone);
        public DateTime ConvertToUTC(DateTime SourceDateTime, string SourceTimeZone);
        public TimeZoneRecord GetCurrentTimeZone();
        public DateTime ConvertFromTimeZone(DateTime SourceDateTime, string SourceTimeZone, string DestinationTimeZone);
        public DateTime ConvertFromUTC(DateTime DateTimeUTC, string TimeZone);
        public List<TimeZoneRecord> GetSystemTimeZones();
        public TimeZoneRecord GetTimeZone(string Identifier);

    }
}