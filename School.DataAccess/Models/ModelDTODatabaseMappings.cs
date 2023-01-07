namespace School.DataAccess.Models
{
    internal static class ModelDTODatabaseMappings
    {
        public static Dictionary<Type, TableNames> mappings { get; set; }
        static ModelDTODatabaseMappings()
        {
            mappings.Add(typeof(AttendanceModel), new() { Original = "obecnosci", InsertType = typeof(AttendanceModel), View = "obecnosci" });
            mappings.Add(typeof(AttendanceModel), new() { Original = "obecnosci", InsertType = typeof(AttendanceModel), View = "obecnosci" });
        }
    }

    class TableNames
    {
        public string Original { get; set; }
        public Type? InsertType { get; set; }
        public string? View { get; set; }
    }
}
