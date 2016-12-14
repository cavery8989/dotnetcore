using cityInfo.Entities;
using Microsoft.AspNetCore.Mvc;

namespace cityInfo.Controllers
{
     [Route("api/dummy")]
    public class DummyController : Controller
    {
        
         private CityInfoContext  _ctx;
        public DummyController(CityInfoContext ctx){
            _ctx = ctx;
        }

        [HttpGet()]
        public IActionResult GetCities()
        {
        
            return Ok();
        }
       

       
    }
}