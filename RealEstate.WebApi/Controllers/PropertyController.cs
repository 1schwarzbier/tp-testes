using Microsoft.AspNetCore.Mvc;
using RealEstate.Models;
using RealEstate.Services;
using RealEstate.Services.PropertyService;
using RealEstate.Services.PropertyService.Models;

namespace RealEstate.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertiesService;

        public PropertyController (IPropertyService propertiesService)
        {
            _propertiesService = propertiesService;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<Property>))]
        public async Task<IActionResult> GetAllProperties([FromQuery] PaginatedRequest request)
        {
            var response = await _propertiesService.GetAll(request);

            if (!response.IsSuccessful)
                return StatusCode(response.StatusCode, response.ServiceErrors);

            return Ok(response.Resource);
        }

        [HttpGet("{id:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Property))]
        public async Task<IActionResult> GetProperty(Guid id)
        {
            var response = await _propertiesService.Get(id);

            if (!response.IsSuccessful)
                return StatusCode(response.StatusCode, response.ServiceErrors);

            return Ok(response.Resource);
        }
        
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddProperty([FromBody] InsertPropertyRequest request)
        {
            var response = await _propertiesService.Insert(request);
            
            if (!response.IsSuccessful)
                return StatusCode(response.StatusCode, response.ServiceErrors);

            return NoContent();
        }
        
        [HttpPut("")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateProperty([FromBody] UpdatePropertyRequest request)
        {
            var response = await _propertiesService.Update(request);
            
            if (!response.IsSuccessful)
                return StatusCode(response.StatusCode, response.ServiceErrors);

            return NoContent();
        }
        
        [HttpDelete("{id:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProperty(Guid id)
        {
            var response = await _propertiesService.Delete(id);

            if (!response.IsSuccessful)
                return StatusCode(response.StatusCode, response.ServiceErrors);

            return NoContent();
        }
    }
}
