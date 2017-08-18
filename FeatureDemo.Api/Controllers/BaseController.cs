using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeatureDemo.Api.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FeatureDemo.Api.Controllers
{
    public class BaseController : Controller
    {
        public readonly IRepository repo;
        public BaseController(IRepository _repo) : base()
        {
            repo = _repo;
        }
    }
}
