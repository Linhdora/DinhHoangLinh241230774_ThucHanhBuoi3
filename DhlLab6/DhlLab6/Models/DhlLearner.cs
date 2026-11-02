namespace DhlLab6.Models
{
    public class DhlLearner
    {
        public DhlLearner()
        {
            Enrollments = new HashSet<DhlEnrollment>();

        }
        public int LearnerID { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstMidName { get; set; } = string.Empty;
        public DateTime EnrollmentDate { get; set; }
        public int MajorID { get; set; }
        public virtual DhlMajor? Major { get; set; }
        public virtual ICollection<DhlEnrollment> Enrollments { get; set; }
    }
}
