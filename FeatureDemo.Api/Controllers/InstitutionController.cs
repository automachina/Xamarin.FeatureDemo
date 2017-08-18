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
	public class InstitutionController : BaseController
	{
		public InstitutionController(IRepository _repo) : base(_repo)
		{
		}

		// GET: api/institution
		[HttpGet]
		public IActionResult GetAll()
		{
            return Ok(repo.GetInstitutions());
		}

		// GET api/institution/{Guid}
		[HttpGet("{id}")]
		public IActionResult Get(Guid id)
		{
            var inst = repo.GetInstitution(id);
			if (inst == null)
				return NotFound($"An Institution with the Id {id} was not found!");

			return Ok(inst);
		}

		// POST api/institution
		[HttpPost]
        public IActionResult Post([FromBody]Institution inst)
		{
            var newInst = repo.AddInstitution(inst);
			if (newInst == null)
				return BadRequest("The new Institution was not added!");

			return Ok(newInst);
		}

		// PUT api/institution/{Guid}
		[HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]Institution inst)
		{
            var updInst = repo.UpdateInstitution(inst);
			if (updInst == null)
				return BadRequest($"An Institution with the Id {inst.Id} could not be found!");

			return Ok(updInst);
		}

		// DELETE api/institution/{Guid}
		[HttpDelete("{id}")]
		public IActionResult Delete(Guid id)
		{
            if (!repo.DeleteInstitution(id))
			{
				return BadRequest($"An Institution with the Id of {id} could not be found!");
			}
			return Ok();
		}
	}


}
