
using Microsoft.AspNetCore.Mvc;
using UnitConversionApi.Services;

namespace UnitConversionApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ConversionsController : ControllerBase
{
    private readonly IUnitConversionService _conversionService;

    public ConversionsController(IUnitConversionService conversionService)
    {
        _conversionService = conversionService;
    }

    [HttpGet("convert")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Convert(
        [FromQuery] double value,
        [FromQuery] string from,
        [FromQuery] string to)
    {
        try
        {
            var response = _conversionService.Convert(value, from, to);
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("units")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetUnits()
    {
        return Ok(_conversionService.GetSupportedUnits());
    }
}
