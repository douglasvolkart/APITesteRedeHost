using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APIDesafio.Models;
using Microsoft.AspNetCore.Cors;
using APIDesafio.Core;

namespace APIDesafio.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        static volatile public string dateSystem;

        static volatile public bool updated;

        // GET api/values
        [HttpGet]
        [EnableCors("AllowAllOrigins")]
        public JsonResult Get()
        {
            if (updated != true)
            {
                dateSystem = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            }

            var result = new { date = dateSystem };

            return Json(result);
            
        }

   


        // PUT api/values/
        [HttpPut]
        [EnableCors("AllowAllOrigins")]
        public JsonResult Put([FromBody]Data model)
        {
            var coreController = new CoreController();

            dateSystem = coreController.ChangeDate(model.date, model.op, model.value);

            updated = true;

            var result = new { date = dateSystem };

            return Json(result);
            
        }

        
    }
}
