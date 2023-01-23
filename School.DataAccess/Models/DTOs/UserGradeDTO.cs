namespace School.DataAccess.Models.Dtos;

/// <summary>
/// Model odpowiadający widokowi v_frekwencja
/// </summary>
public class UserGradeDTO
{
    public int Uczen_Id { get; set; }
    public int Przedmiot_Oddzial_Id { get; set; }
    public string Nazwa_Pracy { get; set; }
}