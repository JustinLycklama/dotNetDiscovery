class ScheduledFlight {

    private static int NextFlightNumber = 1;

    public Airport departure { get; }
    public Airport destination { get; }

    int flightNumber;

    int date;

    public readonly int capacity = 20; 

    private List<Order> orders = new List<Order>();
    public List<Order> Orders { get => orders; private set => orders = value; }


    public ScheduledFlight(Airport departure, Airport destination, int date) {
        this.departure = departure;
        this.destination = destination;
        this.date = date;

        flightNumber = NextFlightNumber;
        NextFlightNumber += 1;
    }

    public void AddOrder(Order order) {
        if (orders.Count >= capacity) {
            throw new InvalidOperationException("You cannot add any more orders to this flight");
        }

        orders.Add(order);
        order.flight = this;
    }

    public static void ResetFlightNumbers() {
        NextFlightNumber = 1;
    }

    public String Description() {
        return $"Flight: {flightNumber}, departure: {departure.ToString()}, arrival: {destination.ToString()}, day: {date}";
    }
} 