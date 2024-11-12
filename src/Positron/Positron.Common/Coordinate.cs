namespace Positron.Common;

public record struct Coordinate
{
    public float Latitude;
    public float Longitude;

    public static Coordinate Parse(string str)
    {
        var parts = str.Split(',');

        return new Coordinate()
        {
            Latitude = float.Parse(parts[0]),
            Longitude = float.Parse(parts[1])
        };
    }

    public static Coordinate Parse(string latitude, string Longitude)
    {
        return new Coordinate()
        {
            Latitude = float.Parse(latitude),
            Longitude = float.Parse(Longitude)
        };
    }

    public string ToCsv()
    {
        return $"{Latitude},{Longitude}";
    }
}