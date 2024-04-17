using Repo.Imp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using Manager;
using System.Configuration;

using VM;

namespace FunfoxDemo.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UnitOfWork _reader = new UnitOfWork();
        // GET: Accounts
        public ActionResult Login(AccountsVM user=null, string returnUrl = "")
        {
            //ValidateUser_Result validate = CheckCookies();
            //if (validate.ID > 0)
            //{
            //    Session[SessionKeys.UserID] = validate.ID;
            //    Session[SessionKeys.Name] = validate.FirstName;
            //    Session[SessionKeys.Photo] = validate.Attachment;
            //    Session[SessionKeys.UserTypeID] = validate.UserTypeID;
            //    if (!String.IsNullOrEmpty(returnUrl))
            //    {
            //        return Redirect(returnUrl);
            //    }
            //    else if (validate.UserTypeID == (int)UserTypesEnum.Admin || validate.UserTypeID == (int)UserTypesEnum.Editor)
            //    {
            //        return RedirectToAction("Index", "ManageDashboard", new { area = "Dashboard" });
            //    }
            //    else
            //    {
            //        return RedirectToAction("WelcomePage", "Users");
            //    }
            //}
            return View(user);
        }
        [HttpPost]
        public ActionResult Login(AccountsVM model)
        {
            //try
            //{
            //    if (!string.IsNullOrEmpty(model.LoginEmail) && !string.IsNullOrEmpty(model.LoginPassword))
            //    {
            //        var validate = _reader.ValidateUserRepository.GetFromProc("ValidateUser '" + model.LoginEmail + "','" + model.LoginPassword + "'").FirstOrDefault();
            //        if (validate != null)
            //        {
            //            string EncodedCredentials = FunctionManager.Base64Encode(model.LoginEmail + ":" + model.LoginPassword);
            //            Session[SessionKeys.UserID] = validate.ID;
            //            Session[SessionKeys.Name] = validate.FirstName;
            //            Session[SessionKeys.Photo] = validate.Attachment;
            //            Session[SessionKeys.UserTypeID] = validate.UserTypeID;
            //            if (validate.UserTypeID == (int)UserTypesEnum.Admin || validate.UserTypeID == (int)UserTypesEnum.Editor)
            //            {
            //                return RedirectToAction("Index", "ManageDashboard", new { area = "Dashboard" });
            //            }
            //            else if(validate.UserTypeID == (int)UserTypesEnum.Customer)
            //            {
            //                return RedirectToAction("WelcomePage", "Users");
            //            }
            //            else
            //            {
            //                TempData[TempDataKeys.LoginTemp] = ResultKeys.Authorize;
            //                return View(model);
            //            }
            //        }
            //        else
            //        {
            //            TempData[TempDataKeys.LoginTemp] = ResultKeys.NotFound;
            //            return View(model);
            //        }
            //    }
            //    else
            //    {
            //        TempData[TempDataKeys.LoginTemp] = ResultKeys.Validation;
            //        return View(model);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    TempData[TempDataKeys.LoginTemp] = ex.Message;
            //    return View(model);
            //}
            return View(model);
        }
       
        public ActionResult Logout()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(FunctionManager.DateTimeNow().AddHours(-1));
            Response.Cache.SetNoStore();
            if (Request.Cookies["UserDetails"] != null)
            {
                Response.Cookies["UserDetails"].Expires = FunctionManager.DateTimeNow().AddDays(-1);
            }
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login");
        }
        [HttpPost]
        public ActionResult Register(AccountsVM user)
        {
            //User model = new Entity.User();
            //string result = string.Empty;
            //try
            //{
            //        var existPhone = _reader.UserRepository.Get(a => a.Phone == user.RegisterPhone);
            //        var existEmail = _reader.UserRepository.Get(a => a.Email == user.Email);
            //        if (existPhone.Any())
            //        {
            //            TempData[TempDataKeys.RegisterTemp] = "Phone Number Already Exist.";
            //        }
            //        else if (existEmail.Any())
            //        {
            //            TempData[TempDataKeys.RegisterTemp] = "Email Already Exist.";
            //        }
            //        else if (user != null)
            //        {
            //            model.City = "Lahore";
            //            model.Country = "Pakistan";
            //            model.IsActive = true;
            //            model.Email = user.Email;
            //            model.Phone = user.RegisterPhone;
            //            model.Password = user.RegisterPassword;
            //            model.FirstName = user.Name;
            //            model.CreatedDate = FunctionManager.DateTimeNow();
            //            model.UserTypeID = (int)UserTypesEnum.Customer;
            //            _reader.UserRepository.Insert(model);
            //            _reader.Save();
            //            Session[SessionKeys.UserID] = model.ID;
            //            Session[SessionKeys.Name] = model.FirstName;
            //            Session[SessionKeys.Photo] = model.Attachment;
            //            Session[SessionKeys.UserTypeID] = model.UserTypeID;
            //            var validate = _reader.ValidateUserRepository.GetFromProc("ValidateUser '" + model.Email + "','" + model.Password + "'").FirstOrDefault();
                        
            //            String msgbody = System.IO.File.ReadAllText(Server.MapPath("~/Emails/Register.html"));
            //            var strBody = msgbody
            //                .Replace("_name_", model.FirstName)
            //                .Replace("_phone_", model.Phone)
            //                .Replace("_email_", model.Email)
            //                .Replace("_address_", model.Address)
            //                .Replace("_username_", model.Email)
            //                .Replace("_password_", model.Password)
            //                ;
            //            EmailSend("Registration", model.Email, strBody,"Dvoxe");
            //            TempData["ThanksMessage"] = "Your account is created successfully.";
            //        SetCookies(validate);
            //        return RedirectToAction("Thanks", "Static");
            //        }
            //}
            //catch (Exception ex)
            //{
            //    TempData[TempDataKeys.RegisterTemp] = ex.Message;
            //}
            return RedirectToAction("Login");
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(User model)
        {
            //if (model != null)
            //{
            //    var validate = _reader.UserRepository.GetSingle(a => a.Email == model.Email);
            //    if (validate != null)
            //    {
            //        String msgbody = System.IO.File.ReadAllText(Server.MapPath("~/Emails/ForgotPassword.html"));
            //        var strBody = msgbody
            //            .Replace(" _name_", validate.FirstName)
            //            .Replace("_phone_", validate.Phone)
            //            .Replace("_email_", validate.Email)
            //            .Replace("_address_", validate.Address)
            //            .Replace("_password_", validate.Password)
            //            ;
            //        TempData["ThanksMessage"] = "An Email with password sended successfully.";
            //        return RedirectToAction("Thanks", "Static");
            //    }
            //    else
            //    {
            //        TempData[TempDataKeys.LoginTemp] = ResultKeys.NotFound;
            //        return View(model);
            //    }
            //}
            return View();
        }
       public class User
        {

        }
    }
}