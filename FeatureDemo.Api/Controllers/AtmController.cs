using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeatureDemo.Api.Models;
using FeatureDemo.Api.Repository;
using Microsoft.AspNetCore.Mvc;


namespace FeatureDemo.Api.Controllers
{
    [Route("api/[controller]")]
    public class AtmController : BaseController
    {
        public AtmController(IRepository _repo) : base(_repo)
        {
        }

        // GET: api/atm
        [HttpGet]
        public IActionResult GetAllAtms()
        {
            SetInstitutionId();
            return Ok(repo.GetAtms());
        }

		// GET: api/atm/institution/{Guid}
		[HttpGet("institution/{id}")]
		public IActionResult GetInstitutionAtms(Guid id)
		{
            var atms = repo.GetAtms(id);
			if (atms == null)
				return NotFound($"The Institution with the Id {id} was not found!");

			return Ok(atms);
		}

		// GET api/atm/{Guid}
		[HttpGet("{id}")]
		public IActionResult GetAtm(Guid id)
		{
            SetInstitutionId();
            var atm = repo.GetAtm(id);
			if (atm == null)
				return NotFound($"An Atm with the Id {id} was not found!");

			return Ok(atm);
		}

		// POST api/atm
		[HttpPost]
        public IActionResult Post([FromBody]Atm atm)
		{
            var modelStatus = ValidateModel(atm);
            if (!modelStatus.IsValid) return BadRequest(modelStatus.ErrorMessage);
            
            SetInstitutionId();
            var newAtm = repo.AddAtm(atm);
			if (newAtm == null)
				return BadRequest("The new Atm was not added!");

			return Ok(newAtm);
		}

		// PUT api/atm/{Guid}
		[HttpPut("{id}")]
		public IActionResult Put(Guid id, [FromBody]Atm atm)
		{
			var modelStatus = ValidateModel(atm);
			if (!modelStatus.IsValid) return BadRequest(modelStatus.ErrorMessage);

            SetInstitutionId();
            var updAtm = repo.UpdateAtm(atm);
			if (updAtm == null)
				return BadRequest($"An Atm with the Id {atm.Id} could not be found!");

			return Ok(updAtm);
		}

        // DELETE api/atm/{Guid}
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            SetInstitutionId();
            if (!repo.DeleteAtm(id))
            {
                return BadRequest($"An Atm with an Id of {id} could not be found!");
            }
            return Ok();
        }
    }
}
