using System;
using System.Collections.Generic;

namespace HospitalManagement.Models.Domain;

public partial class PatientDatum
{
    public int PatientId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public int? Instance { get; set; }

    public bool? Status { get; set; }
}
