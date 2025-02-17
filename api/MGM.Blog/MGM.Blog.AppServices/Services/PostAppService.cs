using MGM.Blog.AppServices.Converters;
using MGM.Blog.AppServices.ViewModel;
using MGM.Blog.Domain.Dtos;
using MGM.Blog.Domain.Services;
using MGM.Blog.Infrastructure.Api.ErrorHandling.HttpExceptions;
using System.Net;

namespace MGM.Blog.AppServices.Services
{
    internal class PostAppService(
        IPostService service, 
        IContextAccessorService contextAccessorService,
        IDefaultHttpClientErrorResponseHandler defaultHttpClientErrorResponseHandler) : IPostAppService
    {
        public async Task CreateAsync(PostViewModel vm)
        {
            await service.CreateAsync(vm.ToDto() with
            {
                UserId = TryParseUserId(contextAccessorService)
            });
        }

        public async Task DeleteAsync(Guid id)
            => await service.DeleteAsync(id);

        public async Task<IEnumerable<PostDto>> ListByUserAsync() =>
            await service.ListByUserAsync();

        public async Task UpdateAsync(PostViewModel vm)
        {
            var result = await service.UpdateAsync(vm.Id ?? Guid.NewGuid(), vm.ToDto() with
            {
                UserId = TryParseUserId(contextAccessorService)
            });

            if (!result.Success)
                throw defaultHttpClientErrorResponseHandler.ThrowResponseError(HttpStatusCode.BadRequest, string.Empty, string.Join(',', result.Error));
        }

        private Guid TryParseUserId(IContextAccessorService contextAccessorService)
        {
            var rawUserId = contextAccessorService.GetUserId();
            if (!Guid.TryParse(rawUserId, out Guid userId))
                throw defaultHttpClientErrorResponseHandler.ThrowResponseError(HttpStatusCode.BadRequest, string.Empty, "Usuário não autenticado para essa operação.");
            return userId;
        }
    }
}
