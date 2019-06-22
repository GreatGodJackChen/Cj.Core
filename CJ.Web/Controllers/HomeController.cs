using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CJ.Web.Models;
using CJ.Application.Test;
using System.Reflection;

namespace CJ.Web.Controllers
{
    public class HomeController : Controller
    {
        private ITestAppService _testAppService;
        private ITestAutofacAppService _testAutofacAppService;
        public HomeController(ITestAppService testAppService, ITestAutofacAppService testAutofacAppService)
        {
            _testAppService = testAppService;
            _testAutofacAppService = testAutofacAppService;
            var ty = typeof(HomeController);
            var ty2 = ty.GetTypeInfo().IsAbstract;
        }
        public IActionResult Index()
        {
            var t= _testAppService.Test();
            var tAutofac = _testAutofacAppService.TestAutofac();
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
