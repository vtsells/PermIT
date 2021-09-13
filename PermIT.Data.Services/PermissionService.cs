using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermIT.Data.Services
{
    public class PermissionService : Service
    {
        public IEnumerable<Permission_AsSelectList_Result> AsSelectList(int applicationId, int parentId) =>
           db.Permission_AsSelectList(applicationId,parentId);
        public async Task<Permission> Add(Permission model)
        {
            db.Permissions.Add(model);
            await db.SaveChangesAsync();
            return model;
        }
        public async Task<Permission>Edit(Permission model)
        {
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return model;
        }
        public async Task<Permission> Delete(int id)
        {
            var found = await db.Permissions.FindAsync(id);
            db.Permissions.Remove(found);
            await db.SaveChangesAsync();
            return found;
        }
        public async Task<Permission> Get(int id) =>
         await db.Permissions.FindAsync(id);
    }
}
