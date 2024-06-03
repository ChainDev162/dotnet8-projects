// works, but not as i intended it to.
using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace login2token_experimental;

internal abstract class Program
{
    private static readonly HttpClient Client = new HttpClient();
    private static readonly ILogger<Program> Logger;

    static Program()
    {
        using var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });
        Logger = loggerFactory.CreateLogger<Program>();
    }

    private static async Task Main()
    {
        Logger.LogInformation("Program started");

        const string multiLineString = """
                                        _        _______  _______ _________ _        _______ _________ _______  _        _______  _       
                                       ( \      (  ___  )(  ____ \\__   __/( (    /|/ ___   )\__   __/(  ___  )| \    /\(  ____ \( (    /|
                                       | (      | (   ) || (    \/   ) (   |  \  ( |\/   )  |   ) (   | (   ) ||  \  / /| (    \/|  \  ( |
                                       | |      | |   | || |         | |   |   \ | |    /   )   | |   | |   | ||  (_/ / | (__    |   \ | |
                                       | |      | |   | || | ____    | |   | (\ \) |  _/   /    | |   | |   | ||   _ (  |  __)   | (\ \) |
                                       | |      | |   | || | \_  )   | |   | | \   | /   _/     | |   | |   | ||  ( \ \ | (      | | \   |
                                       | (____/\| (___) || (___) |___) (___| )  \  |(   (__/\   | |   | (___) ||  /  \ \| (____/\| )  \  |
                                       (_______/(_______)(_______)\_______/|/    )_)\_______/   )_(   (_______)|_/    \/(_______/|/    )_)
                                       """;

        Console.Clear();
        Console.WriteLine(multiLineString);

        Console.Write("enter email: ");
        var email = Console.ReadLine();
        Console.Write("enter password: ");
        var pass = Console.ReadLine();

        Logger.LogInformation("Making request...");
        await GetToken(email, pass);
    }

    private static async Task GetToken(string? email, string? pass)
    {
        const string url = "https://discord.com/api/v9/auth/login";
        var payload = new Dictionary<string, string?>()
        {
            { "login", email }, { "password", pass }
        };
        var jsonPayload = JsonConvert.SerializeObject(payload);

        using var stringContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        var response = await Client.PostAsync(url, stringContent);
        if (response.IsSuccessStatusCode)
        {
            Logger.LogInformation("Request made successfully");

            var responseContent = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(responseContent);

            Logger.LogInformation("uid: ");
            Console.WriteLine(json["user_id"]);
            Logger.LogInformation("token: ");
            Console.WriteLine(json["token"]);
        }
        else
        {
            Logger.LogError("Error: {Reason}", response.ReasonPhrase);
        }
    }
}
