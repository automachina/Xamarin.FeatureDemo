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
	public class ItemController : BaseController
	{
		public ItemController(IRepository _repo) : base(_repo)
		{
		}

		// GET: api/item
		[HttpGet]
		public IActionResult GetAllUsers()
		{
            return Ok(repo.GetItems());
		}

		// GET api/item/{Guid}
		[HttpGet("{id}")]
		public IActionResult GetItem(Guid id)
		{
            var item = repo.GetItem(id);
			if (item == null)
				return NotFound($"An Item with the Id {id} was not found!");

			return Ok(item);
		}

		// POST api/item
		[HttpPost]
		public IActionResult Post([FromBody]Item item)
		{
			var newItem = repo.AddItem(item);
			if (newItem == null)
                return BadRequest("The new Item was not added!");

			return Ok(newItem);
		}

		// PUT api/item/{Guid}
		[HttpPut("{id}")]
		public IActionResult Put(Guid id, [FromBody]Item item)
		{
            var updItem = repo.UpdateItem(item);
			if (updItem == null)
				return BadRequest($"An Item with the Id {item.Id} could not be found!");

			return Ok(updItem);
		}

		// DELETE api/item/{Guid}
		[HttpDelete("{id}")]
		public IActionResult Delete(Guid id)
		{
			if (!repo.DeleteItem(id))
			{
				return BadRequest($"An Item with the Id of {id} could not be found!");
			}
			return Ok();
		}
	}
}
