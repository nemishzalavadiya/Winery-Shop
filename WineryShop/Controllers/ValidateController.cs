using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WineryShop.Core.Models;
namespace WineryShop.Controllers
{
    public class ValidateController : Controller
    {
        // GET: Validate
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        } //this function Convert to Decord your Password
        public string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }
        public ActionResult Index()
        {
            return RedirectToAction("Index","Home");
        }
      
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult LoginDone(Login login)
        {
            ConModel11 db = new ConModel11();
            string ValPas = EncodePasswordToBase64(login.Password);
            var model = db.Logins.FirstOrDefault(x=>x.Username.Equals(login.Username) && x.Password.Equals(ValPas));
            if (model == null)
            {
                TempData["msg"] = "Invalide Credentials";
            }
            else
            {
                TempData["msg"] = "Login SuccessFully " + model.Designation;
                if (model.Designation.Equals("admin"))
                {
                    Session["Admin"] = "admin";
                }
                Session["Username"] = login.Username;
            }
           
            return RedirectToAction("Index","Home");
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterDone(Login login)
        {
            ConModel11 db = new ConModel11();

            if (db.Logins.Find(login.Username) == null)
            {
                Login l = new Login();
                l.Username = login.Username;
                l.Password = EncodePasswordToBase64(login.Password);
                l.Designation = "user";
                if (login.Email != null)
                {

                    l.Email = login.Email;
                }
                db.Logins.Add(l);
                db.SaveChanges();
                TempData["msg"] = "Registration SuccessFully";
                return RedirectToAction("Index", "Home");
            }
            else {
                TempData["msg"] = "Provided Username isnot Available";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout() {
            TempData["msg"] = "Logged Out SuccessFully";
            Session["Username"] = null;
            if (Session["Admin"] != null)
            {
                Session["Admin"] = null;
            }
            return RedirectToAction("Index","Home");
        }
        public ActionResult Fotget() {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Forgot(string reportName) {
           
            ConModel11 db = new ConModel11();
            string user = reportName;

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            Login model = db.Logins.Find(user);

            if (model == null) {
                TempData["msg"] = "Invalide Username : "+user;
                return RedirectToAction("Index","Home");
            }
            if (model.Email == null)
            {
                TempData["msg"] = "Sorry You haven't Register your Email address";
                return RedirectToAction("Index", "Home");
            }
            model.Password = EncodePasswordToBase64(finalString);
            db.SaveChanges();

            var smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com", // set your SMTP server name here
                Port = 587, // Port 
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("17ceuog074@ddu.ac.in", "vicyqgbsihpnfoou")
            };
            try
            {
                using (var message = new MailMessage("17ceuog074@ddu.ac.in", model.Email)
                {
                    Subject = "New Password",
                    Body = "Here is your new password \n donot share with anyone \n password: " + finalString

                })
                    await smtpClient.SendMailAsync(message);
            }
            catch (Exception e) {
                TempData["msg"] = "Your registered Email is invalide";
                return RedirectToAction("Index","Home");
            }
            TempData["msg"] = "New Password sended to registered email address";
            return RedirectToAction("Index","Home");
        }
        public ActionResult Email() {
            return View();
        }
        public ActionResult EmailSet(string Email) {
            ConModel11 db = new ConModel11();
            string user = Session["Username"].ToString();
            Login model = db.Logins.Find(user);
            model.Email = Email;
            db.SaveChanges();
            TempData["msg"] = "Email set Successfully";
            return RedirectToAction("Index","Home");
        }
   
    }
}