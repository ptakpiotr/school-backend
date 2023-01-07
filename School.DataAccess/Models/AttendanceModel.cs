namespace School.DataAccess.Models
{
    public class AttendanceModel
    {
        public int Id { get; set; }
        public string Uczen_id { get; set; }
        public bool Obecny { get; set; }
        public DateTime Data { get; set; }
    }
}
