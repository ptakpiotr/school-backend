namespace School.DataAccess.Models
{
    public class ScheduleModel
    {
        public int Id { get; set; }
        public DateTime TerminOd { get; set; }
        public DateTime TerminDo { get; set; }
        public string Przedmiot { get; set; }
        public int Rok { get; set; }
    }
}
