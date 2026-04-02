using System.ComponentModel.DataAnnotations;

namespace UserManagementAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }= null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; }=null!;

        [Required]
        public string Role { get; set; }= null!;
    }
}