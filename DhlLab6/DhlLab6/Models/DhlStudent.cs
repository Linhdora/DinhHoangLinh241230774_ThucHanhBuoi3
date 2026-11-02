using System.Reflection;
using System.ComponentModel.DataAnnotations;
namespace DhlLab6.Models
{
    public class DhlStudent
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Họ tên")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Độ dài ít nhất từ {2} đến {1} kí tự")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Email bắt buộc phải được nhập")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        [RegularExpression(@"^[\w-\.]+@gmail\.com$", ErrorMessage = "{0} phải có đuôi @gmail.com.")]
        public string? Email { get; set; }
        [Required]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "{0} phải có từ 8 ký tự trở lên, bao gồm chữ hoa, thường, số và ký tự đặc biệt")]
        [StringLength(100, MinimumLength = 8)]
        public string? Password { get; set; }
        [Required]
        [Display(Name = "Ngành học")]
        public Branch? Branch { get; set; }
        [Required]
        [Display(Name = "Giới tính")]
        public Gender? Gender { get; set; }
        [Display(Name = "Chính quy")]
        public bool IsRegular { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        [Display(Name = "Địa chỉ")]
        public string? Address { get; set; }
        [Range(typeof(DateTime), "1963-01-01", "2005-12-31", ParseLimitsInInvariantCulture = true)]
        [Required]
        [Display(Name = "Ngày sinh")]
        public DateTime DateOfBorth { get; set; }
        [Required]
        [Display(Name = "Ảnh đại diện")]
        public string? AvatarUrl { get; set; }
        [Display(Name = "Điểm")]
        [Required(ErrorMessage = "Trường {0} là bắt buộc.")]
        [Range(0.0, 10.0, ErrorMessage = "{0} phải nằm trong khoảng từ {1} đến {2}.")]
        public double Score { get; set; }
    }
}
