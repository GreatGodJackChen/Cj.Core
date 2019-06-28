using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CJ.Web.Models;
using CJ.Application.Test;
using System.Reflection;
using Autofac.Core;
using CJ.Data.FirstModels;
using CJ.Application;
using CJ.Application.Address;
using CJ.Repositories.Interceptor;

namespace CJ.Web.Controllers
{
    public class HomeController : Controller
    {
        private ITestAppService _testAppService;
        private ITestAutofacAppService _testAutofacAppService;
        private readonly IPersonAppService _personAppService;
        private readonly IAddressAppService _addressAppService;

        private readonly IServiceProvider _serviceProvider;
        //public HomeController(ITestAppService testAppService, ITestAutofacAppService testAutofacAppService,IPersonAppService personAppService)
        //{
        //    _testAppService = testAppService;
        //    _testAutofacAppService = testAutofacAppService;
        //    _personAppService = personAppService;
        //    var ty = typeof(HomeController);
        //    var ty2 = ty.GetTypeInfo().IsAbstract;
        //}
        public HomeController( IPersonAppService personAppService, IServiceProvider serviceProvider,IAddressAppService addressAppService)
        {
            _personAppService = personAppService;
            _serviceProvider = serviceProvider;
            _addressAppService = addressAppService;
        }
        public IActionResult Index()
        {
            var address = _addressAppService.GetAddresses();
            //var tt = _serviceProvider.GetService(typeof(UnitOfWorkInterceptor));
            var testPerson = _personAppService.GetPersons();
           //var tAutofac = _testAutofacAppService.TestAutofac();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
