namespace Positron.Common.Models
{
    public class VehiclePosition
    {
        public int Id { get; set; }
        public string? Registration { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public DateTime RecordedDateTimeUtc { get; set; }
        public Coordinate Coordinate => new() { Latitude = Latitude, Longitude = Longitude };

        public override string ToString()
        {
            return $"{Id} {Registration} {Latitude} {Longitude} {RecordedDateTimeUtc}";
        }

        public string ToCsv()
        {
            return $"{Id},{Registration},{Latitude},{Longitude},{RecordedDateTimeUtc}";
        }
    }
}
