using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Aircon.Business;
using Aircon.Common.Dto;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace aircon.Controllers
{
    [Route("api/[controller]")]
    public class NetPresentValueController : Controller
    {
        private readonly INetPresentValueBusiness _netPresentValueBusiness;

        public NetPresentValueController(INetPresentValueBusiness netPresentValueBusiness)
        {
            _netPresentValueBusiness = netPresentValueBusiness;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Json(await _netPresentValueBusiness.All());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _netPresentValueBusiness.DetailsById(id);
            if (result.Result == null)
                return NotFound();
            return Json(result);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]NpvCalculateParameter data)
        {
            var result = await _netPresentValueBusiness.Add(data);
            if (!result.IsSuccess)
                return StatusCode((int)HttpStatusCode.InternalServerError);
            return Json(result);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
