using System.ComponentModel.DataAnnotations;

namespace DhlLab5.Models;

public sealed class DhlStudent
{
    public int Id { get; init; }

    [Required(ErrorMessage = "Vui lòng nhập họ tên.")]
    [StringLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự.")]
    public string FullName { get; init; } = string.Empty;

    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Vui lòng chọn ngày sinh.")]
    public DateTime DateOfBirth { get; init; }

    [Required(ErrorMessage = "Vui lòng nhập ngành học.")]
    public string Major { get; init; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng chọn giới tính.")]
    public string Gender { get; init; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng chọn hệ đào tạo.")]
    public string Program { get; init; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập địa chỉ.")]
    public string Address { get; init; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập email.")]
    [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
    public string Email { get; init; } = string.Empty;
}
