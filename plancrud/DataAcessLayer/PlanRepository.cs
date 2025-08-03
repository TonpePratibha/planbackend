using DataAcessLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer
{
  public class PlanRepository:Iplanrepo
    {
        
        
            public DBContext _context;
        public PlanRepository(DBContext context) {
            _context = context; }
            public List<PlanCrud> GetAllPlans() { 
            return _context.PlanCruds.FromSqlRaw("EXEC GetAllPlans").ToList(); }
          //  object Iplanrepo.GetAllPlans() {
           // return GetAllPlans(); } 
        //public PlanCrud GetPlanById(int id) {
       //     return _context.PlanCruds.FromSqlRaw("EXEC GetPlanById @Id", new SqlParameter("@Id", id)) .FirstOrDefault(); } 
       public PlanCrud GetPlanById(int id) {  return _context.PlanCruds.FromSqlRaw("EXEC GetPlanById @Id", new SqlParameter("@Id", id)).AsEnumerable().FirstOrDefault(); }
        //public object GetPlanById(int id) { return _context.PlanCruds .FromSqlRaw("EXEC GetPlanById @Id", new SqlParameter("@Id", id)).AsEnumerable() .FirstOrDefault(); } 
        public void AddPlan(PlanCrud plan) {  var idParam = new SqlParameter("@Id", SqlDbType.Int) { Direction = ParameterDirection.Output }; 
            var nameEnParam = new SqlParameter("@PlanNameEnglish", plan.PlanNameEnglish); var nameArParam = new SqlParameter("@PlanNameArabic", plan.PlanNameArabic); 
            var classNameParam = new SqlParameter("@ClassName", plan.ClassName); 
            var autoHdfParam = new SqlParameter("@IsAutoHDFLoad", plan.IsAutoHdfload); 
            var multipleSpouseParam = new SqlParameter("@MultipleSpouseAllowed", plan.MultipleSpouseAllowed); 
            var preExistingDiseaseParam = new SqlParameter("@PreExistingDiseaseAllowed", plan.PreExistingDiseaseAllowed); 
            var minAgeParam = new SqlParameter("@MinAgeLimit", plan.MinAgeLimit); 
            var maxAgeParam = new SqlParameter("@MaxAgeLimit", plan.MaxAgeLimit); 
            _context.Database.ExecuteSqlRaw( "EXEC Addpplan @PlanNameEnglish, @PlanNameArabic, @ClassName, @IsAutoHDFLoad, @MultipleSpouseAllowed, @PreExistingDiseaseAllowed, @MinAgeLimit, @MaxAgeLimit, @Id OUTPUT", nameEnParam, nameArParam, classNameParam, autoHdfParam, multipleSpouseParam, preExistingDiseaseParam, minAgeParam, maxAgeParam, idParam); _context.Database.ExecuteSqlRaw( "EXEC Addpplan @PlanNameEnglish, @PlanNameArabic, @ClassName, @IsAutoHDFLoad, @MultipleSpouseAllowed, @PreExistingDiseaseAllowed, @MinAgeLimit, @MaxAgeLimit, @Id OUTPUT", nameEnParam, nameArParam, classNameParam, autoHdfParam, multipleSpouseParam, preExistingDiseaseParam, minAgeParam, maxAgeParam, idParam);plan.PlanTemplateCode = (int)idParam.Value; }

        //public int AddPlan(Planmodel dto) { 
        //   var entity = new PlanCrud { PlanNameEnglish = dto.PlanNameEnglish, PlanNameArabic = dto.PlanNameArabic, ClassName = dto.ClassName, IsAutoHdfload = dto.IsAutoHdfload, MultipleSpouseAllowed = dto.MultipleSpouseAllowed, PreExistingDiseaseAllowed = dto.PreExistingDiseaseAllowed, MinAgeLimit = dto.MinAgeLimit, MaxAgeLimit = dto.MaxAgeLimit }; _context.PlanCruds.Add(entity); try { _context.SaveChanges(); }
        //   catch (DbUpdateException ex) { Exception inn = ex; while (inn.InnerException != null) inn = inn.InnerException; Console.WriteLine("Inner Exception: " + inn.Message); foreach (var entry in ex.Entries) { Console.WriteLine($"{entry.Entity.GetType().Name} in state {entry.State}"); } throw; } return entity.PlanTemplateCode; } 
          public void UpdatePlan(PlanCrud plan) { 
                var idParam = new SqlParameter("@Id", plan.PlanTemplateCode);
                var nameEnParam = new SqlParameter("@PlanNameEnglish", plan.PlanNameEnglish);
                var nameArParam = new SqlParameter("@PlanNameArabic", plan.PlanNameArabic); 
                var classNameParam = new SqlParameter("@ClassName", plan.ClassName); 
                var autoHdfParam = new SqlParameter("@IsAutoHDFLoad", plan.IsAutoHdfload); 
                var multipleSpouseParam = new SqlParameter("@MultipleSpouseAllowed", plan.MultipleSpouseAllowed);
                var preExistingDiseaseParam = new SqlParameter("@PreExistingDiseaseAllowed", plan.PreExistingDiseaseAllowed);
                var minAgeParam = new SqlParameter("@MinAgeLimit", plan.MinAgeLimit);
                var maxAgeParam = new SqlParameter("@MaxAgeLimit", plan.MaxAgeLimit); _context.Database.ExecuteSqlRaw( "EXEC UpdatePlan @Id, @PlanNameEnglish, @PlanNameArabic, @ClassName, @IsAutoHDFLoad, @MultipleSpouseAllowed, @PreExistingDiseaseAllowed, @MinAgeLimit, @MaxAgeLimit", idParam, nameEnParam, nameArParam, classNameParam, autoHdfParam, multipleSpouseParam, preExistingDiseaseParam, minAgeParam, maxAgeParam); }
           
     /*   public void UpdatePlan(PlanCrud plan)
        {
            if (plan.MinAgeLimit < 0 || plan.MaxAgeLimit < plan.MinAgeLimit)
            {
                throw new ArgumentException("Invalid age limits. MinAgeLimit must be >= 0 and MaxAgeLimit must be >= MinAgeLimit.");
            }

            var idParam = new SqlParameter("@PlanTemplateCode", plan.PlanTemplateCode);
            var nameEnParam = new SqlParameter("@PlanNameEnglish", plan.PlanNameEnglish);
            var nameArParam = new SqlParameter("@PlanNameArabic", plan.PlanNameArabic);
            var classNameParam = new SqlParameter("@ClassName", plan.ClassName);
            var autoHdfParam = new SqlParameter("@IsAutoHDFLoad", plan.IsAutoHdfload);
            var multipleSpouseParam = new SqlParameter("@MultipleSpouseAllowed", plan.MultipleSpouseAllowed);
            var preExistingDiseaseParam = new SqlParameter("@PreExistingDiseaseAllowed", plan.PreExistingDiseaseAllowed);
            var minAgeParam = new SqlParameter("@MinAgeLimit", plan.MinAgeLimit);
            var maxAgeParam = new SqlParameter("@MaxAgeLimit", plan.MaxAgeLimit);

            _context.Database.ExecuteSqlRaw(
                "EXEC UpdatePlan @PlanTemplateCode, @PlanNameEnglish, @PlanNameArabic, @ClassName, @IsAutoHDFLoad, @MultipleSpouseAllowed, @PreExistingDiseaseAllowed, @MinAgeLimit, @MaxAgeLimit",
                idParam, nameEnParam, nameArParam, classNameParam, autoHdfParam, multipleSpouseParam, preExistingDiseaseParam, minAgeParam, maxAgeParam
            );
        }
     */
        public void DeletePlan(int id) { 
            var idParam = new SqlParameter("@Id", id); _context.Database.ExecuteSqlRaw("EXEC DeletePlan @Id", idParam); } 

        }
}
