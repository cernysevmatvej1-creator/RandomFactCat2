using ApiDeepSeekl.InterfisRepotisiory;
using ApiDeepSeekl.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using ApiDeepSeekl.InterfaceService_;

using System;
using System.Collections.Generic;
using System.Net.Http.Json;

using System.Diagnostics;


namespace ApiDeepSeekl.Repotisiory
{
    public class UserRepotisiory : IUserRepotisiory
    {
        private const string username = "username";
        private const string _jwt = "jwt";
        private const string _refreshToken = "refreshToken";

        public string GetNik() => Preferences.Get(username, "");
        public void SaveNik(string usernam) => Preferences.Set(username, usernam);
        public async Task<string> GetJwt() => await SecureStorage.GetAsync(_jwt);
        public async Task SaveJwt(string jwt) { await SecureStorage.SetAsync(_jwt, jwt); }
        public async Task<string> GetRefreshToken() => await SecureStorage.GetAsync(_refreshToken);
        public async Task SaveRefreshToken(string token) => await SecureStorage.SetAsync(_refreshToken, token);
    }
}
