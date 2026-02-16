using PokemonReviewApp.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryrepository;
        private readonly IMapper _mapper;
        public CountryController(ICountryRepository countryrepository, IMapper mapper)
        {
            _mapper = mapper;
            _countryrepository = countryrepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        public IActionResult GetCountries()
        {
            var countries = _mapper.Map<List<CountryDto>>(_countryrepository.GetCountries());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(countries);
        }

        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetCountry(int countryId)
        {
            if (!_countryrepository.CountryExists(countryId))
                return NotFound();

            var country = _mapper.Map<CountryDto>(_countryrepository.GetCountry(countryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(country);
        }

        [HttpGet("/owners/{ownerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Country))]
        public IActionResult GetCountryByOwner(int ownerId)
        {
            var country = _mapper.Map<CountryDto>(_countryrepository.GetCountryByOwner(ownerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(country);
        }

        [HttpPost("createCountry")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCountry([FromBody] CountryDto countryCreate)
        {
            if (countryCreate == null)
                return BadRequest(ModelState);

            var country = _countryrepository.GetCountries()
                .Where(c => c.Name.Trim().ToUpper() == countryCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (country != null)
            {
                ModelState.AddModelError("", "Country already exist:");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest();

            var countryMap = _mapper.Map<Country>(countryCreate);

            if (!_countryrepository.CreateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wrong:");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }

        [HttpPut("{countryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCountry(int countryId, [FromBody] CountryDto updateCountry)
        {
            if (updateCountry == null)
                return BadRequest(ModelState);

            if (countryId != updateCountry.Id)
                return BadRequest(ModelState);

            if (!_countryrepository.CountryExists(countryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var countryMap = _mapper.Map<Country>(updateCountry);

            if (!_countryrepository.UpdateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}