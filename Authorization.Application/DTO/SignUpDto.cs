using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Authorization.Application.DTO;

public partial record SignUpDto
{
    
    [Required(ErrorMessage = "Username is required.")]
    [StringLength(100, ErrorMessage = "Username cannot exceed 100 characters.")]
    public string Username { get; init; } = string.Empty;

    [Required(ErrorMessage = "First name is required.")]
    [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
    public string FirstName { get; init; } = string.Empty;

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
    public string LastName { get; init; } = string.Empty;

    [EmailAddress] public string Email { get; init; } = string.Empty;

    public string PhoneNumber { get; init; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "You must use at least 1 uppercase letter and a number.")]
    public string Password { get; init; } = string.Empty;

    [Required(ErrorMessage = "Confirm password is required.")]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;

    public int RoleId { get; init; }
    [Required(ErrorMessage = "UserGroup is required")]
    public int UserGroupId { get; init; }
    [Required(ErrorMessage = "Department is required.")]
    public int DepartmentId { get; init; }

    public bool IsValidPassword()
    {
        return PasswordRegex().IsMatch(Password);
    }

    [GeneratedRegex(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#\$%\^&\*\(\)\-_=\+\[\]\{\};:'"",<>\./?\\|`]).+$")]
    private static partial Regex PasswordRegex();
}