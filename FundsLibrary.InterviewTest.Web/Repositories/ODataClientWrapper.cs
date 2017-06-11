using FundsLibrary.InterviewTest.Common.Domain;
using Simple.OData.Client;
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

        public async Task<IEnumerable<Security>> GetFundsForManager(Guid managerGuid)
        {
            var settings = new ODataClientSettings
            {
                BaseUri = new Uri(_serviceAppUrl),
                OnTrace = (format, args) => Console.WriteLine(String.Format(format, args)),
                IgnoreUnmappedProperties = true
            };

            settings.BeforeRequest = (message) =>
            {
                message.Headers.Add("Authorization",
                    "Bearer " + _authToken);
                message.Headers.Add("Accept", 
                    "application/json");
            };

            var client = new ODataClient(settings);

            var annotations = new ODataFeedAnnotations();
            
            var teamMembers = await client
                .For<Security>()
                .Search($"{managerGuid}")
                .FindEntriesAsync();
                                        
            return teamMembers;
        }
    }
}