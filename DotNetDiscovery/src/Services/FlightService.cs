using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// Separate our business logic from data retrieval with a service
class FlightService {

    // This would be replaced with an API call or some async action to fetch data
    public void LoadScenarioOne(Action<List<ScheduledFlight>> completion) {
        // Our first scenario has orders from YUL that are sending boxes to Toronto, Calgary, and Vancouver,
        var dayOne = new List<Airport>{ Airport.YYZ, Airport.YYC, Airport.YVR };
        var dayTwo = new List<Airport>{ Airport.YYZ, Airport.YYC, Airport.YVR };

        List<ScheduledFlight> flights = new List<ScheduledFlight>();

        foreach (Airport destination in dayOne) {
            flights.Add(new ScheduledFlight (
                departure: Airport.YUL,
                destination: destination,
                date: 1
            ));
        }

        foreach (Airport destination in dayTwo) {
            flights.Add(new ScheduledFlight (
                departure: Airport.YUL,
                destination: destination,
                date: 2
            ));
        }

        // Call the completion callback with the result
        completion(flights);
    }
}