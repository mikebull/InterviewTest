﻿using FundsLibrary.InterviewTest.Common;
using FundsLibrary.InterviewTest.Service.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace FundsLibrary.InterviewTest.Service.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserRepository _repository;

        // ReSharper disable once UnusedMember.Global
        public UserController()
            : this(new UserMemoryDb())
        {}

        public UserController(IUserRepository injectedRepository)
        {
            _repository = injectedRepository;
        }

        public async Task<Guid> Post(User user)
        {
            return await _repository.Create(user);
        }

        public async Task<IEnumerable<User>> Get()
        {
            return await _repository.GetAll();
        }

        [Route("api/User/{id:Guid}")]
        public async Task<User> Get(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task<User> Get(string id)
        {
            return await _repository.GetByUsername(id);
        }
    }
}
