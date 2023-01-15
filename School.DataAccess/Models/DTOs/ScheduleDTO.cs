namespace School.DataAccess.Models.Dtos;

/// <summary>
/// DTO dla modelu ScheduleModel
/// </summary>
public class ScheduleDTO
{
    public int Przedmiot_oddzial_id { get; set; }
    public DateTime Termin_Od { get; set; }
    public DateTime Termin_Do { get; set; }
}

