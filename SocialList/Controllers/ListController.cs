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
            var tasks = await GetTasks();
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

            var requestUri = x;
            var response = await client.GetAsync(requestUri);
            IList<string> r = null;
            if (response.IsSuccessStatusCode)
            {
                r = await response.Content.ReadAsAsync<IList<string>>();
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