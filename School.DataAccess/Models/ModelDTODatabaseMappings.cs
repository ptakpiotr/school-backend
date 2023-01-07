namespace School.DataAccess.Models
{
    internal static class ModelDTODatabaseMappings
    {
        public static Dictionary<Type, TableNames> mappings { get; set; } = new();
        static ModelDTODatabaseMappings()
        {
            mappings.Add(typeof(AttendanceModel), new() { Original = "obecnosci", View = "obecnosci" });
            mappings.Add(typeof(ClassModel), new() { Original = "klasy", View = "v_all_class" });
            mappings.Add(typeof(GradeModel), new() { Original = "oceny", View = "oceny" });
            mappings.Add(typeof(PaymentModel), new() { Original = "oplaty", View = "v_all_payments" });
            mappings.Add(typeof(ScheduleModel), new() { Original = "plan_zajec", View = "v_schedule" });
            mappings.Add(typeof(StudentModel), new() { Original = "uczniowie", View = "v_all_students" });
            mappings.Add(typeof(SubjectModel), new() { Original = "przedmioty", View = "v_all_subjects" });
            mappings.Add(typeof(TeacherModel), new() { Original = "nauczyciele", View = "nauczyciele" });
        }
    }

    class TableNames
    {
        public string Original { get; set; }
        public string? View { get; set; }
    }
}
