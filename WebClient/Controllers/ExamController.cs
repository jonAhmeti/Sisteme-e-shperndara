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
    [Route("Api/Exam")]
    public class ExamController : Controller
    {
        private readonly IProvimetRepository _repo;
        private readonly ILendetRepository _repoSubject;

        public ExamController(IProvimetRepository repository, ILendetRepository repoSubject)
        {
            _repo = repository;
            _repoSubject = repoSubject;
        }

        [Route("Index")]
        public async Task<IActionResult> Index(string token)
        {
            var exams = await _repo.GetAllAsync(StaticDetails.ExamsUrl + "/GetAll", token);
            if (exams != null)
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [Route("Create")]
        public async Task<IActionResult> Create(string token)
        {
            ViewBag.Subjects = await _repoSubject.GetAllAsync(StaticDetails.SubjectsUrl + "/GetAll", token);
            return View();
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create(Provimet exam)
        {
            var success = await _repo.CreateAsync(StaticDetails.ExamsUrl,
                exam);
            return RedirectToAction("Index");
        }

        [Route("Edit/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id, string token)
        {
            var exam = await _repo.GetAsync(StaticDetails.ExamsUrl, id);
            ViewBag.Subjects = await _repoSubject.GetAllAsync(StaticDetails.SubjectsUrl + "/GetAll", token);
            ViewBag.Subject = await _repoSubject.GetAsync(StaticDetails.SubjectsUrl, (int) exam.LendaId);
            return View(exam);
        }

        [Route("Edit/{id:int}")]
        public async Task<IActionResult> EditPut(Provimet exam)
        {
            var success = await _repo.UpdateAsync(StaticDetails.ExamsUrl +
                                                  $"/{exam.Id}", exam);
            return RedirectToAction("Index");
        }

        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _repo.DeleteAsync(StaticDetails.ExamsUrl + $"/{id}");
            return RedirectToAction("Index");
        }

        [Route("Details")]
        public async Task<IActionResult> Details(Provimet exam)
        {
            ViewBag.Subject = await _repoSubject.GetAsync(StaticDetails.SubjectsUrl, (int)exam.LendaId);
            return View(exam);
        }
    }
}