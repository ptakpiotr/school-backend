namespace School.DataAccess.Models
{
    public class AttendancePerClassModel
    {
        public int KlasaId { get; set; }
        public int Rok { get; set; }
        public bool Obecny { get; set; }
        public DateTime Data { get; set; }
    }
}
