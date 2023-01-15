namespace School.DataAccess.Models.Dtos;

/// <summary>
/// DTO dla modelu PaymentModel
/// </summary>
public class PaymentDTO
{
    public int Rodzaj_id { get; set; }
    public int Uczen_id { get; set; }
    public decimal Wartosc { get; set; }
}
