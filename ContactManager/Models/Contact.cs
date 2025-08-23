using System.ComponentModel.DataAnnotations;

namespace ContactManager.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required, StringLength(120)]
        public string FullName { get; set; } = string.Empty;

        [EmailAddress, StringLength(150)]
        public string? Email { get; set; }

        [Phone, StringLength(25)]
        public string? Phone { get; set; }

        [StringLength(255)]
        public string? Address { get; set; }

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
