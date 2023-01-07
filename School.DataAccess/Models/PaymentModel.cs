namespace School.DataAccess.Models;

public class PaymentModel
{
    public int Id { get; set; }
    public string Powod { get; set; }
    public int UczenId { get; set; }
    public decimal Wartosc { get; set; }
}
