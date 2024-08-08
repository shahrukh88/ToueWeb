using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Tour_Web.Models;
using Tour_Web.Models.DTO;
using Tour_Web.Services.IServices;

namespace Tour_Web.Controllers
{
    public class CountryController : Controller
    {

        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;

        public CountryController(ICountryService countryService, IMapper mapper)
        {
            _countryService = countryService;
            _mapper = mapper;
        }

        public async Task <IActionResult> IndexCountry()
        {
            List<CountryDTO> list = new();

            var response = await _countryService.GetAllAsync<APIResponse>();
            if(response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CountryDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        public async Task<IActionResult> CreateCountry()
        {
            
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCountry(CountryCreateDTO model)
        {
            
            if(ModelState.IsValid)
            { 
                var response = await _countryService.CreateAsync<APIResponse>(model);
                 if (response != null && response.IsSuccess)
                 {
                    TempData["success"] = "Country created successfully";
                    return RedirectToAction(nameof(IndexCountry));

                   }
                }
            return View(model);
        }


        public async Task<IActionResult> UpdateCountry(int countryId)
        {
            var response = await _countryService.GetAsync<APIResponse>(countryId);
            if (response != null && response.IsSuccess)
            {
                CountryDTO model = JsonConvert.DeserializeObject<CountryDTO>(Convert.ToString(response.Result));
                return View(_mapper.Map<CountryUpdateDTO>(model));
            }

            return NotFound();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCountry(CountryUpdateDTO model)
        {

            if (ModelState.IsValid)
            {
                var response = await _countryService.UpdateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Updated successfully";
                    return RedirectToAction(nameof(IndexCountry));
                }
            }
            return View(model);
        }


        public async Task<IActionResult> DeleteCountry(int countryId)
        {
            var response = await _countryService.GetAsync<APIResponse>(countryId);
            if (response != null && response.IsSuccess)
            {
                CountryDTO model = JsonConvert.DeserializeObject<CountryDTO>(Convert.ToString(response.Result));
                return View(model);
            }

            return NotFound();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCountry(CountryDTO model)
        {

                var response = await _countryService.DeleteAsync<APIResponse>(model.Id);
                if (response != null && response.IsSuccess)
                {
                TempData["success"] = "Delete successfully";
                return RedirectToAction(nameof(IndexCountry));
                }
             return View(model);
        }

    }
}
