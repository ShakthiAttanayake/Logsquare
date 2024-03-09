using Logsquare.Dto;
using Logsquare.Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Logsquare.AthenticationProvider;
using Logsquare.AthenticationProvider.Models;

namespace Logsquare.Query
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthResponseDto>
    {
        private readonly LogsqureDbContext _logsqureDbContext;
        private readonly IAuthenticationService _authenticationService;
        public LoginQueryHandler(LogsqureDbContext logsqureDbContext, IAuthenticationService authenticationService)
        {
            _logsqureDbContext = logsqureDbContext;
            _authenticationService = authenticationService; 
        }
        public async Task<AuthResponseDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<UserDto> userList = new List<UserDto>();
                var user = await _logsqureDbContext.Users.FirstOrDefaultAsync(u => u.UserName == request.authDto.UserName && u.Password == request.authDto.PassWord);

                if (user != null)
                {
                    var token = await _authenticationService.GenerateJWTTokenAsync(
                        new IdentityUser()
                        {
                            UserName = user.UserName,
                            Password = user.Password
                        }
                        );
                    return new AuthResponseDto() { Token = token.Token };
                }
                else 
                {
                    return new AuthResponseDto();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }


}
