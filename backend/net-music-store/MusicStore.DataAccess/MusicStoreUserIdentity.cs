using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MusicStore.DataAccess;

public class MusicStoreUserIdentity : IdentityUser
{
    [StringLength(100)]
    public string FirstName { get; set; }
    [StringLength(100)]
    public string LastName { get; set; }
    public int Age { get; set; }
    public TypeDocument TypeDocument { get; set; }
    
    [StringLength(20)]
    public string DocumentNumber { get; set; }
}

public enum TypeDocument
{
    Dni,
    Pasaporte
}