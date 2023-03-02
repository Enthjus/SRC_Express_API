using SRC_Express_API.Models;
using SRC_Express_API.ModelsInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRC_Express_API.Services
{
    public interface AccountService
    {
        public List<AccountInfo> FindAllAccountBySA();
        public List<AccountInfo> FindAllAccountByAdmin();
        public AccountInfo FindIDAccountBySA(string idacc);
        public AccountInfo UpdateAccount(AccountInfo acc);
        public bool DeleteAccount(string idacc);
        public Boolean DeletePhoto(string idacc);

    }
}
