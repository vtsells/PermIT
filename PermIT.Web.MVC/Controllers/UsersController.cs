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
    public class UsersController : Controller
    {
        // GET: Users
        public async Task<JsonResult> GetUnsyncedUsers()
        {
           using(var actions = new UserActions())
            {
                return Json(await actions.GetUnsycedUsers(),JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<JsonResult> GetAll()
        {
            using(var service = new UserService())
            {
                return Json(await service.GetAll(), JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<JsonResult> GetAllIncludeJobs()
        {
            using (var service = new UserService())
            {
                return Json(await service.GetAllIncludeJobs(), JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AsSelectList(bool? enabled)
        {
            var usersService = new UserService();

                return Json(usersService.AsSelectList(enabled), JsonRequestBehavior.AllowGet);
            
        }
        public async Task<JsonResult> SetStatus(int userId,bool status)
        {
            using(var users = new UserService())
            {
                await users.SetStatus(userId, status);
            }
            return Json("Success");
        }
        public async Task<JsonResult> SyncUsers()
        {
            using(var actions = new UserActions())
            {
                await actions.SyncUsers();
            }
            return Json("Success");
        }
        public async Task<JsonResult> AssignJob(int userId, int jobId)
        {
            using(var actions = new UserActions())
            {
                await actions.AssignJob(userId, jobId);
            }
            return Json("success");
        }
    }
}