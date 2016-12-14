using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

using cityInfo.Models;

using System.Linq;
using Microsoft.AspNetCore.JsonPatch;
using cityInfo.Services;

namespace cityInfo.Controllers
{
    [RouteAttribute("api/cities")]
    public class PointsOfInterestController : Controller
    {

        private ILogger<PointsOfInterestController> _logger;
        private IMailService _localMailService;

        public PointsOfInterestController (ILogger<PointsOfInterestController> logger, IMailService localMailservice)
        {
            _logger = logger;
            _localMailService = localMailservice;

        }

        [HttpGetAttribute("{cityId}/pointsofinterest")]
        public IActionResult getPointsOfInterest(int cityId)
        {
            try
            {
                var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
                if(city == null)
                {
                    _logger.LogInformation($"City with an id of {cityId} wasn't found when accessing points of interest.");
                    return NotFound();
                }
                return Ok(city.PointsOfInterest);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception when trying to get city with id of {cityId}.", ex);
                return StatusCode(500, "A problem happened when executing the code.");
            }
        }

        [HttpGetAttribute("{cityId}/pointsofinterest/{pointOfInterestId}", Name = "GetPointOfInterest")]
        public IActionResult getPointOfInterest(int cityId, int pointOfInterestId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if(city == null)
            {
                return NotFound();
            }
            var pointOfInterest = city.PointsOfInterest.Find(p => p.Id == pointOfInterestId);
            if(pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(pointOfInterest);
        }

        [HttpPostAttribute("{cityId}/pointsofinterest")]
        public IActionResult CreatePointOfInterest(int cityId, 
            [FromBody]PointOfInterestForCreationDto pointOfInterest)
        {
            if(pointOfInterest == null)
            {
                return BadRequest();
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if(city == null)
            {
                return NotFound();
            }

            var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);

            var newPointOfInterest = new PointOfInterestDto()
            {
                Id = maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointsOfInterest.Add(newPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", new
            {cityId = cityId, pointOfInterestId = newPointOfInterest.Id}, newPointOfInterest);
            
        }

        [HttpPutAttribute("{cityId}/pointsofinterest/{id}")]
        public IActionResult UpdatePointOfInterest (int cityId, [FromBodyAttribute] PointOfInterestForUpdateDto pointOfInterest, int id) 
        {
            if(pointOfInterest == null)
            {
                return BadRequest();
            }

            if(pointOfInterest.Description == pointOfInterest.Name){
                ModelState.AddModelError("Description", "Name and Description must not match.");
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if(city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);

            if(pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            pointOfInterestFromStore.Name = pointOfInterest.Name;
            pointOfInterestFromStore.Description = pointOfInterest.Description;
            return NoContent();
        }

        [HttpPatchAttribute("{cityId}/pointsofinterest/{id}")]
        public IActionResult PartiallyUpdatedPointOfInterest(int cityId, int id,
        [FromBodyAttribute] JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc)
        {
            if(patchDoc == null)
            {
                return BadRequest("No Patch Doc");
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if(city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);

            if(pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
            {
                Name = pointOfInterestFromStore.Name,
                Description = pointOfInterestFromStore.Description
            };

            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(pointOfInterestToPatch.Description == pointOfInterestToPatch.Name){
                ModelState.AddModelError("Description", "Name and Description must not match.");
            }
            TryValidateModel(pointOfInterestToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

            return NoContent();

        }

        [HttpDeleteAttribute("{cityId}/pointsOfInterest/{id}")]
        public IActionResult DeletePointOfInterest (int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if(city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);

            if(pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            city.PointsOfInterest.Remove(pointOfInterestFromStore);

            _localMailService.Send("It worked", "The local service thingy worked which is really good news");

            return NoContent();
        }
    }
}
