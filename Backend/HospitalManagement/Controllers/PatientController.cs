using HospitalManagement.Data;
using HospitalManagement.Models.Domain;
using HospitalManagement.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly HospitalManagementContext dbContext;
        public PatientController(HospitalManagementContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PatientDto>> GetStudents()
        {
            var patient=dbContext.PatientData.ToList();
            var patients = new List<PatientDto>();
            foreach (var pat in patient) {
                patients.Add(new PatientDto
                {
                    PatientId = pat.PatientId,
                    Name = pat.Name,
                    Email = pat.Email,
                    Instance = pat.Instance,
                    Status = pat.Status,
                });
            }
            return Ok(patients);
        }
    }
}
