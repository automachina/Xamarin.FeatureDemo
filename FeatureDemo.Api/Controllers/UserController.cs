using System;
using System.Collections.Generic;
using System.Linq;
using FeatureDemo.Api.Models;
using FeatureDemo.Api.Repository;
using FeatureDemo.Api.Utilities;
using FeatureDemo.Model.Client;
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
            SetInstitutionId();
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
            SetInstitutionId();
            var user = repo.GetUser(id);
            if (user == null)
                return NotFound($"A User with the Id {id} was not found!");

            return Ok(user);
		}

		// POST api/user
		[HttpPost]
		public IActionResult Post([FromBody]User user)
		{
            SetInstitutionId();
            var newUser = repo.AddUser(user);
            if (newUser == null)
                return BadRequest("The new User was not added!");

            return Ok(newUser);
		}

		// PUT api/user/{Guid}
		[HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]User user)
		{
            SetInstitutionId();
            var updUser = repo.UpdateUser(user);
            if (updUser == null)
                return BadRequest($"A User with the Id {user.Id} could not be found!");

            return Ok(updUser);
		}

		// DELETE api/user/{Guid}
		[HttpDelete("{id}")]
		public IActionResult Delete(Guid id)
		{
            SetInstitutionId();
			if (!repo.DeleteUser(id))
			{
				return BadRequest($"A User with the Id of {id} could not be found!");
			}
			return Ok();
		}

        // GET api/user/verify/{hash}
        [HttpGet("verify/{hash}")]
        public IActionResult Verify(string hash)
        {
            if (string.IsNullOrEmpty(hash)) return BadRequest(new StandardErrorResponse { Message = "Missing verification hash."});

            var validHashes = new List<string> {
                Hash.SHA1HashStringForUTF8String("8745619820110"), //87456 19820110 = BD3720A6B6048F0838AD1ECC813C273A0B066A77
                Hash.SHA1HashStringForUTF8String("8570219750821")  //85702 19750821 = 5D94F40F6738D353A2F033201A0C09FF12D7247C
            };

            var match = validHashes.SingleOrDefault(h => h.ToUpper() == hash.ToUpper());
            if (match == null) return BadRequest(new StandardErrorResponse { Message = "Verification hash not found." });

            return Ok(new VerificationResponse { Token = Crypto.EncryptString(match,AppSettings.ServerKey) });
        }

        //// POST api/user/create
        //[HttpPost("create")]
        //public IActionResult Create(ProfileCreateRequest profile)
        //{
        //    if (!ModelState.IsValid || profile == null) return BadRequest(new StandardErrorResponse { Message = "Missing or invalide profile object." });

        //    repo.CreateUser;
        //}
	}
}
