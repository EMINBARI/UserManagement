using UserManagement.Core.Interfaces;

namespace UserManagement.Core.Models;

public class Permission
{
    public const int MAX_NAME_LENGTH = 36;
    public const int MAX_DESCRIPTION_LENGTH = 256;
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public bool IsEnabled { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; }
}