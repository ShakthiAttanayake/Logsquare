using Logsquare.Domain;
using Logsquare.Dto;
using Logsquare.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Logsquare.API.Controllers
{
    public class AuthenticationController
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("Login", Name = "Login")]
        public async Task<AuthResponseDto> Login([FromBody] AuthDto authDto)
        {
            var result = await _mediator.Send(new LoginQuery(authDto));
            return result;
        }
    }
}
