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
    [Route("Api/StudentExam")]
    public class StudentExamController : Controller
    {
        private readonly IProvimetStudenteveRepository _repo;
        private readonly IStudentetRepository _repoStudent;
        private readonly IProvimetRepository _repoExam;
        private readonly ILendetRepository _repoSubject;

        public StudentExamController(IProvimetStudenteveRepository repository,
            IStudentetRepository repoStudent, IProvimetRepository repoExam, ILendetRepository repoSubject)
        {
            _repo = repository;
            _repoStudent = repoStudent;
            _repoExam = repoExam;
            _repoSubject = repoSubject;
        }

        [Route("Index")]
        public async Task<IActionResult> Index(string token)
        {
            var studentExams = await _repo.GetAllAsync(StaticDetails.StudentExamsUrl + "/GetAll", token);
            if (studentExams != null)
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [Route("Create")]
        public async Task<IActionResult> Create(string token)
        {
            ViewBag.Students = await _repoStudent.GetAllAsync(StaticDetails.StudentsUrl + "/GetAll", token);
            ViewBag.Exams = await _repoExam.GetAllAsync(StaticDetails.ExamsUrl + "/GetAll", token);
            return View();
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create(ProvimetStudenteve exam)
        {
            var success = await _repo.CreateAsync(StaticDetails.StudentExamsUrl,
                exam);
            return RedirectToAction("Index");
        }

        [Route("Edit/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id, string token)
        {
            ViewBag.Students = await _repoStudent.GetAllAsync(StaticDetails.StudentsUrl + "/GetAll", token);
            ViewBag.Exams = await _repoExam.GetAllAsync(StaticDetails.ExamsUrl + "/GetAll", token);
            var exam = await _repo.GetAsync(StaticDetails.StudentExamsUrl, id);
            ViewBag.Student = await _repoStudent.GetAsync(StaticDetails.StudentsUrl, exam.StudentId);
            return View(exam);
        }

        [Route("Edit/{id:int}")]
        public async Task<IActionResult> EditPut(ProvimetStudenteve exam)
        {
            var success = await _repo.UpdateAsync(StaticDetails.StudentExamsUrl +
                                                  $"/{exam.Id}", exam);
            return RedirectToAction("Index");
        }

        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _repo.DeleteAsync(StaticDetails.StudentExamsUrl + $"/{id}");
            return RedirectToAction("Index");
        }

        [Route("Details")]
        public async Task<IActionResult> Details(ProvimetStudenteve exam)
        {
            ViewBag.Student = await _repoStudent.GetAsync(StaticDetails.StudentsUrl, exam.StudentId);
            ViewBag.Exam = await _repoExam.GetAsync(StaticDetails.ExamsUrl, exam.ProvimId);
            ViewBag.Subject = await _repoSubject.GetAsync(StaticDetails.SubjectsUrl, ViewBag.Exam.LendaId);
            return View(exam);
        }
    }
}