using Gladiators.Application.GameManager.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gladiators.Common.API
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class ApiControllerBase : ControllerBase
    {
        protected readonly IGameManagerRepository repository;
        public ApiControllerBase(IGameManagerRepository repository)
        {
            this.repository = repository;
        }
    }
}