enum Airport
{
    YUL,
    YYZ,
    YYC,
    YVR
}

static class AirportExtensions
{
    public static String CityName(this Airport airport)
    {
        switch (airport)
        {
            case Airport.YUL:
                return "Montreal";
            case Airport.YYZ:
                return "Toronto";
            case Airport.YYC:
                return "Calgary";
            case Airport.YVR:
                return "Vancouver";
            default:
                throw new ArgumentOutOfRangeException(nameof(airport), airport, "Unknown airport code");
        }
    }
}