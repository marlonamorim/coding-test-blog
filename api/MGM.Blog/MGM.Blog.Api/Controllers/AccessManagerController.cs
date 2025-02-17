using MGM.Blog.Api.Auth;
using MGM.Blog.AppServices.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MGM.Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Gestão de acesso")]
    public class AccessManagerController(IAccessManager accessManager) : Controller
    {
        private readonly IAccessManager _accessManager = accessManager;

        [HttpPost(Name = "Login")]
        [SwaggerOperation(Summary = "Realiza autenticação de um usuário pelas credenciais de acesso.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(LoginViewModel))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
        public async Task<IActionResult> PostAsync([FromBody] CredentialViewModel model)
        {
            if (await _accessManager.CredentialsIsValid(model))
                return Ok(await _accessManager.GenerateTokenAsync(model.Email));

            return Unauthorized();
        }
    }
}