using Kloudscript.Quiz.ZipCode.Cache.Constants;
using Kloudscript.Quiz.ZipCode.Cache.Services;
using Kloudscript.Quiz.ZipCode.Cache.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Kloudscript.Quiz.ZipCode.Cache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateLookUpController : ControllerBase
    {
        #region private members
        private readonly IUSPSService _uSPSService;
        #endregion
        public StateLookUpController(IUSPSService uSPSService)
        {
            _uSPSService = uSPSService;
        }

        [HttpGet, Route("GetStateByZipCode")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] 
        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IActionResult> GetStateByZipCode([Required] string zipCode)
        {
            if (string.IsNullOrEmpty(zipCode))
                return BadRequest();

            try
            {
                //only if url is not valid.... 
                if (!Helper.IsUSOrCanadianZipCode(zipCode))
                    return BadRequest(Messages.NotValidZipCode);

                string state = await _uSPSService.GetCityState(zipCode);

                //not found any url matched with the shortCode.... 
                if (state is null || string.IsNullOrEmpty(state))
                    return NotFound(Messages.NotFound);

                //getting back state from address object.... 
                return Ok(state);
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
