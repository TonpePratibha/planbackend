using DataAcessLayer;
using DataAcessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace plancrud.Controllers
{
    [Route("api/plans")]
    [ApiController]
    public class PlanController : ControllerBase
    {

            private readonly DataAcessLayer.Iplanrepo _planRepository; public PlanController(DataAcessLayer.Iplanrepo planRepository) { _planRepository = planRepository; }
            [HttpGet] public ActionResult<List<PlanCrud>> GetAll() { var plans = _planRepository.GetAllPlans(); return Ok(plans); }
            [HttpGet("{id}")] public ActionResult<PlanCrud> GetPlanById(int id) { var plan = _planRepository.GetPlanById(id); if (plan == null) { return NotFound(); } return Ok(plan); }
        [HttpPost]
        public IActionResult AddPlan([FromBody] PlanCrud planDto)
        {
            if (planDto == null)
                return BadRequest("Plan data is null.");

            // Call the repository to add the plan
            _planRepository.AddPlan(planDto); // This sets planDto.PlanTemplateCode internally

            // Get the newly created plan using the generated PlanTemplateCode
            var createdPlan = _planRepository.GetPlanById(planDto.PlanTemplateCode);

            if (createdPlan == null)
                return NotFound("Plan could not be retrieved after creation.");

            // Return 201 with the created plan
            return CreatedAtAction(nameof(GetPlanById), new { id = createdPlan.PlanTemplateCode }, createdPlan);
        }

        // [HttpPost] 
        // public ActionResult<PlanCrud> AddPlan([FromBody] Planmodel planDto) { if (planDto.ClassName == null || planDto.ClassName.Length != 1) return BadRequest("className must be a single character."); return Ok(planDto);  }

        [HttpPut("{id}")] 
            public IActionResult UpdatePlan(int id, [FromBody] PlanCrud plan) { if (id != plan.PlanTemplateCode) { return BadRequest(); } _planRepository.UpdatePlan(plan); return Ok(plan); }
        [HttpDelete("{id}")]
        public IActionResult DeletePlan(int id) { _planRepository.DeletePlan(id); return NoContent(); } } }
        

