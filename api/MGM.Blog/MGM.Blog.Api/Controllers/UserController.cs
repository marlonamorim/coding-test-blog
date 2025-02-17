using MGM.Blog.AppServices.Services;
using MGM.Blog.AppServices.ViewModel;
using MGM.Blog.Infrastructure.Api.Filters;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MGM.Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Gestão de usuários")]
    public class UserController(IUserAppService appService) : ControllerBase
    {
        private readonly IUserAppService _appService = appService;

        [HttpPost]
        [SwaggerOperation(Summary = "Cadastro de usuário")]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(void))]
        [ServiceFilter(typeof(ExceptionFilter))]
        public async Task<IActionResult> PostAsync([FromBody] UserViewModel model)
        {
            await _appService.CreateAsync(model);
            return Created();
        }
    }
}
