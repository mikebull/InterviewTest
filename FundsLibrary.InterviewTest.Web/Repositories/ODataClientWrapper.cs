using FundsLibrary.InterviewTest.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FundsLibrary.InterviewTest.Web.Repositories
{
    public interface IODataClientWrapper
    {
        Task<IEnumerable<Security>> GetFundsForManager(Guid managerGuid);
    }

    public class ODataClientWrapper : IODataClientWrapper
    {
        private readonly string _serviceAppUrl;
        private readonly string _authToken;

        public ODataClientWrapper(string serviceAppUrl, string authToken)
        {
            _serviceAppUrl = serviceAppUrl;
            _authToken = authToken;
        }

        public Task<IEnumerable<Security>> GetFundsForManager(Guid managerGuid)
        {
            throw new NotImplementedException();
        }
    }
}