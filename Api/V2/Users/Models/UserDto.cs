using System.ComponentModel.DataAnnotations;

namespace Api.V2.Users.Models;

public record UserDto
{
    public Guid Guid { get; set; }

 #pragma warning disable CS8618  
    /// <summary>
    /// Имя пользователя
    /// </summary>
    [Required]
    [Display(Name = "Имя")]
    public string Name { get; set; }

    /// <summary>
    /// Email пользователя
    /// </summary>
    [Required]
    [Display(Name = "Email")]
    public string Email { get; set; }
#pragma warning restore CS8618
    
    /// <summary>
    /// Возраст пользователя
    /// </summary>
    [Required]
    [Display(Name = "Возраст")]
    public int Age { get; set; }
}