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
    [Route("Api/Professor")]
    public class ProfessorController : Controller
    {
        private readonly IProfesoretRepository _repo;

        public ProfessorController(IProfesoretRepository repository)
        {
            _repo = repository;
        }

        [Route("Index")]
        public async Task<IActionResult> Index(string token)
        {
            var professors = await _repo.GetAllAsync(StaticDetails.ProfessorsUrl + "/GetAll", token);
            if (professors != null)
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
        public async Task<IActionResult> Create(Profesoret professor)
        {
            var success = await _repo.CreateAsync(StaticDetails.ProfessorsUrl,
                professor);
            return RedirectToAction("Index");
        }

        [Route("Edit/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var professor = await _repo.GetAsync(StaticDetails.ProfessorsUrl, id);
            return View(professor);
        }

        [Route("Edit/{id:int}")]
        public async Task<IActionResult> EditPut(Profesoret professor)
        {
            var success = await _repo.UpdateAsync(StaticDetails.ProfessorsUrl +
                                                  $"/{professor.Id}", professor);
            return RedirectToAction("Index");
        }

        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _repo.DeleteAsync(StaticDetails.ProfessorsUrl + $"/{id}");
            return RedirectToAction("Index");
        }

        [Route("Details")]
        public async Task<IActionResult> Details(Profesoret professor)
        {
            return View(professor);
        }
    }
}