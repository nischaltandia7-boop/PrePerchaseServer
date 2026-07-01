using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrePerchaseServer.Models.mediaBank;

[ApiController]
[Route("api/[controller]")]
public class MediabankController : ControllerBase
{
    private readonly IMediabankService _service;

    public MediabankController(IMediabankService service)
    {
        _service = service;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(
        [FromForm] IFormFile file,
        [FromForm] CreateMediabankDto dto)
    {
        var result = await _service.UploadAsync(file, dto);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}