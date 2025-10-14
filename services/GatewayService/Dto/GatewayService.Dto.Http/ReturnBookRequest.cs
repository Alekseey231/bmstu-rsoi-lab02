using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace GatewayService.Dto.Http;

/// <summary>
/// Запрос на возврат книги
/// </summary>
[DataContract]
public class ReturnBookRequest
{
    /// <summary>
    /// Состояние книги
    /// </summary>
    [Required]
    [DataMember(Name = "condition")]
    public string Condition { get; set; }

    /// <summary>
    /// Дата возврата
    /// </summary>
    [Required]
    [DataMember(Name = "date")]
    public DateTime Date { get; set; }

    public ReturnBookRequest(string condition, DateTime date)
    {
        Condition = condition;
        Date = date;
    }
}
