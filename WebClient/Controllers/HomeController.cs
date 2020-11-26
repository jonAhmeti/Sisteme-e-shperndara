using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebClient.Models;
using WebClient.Repository.IRepository;

namespace WebClient.Controllers
{
    public interface IAuthenticateRepo
    {
        Task<string> GetUserToken(string url, User user);
        Task<User> GetUser(User user);
    }
    public class AuthenticateRepo : IAuthenticateRepo
    {
        private readonly IHttpClientFactory _clientFactory;

        public AuthenticateRepo(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        //////////////////DIVIDE
        ///
        //////////////////////////
        public async Task<string> GetUserToken(string url, User user)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            if (user != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8,
                    "application/json");
            }
            else
                return "Failed: User not found";

            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return "Failed.";
        }

        public async Task<User> GetUser(User user)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, StaticDetails.UsersUrl
                                                                 + "/GetUser");

            if (user != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8,
                    "application/json");
            }
            else
                return null;

            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
            }

            return null;
        }
    }

    [Route("Home")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthenticateRepo _repoAuth;

        public HomeController(ILogger<HomeController> logger, IAuthenticateRepo repoAuth)
        {
            _logger = logger;
            _repoAuth = repoAuth;
        }

        [Route("Index")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Index")]
        [HttpPost]
        public async Task<IActionResult> IndexToken(User user)
        {
            var tokenUser = await _repoAuth.GetUser(user);
            string token = await _repoAuth.GetUserToken(StaticDetails.AuthenticateUrl, user);
            tokenUser.Token = token;
            return View(tokenUser);
        }


        [Route("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
