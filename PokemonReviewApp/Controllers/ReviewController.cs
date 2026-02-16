using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviwRepositry;
        private readonly IMapper _mapper;
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IPokemonRepository _pokemonRepository;

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper, IReviewerRepository reviewerRepository, IPokemonRepository pokemonRepository)
        {
            _reviwRepositry = reviewRepository;
            _mapper = mapper;
            _pokemonRepository = pokemonRepository;
            _reviewerRepository = reviewerRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviwRepositry.GetReviews());
            return Ok(reviews);
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewId)
        {
            if (!_reviwRepositry.IsReviewExists(reviewId))
                return NotFound();

            var review = _mapper.Map<ReviewDto>(_reviwRepositry.GetReview(reviewId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }

        [HttpGet("pokemon/{pokeId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewForAPokemon(int pokeId)
        {
            var review = _mapper.Map<List<ReviewDto>>(_reviwRepositry.GetReviewOfApokemon(pokeId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(review);
        }

        [HttpPost("createReview")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromQuery] int reviewerId, [FromQuery] int pokeId, [FromBody] ReviewDto reviewCreate)
        {
            if (reviewCreate == null)
                return BadRequest(ModelState);

            var revews = _reviwRepositry.GetReviews()
                .Where(c => c.Title.Trim().ToUpper() == reviewCreate.Title.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (revews != null)
            {
                ModelState.AddModelError("", "Review already exist:");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest();

            var reviewMap = _mapper.Map<Review>(reviewCreate);

            reviewMap.Pokemon = _pokemonRepository.GetPokemon(pokeId);
            reviewMap.Reviewer = _reviewerRepository.GetReviewer(reviewerId);

            if (!_reviwRepositry.CreateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong:");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }

        [HttpPut("{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReview(int reviewId, [FromBody] ReviewDto UpdateReview)
        {
            if (UpdateReview == null)
                return BadRequest(ModelState);

            if (reviewId != UpdateReview.Id)
                return BadRequest(ModelState);

            if (!_reviwRepositry.IsReviewExists(reviewId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var reviewMap = _mapper.Map<Review>(UpdateReview);

            if (!_reviwRepositry.UpdateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}