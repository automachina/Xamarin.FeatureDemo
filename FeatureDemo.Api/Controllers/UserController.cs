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
	public class UserController : BaseController
	{
		public UserController(IRepository _repo) : base(_repo)
		{
		}

		// GET: api/user
		[HttpGet]
		public IActionResult GetAllUsers()
		{
            return Ok(repo.GetUsers());
		}

		// GET: api/user/institution/{Guid}
		[HttpGet("institution/{id}")]
		public IActionResult GetInstitutionUsers(Guid id)
		{
            var users = repo.GetUsers(id);
            if (users == null)
                return NotFound($"The Institution with the Id {id} was not found!");

            return Ok(users);
		}

		// GET api/user/{Guid}
		[HttpGet("{id}")]
		public IActionResult GetUser(Guid id)
		{
            var user = repo.GetUser(id);
            if (user == null)
                return NotFound($"A User with the Id {id} was not found!");

            return Ok(user);
		}

		// POST api/user
		[HttpPost]
		public IActionResult Post([FromBody]User user)
		{
            var newUser = repo.AddUser(user);
            if (newUser == null)
                return BadRequest("The new User was not added!");

            return Ok(newUser);
		}

		// PUT api/user/{Guid}
		[HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]User user)
		{
            var updUser = repo.UpdateUser(user);
            if (updUser == null)
                return BadRequest($"A User with the Id {user.Id} could not be found!");

            return Ok(updUser);
		}

		// DELETE api/user/{Guid}
		[HttpDelete("{id}")]
		public IActionResult Delete(Guid id)
		{
			if (!repo.DeleteUser(id))
			{
				return BadRequest($"A User with the Id of {id} could not be found!");
			}
			return Ok();
		}
	}
}
