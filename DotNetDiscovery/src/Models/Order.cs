using System.Text.Json.Serialization;

class Order
{
    public string orderNumber { get; set; } = ""; // Injected dynamically

    [JsonPropertyName("destination")] // Map lowercase JSON property to C# PascalCase
    public required string destinationString { get; set; }

    public Airport? requestedDestination {
        get {
            Airport airport;
            var success = Enum.TryParse(destinationString, out airport);

            return success ? airport : null;
        }
    }

    public ScheduledFlight? flight;

    public String Description() {
        return $"Order: {orderNumber}, destination: {destinationString}";
    }

    public String ScheduledDescription() {
        if (flight != null) {
            return $"Order: {orderNumber}, {flight.Description()}";
        } else {
            return $"Order: {orderNumber}, Flight: not scheduled";
        }
    }
}