using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using Tour_Web.Models;
using Tour_Web.Models.DTO;
using Tour_Web.Models.Models.VM;
using Tour_Web.Services;
using Tour_Web.Services.IServices;

namespace Tour_Web.Controllers
{
    public class CityController : Controller
    {

        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;

        private readonly IMapper _mapper;

        public CityController(ICityService cityService, ICountryService countryService, IMapper mapper)
        {
            _cityService = cityService;
            _countryService = countryService;
            _mapper = mapper;
        }

        public async Task <IActionResult> IndexCity()
        {
            List<CityDTO> list = new();

            var response = await _cityService.GetAllAsync<APIResponse>();
            if(response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CityDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        public async Task<IActionResult> CreateCity()
        {
            CityCreateVM cityCreatVM = new();

            var response = await _countryService.GetAllAsync<APIResponse>();

            if (response != null && response.IsSuccess)
            {
                cityCreatVM.CountryList = JsonConvert.DeserializeObject<List<CountryDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            }
            return View(cityCreatVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCity(CityCreateVM model)
        {
            if (ModelState.IsValid)
            {

                var response = await _cityService.CreateAsync<APIResponse>(model.CityNumber);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "City created successfully";
                    return RedirectToAction(nameof(IndexCity));
                }
                else
                {
                    if(response.ErrorMessages.Count>0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var resp = await _countryService.GetAllAsync<APIResponse>();
            if (resp != null && resp.IsSuccess)
            {
                model.CountryList = JsonConvert.DeserializeObject<List<CountryDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }); ;
            }
            return View(model);
        }




        public async Task<IActionResult> UpdateCity(int cityNumber)
        {
            CityUpdateVM cityVM = new();

            var response = await _cityService.GetAsync<APIResponse>(cityNumber);
            if (response != null && response.IsSuccess)
            {
                CityDTO model = JsonConvert.DeserializeObject<CityDTO>(Convert.ToString(response.Result));
                cityVM.CityNumber =_mapper.Map<CityUpdateDTO>(model);
            }

            response = await _countryService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                cityVM.CountryList = JsonConvert.DeserializeObject<List<CountryDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }); 
                return View(cityVM);
            }

            return NotFound();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCity(CityUpdateVM model)
        {

            if (ModelState.IsValid)
            {
                var response = await _cityService.UpdateAsync<APIResponse>(model.CityNumber);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Updated successfully";
                    return RedirectToAction(nameof(IndexCity));
                }
                else
                {
                    if(response.ErrorMessages.Count>0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }
            var resp = await _countryService.GetAllAsync<APIResponse>();
            if (resp != null && resp.IsSuccess)
            {
                model.CountryList = JsonConvert.DeserializeObject<List<CountryDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });

            }


            return View(model);

        }



        public async Task<IActionResult> DeleteCity(int cityNumber)
        {
            CityDeleteVM cityVM = new();

            var response = await _cityService.GetAsync<APIResponse>(cityNumber);
            if (response != null && response.IsSuccess)
            {
                CityDTO model = JsonConvert.DeserializeObject<CityDTO>(Convert.ToString(response.Result));
                cityVM.CityNumber = model;
            }

            response = await _countryService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                cityVM.CountryList = JsonConvert.DeserializeObject<List<CountryDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                return View(cityVM);
            }


            return NotFound();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCity(CityDeleteVM model)
        {

            var response = await _cityService.DeleteAsync<APIResponse>(model.CityNumber.CountryID);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Deleted successfully";
                return RedirectToAction(nameof(IndexCity));
            }
            return View(model);
        }


    }
}
