using System.ComponentModel.DataAnnotations;

namespace SmartException.Api.v1.Users.Models;

public class UserDto
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    [Required]
    [Display(Name = "Имя")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Возраст пользователя
    /// </summary>
    [Required]
    [Display(Name = "Возраст")]
    public int Age { get; set; }

    /// <summary>
    /// Email пользователя
    /// </summary>
    [Required]
    [Display(Name = "Email")]
    public string Email { get; set; }
}