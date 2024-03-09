using Logsquare.Application.Common.Interfaces;
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
    public class ExportUsersToExcelQueryHandler : IRequestHandler<ExportUsersToExcelQuery, byte[]>
    {
        private readonly LogsqureDbContext _logsqureDbContext;
        private readonly IExcelExport _excelExport;
        public ExportUsersToExcelQueryHandler(LogsqureDbContext logsqureDbContext, IExcelExport excelExport)
        {
            _logsqureDbContext = logsqureDbContext;
            _excelExport = excelExport;
        }
        public async Task<byte[]> Handle(ExportUsersToExcelQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<UserDto> userList = new List<UserDto>();
                ExportUserDto exportUserDto = new ExportUserDto();
                var users = await _logsqureDbContext.Users.Where(u => !u.IsDeleted).ToListAsync();
                foreach (var item in users)
                {
                    userList.Add(new UserDto()
                    {
                        Id = item.Id,
                        UserName = item.UserName,
                        Email = item.Email,
                    });
                }
                var result = await _excelExport.ExportFileAsync(users,"User List Excel Export ", exportUserDto);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }


}
