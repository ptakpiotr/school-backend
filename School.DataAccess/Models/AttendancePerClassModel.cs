namespace School.DataAccess.Models
{
    public class AttendancePerClassModel
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int Rok { get; set; }
        public bool Obecny { get; set; }
        public DateTime Data { get; set; }
    }
}
