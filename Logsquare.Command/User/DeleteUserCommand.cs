using Logsquare.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logsquare.Command.User
{
    public record class DeleteUserCommand(int id) : IRequest<bool>;
}
