using FundsLibrary.InterviewTest.Common;
using Microsoft.AspNet.Identity;
using System;

namespace FundsLibrary.InterviewTest.Web.Services
{
    public class FundsLibraryUserManager : UserManager<User, Guid>
    {
        public FundsLibraryUserManager(IUserStore<User, Guid> store)
            : base(store)
        {}
    }
}