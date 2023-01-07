namespace School.DataAccess.Models
{
    public class ScheduleModel
    {
        public int Id { get; set; }
        public DateTime TerminOd { get; set; }
        public DateTime TerminDo { get; set; }
        public int PrzedmiotOddzialId { get; set; }
        public int KlasaId { get; set; }
        public int PrzedmiotId { get; set; }
    }
}
