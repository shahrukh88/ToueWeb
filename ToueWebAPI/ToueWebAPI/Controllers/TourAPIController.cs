using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using ToueWebAPI.Data;
using ToueWebAPI.Models;
using ToueWebAPI.Models.DTO;

namespace ToueWebAPI.Controllers
{
    [Route("api/TourAPI")]
    [ApiController]
    public class TourAPIController : ControllerBase
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public ActionResult <IEnumerable<CountryDTO>>GetCountries()
        {

            return Ok (CountryStore.countryList);
        }

        [HttpGet("{id:int}",Name ="GetCountry")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public ActionResult <CountryDTO> GetCountry(int id)
        {
            if(id==0)
            {
                return BadRequest();
            }

            var country = CountryStore.countryList.FirstOrDefault(u => u.Id == id);
            
            if (country == null)
            {
                return NotFound();
            }
            return Ok (country);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<CountryDTO> CreateCountry([FromBody]CountryDTO countryDTO)
        {

             if(CountryStore.countryList.FirstOrDefault(u=>u.Name.ToLower()==countryDTO.Name.ToLower()) != null)
              
                {
                ModelState.AddModelError("CustomError", "Country already Exists");
                return BadRequest();
                }
            
            if (countryDTO == null)
            {
                return BadRequest(countryDTO);
            }
            
            if(countryDTO.Id>0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            countryDTO.Id = CountryStore.countryList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;

            CountryStore.countryList.Add(countryDTO);

            return CreatedAtRoute ("GetCountry", new { id = countryDTO.Id }, countryDTO);

        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpDelete("{id:int}",Name = "DeleteCountry")]

        public IActionResult DeleteCountry(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var country = CountryStore.countryList.FirstOrDefault(u => u.Id == id);
            if(country == null)
            {
                return NotFound();
            }
            CountryStore.countryList.Remove(country);
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:int}",Name = "UpdateCountry")]
        public IActionResult UpdateCountry(int id, [FromBody]CountryDTO countryDTO)
        {
            if (countryDTO == null || id!=countryDTO.Id)
            {
                return BadRequest();
            }
            var country = CountryStore.countryList.FirstOrDefault(u => u.Id == id);
            country.Name = countryDTO.Name;

            return NoContent();

        }

        [HttpPatch("{id:int}", Name = "UpdatePartialCountry")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdatePartialCountry(int id, JsonPatchDocument<CountryDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var country = CountryStore.countryList.FirstOrDefault(u => u.Id == id);
            if (country == null)
            {
                return BadRequest();
            }
               
            patchDTO.ApplyTo(country,ModelState);
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
 