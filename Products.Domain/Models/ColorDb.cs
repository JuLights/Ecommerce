using Shared.Models;

namespace Products.Domain.Models;

public record ColorDb
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}