using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Redis.Repositories;

namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private readonly ICacheRepository _repository;
        public CacheController(ICacheRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("{key}")]
        public async Task<IActionResult> SetCache([FromRoute] string key, [FromBody] object value)
        {
            await _repository.Set(key, value, TimeSpan.FromSeconds(3600));
            return Ok();
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> GetCache([FromRoute] string key)
        {
            return Ok(await _repository.Get<object>(key));
        }
    }
}
