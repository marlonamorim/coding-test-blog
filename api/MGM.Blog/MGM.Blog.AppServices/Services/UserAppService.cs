using MGM.Blog.AppServices.Converters;
using MGM.Blog.AppServices.ViewModel;
using MGM.Blog.Domain.Services;

namespace MGM.Blog.AppServices.Services
{
    internal class UserAppService(IUserService service) : IUserAppService
    {
        public async Task CreateAsync(UserViewModel vm)
            => await service.CreateAsync(vm.ToDto());
    }
}
