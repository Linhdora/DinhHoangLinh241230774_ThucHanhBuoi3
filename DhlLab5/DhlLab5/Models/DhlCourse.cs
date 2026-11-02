using System.ComponentModel.DataAnnotations.Schema;

namespace DhlLab5.Models
{
    public class DhlCourse
    {
        public DhlCourse()
        {
            Enrollments = new HashSet<DhlEnrollment>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseID { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Credits { get; set; }
        public virtual ICollection<DhlEnrollment> Enrollments { get; set; }

    }
}
