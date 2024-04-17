using Entity;
using Manager;

using Repo.Imp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VM;
//using Web.Security;

namespace Web.Controllers
{
    //[SessionExpireAttribute]
    public class UsersController : Controller
    {
        private readonly UnitOfWork _reader = new UnitOfWork();
        int userID = 0;
        // GET: Users
        public ActionResult Account()
        {
            if (Session[SessionKeys.UserID]!=null)
            {
                userID =Convert.ToInt32(Session[SessionKeys.UserID]);
               
            }
            User model = new User();
            //new Entity.User();
            if (userID >0)
            {
                ///model = _reader.UserRepository.GetByID(userID);
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Account(User model)
        {
            try
            {
                //string result = string.Empty;
                //_reader.UserRepository.Update(model);
                //_reader.Save();
                //result = ResultKeys.RecordUpdated;
                //TempData[TempDataKeys.SuccessTemp] = result;
                return View(model);
            }
            catch (Exception ex)
            {

                TempData[TempDataKeys.ExceptionTemp] = ex.Message;
                return View(model);
            }

            
            
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(string oldPassword,string newPassword)
        {
            return View();
        }
     
        public ActionResult WelcomePage()
        {
            if (Session[SessionKeys.UserID] != null)
            {
                userID = Convert.ToInt32(Session[SessionKeys.UserID]);
            }
            User model =new User();
            //new Entity.User();
            //if (userID > 0)
            //{
            //    model = _reader.UserRepository.GetByID(userID);
            //}
            return View(model);
        }
        public class User
        {

        }
    }
}