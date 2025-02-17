using MGM.Blog.AppServices.Services;
using MGM.Blog.AppServices.ViewModel;
using MGM.Blog.Infrastructure.Api.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MGM.Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Gestão de motos")]
    public class PostController(IPostAppService postAppService) : ControllerBase
    {
        [HttpGet]
        [SwaggerOperation(Summary = "Lista de postagens")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<PostViewModel>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
        public async Task<IActionResult> ListAllByUserAsync()
            => Ok(await postAppService.ListByUserAsync());

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Bearer")]
        [SwaggerOperation(Summary = "Criação de postagem")]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(void))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
        [ServiceFilter(typeof(ExceptionFilter))]
        public async Task<IActionResult> PostAsync([FromBody] PostViewModel model)
        {
            await postAppService.CreateAsync(model);
            return Created();
        }

        [HttpPatch]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Bearer")]
        [SwaggerOperation(Summary = "Atualização parcial de postagem")]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(void))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
        [ServiceFilter(typeof(ExceptionFilter))]
        public async Task<IActionResult> PatchAsync([FromBody] PostViewModel model)
        {
            await postAppService.UpdateAsync(model);
            return Created();
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Bearer")]
        [SwaggerOperation(Summary = "Exclusão de postagem por identificador")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(void))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
        [ServiceFilter(typeof(ExceptionFilter))]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await postAppService.DeleteAsync(id);
            return Ok();
        }
    }
}
