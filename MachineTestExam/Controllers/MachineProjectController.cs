using MachineTestExam.Models;
using Microsoft.AspNetCore.Mvc;
using MachineTestExam.Logic;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Reflection;

namespace MachineTestExam.Controllers
{
    public class MachineProjectController : Controller
    {

        [HttpGet]
       
            public IActionResult LoginPage()
            {
                return View();
            }
        [HttpPost]
            public IActionResult LoginPage(LoginModel obj)
            {
                if (ModelState.IsValid)
                {
                    DataTable dt = new DataTable();
                    dt = InsertingData.login(obj);

                    if (dt.Rows.Count > 0)
                    {

                        return RedirectToAction("PhotoUpload", "MachineProject");
                    }

                    else
                    {
                        return View(obj);
                    }
                }
                else
                {
                    return View();
                }
            }
        [HttpGet]
            public IActionResult RegisterPage()
            {
                return View();
            }
        [HttpPost]
       
        [ValidateAntiForgeryToken]
        public IActionResult RegisterPage(Registration obj)
            {
                
                if (ModelState.IsValid)
                {
                    bool res = InsertingData.Insertdata(obj);

                    if (res == true)
                    {
                    ViewBag.Message = "formsubmitted";

                    return RedirectToAction("LoginPage");
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
        public IActionResult Logout()
        {
            return View();
        }
        public IActionResult DiaplayData()
        {
            
                return View(InsertingData.GetALLData());
            
        }
        [SetSessionGlobally]
        [ValidateAntiForgeryToken]
        public IActionResult Delete()
        {
            return View( InsertingData.DeleteData);
        }
        

        [HttpGet]
        public IActionResult Update(int? UserId)
        {
            return View(InsertingData.GetDataByID((int)UserId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SetSessionGlobally]
        
        public IActionResult Update(Registration obj)
        {
            if (ModelState.IsValid)
            {
                bool res = InsertingData.UpdateData(obj);
                if (res == true)
                {
                    return RedirectToAction("DiaplayData");
                }
                else
                {
                    return View(obj);
                }
            }
            return View();
        }

      
      
        public IActionResult PhotoUpload()
        {
            return View();
        }

        //forget password

        [HttpGet]
       
    
        public IActionResult Forgetpassword()
        {
            return View();
        }
        public IActionResult Forgetpassword(Registration OBJ)
        {
            Random rand = new Random();
            HttpContext.Session.SetString("OTP", rand.Next(1111, 9999).ToString());
            bool result = SendEmail(OBJ.EmailId);
            if (result == true)
            {

                return RedirectToAction("FORGOTPASSWORDOtp", "MachineProject");
            }
            return View();
        }
        [HttpGet]
        public IActionResult FORGOTPASSWORDOtp()
        {
            return View();
        }
        [HttpPost]
  
        
        public IActionResult FORGOTPASSWORDOtp(OtpModel obj)
        {

            if (obj.Otp.Equals(HttpContext.Session.GetString("OTP")))
            {
                return RedirectToAction("SETTINGNEWPASSWORD", "MachineProject");
            }
            else
            {
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult SETTINGNEWPASSWORD()
        {
            return View();
        }
        [HttpPost]

        public IActionResult SETTINGNEWPASSWORD(ForgetPasswordModel OBJ)
        {
            if (ModelState.IsValid)
            {
                bool res = InsertingData.UPDATEDATABYEMAILID(OBJ);
                if (res == true)
                {
                    return RedirectToAction("Loginpage", "MachineProject");
                }
                else
                {
                    return View(OBJ);
                }
            }
            else
            {
                return View(OBJ);
            }
        }



        public bool SendEmail(string receiver)
        {
            bool chk = false;
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("pavanpuppala1616@gmail.com");
                mail.To.Add(receiver);
                mail.IsBodyHtml = true;
                mail.Subject = "OTP";
                mail.Body = "Your OTP is :" + HttpContext.Session.GetString("OTP");
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.Credentials = new NetworkCredential("pavanpuppala1616@gmail.com", "yvddbdfpddgilocn");
                client.EnableSsl = true;
                client.Send(mail);
                chk = true;
            }
            catch (Exception)
            {
                throw;
            }
            return chk;
        }



        [HttpGet]
        public IActionResult VerifyOtp()
        {
            return View();
        }
        [HttpPost]
    
        public IActionResult VerifyOtp(OtpModel obj)
        {
            if (obj.Otp.Equals(HttpContext.Session.GetString("OTP")))
            {
                return RedirectToAction("LoginPage", "MachineProject");
            }
            else
            {
                return View();
            }
        }








        public IActionResult show()
            {
            return View();
        }











    }
}


