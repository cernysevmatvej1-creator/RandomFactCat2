using ApiDeepSeekl.Common;
using ApiDeepSeekl.Model;
using System;
using System.Collections.Generic;
using System.Text;
namespace ApiDeepSeekl.InterfaceService_
{
   public interface ICatService 
    {
        Task<Fact> GetFact();
        Task SaveFact(string username,Fact  fact);
        Task<Result<List<Fact>>> ListGetFacts();
        Task DeleteFact(int id);

    }
}
