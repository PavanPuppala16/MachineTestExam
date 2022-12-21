using MachineTestExam.Logic;
using MachineTestExam.Models;
using Microsoft.AspNetCore.Mvc;

namespace MachineTestExam.Controllers
{
    public class PaymentController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(PaymentMode obj)
        {

            if (ModelState.IsValid)
            {
                bool res = InsertingData.paymentInserting(obj);

                if (res == true)
                {
                    ViewBag.Message = "PaymentMethod ";

                    return RedirectToAction("PhotoUpload", "MachineProject");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
        public IActionResult DiaplayPayment()
        {

            return View(InsertingData.GetALLDataByPayment());

        }
    }
}
