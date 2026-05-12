using ApiDeepSeekl.InterfaceService_;
using ApiDeepSeekl.InterfisRepotisiory;
using ApiDeepSeekl.Model;
using System;
using System.Collections.Generic;
using System.Text;
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
using ApiDeepSeekl.Common;
using System.Diagnostics;
namespace ApiDeepSeekl.Service
{
    public class UserService : IUserService
    {
        private IUserRepotisiory _userRepotisiory;
        private static readonly HttpClient client = new HttpClient();
        public UserService( IUserRepotisiory userRepotisiory)
        {
         
            _userRepotisiory = userRepotisiory;
        }
        public string GetNik()
        {
         return  _userRepotisiory.GetNik();   
        }

        public async Task SignAnonimal(string username)
        {
            var request = new { UserName = username, UserId = "" };

            var response = await client.PostAsJsonAsync("https://localhost:44373/api/Answers/login", request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AutrResult>();
                await _userRepotisiory.SaveJwt(result.AssetsToken);
                await _userRepotisiory.SaveRefreshToken(result.RefreshToken);
                Debug.WriteLine("УРА УСПЕХ");
            }
          
        }
        public  async Task<Result> RefreshToken()
        {
            var refreshToken = await _userRepotisiory.GetRefreshToken();

            if (refreshToken == null)
                return Result.Fail("Refresh token отсутствует");

            var response = await client.PostAsJsonAsync(
                "https://localhost:44373/api/Answers/refresh",
                new { RefreshToken = refreshToken, AssetsToken = "" }
            );

            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                // Простой ручной парсинг JSON
                var assetsToken = ExtractJsonValue(responseBody, "assetsToken");

                if (string.IsNullOrEmpty(assetsToken))
                {
                    Debug.WriteLine("❌ Не удалось извлечь assetsToken из ответа");
                    return Result.Fail("Ошибка обработки ответа");
                }

                Debug.WriteLine($"✅ Токен получен: {assetsToken.Substring(0, 50)}...");
                await _userRepotisiory.SaveJwt(assetsToken);
                return Result.Ok();
            }

            return Result.Fail($"Ошибка обновления токена");
        }


        private string ExtractJsonValue(string json, string key)
        {
            try
            {
                var searchKey = $"\"{key}\":\"";
                var startIndex = json.IndexOf(searchKey);
                if (startIndex == -1) return null;

                startIndex += searchKey.Length;
                var endIndex = json.IndexOf("\"", startIndex);
                if (endIndex == -1) return null;

                return json.Substring(startIndex, endIndex - startIndex);
            }
            catch
            {
                return null;
            }
        }
    }
}
