using MGM.Blog.AppServices.ViewModel;
using MGM.Blog.Domain.Dtos;

namespace MGM.Blog.AppServices.Services
{
    public interface IUserAppService
    {
        Task CreateAsync(UserViewModel vm);
    }
}
