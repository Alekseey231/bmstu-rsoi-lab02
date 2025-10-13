using Microsoft.Extensions.Logging;
using LibraryService.Core.Interfaces;
using LibraryService.Core.Models;

namespace LibraryService.Services.LibraryService;

public class LibraryService : ILibraryService
{
    private readonly ILibraryRepository _libraryRepository;
    private readonly ILogger<LibraryService> _logger;

    public LibraryService(ILibraryRepository libraryRepository, ILogger<LibraryService> logger)
    {
        _libraryRepository = libraryRepository;
        _logger = logger;
    }

    public async Task<List<Library>> GetAllLibrariesAsync(string city, int? limit = null, int? offset = null)
    {
        _logger.LogDebug("Getting all libraries");
        
        var result = await _libraryRepository.GetAllLibrariesAsync(city, limit, offset);
        
        _logger.LogInformation("Got {Count} libraries", result.Count);
        
        return result;
    }

    public async Task<int> GetCountOfLibrariesAsync(string city)
    {
        _logger.LogDebug("Getting count of libraries");
        
        var result = await _libraryRepository.GetCountOfLibrariesAsync(city);
        
        _logger.LogInformation("Got {Count} libraries", result);
        
        return result;
    }

    public async Task<List<InventoryItem>> GetLibraryBooksAsync(Guid libraryUid, bool? showAll, int? offset = null, int? limit = null)
    {
        _logger.LogDebug("Getting library books");
        
        var result = await _libraryRepository.GetLibraryBooksAsync(libraryUid, showAll, offset, limit);
        
        _logger.LogInformation("Got {Count} library books", result.Count);
        
        return result;
    }

    public async Task<int> GetCountOfLibraryBooksAsync(Guid libraryUid, bool? showAll)
    {
        _logger.LogDebug("Getting count of library books");
        
        var result = await _libraryRepository.GetCountOfLibraryBooksAsync(libraryUid, showAll);
        
        _logger.LogInformation("Got {Count} library books", result);
        
        return result;
    }

    public async Task CheckOutBookAsync(Guid libraryId, Guid bookUid)
    {
        _logger.LogDebug("Checking out book {BookUid} in library {LibraryId}", bookUid, libraryId);
        
        await _libraryRepository.CheckOutBookAsync(libraryId, bookUid);
        
        _logger.LogInformation("Book {BookUid} has been checked out int library {LibraryId}", bookUid, libraryId);
    }

    public async Task CheckInBookAsync(Guid libraryId, Guid bookUid)
    {
        _logger.LogDebug("Checking in book {BookUid} in library {LibraryId}", bookUid, libraryId);
        
        await _libraryRepository.CheckInBookAsync(libraryId, bookUid);
        
        _logger.LogInformation("Book {BookUid} has been checked in int library {LibraryId}", bookUid, libraryId);
    }
}