using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoShop.UserManager.Application.Users.Commands.DeactivateUser
{
    public record DeactivateUserCommand(Guid userId):IRequest;
    
}
