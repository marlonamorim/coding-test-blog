using MGM.Blog.Domain.Dtos;
using MGM.Blog.Domain.Models;

namespace MGM.Blog.Domain.Converters
{
    public static class UserConverter
    {
        public static User ToEntity(this UserDto dto)
            => User.Create(dto.FirstName, dto.LastName, dto.BirthDate, dto.Email, dto.Password, dto.TaxDocument!);

        public static UserDto ToDto(this User entity) =>
            new(
                entity.Id,
                entity.LastName,
                entity.FirstName,
                entity.BirthDate,
                entity.Email,
                entity.Password,
                entity.TaxDocument,
                entity.Posts?.ToCollectionPostDto()
            );
    }
}
