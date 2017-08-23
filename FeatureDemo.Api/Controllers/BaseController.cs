using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeatureDemo.Api.Repository;
using FeatureDemo.Api.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace FeatureDemo.Api.Controllers
{
    public class BaseController : Controller
    {
        public readonly IRepository repo;
        public BaseController(IRepository _repo) : base()
        {
            repo = _repo;
        }

		internal Guid? InstitutionId
		{
			get
			{
                if (HttpContext != null && HttpContext.Request.Headers.ContainsKey(AppSettings.InstitutionHeader) && Guid.TryParse(HttpContext.Request.Headers["Institution"].ToString(), out Guid id))
                {
                    return id;
                }
                return null;
			}
		}
    }
}
