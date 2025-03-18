using OutSystems.ExternalLibraries.SDK;

namespace TimeZone {
    /// <summary>
    
    /// </summary>
    [OSStructure]
    public struct TimeZoneRecord {
        /// <summary>
        /// </summary>
        public string Identifier;

        /// <summary>
        /// </summary>
        public string StandardName;

        /// <summary>
        /// </summary>
        public string DisplayName;

        /// <summary>
        /// </summary>
        public int UtcOffset;

        /// <summary>
        /// </summary>
        public bool SupportsDaylightSaving;

        /// <summary>
        /// </summary>
        public bool IsDaylightSaving;

        public TimeZoneRecord(string identifier, string standardName, string displayName, int utcOffset, bool supportsDaylightSaving, bool isDaylightSaving)
        {
            Identifier = identifier;
            StandardName = standardName;
            DisplayName = displayName;
            UtcOffset = utcOffset;
            SupportsDaylightSaving = supportsDaylightSaving;
            IsDaylightSaving = isDaylightSaving;
        }
    }

}