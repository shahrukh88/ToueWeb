using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public TourAPIController(ApplicationDbContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task <ActionResult <IEnumerable<CountryDTO>>>GetCountries()
        {
            IEnumerable<Country> countryList = await _db.Countries.ToListAsync();
            return Ok (_mapper.Map<List<CountryDTO>>(countryList));
        }

        [HttpGet("{id:int}",Name ="GetCountry")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public async Task<ActionResult <CountryDTO>> GetCountry(int id)
        {
            if(id==0)
            {
                return BadRequest();
            }

            var country = await _db.Countries.FirstOrDefaultAsync(u => u.Id == id);
            
            if (country == null)
            {
                return NotFound();
            }
            return Ok (_mapper.Map<CountryDTO>(country));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task <ActionResult<CountryDTO>> CreateCountry([FromBody]CountryCreateDTO createDTO)
        {

             if(await _db.Countries.FirstOrDefaultAsync(u=>u.Name.ToLower()==createDTO.Name.ToLower()) != null)
              
                {
                ModelState.AddModelError("CustomError", "Country already Exists");
                return BadRequest();
                }
            
            if (createDTO == null)
            {
                return BadRequest(createDTO);
            }

            Country model = _mapper.Map<Country>(createDTO);


      /* Country model = new()
            {
                Name = countryDTO.Name,
                Description = countryDTO.Description,
                Contenent = countryDTO.Contenent,
                ImageUrl = countryDTO.ImageUrl,
            }; */


            await _db.Countries.AddAsync(model);
           await _db.SaveChangesAsync();
            return CreatedAtRoute ("GetCountry", new { id = model.Id }, model);

        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpDelete("{id:int}",Name = "DeleteCountry")]

        public async Task <IActionResult> DeleteCountry(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var country = await _db.Countries.FirstOrDefaultAsync(u => u.Id == id);
            if(country == null)
            {
                return NotFound();
            }
            _db.Countries.Remove(country);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:int}",Name = "UpdateCountry")]
        public async Task<IActionResult>  UpdateCountry(int id, [FromBody]CountryUpdateDTO updateDTO)
        {
            if (updateDTO == null || id!=updateDTO.Id)
            {
                return BadRequest();
            }
            Country model = _mapper.Map<Country>(updateDTO);

            /*Country model = new()
            { 
                Id=countryDTO.Id,
                Name = countryDTO.Name,
                Description=countryDTO.Description,
                Contenent=countryDTO.Contenent,
                ImageUrl=countryDTO.ImageUrl
            };*/
            _db.Countries.Update(model);
           await _db.SaveChangesAsync();
            return NoContent();

        }

        [HttpPatch("{id:int}", Name = "UpdatePartialCountry")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task <IActionResult> UpdatePartialCountry(int id, JsonPatchDocument<CountryUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            var country = await _db.Countries.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

            CountryUpdateDTO countryDTO = _mapper.Map<CountryUpdateDTO>(country);

            //CountryUpdateDTO countryDTO = new()
            //{
            //    Id = country.Id,
            //    Name = country.Name,
            //    Description = country.Description,
            //    Contenent = country.Contenent,
            //    ImageUrl = country.ImageUrl
            //};

            if (country == null)
            {
                return BadRequest();
            }
               
            patchDTO.ApplyTo(countryDTO,ModelState);

            Country model = _mapper.Map<Country>(countryDTO);

            //Country model = new()
            //{
            //    Id = countryDTO.Id,
            //    Name = countryDTO.Name,
            //    Description = countryDTO.Description,
            //    Contenent = countryDTO.Contenent,
            //    ImageUrl = countryDTO.ImageUrl
            //};

            _db.Countries.Update(model);
           await _db.SaveChangesAsync();
            
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
 