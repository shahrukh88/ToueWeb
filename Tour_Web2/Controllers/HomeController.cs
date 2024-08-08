using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using Tour_Web.Models;
using Tour_Web.Models.DTO;
using Tour_Web.Services.IServices;

namespace Tour_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;

        public HomeController(ICountryService countryService, IMapper mapper)
        {
            _countryService = countryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<CountryDTO> list = new();

            var response = await _countryService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CountryDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

    }
}
