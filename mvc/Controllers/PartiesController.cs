using mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc.Controllers
{
    public class PartiesController : Controller
    {
        public ActionResult List()
        {
            List<PartiesModel> list = PartiesModel.GetList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}