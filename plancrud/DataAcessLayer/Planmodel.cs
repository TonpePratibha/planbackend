using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer
{
 public  class Planmodel
    {
        public string PlanNameEnglish { get; set; } = null!; 
        public string PlanNameArabic { get; set; } = null!;

        public string ClassName { get; set; } = null!;
  
        public bool IsAutoHdfload { get; set; }

        public bool MultipleSpouseAllowed { get; set; }

        public bool PreExistingDiseaseAllowed { get; set; }

        public int MinAgeLimit { get; set; }

        public int MaxAgeLimit { get; set; }
    }
}
