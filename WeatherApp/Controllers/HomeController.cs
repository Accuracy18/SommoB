using System.Diagnostics;
using Jojo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Jojo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _client;

        public HomeController(ILogger<HomeController> logger, HttpClient client){
            _logger = logger;
            _client = client;
        }

        public IActionResult Index(){
            return View();
        }

        public IActionResult Privacy(){
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SendMessage(string lat, string lon){
            string x = "https://ai-weather-by-meteosource.p.rapidapi.com/current?lat={0}&lon=74.0060&timezone=auto&language=en&units=auto";
            var request = new HttpRequestMessage{
                Method = HttpMethod.Get,
                RequestUri = new Uri(String.Format(x, lat, lon)),
                Headers ={
                    { "X-RapidAPI-Key", "97a1b0f693mshc417452afb3f8bfp1c4f75jsn925e086ad1ac" },
                    { "X-RapidAPI-Host", "ai-weather-by-meteosource.p.rapidapi.com" }
                }
            };

            using (var response = await _client.SendAsync(request)){
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
                return Ok(body);
            }
        }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}