using FundsLibrary.InterviewTest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FundsLibrary.InterviewTest.Web.Repositories
{
    public interface ISecuritiesRepository
    {
        Task<IEnumerable<Fund>> GetFunds(Guid managerGuid);
    }

    public class SecuritiesRepository : ISecuritiesRepository
    {
        private readonly IODataClientWrapper _client;

        public SecuritiesRepository(IODataClientWrapper client)
        {
            _client = client;
        }

        public Task<IEnumerable<Fund>> GetFunds(Guid managerGuid)
        {
            throw new NotImplementedException();
        }
    }
}