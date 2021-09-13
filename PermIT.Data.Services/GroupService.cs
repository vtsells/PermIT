using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermIT.Data.Services
{
    public class GroupService : Service
    {
        public IEnumerable<Group_AsSelectList_Result> AsSelectList(int applicationId) =>
           db.Group_AsSelectList(applicationId);
        public async Task<Group> Add(Group model)
        {
            db.Groups.Add(model);
            await db.SaveChangesAsync();
            return model;
        }
        public async Task<Group>Edit(Group model)
        {
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return model;
        }
        public async Task<Group> Delete(int id)
        {
            var found = await db.Groups.FindAsync(id);
            db.Groups.Remove(found);
            await db.SaveChangesAsync();
            return found;
        }
        public async Task<Group> Get(int id) =>
            await db.Groups.FindAsync(id);
    }
}
