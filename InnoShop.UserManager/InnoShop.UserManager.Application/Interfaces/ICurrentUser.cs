using InnoShop.UserManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoShop.UserManager.Domain.Interfaces
{
    public interface ICurrentUser
    {
        User User { get; init; }
        string RoleName { get; init; }
    }
}
