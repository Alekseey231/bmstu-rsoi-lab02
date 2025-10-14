using LibraryService.Dto.Http;
using Refit;

namespace GatewayService.Server.Clients;

public interface ILibraryServiceClient
{
    [Get("/api/v1/libraries")]
    Task<LibraryPaginationResponse> GetLibrariesAsync(
        [Query] string city,
        [Query] int? page = null,
        [Query] int? size = null);

    [Get("/api/v1/libraries/{libraryUid}/books")]
    Task<LibraryBookPaginationResponse> GetBooksAsync(
        Guid libraryUid,
        [Query] bool? showAll = null,
        [Query] int? page = null,
        [Query] int? size = null);

    [Post("/api/v1/libraries/{libraryUid}/books/{bookUid}/checkout")]
    Task CheckOutBookAsync(Guid libraryUid, Guid bookUid);

    [Post("/api/v1/libraries/{libraryUid}/books/{bookUid}/checkin")]
    Task CheckInBookAsync(Guid libraryUid, Guid bookUid);
}