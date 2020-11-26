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
    [Route("Api/Subject")]
    public class SubjectController : Controller
    {
        private readonly ILendetRepository _repo;
        private readonly IProfesoretRepository _repoProfessor;
        private readonly IDrejtimetRepository _repoBranch;

        public SubjectController(ILendetRepository repository, IProfesoretRepository repoProfessor,
            IDrejtimetRepository repoBranch)
        {
            _repo = repository;
            _repoProfessor = repoProfessor;
            _repoBranch = repoBranch;
        }

        [Route("Index")]
        public async Task<IActionResult> Index(string token)
        {
            var subjects = await _repo.GetAllAsync(StaticDetails.SubjectsUrl + "/GetAll", token);
            if (subjects != null)
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [Route("Create")]
        public async Task<IActionResult> Create(string token)
        {
            ViewBag.Professors = await _repoProfessor.GetAllAsync(StaticDetails.ProfessorsUrl + "/GetAll", token);
            ViewBag.Branches = await _repoBranch.GetAllAsync(StaticDetails.BranchesUrl + "/GetAll", token);
            return View();
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create(Lendet subject)
        {
            var success = await _repo.CreateAsync(StaticDetails.SubjectsUrl,
                subject);
            return RedirectToAction("Index");
        }

        [Route("Edit/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id, string token)
        {
            ViewBag.Professors = await _repoProfessor.GetAllAsync(StaticDetails.ProfessorsUrl + "/GetAll", token);
            ViewBag.Branches = await _repoBranch.GetAllAsync(StaticDetails.BranchesUrl + "/GetAll", token);
            var subject = await _repo.GetAsync(StaticDetails.SubjectsUrl, id);
            return View(subject);
        }

        [Route("Edit/{id:int}")]
        public async Task<IActionResult> EditPut(Lendet subject)
        {
            var success = await _repo.UpdateAsync(StaticDetails.SubjectsUrl +
                                                  $"/{subject.Id}", subject);
            return RedirectToAction("Index");
        }

        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _repo.DeleteAsync(StaticDetails.SubjectsUrl + $"/{id}");
            return RedirectToAction("Index");
        }

        [Route("Details")]
        public async Task<IActionResult> Details(Lendet subject)
        {
            return View(subject);
        }
    }
}