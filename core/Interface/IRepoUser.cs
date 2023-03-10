using core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Interface
{
    public interface IRepoUser
    {
        Task<int> Register(user user, string password);
        Task<string> login(string userName, string password);
        Task<bool> UserExiste(string username);
    }
}
