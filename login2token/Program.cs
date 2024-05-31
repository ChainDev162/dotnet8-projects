using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace things
{
    internal abstract class Program
    {
        private static async Task Main()
        {
            Console.WriteLine("enter email: ");
            var email = Console.ReadLine();
            Console.WriteLine("enter password: ");
            var pass = Console.ReadLine();
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
                Console.WriteLine("\nlogin successful");
                var responseContent = await response.Content.ReadAsStringAsync();
                // Console.WriteLine(responseContent); // this has the full json
                var json = JObject.Parse(responseContent);
                Console.WriteLine($"uid: {json["user_id"]}");
                Console.WriteLine($"token: {json["token"]}");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine(response.ReasonPhrase);
            }
        }
    }
}
