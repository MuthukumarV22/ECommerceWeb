using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Data.Entity;
using System.Data;
using System.Web.Mvc;

namespace ECommerceWeb.Controllers
{
    public class AccountController : Controller
    {
        dbEcommerceEntities _context;
        public AccountController()
        {
            _context = new dbEcommerceEntities();
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Tble_User user)
        {
            if (!ModelState.IsValid)
                return View("Register", user);

            if(ModelState.IsValid)
            {
                _context.Tble_User.Add(user);
                _context.SaveChanges();

                return Content("Registered!!!");
            }
            

            return Content("Registered!!!");
        }
        public ActionResult UserLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserLogin(Tble_User user)
        {
            if (!ModelState.IsValid)
                return View("UserLogin", user);

            var loginuser = _context.Tble_User.Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefault();

            if (loginuser == null)
            {
                ModelState.AddModelError("Email", "Email or Password Is Incorrect, please try with correct username or password");
                return View("Login", user);
            }
            else
            {
                Session["Role"] = loginuser.UserRole;
                return RedirectToAction("Index", "Home");
            }

        }
        public ActionResult UserRegister()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserRegister(Tble_User user)
        {
            if (!ModelState.IsValid)
                return View("UserRegister", user);

            if (_context.Tble_User.Where(u => u.Email == user.Email || u.FirstName == user.FirstName).Any())
            {
                ModelState.AddModelError("Email", "UserName or Email Already Exists");
                return View("UserRegister", user);
            }

            //if (ModelState.IsValid)
            //{
            //    return View("UserRegister", user);
            //}
            _context.Tble_User.Add(user);
            _context.SaveChanges();
            ViewBag.msg = "Registered Successfully!!...";

            return RedirectToAction("UserLogin");
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("UserLogin");
        }
    }
}