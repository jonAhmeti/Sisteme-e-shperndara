using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using WebClient.Models;
using WebClient.Repository.IRepository;

namespace WebClient.Controllers
{
    [Controller]
    [Route("Api/Student")]
    public class StudentController : Controller
    {
        private readonly IStudentetRepository _repo;
        private readonly IDrejtimetRepository _repoBranch;
        private readonly IStatusetRepository _repoStatus;
        private readonly IAuthenticateRepo _repoUser;

        public StudentController(IStudentetRepository repository, IDrejtimetRepository repoBranch,
            IStatusetRepository repoStatus, IAuthenticateRepo repoUser)
        {
            _repo = repository;
            _repoBranch = repoBranch;
            _repoStatus = repoStatus;
            _repoUser = repoUser;
        }

        [Route("Index")]
        public async Task<IActionResult> Index(string token)
        {
            
            var students = await _repo.GetAllAsync(StaticDetails.StudentsUrl + "/GetAll", token);
            if (students != null)
            {
                ViewBag.Token = token;
                return View(students);
            }

            return RedirectToAction("Index", "Home");
        }


        [Route("Create")]
        public async Task<IActionResult> Create(string token)
        {
            ViewBag.Statuses = await _repoStatus.GetAllAsync(StaticDetails.StatusesUrl + "/GetAll", token);
            ViewBag.Branches = await _repoBranch.GetAllAsync(StaticDetails.BranchesUrl + "/GetAll", token);
            return View();
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create(Studentet student, string token)
        {
            student.CreateDate = DateTime.Now;
            student.UpdateDate = DateTime.Now;
            var success = await _repo.CreateAsync(StaticDetails.StudentsUrl,
                student);
            return RedirectToAction("Index");
        }

        [Route("Edit/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id, string token)
        {
            ViewBag.Statuses = await _repoStatus.GetAllAsync(StaticDetails.StatusesUrl + "/GetAll", token);

            var student = await _repo.GetAsync(StaticDetails.StudentsUrl, id);
            ViewBag.Branches = await _repoBranch.GetAllAsync(StaticDetails.BranchesUrl + "/GetAll", token);
            return View(student);
        }

        [Route("Edit/{id:int}")]
        public async Task<IActionResult> EditPut(Studentet student)
        {
            var success = await _repo.UpdateAsync(StaticDetails.StudentsUrl +
                                                  $"/{student.Id}", student);
            return RedirectToAction("Index");
        }

        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _repo.DeleteAsync(StaticDetails.StudentsUrl + $"/{id}");
            return RedirectToAction("Index");
        }

        [Route("Details")]
        public async Task<IActionResult> Details(Studentet student)
        {
            return View(student);
        }
    }
}