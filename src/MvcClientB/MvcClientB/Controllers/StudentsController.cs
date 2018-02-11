using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MvcClientB.Entities;
using MvcClientB.HttpHelpers;
using MvcClientB.Models;
using Newtonsoft.Json;

namespace MvcClientB.Controllers
{
    public class StudentsController : Controller
    {
        private readonly HttpClient _httpClient;

        string requestEndpoint = "api/students/";

        public StudentsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));      

        }

        //Getting all Students
        [HttpGet]
        public async Task<IActionResult> Index()
        {     
            var accesstoken = await HttpContext.GetTokenAsync("access_token");
           _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);       
           _httpClient.SetBearerToken(accesstoken);

            HttpResponseMessage httpResponse = _httpClient.GetAsync(requestEndpoint).Result;
           
            if (httpResponse.IsSuccessStatusCode)
            {
                List<Student> students = await httpResponse.Content.ReadAsJsonAsync<List<Student>>();
                return View(students);
            }

            var error = httpResponse.ReasonPhrase;
            return Content(error + " - " + "Could not get students. Please try again");

        }

        //Getting the view first
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        //Adding student data to the view
        [HttpPost]
        public async Task<ActionResult> Create(Student student)
        {
         
            var accesstoken = await HttpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);
            _httpClient.SetBearerToken(accesstoken);
           


            string stringData = JsonConvert.SerializeObject(student);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await _httpClient.PostAsync(requestEndpoint, contentData);

            if (httpResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var error = httpResponse.ReasonPhrase;
            return Content(error + " - " + "Could not add student. Please try again");

        }

        //Getting the student
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {

            var accesstoken = await HttpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);
            _httpClient.SetBearerToken(accesstoken);

            HttpResponseMessage httpResponse = await _httpClient.GetAsync(requestEndpoint + id);

            if (httpResponse.IsSuccessStatusCode)
            {
                Student student = await httpResponse.Content.ReadAsJsonAsync<Student>();
                return View(student);
            }

            var error = httpResponse.ReasonPhrase;
            return Content(error + " - " + "Could not get student. Please try again");

        }
        //Posting(aka Put) the updated Student
        [HttpPost]
        public async Task<ActionResult> Edit(Student student)
        {

            var accesstoken = await HttpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);
            _httpClient.SetBearerToken(accesstoken);


            string stringData = JsonConvert.SerializeObject(student);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await _httpClient.PutAsync(requestEndpoint + student.ID, contentData);

            if (httpResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var error = httpResponse.ReasonPhrase;
            return Content(error + " - " + "Could not save changes made to student. Please try again");

        }
        //Getting the student to delete
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {

            var accesstoken = await HttpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);
            _httpClient.SetBearerToken(accesstoken);

            HttpResponseMessage httpResponse = await _httpClient.GetAsync(requestEndpoint + id);

            if (httpResponse.IsSuccessStatusCode)
            {
                Student student = await httpResponse.Content.ReadAsJsonAsync<Student>();
                return View(student);
            }

            var error = httpResponse.ReasonPhrase;
            return Content(error + " - " + "Could not get student. Please try again");

        }
        //Deleting the student
        [HttpPost]
        public async Task<ActionResult> Delete(int id, Student student)
        {

            var accesstoken = await HttpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);
            _httpClient.SetBearerToken(accesstoken);

            HttpResponseMessage httpResponse = await _httpClient.DeleteAsync(requestEndpoint + id);

            if (httpResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var error = httpResponse.ReasonPhrase;
            return Content(error + " - " + "Could not delete student. Please try again");

        }

        //Getting the student
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {

            var accesstoken = await HttpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);
            _httpClient.SetBearerToken(accesstoken);

            HttpResponseMessage httpResponse = await _httpClient.GetAsync(requestEndpoint + id);

            if (httpResponse.IsSuccessStatusCode)
            {
                Student student = await httpResponse.Content.ReadAsJsonAsync<Student>();
                return View(student);
            }

            var error = httpResponse.ReasonPhrase;
            return Content(error + " - " + "Could not get details of student. Please try again");

        }

    }

}