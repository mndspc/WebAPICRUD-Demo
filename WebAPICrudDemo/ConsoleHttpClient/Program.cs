using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHttpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            GetAll();
            Console.ReadLine();
        }

        static async Task CallPost()
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54954/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                //POST Method
                var EmpProfilePost = new EmpProfile() { EmpCode = 111, EmpName = "Donis", DateOfBirth = DateTime.Parse("01-01-1980"), Email = "donis@gmail.com", DeptCode = 100 };
                HttpResponseMessage responsePost = await client.PostAsJsonAsync("Emp/SaveEmp", EmpProfilePost);
                if (responsePost.IsSuccessStatusCode)
                {
                    // Get the URI of the created resource.
                    Uri returnUrl = responsePost.Headers.Location;
                    Console.WriteLine(returnUrl);
                }

            }

        }

        static async Task CallDelete()
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54954/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                //Delete Method
                int EmpCode = 111;
                HttpResponseMessage responseDelete = await client.DeleteAsync($"Emp/DeleteEmp/{EmpCode}");
                if (responseDelete.IsSuccessStatusCode)
                {
                    Console.WriteLine("Success");
                }
            }

        }

        static async Task CallUpdate()
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54954/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                ////PUT Method
                var EmpProfilePut = new EmpProfile() { EmpCode = 111, EmpName = "Donis123", DateOfBirth = DateTime.Parse("01-01-1980"), Email = "donis@gmail.com", DeptCode = 100 };
                HttpResponseMessage responsePut = await client.PutAsJsonAsync("Emp/UpdateEmp", EmpProfilePut);
                if (responsePut.IsSuccessStatusCode)
                {
                    Console.WriteLine("Success");
                }

            }

        }

        static async Task CallGetById()
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54954/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //GET Method
                int EmpCode = 101;
                HttpResponseMessage response = await client.GetAsync($"Emp/GetById/{EmpCode}");
                if (response.IsSuccessStatusCode)
                {
                    EmpProfile empProfile = await response.Content.ReadAsAsync<EmpProfile>();
                    Console.WriteLine($"{empProfile.EmpCode}\t{empProfile.EmpName}\t{empProfile.DateOfBirth}\t{empProfile.Email}\t{empProfile.DeptCode}");

                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }


            }

        }

        static async Task GetAll()
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
                    foreach (var empProfile in empProfiles)
                    {
                        Console.WriteLine($"{empProfile.EmpCode}\t{empProfile.EmpName}\t{empProfile.DateOfBirth}\t{empProfile.Email}\t{empProfile.DeptCode}");
                    }

                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }


            }

        }
    }
}
