using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class User : BaseEntity
{
    public string Name { get; set; }
    public int Age { get; set; }
    
    public string Email { get; set; }
}