using Kloudscript.Quiz.Cache.Capability.Constants;
using Kloudscript.Quiz.Cache.Capability.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace Kloudscript.Quiz.Cache.Capability.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeyValueController : ControllerBase
    { 
        private readonly IMemoryCache _memoryCache;
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        public KeyValueController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet, Route("GetValueByKey")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync([Required] string key)
        {
            if (key is null || string.IsNullOrEmpty(key))
                return BadRequest(Messages.BadRequest);

            if (!_memoryCache.TryGetValue(key, out KeyValue keyValue))
            {
                return NotFound(Messages.NotFound);
            }
            else
            {
                try
                {
                    await semaphore.WaitAsync();

                    if (!_memoryCache.TryGetValue(key, out keyValue))
                    {
                        return NotFound(Messages.NotFound);
                    }
                    else
                    {
                        _memoryCache.Get<KeyValue>(key);
                    }
                }
                finally
                {
                    semaphore.Release();
                }
            }

            return Ok(keyValue);
        }

        [HttpPost, Route("SetValueByKey")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([Required][FromBody] KeyValue keyValue)
        {
            if (keyValue is null)
                return BadRequest();

            _memoryCache.Remove(keyValue.Key);
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                                .SetSlidingExpiration(TimeSpan.FromMinutes(keyValue.Expiration))
                                .SetAbsoluteExpiration(TimeSpan.FromMinutes(keyValue.Expiration))
                                .SetPriority(CacheItemPriority.Normal);

            _memoryCache.Set(keyValue.Key, keyValue, cacheEntryOptions);
            return new ObjectResult(keyValue) { StatusCode = (int)HttpStatusCode.Created };
        }
    }
}
