
using System;
using System.Collections.Generic;


namespace TimeZone
{
    /// <summary>
    /// 
    /// </summary>
    public class TimeZone : ITimeZone
    {
        /// <summary>
        /// Converts a time to the time in a particular time zone.
        /// </summary>
        /// <param name="SourceDateTime">The date and time to convert.</param>
        /// <param name="SourceTimeZone">The timezone of the provided date time. Example: "Hawaiian Standard Time"</param>
        /// <param name="DestinationTimeZone">The time zone to convert dateTime to. Example: "Tokyo Standard Time"</param>
        /// <returns>Date Time</returns>
        public DateTime ConvertFromTimeZone(DateTime SourceDateTime, string SourceTimeZone, string DestinationTimeZone)
        {
            return TimeZoneInfo.ConvertTime(DateTime.SpecifyKind(SourceDateTime, 
                DateTimeKind.Unspecified), TimeZoneInfo.FindSystemTimeZoneById(SourceTimeZone), 
                TimeZoneInfo.FindSystemTimeZoneById(DestinationTimeZone)
                );
        }
        /// <summary>
        /// Converts a Coordinated Universal Time (UTC) to the time in a specified time zone.
        /// </summary>
        /// <param name="TimeZone">The time zone to convert dateTime to. Example: "Central Standard Time"</param>
        /// <returns>The date and time in the destination time zone. Its Kind property is Utc if destinationTimeZone is Utc; otherwise, its Kind property is Unspecified.</returns>
        public DateTime ConvertFromUTC(DateTime DateTimeUTC, string TimeZone)
        {
            DateTime timeUtc = DateTime.SpecifyKind(DateTimeUTC, DateTimeKind.Unspecified);
            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById(TimeZone);
            return TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
        }


        /// <summary>
        /// Converts a Windows time zone ID to an equivalent IANA time zone name
        /// </summary>
        /// <param name="Timezone">The Windows time zone ID to convert. Example: "Eastern Standard Time"</param>
        /// <param name="Country">An optional two-letter ISO Country/Region code, used to get a a specific mapping. Example: "CA"</param>
        public string ConvertTimeZoneToIANA(string timezone, string country)
        {
            return TimeZoneConverter.TZConvert.WindowsToIana(timezone, string.IsNullOrWhiteSpace(country) ? null : country);
        }


        /// <summary>
        /// Convert an IANA time zone name to the best fitting Windows time zone ID.
        /// </summary>
        /// <param name="IANATimezone">Example: "America/New_York"</param>
        /// <returns>TimeZoneStructure"</returns>
        public TimeZoneRecord GetIANATimeZone(string ssIANATimezone)
        {
            return (TimeZoneRecord)GetTimeZone(TimeZoneConverter.TZConvert.IanaToWindows(ssIANATimezone));
        }

        /// <summary>
        /// Converts a date and time to Coordinated Universal Time (UTC).
        /// </summary>
        /// <param name="SourceDateTime">The date and time to convert.</param>
        /// <param name="SourceTimeZone">Example: "Hawaiian Standard Time"</param>
        /// <returns>DateTime</returns>
        public DateTime ConvertToUTC(DateTime SourceDateTime, string SourceTimeZone)
        {
            return TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(SourceDateTime, DateTimeKind.Unspecified), TimeZoneInfo.FindSystemTimeZoneById(SourceTimeZone));
        }

        /// <summary>
        /// Instantiates a TimeZoneInfo object that represents the local time zone.
        /// </summary>
        /// <returns>TimeZoneStructure</returns>
        public TimeZoneRecord GetCurrentTimeZone()
        {
            TimeZoneRecord TZs = new();

            TimeZoneInfo tzi = TimeZoneInfo.Local;
            TZs.Identifier = tzi.Id;
            TZs.StandardName = tzi.StandardName;
            TZs.DisplayName = tzi.DisplayName;
            TZs.UtcOffset = (int)tzi.GetUtcOffset(DateTime.Now).TotalMinutes;
            TZs.SupportsDaylightSaving = tzi.SupportsDaylightSavingTime;
            TZs.IsDaylightSaving = tzi.IsDaylightSavingTime(DateTime.Now);

            return TZs;
        }

       

        public List<TimeZoneRecord> GetSystemTimeZones()
        {

            List<TimeZoneInfo> timeZones = new List<TimeZoneInfo>(TimeZoneInfo.GetSystemTimeZones());

            timeZones.Sort((left, right) =>
            left.BaseUtcOffset.CompareTo(right.BaseUtcOffset) == 0
        ?       string.CompareOrdinal(left.DisplayName, right.DisplayName)
                : left.BaseUtcOffset.CompareTo(right.BaseUtcOffset)
            );

            List<TimeZoneRecord> timeZonesOut = new List<TimeZoneRecord>();

            foreach (TimeZoneInfo tzi in timeZones)
            {
                TimeZoneRecord r = new TimeZoneRecord(
                    tzi.Id,
                    tzi.StandardName,
                    tzi.DisplayName,
                    (int)tzi.GetUtcOffset(DateTime.Now).TotalMinutes,
                    tzi.SupportsDaylightSavingTime,
                    tzi.IsDaylightSavingTime(DateTime.Now)
                    );

                timeZonesOut.Add(r);
            }

            return timeZonesOut;

            //throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a TimeZoneInfo object based on its identifier.
        /// </summary>
        /// <param name="Identifier">The time zone identifier, which corresponds to the Id property. Example: "Tokyo Standard Time"</param>
        /// <returns>TimeZoneStructure</returns>
        public TimeZoneRecord GetTimeZone(string Identifier)
        {
            TimeZoneRecord tzs = new();

            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(Identifier);

            tzs.Identifier = tzi.Id;
            tzs.StandardName = tzi.StandardName;
            tzs.DisplayName = tzi.DisplayName;
            tzs.UtcOffset = (int)tzi.GetUtcOffset(DateTime.Now).TotalMinutes;
            tzs.SupportsDaylightSaving = tzi.SupportsDaylightSavingTime;
            tzs.IsDaylightSaving = tzi.IsDaylightSavingTime(DateTime.Now);
            return tzs;
        }
    }
}