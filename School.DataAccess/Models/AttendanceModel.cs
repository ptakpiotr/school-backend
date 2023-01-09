namespace School.DataAccess.Models
{
    public class AttendanceModel
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public bool Obecny { get; set; }
        public DateTime Data { get; set; }
    }
}
