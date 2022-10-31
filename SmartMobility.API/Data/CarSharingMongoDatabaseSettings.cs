namespace SmartMobility.API.Data
{
    public class CarSharingMongoDatabaseSettings: MongoDatabaseSettingsBase
    {
        public string ActiveBookingsCollectionName { get; set; } = "ActiveBookings";
        public string ActiveParkingsCollectionName { get; set; } = "ActiveParkings";
        public string PermanentBookingsCollectionName { get; set; } = "PermanentBookings";
        public string PermanentParkingsCollectionName { get; set; } = "PermanentParkings";

    }
}
