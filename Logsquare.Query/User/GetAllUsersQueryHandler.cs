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

namespace Logsquare.Application
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly LogsqureDbContext _logsqureDbContext;
        public GetAllUsersQueryHandler(LogsqureDbContext logsqureDbContext)
        {
            _logsqureDbContext = logsqureDbContext;
        }
        public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<UserDto> userList = new List<UserDto>();
                var users = await _logsqureDbContext.Users.ToListAsync();
                foreach (var item in users)
                {
                    userList.Add(new UserDto()
                    {
                        Id = item.Id,
                        UserName = item.UserName,
                        Email = item.Email,
                        Password = item.Password,
                    });
                }
                return userList;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }


}
