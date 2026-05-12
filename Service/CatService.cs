using ApiDeepSeekl.InterfaceService_;
using ApiDeepSeekl.InterfisRepotisiory;
using ApiDeepSeekl.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using ApiDeepSeekl.Common;
using System.Text.Json;
namespace ApiDeepSeekl.Service
{
    public class CatService : ICatService
    {
        private static readonly HttpClient client = new HttpClient();
        private IFactRepotisiory _factRepotisiory;
        private IUserRepotisiory _userRepotisiory;
        public CatService(IFactRepotisiory factRepotisiory, IUserRepotisiory userRepotisiory)
        {
            _factRepotisiory = factRepotisiory;
            _userRepotisiory = userRepotisiory;
        }

        public async Task<Fact> GetFact()
        {

            try
            {
                return await _factRepotisiory.GetFact();
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        public async Task SaveFact(string username, Fact fact)
        {
            await _factRepotisiory.SaveFact(username, fact);
            _userRepotisiory.SaveNik(username);

        }
        public async Task<Result<List<Fact>>> ListGetFacts()
        {
            var username = _userRepotisiory.GetNik();
            var token = await _userRepotisiory.GetJwt();
                    
            if (username == null || token == null)
                return null;

            var check = await _factRepotisiory.ListGetFacts(username, token);

            if (check.Message == "401")
            {
                    return Result<List<Fact>>.Fail("401");
            }
          

            if (check.Data != null)
                return Result<List<Fact>>.Ok(check.Data);

            Debug.WriteLine("Ошибка нету фактов");
            return null;
        }

        public async Task DeleteFact(int id)
        {
            var token = await _userRepotisiory.GetJwt();

           
            await _factRepotisiory.DeleteFact(id,token );
        }
    }

}
