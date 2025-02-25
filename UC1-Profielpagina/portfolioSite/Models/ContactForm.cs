using System.ComponentModel.DataAnnotations;

namespace Showcase_Contactpagina.Models;

public class Contactform {
    [Required] [StringLength(60)] public string FirstName { get; set; }

    [Required] [StringLength(60)] public string LastName { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(200)]
    public string Email { get; set; }

    [Phone] [StringLength(20)] public string Phone { get; set; }

    [Required] [StringLength(600)] public string Message { get; set; }

    [Required] [StringLength(200)] public string Subject { get; set; }
}