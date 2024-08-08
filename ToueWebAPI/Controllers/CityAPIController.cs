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
    [Route("api/CityAPI")]
    [ApiController]
    public class CityAPIController : ControllerBase
    {

        protected APIResponse _response;
        private readonly ICityRepository _dbcity;
        private readonly ICountryRepository _dbcountry;
        private readonly IMapper _mapper;
        
        
        public CityAPIController(ICityRepository dbcity,IMapper mapper,ICountryRepository country)
        {
            _dbcity = dbcity;
            _mapper = mapper;
            this._response = new();
            _dbcountry = country;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task <ActionResult <APIResponse>>GetCities()
        {
            try {
                IEnumerable<City> cityList = await _dbcity.GetAllAsync(includeProperties:"Country");
                _response.Result = _mapper.Map<List<CityDTO>>(cityList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch(Exception ex) {
               
                _response.IsSuccess=false;
                _response.ErrorMessages=new List<string>() { ex.ToString()};
            }
            return _response;
        }

        [HttpGet("{id:int}",Name ="GetCity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public async Task<ActionResult <APIResponse>> GetCity(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                var city = await _dbcity.GetAsync(u => u.CityNumber == id);

                if (city == null)
                {
                    return NotFound();
                }
                _response.Result = _mapper.Map<CityDTO>(city);
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

        public async Task <ActionResult<APIResponse>> CreateCity([FromBody]CityCreateDTO createDTO)
        {
            try
            {
                if (await _dbcity.GetAsync(u => u.CityNumber == createDTO.CityNumber) != null)

                {
                    ModelState.AddModelError("ErrorMessages", "City already Exists");
                    return BadRequest();
                }

                if(await _dbcountry.GetAsync(u=>u.Id == createDTO.CountryID)== null)
                {
                    ModelState.AddModelError("ErrorMessages", "Country ID is invalid");

                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                City city = _mapper.Map<City>(createDTO);




                await _dbcity.CreateAsync(city);
                _response.Result = _mapper.Map<CityDTO>(city);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetCity", new { id = city.CityNumber }, _response);
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

        [HttpDelete("{id:int}",Name = "DeleteCity")]

        public async Task <ActionResult<APIResponse>> DeleteCity(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var city = await _dbcity.GetAsync(u => u.CityNumber == id);
                if (city == null)
                {
                    return NotFound();
                }
                await _dbcity.RemoveAsync(city);
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
        [HttpPut("{id:int}",Name = "UpdateCity")]
        public async Task<ActionResult<APIResponse>>  UpdateCity(int id, [FromBody]CityUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.CityNumber)
                {
                    return BadRequest();
                }

                if (await _dbcountry.GetAsync(u => u.Id == updateDTO.CountryID) == null)
                {
                    ModelState.AddModelError("CustomError", "Country ID is invalid");
                    return BadRequest(ModelState);
                }


                City model = _mapper.Map<City>(updateDTO);

                await _dbcity.UpdateAsync(model);
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

        [HttpPatch("{id:int}", Name = "UpdatePartialCity")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task <IActionResult> UpdatePartialCity(int id, JsonPatchDocument<CityUpdateDTO> patchDTO)
        {

            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            var city = await _dbcity.GetAsync(u => u.CityNumber == id,tracked:false);

            CityUpdateDTO cityDTO = _mapper.Map<CityUpdateDTO>(city);

            if (city == null)
            {
                _response.StatusCode=HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
               
            patchDTO.ApplyTo(cityDTO,ModelState);

            City model = _mapper.Map<City>(cityDTO);


            _dbcity.UpdateAsync(model);
          
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
 