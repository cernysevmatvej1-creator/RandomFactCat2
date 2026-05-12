using System;
using System.Collections.Generic;
using System.Text;

namespace ApiDeepSeekl.InterfisRepotisiory
{
    public interface IUserRepotisiory
    {
        string GetNik();
        void SaveNik(string username);
        Task<string> GetJwt();
        Task SaveJwt(string jwt);
        Task<string> GetRefreshToken();
        Task SaveRefreshToken(string token);
    }
}
