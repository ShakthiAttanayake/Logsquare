using Logsquare.Application.Common.Interfaces;
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
        private readonly IHashAlgorithm _hashAlgorithm;

        public CreateUserCommandHandler(LogsqureDbContext logsqureDbContext, IHashAlgorithm hashAlgorithm)
        {
            _logsqureDbContext = logsqureDbContext;
            _hashAlgorithm = hashAlgorithm;
        }
        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            byte[] salt = null;
            string password = _hashAlgorithm.HashPasword(request.userDto.Password, out salt);
            Logsquare.Domain.User user = new Logsquare.Domain.User()
            {
                UserName = request.userDto.UserName,
                Email = request.userDto.Email,
                Password = password,
                Salt = salt
            };

            var domainUser = await _logsqureDbContext.Users.AddAsync(user);
            await _logsqureDbContext.SaveChangesAsync();

            return 
                    new UserDto() 
                    { 
                        Id = domainUser.Entity.Id,
                        UserName = domainUser.Entity.UserName,
                        Email = domainUser.Entity.Email,
                        Password = null 
                    };
        }
    }
}
