using Microsoft.AspNetCore.Mvc;

namespace cityInfo.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        
        
        [HttpGet()]
        public IActionResult GetCities()
        {
        
            return Ok(CitiesDataStore.Current.Cities);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int? id)
        {
            var cityToreturn = (CitiesDataStore.Current.Cities.Find(city => city.Id == id));
            if(cityToreturn == null){
                return NotFound();
            }

            return Ok(cityToreturn);
            
        }
    }

    
}