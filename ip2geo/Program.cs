// ip2geo by uzrboot
// v1 had ip-only support, but now v2 also has domain name support.
// now v2.1 optimized the code a bit, removed some redundant (aka useless) code. 

using System.Net;
using Newtonsoft.Json;

namespace ip2geo;

// init the damn vars
public class Data(string? city, string? region, string? country, string? loc, string? org, string? postal)
{
    public string? City { get; } = city;
    public string? Region { get; } = region;
    public string? Country { get; } = country;
    public string? Loc { get; } = loc;
    public string? Org { get; } = org;
    public string? Postal { get; } = postal;
}

internal abstract class Program
{
    private static async Task Main()
    {
        // main program stuff
        Console.Title = "ip2geo";
        Console.ForegroundColor = ConsoleColor.Blue;
        const string multiLineString = """
                                       _________ _______  _______  _______  _______  _______ 
                                       \__   __/(  ____ )/ ___   )(  ____ \(  ____ \(  ___  )
                                          ) (   | (    )|\/   )  || (    \/| (    \/| (   ) |
                                          | |   | (____)|    /   )| |      | (__    | |   | |
                                          | |   |  _____)  _/   / | | ____ |  __)   | |   | |
                                          | |   | (       /   _/  | | \_  )| (      | |   | |
                                       ___) (___| )      (   (__/\| (___) || (____/\| (___) |
                                       \_______/|/       \_______/(_______)(_______/(_______)
                                       """;
        Console.Clear();
        Console.WriteLine(multiLineString);
        // todo center text
        // Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (multiLineString.Length / 2)) + "}", multiLineString));
        
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"[{DateTime.Now:HH:mm:ss} WARNING]");
        Console.ResetColor();
        Console.WriteLine(" getting domain name's ips is still being figured out, if you find a solution please contact me on discord, @"fentables", ill hand over the src.");
        Console.Write("enter the ip addr or domain name: ");
        var userInput = Console.ReadLine();

        string? ip;
        if (IPAddress.TryParse(userInput, out var ipAddress))
        {
            ip = userInput;
        }
        else
        {
            // Resolve IP address from domain name
            try
            {
                if (userInput != null) ipAddress = GetIpAddressFromDomain(userInput);
                ip = ipAddress?.ToString();
            }
            catch (Exception)
            {
                Console.WriteLine($"Failed to resolve IP address for domain: {userInput}");
                Console.ReadLine();
                return;
            }
        }

        // Proceed with fetching IP information using the obtained IP address
        var url = $"https://ipinfo.io/{ip}/json";

        using var client = new HttpClient();
        try
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss} INFO] making request...");  
            Console.ResetColor();
            
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss} SUCCESS] request made successfully");  
            Console.ResetColor();
            
            var responseData = await response.Content.ReadAsStringAsync();
            var ipInfo = JsonConvert.DeserializeObject<Data>(responseData);
            
            // self-explanatory
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("country: ");
            Console.ResetColor();
            Console.WriteLine(ipInfo?.Country);
            
            // self-explanatory
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("region/state: ");
            Console.ResetColor();
            Console.WriteLine(ipInfo?.Region);

            // self-explanatory
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("city: ");
            Console.ResetColor();
            Console.WriteLine(ipInfo?.City);
            
            // self-explanatory
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("coords: ");
            Console.ResetColor();
            Console.WriteLine(ipInfo?.Loc);
            
            // self-explanatory
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("asn: ");
            Console.ResetColor();
            Console.WriteLine(ipInfo?.Org);
            
            // self-explanatory
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("zipcode: ");
            Console.ResetColor();
            Console.WriteLine(ipInfo?.Postal);
            
            // self-explanatory
            var coords = ipInfo?.Loc?.Split(',');
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("check out that place: ");
            Console.ResetColor();
            Console.WriteLine($"https://www.google.com/maps/?q={coords?[0]},{coords?[1]}");
            Console.ReadLine();
        }
        catch (HttpRequestException exception)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss} ERROR] {exception.Message}");  
            Console.ResetColor();                                                            
            Console.ReadLine();
        }
    }
    // todo fix this
    private static IPAddress GetIpAddressFromDomain(string domain)
    {
        var addresses = Dns.GetHostAddresses(domain);
        foreach (var address in addresses)
        {
            if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return address;
            }
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss} ERROR] Failed to resolve IP address for domain: {domain}");  // <---<  this snippet doesn't work
        Console.ResetColor();                                                             // <---<
        throw new Exception($"failed to resolve ip addr for domain: {domain}");    // <---<
        // [00:11:09 ERROR] failed to resolve ip addr for domain: google.com
        // expected output ^
        // the "ERROR" string has to be red, as you can see from the var.
        // my discord is @"fentables", as mentioned in 47:136
    }
}
