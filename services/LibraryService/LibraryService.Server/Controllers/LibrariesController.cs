using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using LibraryService.Core.Interfaces;
using LibraryService.Dto.Http;
using LibraryService.Dto.Http.Converters;
using LibraryService.Server.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace LibraryService.Server.Controllers;

[ApiController]
[Route("/api/v1/libraries")]
public class LibrariesController : ControllerBase
{
    private readonly ILibraryService _libraryService;
    private readonly ILogger<LibrariesController> _logger;

    public LibrariesController(ILibraryService libraryService, 
        ILogger<LibrariesController> logger)
    {
        _libraryService = libraryService;
        _logger = logger;
    }

    [HttpGet]
    [SwaggerOperation("Метод для получения библиотек.", "Метод для получения библиотек.")]
    [SwaggerResponse(statusCode: 200, type: typeof(LibraryPaginationResponse), description: "Библиотеки успешно получены.")]
    [SwaggerResponse(statusCode: 500, type: typeof(ErrorResponse), description: "Ошибка на стороне сервера.")]
    public async Task<IActionResult> GetLibraries([Required] [FromQuery] string city,
        [FromQuery] int? page,
        [FromQuery] int? size)
    {
        try
        {
            page ??= 1;
            size ??= int.MaxValue;
        
            var (limit, offset) = PageSizeConverter.ToLimitOffset(page.Value, size.Value);

            //TODO: if page and size not set - don't make second request
            var libraries = await _libraryService.GetAllLibrariesAsync(city, limit, offset);
            var allLibraries = await _libraryService.GetCountOfLibrariesAsync(city);

            var dtoLibraries = libraries.ConvertAll(LibraryConverter.Convert).ToList();

            if (size == int.MaxValue)
                size = allLibraries;

            var response = new LibraryPaginationResponse(page.Value, size.Value, allLibraries, dtoLibraries);
        
            return Ok(response);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in method {Method}", nameof(GetBooks));
            
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("{libraryUid:guid}/books")]
    [SwaggerOperation("Метод для получения книг в библиотеке.", "Метод для получения книг в библиотеке.")]
    [SwaggerResponse(statusCode: 200, type: typeof(LibraryBookPaginationResponse), description: "Книги успешно получены.")]
    [SwaggerResponse(statusCode: 500, type: typeof(ErrorResponse), description: "Ошибка на стороне сервера.")]
    public async Task<IActionResult> GetBooks([Required] [FromRoute] Guid libraryUid,
        [FromQuery] bool? showAll,
        [FromQuery] int? page,
        [FromQuery] int? size)
    {
        try
        {
            page ??= 1;
            size ??= int.MaxValue;
        
            var (limit, offset) = PageSizeConverter.ToLimitOffset(page.Value, size.Value);

            var inventoryItems = await _libraryService.GetLibraryBooksAsync(libraryUid, showAll, offset, limit);
            var allBooksCount = await _libraryService.GetCountOfLibraryBooksAsync(libraryUid, showAll);

            var dtoBooks = inventoryItems.ConvertAll(LibraryBookConverter.Convert).ToList();
            
            if (size == int.MaxValue)
                size = allBooksCount;

            var response = new LibraryBookPaginationResponse(page.Value, size.Value, allBooksCount, dtoBooks);
        
            return Ok(response);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in method {Method}", nameof(GetBooks));
            
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost("{libraryUid:guid}/books/{bookUid:guid}/checkin")]
    [SwaggerOperation("Метод для возврата книги.", "Метод для возврата книги.")]
    [SwaggerResponse(statusCode: 200, description: "Книга успешно возвращена.")]
    [SwaggerResponse(statusCode: 500, type: typeof(ErrorResponse), description: "Ошибка на стороне сервера.")]
    public async Task<IActionResult> CheckInBook([Required] [FromRoute] Guid libraryUid,
        [Required] [FromRoute] Guid bookUid)
    {
        try
        {
            await _libraryService.CheckInBookAsync(libraryUid, bookUid);

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in method {Method}", nameof(CheckInBook));
            
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost("{libraryUid:guid}/books/{bookUid:guid}/checkout")]
    [SwaggerOperation("Метод для получения книги.", "Метод для получения книги.")]
    [SwaggerResponse(statusCode: 200, description: "Книга успешно получена.")]
    [SwaggerResponse(statusCode: 500, type: typeof(ErrorResponse), description: "Ошибка на стороне сервера.")]
    public async Task<IActionResult> CheckOutBook([Required] [FromRoute] Guid libraryUid,
        [Required] [FromRoute] Guid bookUid)
    {
        try
        {
            await _libraryService.CheckOutBookAsync(libraryUid, bookUid);

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in method {Method}", nameof(CheckOutBook));
            
            return StatusCode(500, e.Message);
        }
    }
}