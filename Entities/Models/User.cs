using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public sealed class User : BaseEntity
{
    public Guid Guid { get; set; }

    public string Name { get; set; }
    
    public string Email { get; set; }

    public int Age { get; set; }
}