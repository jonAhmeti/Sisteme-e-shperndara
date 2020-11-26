using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebClient.Models;
using WebClient.Repository.IRepository;

namespace WebClient.Controllers
{
    [Controller]
    [Route("Api")]
    public class ApiController : Controller
    {
        private readonly IStudentetRepository _repo;

        public ApiController(IStudentetRepository repository)
        {
            _repo = repository;
        }

        [Route("Index")]
        public IActionResult Index(User user)
        {
            return View(user);
        }

    }
}