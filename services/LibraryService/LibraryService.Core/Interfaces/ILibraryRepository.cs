using LibraryService.Core.Models;

namespace LibraryService.Core.Interfaces;

public interface ILibraryRepository
{
    Task<List<Library>> GetAllLibrariesAsync(string city, int? limit = null, int? offset = null);
    
    Task<int> GetCountOfLibrariesAsync(string city);
    
    Task<List<InventoryItem>> GetLibraryBooksAsync(Guid libraryUid, bool? showAll, int? offset = null, int? limit = null);
    
    Task<int> GetCountOfLibraryBooksAsync(Guid libraryUid, bool? showAll);
    
    Task CheckOutBookAsync(Guid libraryId, Guid bookUid);
    
    Task CheckInBookAsync(Guid libraryId, Guid bookUid);
}