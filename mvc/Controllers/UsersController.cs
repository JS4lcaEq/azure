using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using mvc.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace mvc.Controllers
{
    public class LoginInfo
    {
        public string AuthenticationType;
        public bool IsAuthenticated;
        public UsersModel User;
    }

    public class UsersController : Controller
    {
        private static CustomUserManager _customUserManager;

        public CustomUserManager CstmUserManager
        {
            get
            {
                return (_customUserManager = HttpContext.GetOwinContext().GetUserManager<CustomUserManager>());
            }
        }

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


        public async System.Threading.Tasks.Task<ActionResult> CreateUser(string invite, string email, string password, string nickName)
        {
            var info = String.Format("AuthenticationType='{0}' IsAuthenticated='{1}' Name='{2}'",
                AuthManager.User.Identity.AuthenticationType,
                AuthManager.User.Identity.IsAuthenticated,
                AuthManager.User.Identity.Name);

            string error = UsersModel.CreateUser(invite, email, password, nickName);

            return Json(invite, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateInvite(string email)
        {
            var info = String.Format("AuthenticationType='{0}' IsAuthenticated='{1}' Name='{2}'",
                AuthManager.User.Identity.AuthenticationType,
                AuthManager.User.Identity.IsAuthenticated,
                AuthManager.User.Identity.Name);

            string invite = UsersModel.CreateInvite(email);

            return Json(invite, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetInfo()
        {
            LoginInfo info = new LoginInfo();
            info.AuthenticationType = AuthManager.User.Identity.AuthenticationType;
            info.IsAuthenticated = AuthManager.User.Identity.IsAuthenticated;
             
            if(!String.IsNullOrEmpty(AuthManager.User.Identity.Name))
            {
                int id = int.Parse(AuthManager.User.Identity.Name);
                UsersModel user = UsersModel.GetSingle(id);
                info.User = user;
            }
            return Json(info, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public async System.Threading.Tasks.Task<ActionResult> Login(string email, string password)
        {
            UsersModel user = UsersModel.Login(email, password);

            if(user != null)
            {

                var usr = new AU(user.Id.ToString());

                ClaimsIdentity claim = await CstmUserManager.CreateIdentityAsync(usr, DefaultAuthenticationTypes.ApplicationCookie);

                AuthManager.SignOut();
                AuthManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = true
                }, claim);

                return Json(true, JsonRequestBehavior.AllowGet);
            }


            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult List()
        {
            List<UsersModel> list = UsersModel.GetList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InvitesList()
        {
            List<UsersModel> list = UsersModel.GetInvitesList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //// GET: Users
        //public ActionResult Index()
        //{
        //    return View();
        //}


    }
}
