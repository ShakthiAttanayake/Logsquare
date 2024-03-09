using Logsquare.Command.User;
using Logsquare.Domain;
using Logsquare.Dto;
using Logsquare.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Logsquare.API.Controllers
{
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public IActionResult Index()
        {
            return View();
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<User>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetAllUsers", Name = "GetAllUsers")]
        public async Task<List<UserDto>> GetEmployeeAllowances()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());
            return result;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetUserById", Name = "GetUserById")]
        public async Task<UserDto> GetUserById(int Id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(Id));
            return result;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("CreateUser", Name = "CreateUser")]
        public async Task<UserDto> CreateUser(UserDto userDetails)
        {
            var result = await _mediator.Send(new CreateUserCommand(userDetails));
            return result;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("UpdateUser", Name = "UpdateUser")]
        public async Task<bool> UpdateUser(UserDto userDetails)
        {
            var result = await _mediator.Send(new UpdateUserCommand(userDetails));
            return result;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("DeleteUser", Name = "DeleteUser")]
        public async Task<bool> DeleteUser(int id)
        {
            var result = await _mediator.Send(new DeleteUserCommand(id));
            return result;
        }
    }
}
