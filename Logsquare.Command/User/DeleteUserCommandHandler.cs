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
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly LogsqureDbContext _logsqureDbContext;

        public DeleteUserCommandHandler(LogsqureDbContext logsqureDbContext)
        {
            _logsqureDbContext = logsqureDbContext;
        }
        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            int rowCount = await _logsqureDbContext.Users.Where(u => u.Id == request.id && !u.IsDeleted)
                                   .ExecuteUpdateAsync(
                                       setter => setter.SetProperty(ud => ud.IsDeleted, true));
            await _logsqureDbContext.SaveChangesAsync();
            return rowCount > 0;
        }
    }
}
