using System.Diagnostics;
using Jojo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
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

 		
		[HttpPost]
		public async Task<IActionResult> SendMessage(ChatModel chat){

			using (var client = new HttpClient()){
				var request = new HttpRequestMessage{
					Method = HttpMethod.Post,
					RequestUri = new Uri("https://openai80.p.rapidapi.com/chat/completions"),
					Headers ={
						{ "X-RapidAPI-Key", "97a1b0f693mshc417452afb3f8bfp1c4f75jsn925e086ad1ac" },
						{ "X-RapidAPI-Host", "openai80.p.rapidapi.com" },
					},
					
					Content = new StringContent("{\n    \"model\": \"gpt-3.5-turbo\",\n    \"messages\": [\n        {\n            \"role\": \"user\",\n            \"content\": "+ $"\"{chat.message}\"" + "\n        }\n    ]\n}"){
						Headers ={
							ContentType = new MediaTypeHeaderValue("application/json")
						}
					}
				};

				using (var response = await client.SendAsync(request)){
					response.EnsureSuccessStatusCode();
					var body = await response.Content.ReadAsStringAsync();
					Console.WriteLine(body);
					return Ok(body);
				}

				Console.WriteLine(chat.message);
			}

			using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8)){  
				string content = reader.ReadToEndAsync().Result;
				//...
				Console.WriteLine(content);
			}
		}
 		              

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}