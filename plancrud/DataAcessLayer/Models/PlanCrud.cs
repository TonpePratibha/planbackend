using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAcessLayer.Models;

[Table("PlanCrud")]
public partial class PlanCrud
{
    [Key]
    public int PlanTemplateCode { get; set; }

    [StringLength(200)]
    public string PlanNameEnglish { get; set; } = null!;

    [StringLength(200)]
    public string PlanNameArabic { get; set; } = null!;

    [StringLength(1)]
    [Unicode(false)]
    public string ClassName { get; set; } = null!;

    [Column("IsAutoHDFLoad")]
    public bool IsAutoHdfload { get; set; }

    public bool MultipleSpouseAllowed { get; set; }

    public bool PreExistingDiseaseAllowed { get; set; }

    public int MinAgeLimit { get; set; }

    public int MaxAgeLimit { get; set; }
}
