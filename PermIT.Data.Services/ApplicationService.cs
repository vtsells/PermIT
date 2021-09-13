using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermIT.Data.Services
{
    public class ApplicationService : Service
    {
        public IEnumerable<Application_AsSelectList_Result> AsSelectList() =>
           db.Application_AsSelectList();
        public async Task<Application> Add(Application model)
        {
            db.Applications.Add(model);
            await db.SaveChangesAsync();
            return model;
        }
        public async Task<Application>Edit(Application model)
        {
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return model;
        }
        public async Task<Application> Delete(int id)
        {
            var found = await db.Applications.FindAsync(id);
            db.Applications.Remove(found);
            await db.SaveChangesAsync();
            return found;
        }
        public async Task<Application> Get(int id) =>
            await db.Applications.FindAsync(id);
    }
}
