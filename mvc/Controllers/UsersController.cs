using mvc.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace mvc.Controllers
{
    public class UsersController : Controller
    {
        public ActionResult List()
        {
            List<UsersModel> list = UsersModel.GetList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //// GET: Users
        //public ActionResult Index()
        //{
        //    return View();
        //}


    }
}
