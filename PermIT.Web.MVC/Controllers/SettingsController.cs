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
    public class SettingsController : Controller
    {
        // GET: Users

        public async Task<JsonResult> Get(string name)
        {
            using(var service = new SettingService())
            {
                
                return Json(await service.Get(name),JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<JsonResult> Update(string name,string value)
        {
            using (var service = new SettingService())
            {

                return Json(await service.Update(name,value), JsonRequestBehavior.AllowGet);
            }
        }

    }
}