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
    [Route("Api/Branch")]
    public class BranchController : Controller
    {
        private readonly IDrejtimetRepository _repo;

        public BranchController(IDrejtimetRepository repository)
        {
            _repo = repository;
        }

        [Route("Index")]
        public async Task<IActionResult> Index(string token)
        {
            var branches = await _repo.GetAllAsync(StaticDetails.BranchesUrl + "/GetAll", token);
            if (branches != null)
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create(Drejtimet branch)
        {
            var success = await _repo.CreateAsync(StaticDetails.BranchesUrl,
                branch);
            return RedirectToAction("Index");
        }

        [Route("Edit/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var branch = await _repo.GetAsync(StaticDetails.BranchesUrl, id);
            return View(branch);
        }

        [Route("Edit/{id:int}")]
        public async Task<IActionResult> EditPut(Drejtimet branch)
        {
            var success = await _repo.UpdateAsync(StaticDetails.BranchesUrl +
                                                  $"/{branch.Id}", branch);
            return RedirectToAction("Index");
        }

        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _repo.DeleteAsync(StaticDetails.BranchesUrl + $"/{id}");
            return RedirectToAction("Index");
        }

        [Route("Details")]
        public async Task<IActionResult> Details(Drejtimet branch)
        {
            return View(branch);
        }
    }
}