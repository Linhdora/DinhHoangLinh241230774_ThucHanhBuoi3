namespace DhlLab5.Models
{
    public class DhlEnrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int LearnerID { get; set; }
        public float Grade { get; set; }
        public virtual DhlLearner? Learner { get; set; }
        public virtual DhlCourse? Course { get; set; }
    }
}
