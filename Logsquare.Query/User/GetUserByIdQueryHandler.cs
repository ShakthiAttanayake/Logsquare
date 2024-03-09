using Logsquare.Dto;
using Logsquare.Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logsquare.Query
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly LogsqureDbContext _logsqureDbContext;
        public GetUserByIdQueryHandler(LogsqureDbContext logsqureDbContext)
        {
            _logsqureDbContext = logsqureDbContext;
        }
        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                UserDto userDto = new UserDto();
                var user = await _logsqureDbContext.Users.FirstOrDefaultAsync(u => u.Id == request.Id && !u.IsDeleted);
                userDto = new UserDto()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                };
                return userDto;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }


}
