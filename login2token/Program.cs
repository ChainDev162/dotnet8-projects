using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace things
{
    internal abstract class Program
    {
        private static async Task Main()
        {
            Console.Title = "login2token";
            Console.ForegroundColor = ConsoleColor.Cyan;
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
            Console.ResetColor();
            
            Console.Write("enter email: ");
            var email = Console.ReadLine();
            Console.Write("enter password: ");
            var pass = Console.ReadLine();
            
            var currentTimeframes = DateTime.Now;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("[" + currentTimeframes.ToString("HH:mm:ss") + " INFO]");
            Console.ResetColor();
            Console.WriteLine(" making request...");
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
            var client = new HttpClient();
            var stringContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, stringContent);
            if (response.IsSuccessStatusCode)
            {
                var now = DateTime.Now;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("[" + now.ToString("HH:mm:ss") + " SUCCESS]");
                Console.ResetColor();
                Console.WriteLine(" request made successfully");
                var responseContent = await response.Content.ReadAsStringAsync();
                // Console.WriteLine(responseContent); // this has the full json
                var json = JObject.Parse(responseContent);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("uid: ");
                Console.ResetColor();
                Console.WriteLine(json["user_id"]);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("token: ");
                Console.ResetColor();
                Console.WriteLine(json["token"]);
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine(response.ReasonPhrase);
            }
        }
    }
}
