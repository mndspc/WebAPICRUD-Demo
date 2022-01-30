using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MVCHttpClient.Models;
namespace MVCHttpClient.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54954/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //GET Method
                HttpResponseMessage response = await client.GetAsync("Emp/GetAll");
                if (response.IsSuccessStatusCode)
                {
                    var empProfiles = await response.Content.ReadAsAsync<EmpProfile[]>();
                    return View(empProfiles);
                }
                else
                {
                    return Content("No Data");
                }
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetById(int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54954/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //GET Method
                HttpResponseMessage response = await client.GetAsync($"Emp/GetById/{id}");
                if (response.IsSuccessStatusCode)
                {
                    EmpProfile empProfile = await response.Content.ReadAsAsync<EmpProfile>();

                    return View(empProfile);
                }
                else
                {
                    return Content("Employee Does not Exist");
                }

            }
        }

        [HttpPost]
        public async Task<ActionResult> SaveEmp(EmpProfile empProfile)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54954/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                //POST Method
                HttpResponseMessage responsePost = await client.PostAsJsonAsync("Emp/SaveEmp", empProfile);
                if (responsePost.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAll");

                }
                else
                {
                    return View("SaveEmp");
                }

            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteEmp(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54954/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                //Delete Method
                int EmpCode = 111;
                HttpResponseMessage responseDelete = await client.DeleteAsync($"Emp/DeleteEmp/{id}");
                if (responseDelete.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAll");
                }
                else
                {
                    return View("DeleteEmp");
                }
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateEmp(EmpProfile empProfile)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54954/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                ////PUT Method
                HttpResponseMessage responsePut = await client.PutAsJsonAsync("Emp/UpdateEmp", empProfile);
                if (responsePut.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAll");
                }
                else{
                    return View("UpdateEmp");
                }

            }
        }
    }
}
