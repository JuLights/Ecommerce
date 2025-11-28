namespace Authorization.Domain.Models;

public record UserDb
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public bool IsAdmin { get; set; } 
    public DateTime CreateDate { get; set; }
    public DateTime LastUpdateDate { get; set; }
    public bool IsDeleted { get; set; }
}