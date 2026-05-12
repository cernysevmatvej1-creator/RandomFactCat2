using ApiDeepSeekl.Common;
using ApiDeepSeekl.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiDeepSeekl.InterfisRepotisiory
{
    public interface IFactRepotisiory
    {
        Task<Fact> GetFact();
        Task SaveFact(string username, Fact fact);
        Task<Result<List<Fact>>> ListGetFacts(string username,string token);
        Task DeleteFact(int id, string token);

    }
}
