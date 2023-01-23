namespace School.DataAccess.Models;

/// <summary>
/// Model odpowiadający widokowi v_wszystkie_platnosci
/// </summary>
public class PaymentModel
{
    public int Id { get; set; }
    public string Powod { get; set; }
    public string Imie { get; set; }
    public string Nazwisko { get; set; }
    public decimal Wartosc { get; set; }
}
