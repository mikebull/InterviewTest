﻿using FundsLibrary.InterviewTest.Common;
using FundsLibrary.InterviewTest.Web.Repositories;
using PagedList;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FundsLibrary.InterviewTest.Web.Controllers
{
    [Authorize]
    public class FundManagerController : Controller
    {
        private readonly IFundManagerRepository _repository;

        private readonly ISecuritiesRepository _securitiesRepository;

        public FundManagerController(IFundManagerRepository repository, ISecuritiesRepository securitiesRepository)
        {
            _repository = repository;
            _securitiesRepository = securitiesRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> Index(string sortOrder = "", int? page = null)
        {
            var fundManagersList = await _repository.GetAll();
            ViewBag.CurrentSort = sortOrder;

            switch (sortOrder)
            {
                case "Name":
                    fundManagersList = fundManagersList.OrderBy(n => n.Name);
                    break;
                case "Location":
                    fundManagersList = fundManagersList.OrderBy(n => n.Location.ToString());
                    break;
                case "Biography":
                    fundManagersList = fundManagersList.OrderBy(n => n.Biography);
                    break;
                case "ManagedSince":
                    fundManagersList = fundManagersList.OrderBy(n => n.ManagedSince);
                    break;
                default:
                    fundManagersList = fundManagersList.OrderByDescending(n => n.Name);
                    break;
            }

            var pageNumber = page ?? 1;
            const int pageSize = 3;

            return View(fundManagersList.ToPagedList(pageNumber, pageSize));
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return _UnverifiedFundManagerId("A Fund Manager Id was not provided or was in an invalid format");
            }
            var result = await _repository.Get(id.Value);
            if (result == null)
            {
                return _UnverifiedFundManagerId($"The Fund Manager id {id.Value} was not found");
            }

            var funds = await _securitiesRepository.GetFunds(result.Id);
            
            result.Funds = funds;
            
            return View(result);
        }

        private RedirectToRouteResult _UnverifiedFundManagerId(string errorMessage)
        {
            return RedirectToAction("Index", "Error", new { errorMessage });
        }

        [HttpGet]
        public ViewResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(FundManager newManager)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.Post(newManager);
                return RedirectToAction("Details", new { id = result });
            }
            return View(newManager);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return _UnverifiedFundManagerId("A Fund Manager Id was not provided or was in an invalid format");
            }
            await _repository.Delete(id.Value);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return _UnverifiedFundManagerId("A Fund Manager Id was not provided or was in an invalid format");
            }
            var result = await _repository.Get(id.Value);
            if (result == null)
            {
                return _UnverifiedFundManagerId($"The Fund Manager id {id.Value} was not found");
            }
            return View(result);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(FundManager editManager)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.Put(editManager);
                return RedirectToAction("Details", new { id = result });
            }

            return View(editManager);
        }
    }
}
