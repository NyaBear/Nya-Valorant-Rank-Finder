using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CaptureOutput
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.Title = "Nya | Valorant Rank Finder";
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("  _   _\n | \\ | |_   _  __ _\n |  \\| | | | |/ _` |\n | |\\  | |_| | (_| |\n |_| \\_|\\__, |\\__,_|\n        |___/\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(" Enter the Username & Tag below Format = (XXX#XXX): \n [ > ] ");
            Console.ResetColor();
            var name = Console.ReadLine();
            var split = name.Split("#");
            var first = split[0].ToString();
            var url = "https://api.tracker.gg/api/v2/valorant/standard/matches/riot/" + split[0].ToString() + "%23" + split[1].ToString() + "?type=competitive";
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                var responseString = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    dynamic data = JObject.Parse(responseString);
                    string meta = string.Concat(data.data.matches[0].segments[0].stats.rank.metadata);
                    JObject jObject = JObject.Parse(meta);
                    string displayName = (string)jObject.SelectToken("tierName");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\n Rank: " + displayName + "\n\n Press any key to close the program: \n [ > ] ");
                    Console.ResetColor();
                    Console.ReadKey();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\n Profile is Invalid / Private / Error!\n\n Press any key to close the program: \n [ > ] ");
                    Console.ResetColor();
                    Console.ReadKey();
                }
            }
        }
    }
}