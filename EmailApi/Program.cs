using System.Text.Json;
using EmailApi.Models;

namespace EmailApi;

// Goal:
// GET in the API
// Deserialize
// Filter users by city
// Return Lis<string> with the emails

internal class Program
{
    static async Task Main(string[] args)
    {
        var emails = await GetEmailsByCity("Gwenborough");

        foreach (var email in emails)
        {
            Console.WriteLine(email);
        }
    }

    public static async Task<List<string>> GetEmailsByCity(string city)
    {
        var url = "https://jsonplaceholder.typicode.com/users";
        using HttpClient client = new HttpClient();
        var response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return [];

        var json = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonSerializer.Deserialize<List<User>>(json);

        if (apiResponse is null)
            return [];

        var emails = apiResponse
            .Where(user => user.address?.city == city)
            .Select(user => user.email)
            .ToList();

        return emails;
    }
}
