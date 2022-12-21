using MachineTestExam.Logic;
using Microsoft.AspNetCore.Mvc;

namespace MachineTestExam.Controllers
{
    public class SelectIDController : Controller
    {
        [HttpGet]
        public IActionResult ViewDropDownVal()
        {
            ViewBag.data = GetID.PopulateData();
            return View();
        }
        [HttpPost]
        public IActionResult ViewDropDownVal(string x)
        {
            ViewBag.data = Request.Form["test"].ToString();
            return View();
        }

        [HttpGet]
        public IActionResult GetDataonDDL()
        {
            ViewBag.data = GetID.PopulateData();
            return View();
        }
        [HttpPost]
        public IActionResult GetDataonDDL(string customers)
        {
            ViewData["Val"] = GetID.GETProductListbyID(Request.Form["test"].ToString());
            ViewBag.data = GetID.PopulateData();
            return View();
        }

    }
}
