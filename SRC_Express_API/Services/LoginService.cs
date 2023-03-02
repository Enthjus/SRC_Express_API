using SRC_Express_API.Models;
using SRC_Express_API.ModelsInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRC_Express_API.Services
{
    public interface LoginService
    {
        public AccountInfo Login(AccountInfo accInfo);
        public Boolean Register(AccountInfo accInfo);
    }
}
