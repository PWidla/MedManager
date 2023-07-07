using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public interface IUser
    {
        string EmailAddress { get; set; }
        string Password { get; set; }
        Role Role { get; set; }
    }
}
