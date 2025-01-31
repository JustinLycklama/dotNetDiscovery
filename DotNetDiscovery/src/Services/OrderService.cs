using System.IO;
using System.Text.Json;

// Separate our business logic from data retrieval with a service
class OrderService {

    // This would be replaced with an API call or some async action to fetch data
    public List<Order> FetchOrders() {
        string filePath = "coding-assigment-orders.json";

        string json = File.ReadAllText(filePath);

        // Deserialize JSON into a Dictionary
        Dictionary<string, Order> orderDict = JsonSerializer.Deserialize<Dictionary<string, Order>>(json) ?? new Dictionary<string, Order>();

        // Convert dictionary to list, injecting OrderNumber dynamically
        List<Order> orders = orderDict
            .Select(kvp => new Order { orderNumber = kvp.Key, destinationString = kvp.Value.destinationString })
            .ToList();

        return orders;
    }
}