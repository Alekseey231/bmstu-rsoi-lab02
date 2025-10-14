using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace GatewayService.Dto.Http;

/// <summary>
/// Ответ на взятие книги
/// </summary>
[DataContract]
public class TakeBookResponse
{
    /// <summary>
    /// UUID бронирования
    /// </summary>
    [Required]
    [DataMember(Name = "reservationUid")]
    public Guid ReservationUid { get; set; }

    /// <summary>
    /// Статус бронирования книги
    /// </summary>
    [Required]
    [DataMember(Name = "status")]
    public string Status { get; set; }

    /// <summary>
    /// Дата начала бронирования
    /// </summary>
    [Required]
    [DataMember(Name = "startDate")]
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Дата окончания бронирования
    /// </summary>
    [Required]
    [DataMember(Name = "tillDate")]
    public DateTime TillDate { get; set; }

    /// <summary>
    /// Информация о книге
    /// </summary>
    [Required]
    [DataMember(Name = "book")]
    public BookInfo Book { get; set; }

    /// <summary>
    /// Информация о библиотеке
    /// </summary>
    [Required]
    [DataMember(Name = "library")]
    public LibraryResponse Library { get; set; }

    /// <summary>
    /// Рейтинг пользователя
    /// </summary>
    [Required]
    [DataMember(Name = "rating")]
    public UserRatingResponse Rating { get; set; }

    public TakeBookResponse(Guid reservationUid, string status, DateTime startDate, DateTime tillDate, BookInfo book, LibraryResponse library, UserRatingResponse rating)
    {
        ReservationUid = reservationUid;
        Status = status;
        StartDate = startDate;
        TillDate = tillDate;
        Book = book;
        Library = library;
        Rating = rating;
    }
}