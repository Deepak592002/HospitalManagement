using System;
using System.Collections.Generic;

namespace HospitalManagement.Models.Domain;

public partial class InsuranceDatum
{
    public int InsuranceId { get; set; }

    public int? PatientId { get; set; }

    public string? InsuranceName { get; set; }

    public virtual PatientDatum Insurance { get; set; } = null!;
}
