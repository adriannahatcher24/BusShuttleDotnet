using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DomainModel;
namespace WebMvc.Models
{
    public class LoginModel
{
    public int Id { get; set; }

    [Required]
    [DataType(DataType.Text)]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public string HashedPassword
    {
        get => HashPassword(this.Password);
    }

    private string HashPassword(string password)
    {
        using (var hasher = new System.Security.Cryptography.SHA256Managed())
        {
            var hashBytes = hasher.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashBytes);
        }
    }
}
}