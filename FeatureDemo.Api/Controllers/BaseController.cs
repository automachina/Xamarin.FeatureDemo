using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeatureDemo.Api.Repository;
using FeatureDemo.Api.Utilities;
using Microsoft.AspNetCore.Hosting;
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

        internal (bool IsValid, string ErrorMessage) ValidateModel<T>(T model)
        {
			if (model == null || !ModelState.IsValid)
			{
                string modelErrors = $"Error: Invalid {typeof(T).Name} Model:\r\n";
                if (AppSettings.Environment != null && AppSettings.Environment.IsDevelopment())
                {
                    ModelState.ToList().ForEach(i =>
                    {
                        modelErrors += $"*\t{i.Key} : {i.Value.AttemptedValue}\r\n";
                        foreach (var err in i.Value.Errors)
                        {

                            modelErrors += string.IsNullOrEmpty(err.ErrorMessage) ? $"\t{err.Exception}" : $"\t*\t{err.ErrorMessage};\r\n\t{err.Exception}";
                        }
                    });
                }
                return (false,modelErrors);
			}
            return (true,null);
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

        internal void SetInstitutionId()
        {
            repo.InstitutionId = InstitutionId;    
        }
    }
}
