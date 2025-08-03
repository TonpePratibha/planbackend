using DataAcessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer
{
  public interface Iplanrepo
    {
        public List<PlanCrud> GetAllPlans();
        public PlanCrud GetPlanById(int id);
        public void AddPlan(PlanCrud plan);
        public void UpdatePlan(PlanCrud plan);
        public void DeletePlan(int id);
    }
}
