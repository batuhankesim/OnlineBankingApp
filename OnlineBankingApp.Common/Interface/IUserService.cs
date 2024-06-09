using OnlineBankingApp.Common.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankingApp.Common.Interface
{
    public interface IUserService
    {
        public Task<bool> RegisterUserAsync(UserRequest request);

        public Task<string> LoginUserAsync(UserRequest request);
    }
}
