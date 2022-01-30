using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAPICrudDemo.Models;
namespace WebAPICrudDemo.Controllers
{
    [EnableCors(origins: "http://localhost:58923/", headers: "*", methods: "*")]
    public class EmpProfileController : ApiController
    {
        [HttpGet]
        [Route("Emp/GetAll")]
        public HttpResponseMessage GetAll()
        {
            using (NordicEMSEntities dbContext = new NordicEMSEntities())
            {
                var result = dbContext.EmpProfiles.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        [HttpGet]
        [Route("Emp/GetById/{id}")]
        public HttpResponseMessage GetById(int id)
        {
            using (NordicEMSEntities dbContext = new NordicEMSEntities())
            {
                var result = dbContext.EmpProfiles.Where(x => x.EmpCode == id).FirstOrDefault();
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
        }

        [HttpPost]
        [Route("Emp/SaveEmp")]
        public HttpResponseMessage Post([FromBody] EmpProfile empProfile)
        {
            using (NordicEMSEntities dbContext = new NordicEMSEntities())
            {
                dbContext.EmpProfiles.Add(empProfile);
                dbContext.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Created);

            }
        }

        [HttpDelete]
        [Route("Emp/DeleteEmp/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            using (NordicEMSEntities dbContext = new NordicEMSEntities())
            {
                var emp = dbContext.EmpProfiles.Where(x => x.EmpCode == id).FirstOrDefault();
                dbContext.EmpProfiles.Remove(emp);
                dbContext.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Gone);

            }
        }

        [HttpPut]
        [Route("Emp/UpdateEmp")]
        public HttpResponseMessage Put([FromBody] EmpProfile empProfile)
        {
            using (NordicEMSEntities dbContext = new NordicEMSEntities())
            {
                var olEmp = dbContext.EmpProfiles.Where(x => x.EmpCode == empProfile.EmpCode).FirstOrDefault();
                olEmp.EmpCode = empProfile.EmpCode;
                olEmp.EmpName = empProfile.EmpName;
                olEmp.DateOfBirth = empProfile.DateOfBirth;
                olEmp.Email = empProfile.Email;
                olEmp.DeptCode = empProfile.DeptCode;
                dbContext.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Accepted);

            }
        }

    }
}
