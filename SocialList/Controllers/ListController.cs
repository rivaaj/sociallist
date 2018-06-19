using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SocialList.Controllers
{
    public class ListController : Controller
    {
        static HttpClient client = new HttpClient();
        private readonly ILogger<ListController> log;

        public ListController(ILogger<ListController> logger)
        {
            this.log = logger;
        }

        public async Task<IActionResult> Index()
        {
            var hostName = Environment.GetEnvironmentVariable("COMPUTERNAME") ?? Environment.GetEnvironmentVariable("HOSTNAME");
    

            var tasks = await GetTasks();
            tasks.Insert(0, $"Web App Host Name={hostName}");

            tasks.Insert(0, $"Web App List Rendered at {DateTime.Now}");
            ViewData["Tasks"] = tasks;

            
            return View(tasks);
        }

        private async Task<IList<string>> GetTasks()
        {
            var x = Environment.GetEnvironmentVariable("BACKEND_URL");
            log.LogInformation("BACKEND_URL={0}", x);
            
            if (String.IsNullOrEmpty(x))
            {
                log.LogError("BACKEND_URL not set");
                return new List<string> { "BACKEND_URL not set" };
            }
            IList<string> r = new List<string>();
            r.Insert(0, $"BACKEND_URL={x}");


            try
            {
                var requestUri = x;
                var response = await client.GetAsync(requestUri);
                
                if (response.IsSuccessStatusCode)
                {
                    r = await response.Content.ReadAsAsync<IList<string>>();
                    r.Insert(0, $"BACKEND_URL={x}");
                }
                else
                {
                    log.LogError("Request to {0} failed with status code {1}", x, response.StatusCode);
                    r.Insert(0, $"Request failed with status {response.StatusCode}");
                }
            } catch(Exception ex)
            {
                log.LogError(ex, "Request failed");
                r.Insert(0, $"Request failed with exception {ex.Message}");
            }

            
            return r;

        }

        [HttpPost]
        public IActionResult AddTask()
        {
            return RedirectToAction("Index");
        }
    }
}