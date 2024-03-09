using Logsquare.Application.Common.Interfaces;
using Logsquare.Domain;
using Logsquare.Dto;
using Logsquare.Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logsquare.Command.User
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly LogsqureDbContext _logsqureDbContext;
        private readonly IHashAlgorithm _hashAlgorithm;

        public UpdateUserCommandHandler(LogsqureDbContext logsqureDbContext, IHashAlgorithm hashAlgorithm)
        {
            _logsqureDbContext = logsqureDbContext;
            _hashAlgorithm = hashAlgorithm;
        }
        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
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

            int rowCount = await _logsqureDbContext.Users.Where(u => u.Id == request.userDto.Id)
                                    .ExecuteUpdateAsync(
                                        setter => setter.SetProperty(ud => ud.UserName,user.UserName)
                                                        .SetProperty(ud => ud.Email,user.Email)
                                                        .SetProperty(ud => ud.Password,user.Password)
                                                        );
            await _logsqureDbContext.SaveChangesAsync();
            return rowCount > 0;
        }
    }
}
