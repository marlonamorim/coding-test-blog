using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MGM.Blog.AppServices.ViewModel
{
    public class UserViewModel
    {
        public Guid? Id { get; set; }

        [Required]
        [NotNull]
        public string FirstName { get; set; } = null!;


        [Required]
        [NotNull]
        public string LastName { get; set; } = null!;

        [Required]
        [NotNull]
        public string Email { get; set; } = null!;

        [Required]
        [NotNull]
        public string Password { get; set; } = null!;

        public DateTime BirthDate { get; set; }

        [Required]
        [NotNull]
        public string TaxDocument { get; set; } = null!;
    }
}
