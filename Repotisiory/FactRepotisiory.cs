using ApiDeepSeekl.Common;
using ApiDeepSeekl.InterfaceService_;
using ApiDeepSeekl.InterfisRepotisiory;
using ApiDeepSeekl.Model;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
namespace ApiDeepSeekl.Repotisiory
{
 public class FactRepotisiory : IFactRepotisiory
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<Fact> GetFact()
        {
            string url = "https://catfact.ninja/fact";

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                 
                    using JsonDocument doc = JsonDocument.Parse(jsonResponse);
                    string fact = doc.RootElement.GetProperty("fact").GetString();
                    Random random = new Random();

                    return new Fact { Id = random.Next(0,1000000), Text = fact };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        public async Task SaveFact(string username, Fact fact)
        {
            var request = new
            {
                Name = username,
                Text = fact.Text
            };
            
            var response = await client.PostAsJsonAsync("https://localhost:44373/api/Answers/save-otvet", request);

            Debug.WriteLine($"Статус ответа: {response.StatusCode}");

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("✅ Данные сохранены успешно!");
            }
            else
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"❌ Ошибка сохранения: {response.StatusCode}");
                Debug.WriteLine($"Тело ошибки: {errorBody}");
            }
        }
        public async Task<Result<List<Fact>>> ListGetFacts(string username, string token)
        {
            if (username == null || token == null)
                return Result<List<Fact>>.Fail("null username is token");

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("https://localhost:44373/api/Answers/facts-user");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                Debug.WriteLine("401 ERORR");
                return Result<List<Fact>>.Fail("401");
                
            }

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<ApiResponse>();
                if (data != null)
                    return Result<List<Fact>>.Ok(data.Data);
            }

            return Result<List<Fact>>.Fail("Нету фактов");
        }

        public async Task DeleteFact(int id, string token)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var response = await client.DeleteAsync($"https://localhost:44373/api/Answers/fact/{id}");

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Факт удалён");
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    Debug.WriteLine("401 - Не авторизован");
                }
                else if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    Debug.WriteLine("403 - Не твой факт");
                }
                else
                {
                    Debug.WriteLine($"Ошибка: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}
