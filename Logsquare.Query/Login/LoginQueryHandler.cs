using Logsquare.Dto;
using Logsquare.Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Logsquare.AthenticationProvider;
using Logsquare.AthenticationProvider.Models;
using Logsquare.Application.Common.Interfaces;

namespace Logsquare.Query
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthResponseDto>
    {
        private readonly LogsqureDbContext _logsqureDbContext;
        private readonly IAuthenticationService _authenticationService;
        private readonly IHashAlgorithm _hashAlgorithm;

        public LoginQueryHandler(LogsqureDbContext logsqureDbContext, IAuthenticationService authenticationService, IHashAlgorithm hashAlgorithm)
        {
            _logsqureDbContext = logsqureDbContext;
            _authenticationService = authenticationService; 
            _hashAlgorithm = hashAlgorithm;
        }
        public async Task<AuthResponseDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var matcheduser =  _logsqureDbContext.Users.FirstOrDefault(u => u.UserName == request.authDto.UserName);
                if (matcheduser != null) 
                {
                    if (_hashAlgorithm.VerifyPassword(request.authDto.PassWord,matcheduser.Password,matcheduser.Salt)) 
                    {
                        var token = await _authenticationService.GenerateJWTTokenAsync(
                        new IdentityUser()
                        {
                            UserName = matcheduser.UserName,
                            Password = matcheduser.Password
                        }
                        );
                        return new AuthResponseDto() { Token = token.Token };
                    }
                }

                return new AuthResponseDto();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }


}
