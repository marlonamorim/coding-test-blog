using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MGM.Blog.AppServices.ViewModel
{
    public class PostViewModel
    {
        public Guid? Id { get; set; }

        [Required]
        [NotNull]
        public string Text { get; set; } = null!;

        [Required]
        [NotNull]
        public string Title { get; set; } = null!;
    }
}
