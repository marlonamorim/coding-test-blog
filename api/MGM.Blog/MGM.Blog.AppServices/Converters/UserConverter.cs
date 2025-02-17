using MGM.Blog.AppServices.ViewModel;
using MGM.Blog.Domain.Dtos;

namespace MGM.Blog.AppServices.Converters
{
    public static class UserConverter
    {
        public static UserDto ToDto(this UserViewModel vm)
            => new(Id: vm.Id ?? Guid.NewGuid(),
                TaxDocument: vm.TaxDocument,
                Email: vm.Email,
                Password: vm.Password,
                BirthDate: vm.BirthDate,
                LastName: vm.LastName,
                FirstName: vm.FirstName);
    }
}
