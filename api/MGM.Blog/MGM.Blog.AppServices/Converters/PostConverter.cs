using MGM.Blog.AppServices.ViewModel;
using MGM.Blog.Domain.Dtos;

namespace MGM.Blog.AppServices.Converters
{
    public static class PostConverter
    {
        public static PostDto ToDto(this PostViewModel vm)
            => new(Id: vm.Id ?? Guid.NewGuid(), Text: vm.Text, Title: vm.Title);
    }
}
