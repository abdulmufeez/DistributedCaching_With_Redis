using CachingWithRedis.Entities;
using CachingWithRedis.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace CachingWithRedis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IDistributedCache _cache;
        public TestController(IDistributedCache cache)
        {
            _cache = cache;
        }

        [HttpPost("set-cache-data")]
        public async Task<ActionResult> SetData(AppUser user)
        {
            await _cache.SetRecordAsync<AppUser>(user.UserName, user);

            return Ok();
        }

        [HttpGet("get-cache-data/{username}")]
        public async Task<ActionResult> GetData(string username)
        {
            var result = await _cache.GetRecordAsync<AppUser>(username);
            return Ok(result);
        }
    }
}