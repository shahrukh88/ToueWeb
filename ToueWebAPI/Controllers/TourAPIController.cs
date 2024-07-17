using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Net;
using ToueWebAPI.Data;
using ToueWebAPI.Models;
using ToueWebAPI.Models.DTO;
using ToueWebAPI.Repository.IRepository;

namespace ToueWebAPI.Controllers
{
    [Route("api/TourAPI")]
    [ApiController]
    public class TourAPIController : ControllerBase
    {

        protected APIResponse _response;
        private readonly ICountryRepository _dbCountry;
        private readonly IMapper _mapper;
        
        
        public TourAPIController(ICountryRepository dbVilla,IMapper mapper)
        {
            _dbCountry = dbVilla;
            _mapper = mapper;
            this._response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task <ActionResult <APIResponse>>GetCountries()
        {
            try {
                IEnumerable<Country> countryList = await _dbCountry.GetAllAsync();
                _response.Result = _mapper.Map<List<CountryDTO>>(countryList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch(Exception ex) {
               
                _response.IsSuccess=false;
                _response.ErrorMessages=new List<string>() { ex.ToString()};
            }
            return _response;
        }

        [HttpGet("{id:int}",Name ="GetCountry")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public async Task<ActionResult <APIResponse>> GetCountry(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                var country = await _dbCountry.GetAsync(u => u.Id == id);

                if (country == null)
                {
                    return NotFound();
                }
                _response.Result = _mapper.Map<CountryDTO>(country);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task <ActionResult<APIResponse>> CreateCountry([FromBody]CountryCreateDTO createDTO)
        {
            try
            {
                if (await _dbCountry.GetAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)

                {
                    ModelState.AddModelError("CustomError", "Country already Exists");
                    return BadRequest();
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                Country country = _mapper.Map<Country>(createDTO);




                await _dbCountry.CreateAsync(country);
                _response.Result = _mapper.Map<List<CountryDTO>>(country);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetCountry", new { id = country.Id }, _response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpDelete("{id:int}",Name = "DeleteCountry")]

        public async Task <ActionResult<APIResponse>> DeleteCountry(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var country = await _dbCountry.GetAsync(u => u.Id == id);
                if (country == null)
                {
                    return NotFound();
                }
                await _dbCountry.RemoveAsync(country);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:int}",Name = "UpdateCountry")]
        public async Task<ActionResult<APIResponse>>  UpdateCountry(int id, [FromBody]CountryUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }
                Country model = _mapper.Map<Country>(updateDTO);

                await _dbCountry.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
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

            var country = await _dbCountry.GetAsync(u => u.Id == id,tracked:false);

            CountryUpdateDTO countryDTO = _mapper.Map<CountryUpdateDTO>(country);

            if (country == null)
            {
                _response.StatusCode=HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
               
            patchDTO.ApplyTo(countryDTO,ModelState);

            Country model = _mapper.Map<Country>(countryDTO);


            _dbCountry.UpdateAsync(model);
          
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
 