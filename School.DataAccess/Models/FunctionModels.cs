namespace School.DataAccess.Models
{
    internal class FunctionModels
    {
        internal class GroupedGradesModel
        {
            public int ClassId { get; set; }
            public string Np { get; set; }
        }

        internal class StudentGradesModel
        {
            public int StudentId { get; set; }
        }

        internal class AttendancePerClassModel
        {
            public int ClassId { get; set; }
        }
    }
}
