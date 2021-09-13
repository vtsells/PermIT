using PermIT.Data.Services;
using PermIT.Data.Services.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Trolled.Web.SPA2.Controllers
{
    public class JobController : Controller
    {
        // GET: Users

        public JsonResult AsSelectList()
        {
            var jobService = new JobService();

                return Json(jobService.AsSelectList(), JsonRequestBehavior.AllowGet);
            
        }

    }
}