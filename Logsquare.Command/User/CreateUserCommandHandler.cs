using Logsquare.Domain;
using Logsquare.Dto;
using Logsquare.Infrastructure.Persistance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logsquare.Command.User
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly LogsqureDbContext _logsqureDbContext;

        public CreateUserCommandHandler(LogsqureDbContext logsqureDbContext)
        {
            _logsqureDbContext = logsqureDbContext;
        }
        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            Logsquare.Domain.User user = new Logsquare.Domain.User()
            {
                UserName = request.userDto.UserName,
                Email = request.userDto.Email,
                Password = request.userDto.Password
            };

            var domainUser = await _logsqureDbContext.Users.AddAsync(user);
            await _logsqureDbContext.SaveChangesAsync();

            return 
                    new UserDto() 
                    { 
                        Id = domainUser.Entity.Id,
                        UserName = domainUser.Entity.UserName,
                        Email = domainUser.Entity.Email,
                        Password = domainUser.Entity.Password 
                    };
        }
    }
}
