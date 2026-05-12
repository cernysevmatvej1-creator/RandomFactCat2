using System;
using System.Collections.Generic;
using System.Text;
using ApiDeepSeekl.Common;
namespace ApiDeepSeekl.InterfaceService_
{
    public  interface IUserService
    {
        Task SignAnonimal(string username);
      string GetNik();
        Task<Result> RefreshToken();
    }
}
