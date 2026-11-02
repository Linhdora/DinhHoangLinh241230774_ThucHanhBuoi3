namespace DhlLab5.Models
{
    public class DhlMajor
    {
        public DhlMajor()
        {
            Learners = new HashSet<DhlLearner>(); 
        }
        public int MajorID { get; set; }
        public string MajorName { get; set; } = string.Empty;
        public virtual ICollection<DhlLearner> Learners { get; set; }
    }
}
