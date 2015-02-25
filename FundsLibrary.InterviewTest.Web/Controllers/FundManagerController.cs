﻿using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using FundsLibrary.InterviewTest.Web.Repositories;

namespace FundsLibrary.InterviewTest.Web.Controllers
{
    public class FundManagerController : Controller
    {
        private readonly IFundManagerModelRepository _repository;

        // ReSharper disable once UnusedMember.Global
        public FundManagerController()
            : this(new FundManagerModelRepository())
        {}

        public FundManagerController(IFundManagerModelRepository repository)
        {
            _repository = repository;
        }

        // GET: FundManager
        public async Task<ActionResult> Index()
        {
            return View(await _repository.GetAll());
        }

        // GET: FundManager/Details/id
        public async Task<ActionResult> Details(Guid id)
        {
            return View(await _repository.Get(id));
        }
    }
}
