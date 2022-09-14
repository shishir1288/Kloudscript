using Kloudscript.Quiz.Url.Shortening.Constants;
using Kloudscript.Quiz.Url.Shortening.Services;
using Kloudscript.Quiz.Url.Shortening.Utilities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Kloudscript.Quiz.Url.Shortening.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class URLShorteningController : ControllerBase
    {
        #region private members
        private readonly IShortUrlService _shortUrlService;
        #endregion

        #region constructor
        public URLShorteningController(IShortUrlService shortUrlService)
        {
            _shortUrlService = shortUrlService;
        }

        #endregion

        #region public members

        [HttpPost, Route("CreateShortUrl")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateShortUrlAsync([Required] string originalUrl)
        {
            if (string.IsNullOrEmpty(originalUrl))
                return BadRequest(Messages.NoUrl);

            //only if url is not valid.... 
            if (!Helper.IsUrlValid(originalUrl))
                return BadRequest(Messages.NotValidUrl);

            //getting back shorted url.... 
            return Created(nameof(originalUrl), await _shortUrlService.ShortenUrl(originalUrl));
        }

        [HttpGet, Route("GetResolvedUrl")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetResolvedUrlAsync([Required] string shortUrl)
        {
            if (string.IsNullOrEmpty(shortUrl))
                return BadRequest(Messages.NoUrl);

            //only if url is not valid.... 
            if (!Helper.IsUrlValid(shortUrl))
                return BadRequest(Messages.NotValidUrl);

            string shortCode = shortUrl.Split('/')[3];
            string originalUrl = await _shortUrlService.GetResolveUrl(shortCode);

            if (originalUrl is null)
            {
                //not found any url matched with the shortCode.... 
                return NotFound(Messages.NotFound);
            }

            //getting back resolved url.... 
            return Ok(originalUrl); 
        }
         
        [HttpGet, Route("GetForwardFullUrl")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] 
        public IActionResult GetForwardFullUrlAsync([Required]string redirectUrl)
        {
            if (string.IsNullOrEmpty(redirectUrl))
                return BadRequest(Messages.NoUrl);

            //only if url is not valid.... 
            if (!Helper.IsUrlValid(redirectUrl))
                return BadRequest(Messages.NotValidUrl);

            return Redirect(redirectUrl);
        }
        #endregion
    }
}
