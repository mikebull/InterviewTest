using FundsLibrary.InterviewTest.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Fund>> GetFunds(Guid managerGuid)
        {
            var securities = await _client.GetFundsForManager(managerGuid);

            var funds = new List<Fund>();

            foreach (var security in securities)
            {
                var staticData = security.StaticData;

                var identification = staticData.Identification;
                var essentials = staticData.Essentials;

                if (identification != null && essentials != null)
                {
                    funds.Add(new Fund
                    {
                        IsinCode = identification.IsinCode,
                        FullName = identification.FullName,
                        IASector = essentials.IaSector,
                        Objectives = essentials.Objectives,
                        BenchmarkDescription = essentials.BenchmarkDescription
                    });
                }
            }

            return funds;
        }
    }
}