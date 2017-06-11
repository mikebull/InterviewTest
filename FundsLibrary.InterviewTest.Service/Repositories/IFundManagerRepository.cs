using FundsLibrary.InterviewTest.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundsLibrary.InterviewTest.Service.Repositories
{
    public interface IFundManagerRepository
    {
        Task<FundManager> GetById(Guid id);
        Task<IEnumerable<FundManager>> GetAll();
        Task<Guid> Update(FundManager fundManager);
        Task<Guid> Create(FundManager fundManager);
        Task<bool> Delete(Guid id);
    }
}
