using System.ComponentModel.DataAnnotations;
using GatewayService.Server.Clients;
using LibraryService.Dto.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GatewayService.Server.Controllers;

[ApiController]
[Route("/api/v1/libraries")]
public class LibraryController : ControllerBase
{
    private readonly ILibraryServiceClient _libraryServiceRequestClient;
    private readonly ILogger<LibraryController> _logger;

    public LibraryController(ILibraryServiceClient libraryServiceRequestClient, ILogger<LibraryController> logger)
    {
        _libraryServiceRequestClient = libraryServiceRequestClient;
        _logger = logger;
    }

    [HttpGet]
    [SwaggerOperation("Получить список библиотек в городе", "Получить список библиотек в городе")]
    [SwaggerResponse(statusCode: 200, type: typeof(LibraryPaginationResponse), description: "Список библиотек в городе")]
    [SwaggerResponse(statusCode: 500, type: typeof(ErrorResponse), description: "Ошибка на стороне сервера")]
    public async Task<IActionResult> GetLibraries(
        [Required][FromQuery] string city,
        [FromQuery] int? page,
        [FromQuery] int? size)
    {
        try
        {
            await Task.Delay(1);
            // Implementation here
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in method {Method}", nameof(GetLibraries));
            return StatusCode(500, new ErrorResponse("Неожиданная ошибка на стороне сервера."));
        }
    }

    [HttpGet("{libraryUid:guid}/books")]
    [SwaggerOperation("Получить список книг в выбранной библиотеке", "Получить список книг в выбранной библиотеке")]
    [SwaggerResponse(statusCode: 200, type: typeof(LibraryBookPaginationResponse), description: "Список книг в библиотеке")]
    [SwaggerResponse(statusCode: 500, type: typeof(ErrorResponse), description: "Ошибка на стороне сервера")]
    public async Task<IActionResult> GetLibraryBooks(
        [Required][FromRoute] Guid libraryUid,
        [FromQuery] int? page,
        [FromQuery] int? size,
        [FromQuery] bool? showAll)
    {
        try
        {
            await Task.Delay(1);
            // Implementation here
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in method {Method}", nameof(GetLibraryBooks));
            return StatusCode(500, new ErrorResponse("Неожиданная ошибка на стороне сервера."));
        }
    }
}
