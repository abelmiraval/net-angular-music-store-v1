using System.ComponentModel.DataAnnotations;

namespace MusicStore.Dto.Request;

public class DtoRegisterUser
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "El correo no tiene el formato correcto")]
    public string Email { get; set; }

    public int Age { get; set; }

    public int TypeDocument { get; set; }

    [Required]
    public string DocumentNumber { get; set; }

    [Required]
    [Compare(nameof(ConfirmPassword), ErrorMessage = "Las claves no coinciden")]
    public string Password { get; set; }

    [Required]
    public string ConfirmPassword { get; set; }

}