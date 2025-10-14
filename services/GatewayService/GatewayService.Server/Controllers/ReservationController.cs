using System.ComponentModel.DataAnnotations;
using GatewayService.Dto.Http;
using GatewayService.Server.Clients;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GatewayService.Server.Controllers;

[ApiController]
[Route("/api/v1/reservations")]
public class ReservationController : ControllerBase
{
    private readonly IReservationServiceClient _reservationServiceRequestClient;
    private readonly ILogger<ReservationController> _logger;

    public ReservationController(IReservationServiceClient reservationServiceRequestClient, ILogger<ReservationController> logger)
    {
        _reservationServiceRequestClient = reservationServiceRequestClient;
        _logger = logger;
    }

    [HttpGet]
    [SwaggerOperation("Получить информацию по всем взятым в прокат книгам пользователя", "Получить информацию по всем взятым в прокат книгам пользователя")]
    [SwaggerResponse(statusCode: 200, type: typeof(List<BookReservationResponse>), description: "Информация по всем взятым в прокат книгам")]
    [SwaggerResponse(statusCode: 500, type: typeof(ErrorResponse), description: "Ошибка на стороне сервера")]
    public async Task<IActionResult> GetReservations([Required][FromHeader(Name = "X-User-Name")] string userName)
    {
        try
        {
            await Task.Delay(1);
            // Implementation here
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in method {Method}", nameof(GetReservations));
            return StatusCode(500, new ErrorResponse("Неожиданная ошибка на стороне сервера."));
        }
    }

    [HttpPost]
    [SwaggerOperation("Взять книгу в библиотеке", "Взять книгу в библиотеке")]
    [SwaggerResponse(statusCode: 200, type: typeof(TakeBookResponse), description: "Информация о бронировании")]
    [SwaggerResponse(statusCode: 400, type: typeof(ValidationErrorResponse), description: "Ошибка валидации данных")]
    [SwaggerResponse(statusCode: 500, type: typeof(ErrorResponse), description: "Ошибка на стороне сервера")]
    public async Task<IActionResult> TakeBook(
        [Required][FromHeader(Name = "X-User-Name")] string userName,
        [Required][FromBody] TakeBookRequest request)
    {
        try
        {
            await Task.Delay(1);
            // Implementation here
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in method {Method}", nameof(TakeBook));
            return StatusCode(500, new ErrorResponse("Неожиданная ошибка на стороне сервера."));
        }
    }

    [HttpPost("{reservationUid:guid}/return")]
    [SwaggerOperation("Вернуть книгу", "Вернуть книгу")]
    [SwaggerResponse(statusCode: 204, description: "Книга успешно возвращена")]
    [SwaggerResponse(statusCode: 404, type: typeof(ErrorResponse), description: "Бронирование не найдено")]
    [SwaggerResponse(statusCode: 500, type: typeof(ErrorResponse), description: "Ошибка на стороне сервера")]
    public async Task<IActionResult> ReturnBook(
        [Required][FromRoute] Guid reservationUid,
        [Required][FromHeader(Name = "X-User-Name")] string userName,
        [Required][FromBody] ReturnBookRequest request)
    {
        try
        {
            await Task.Delay(1);
            // Implementation here
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in method {Method}", nameof(ReturnBook));
            return StatusCode(500, new ErrorResponse("Неожиданная ошибка на стороне сервера."));
        }
    }
}