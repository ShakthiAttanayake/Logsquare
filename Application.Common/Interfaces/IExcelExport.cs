using Logsquare.Domain;
using Logsquare.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logsquare.Application.Common.Interfaces
{
    public interface IExcelExport
    {
        Task<byte[]> ExportFileAsync(List<User> data, string fileName, ExportUserDto exportUserDto);
    }
}
