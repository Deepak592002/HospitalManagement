namespace HospitalManagement.Models.Dto
{
    public class PatientDto
    {
        public int PatientId { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public int? Instance { get; set; }

        public bool? Status { get; set; } 
    }
}
