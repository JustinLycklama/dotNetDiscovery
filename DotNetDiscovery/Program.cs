using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

class Program
{
    static void Main()
    {
        SpeedyAir company = new SpeedyAir();

        // company.PrintFlightSchedule();
        company.GenerateFlightItineraries();
    }
}

// Business logic for our company
class SpeedyAir {

    private int availablePlanes = 3;

    private OrderService orderService = new OrderService();
    private FlightService flightService = new FlightService();

    public void PrintFlightSchedule() {
        
        /*
        As an inventory management user, I can load a flight schedule similar to the one listed in the Scenario above. For
        this story you do not yet need to load the orders. I can also list out the loaded flight schedule on the console.
        */

        // Given no format for flight schedule data is given, I assume this means to load the given Scenario into memory.

        List<ScheduledFlight> flights = flightService.LoadScenarioOne();

        foreach (ScheduledFlight flight in flights) {
            Console.WriteLine(flight.Description());
        }
    }

    public void GenerateFlightItineraries() {
        /*
        As an inventory management user, I can generate flight itineraries by scheduling a batch of orders. These flights
        can be used to determine shipping capacity.
         - Use the json file attached to load the given orders.
         - The orders listed in the json file are listed in priority order ie. 1..N
        */

        List<Order> orders = orderService.FetchOrders();

        foreach (Order order in orders) {
            Console.WriteLine(order.Description());
        }

        // Since we are GENERATING flight itineraties, this means the Scenario should not be used.
        // The Orders are listed IN ORDER of priority. So if the first 60 orders are to Toronto, all 3 planes should go to Toronto the first day.

        // What if I was given 62 Orders:
        // the first order is to Toronto
        // the second order is to Calgary
        // and the NEXT 60 orders are to Vancouver... 

        // I'm going to say these are 'absolute' priorities. So The singular shipment to Toronto and Calgary will happen, along with 20 to Vancouver Today.
        // Tomorrow, the remaining 40 orders will go to Vancouver

        List<ScheduledFlight> scheduledFlights = new List<ScheduledFlight>();
        
        List<Order> scheduledOrders = new List<Order>();
        List<Order> unscheduledOrder = new List<Order>();

        foreach (Order order in orders) {
            if (!order.requestedDestination.HasValue) {
                Console.WriteLine($"Order {order.orderNumber} has invalid destination");
                continue;
            }

            Airport destination = order.requestedDestination.Value;

            Console.WriteLine($"Destination {destination}");

            // Find existing flights to our destination with capacity
            ScheduledFlight? flight = scheduledFlights
                .Where(flight => flight.destination == destination && flight.currentLoad < flight.capacity)
                .FirstOrDefault();

            Console.WriteLine($"Found Flight: {flight}");

            // If there are no more empty flights to our destination, and we have not used all our available planes, schedule a new flight
            if (flight == null && scheduledFlights.Count < availablePlanes) {
                ScheduledFlight newFlight = new ScheduledFlight(
                    departure: Airport.YUL, 
                    destination: destination,
                    date: 1);

                scheduledFlights.Add(newFlight);

                flight = newFlight;
            }

            if (flight == null) {
                unscheduledOrder.Add(order);
                continue;
            }

            flight!.AddOrder(order);
            scheduledOrders.Add(order);
        }

        foreach (Order order in scheduledOrders) {
            Console.WriteLine(order.ScheduledDescription());
        }

        foreach (Order order in unscheduledOrder) {
            Console.WriteLine(order.ScheduledDescription());
        }
    }
}