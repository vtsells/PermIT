using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermIT.Data.Services
{
    public class JobService : Service
    {
        public IEnumerable<Job_AsSelectList_Result> AsSelectList() =>
           db.Job_AsSelectList();
        public async Task<Job> Add(Job model)
        {
            db.Jobs.Add(model);
            await db.SaveChangesAsync();
            return model;
        }
        public async Task<Job>Edit(Job model)
        {
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return model;
        }
        public async Task<Job> Delete(int id)
        {
            var found = await db.Jobs.FindAsync(id);
            db.Jobs.Remove(found);
            await db.SaveChangesAsync();
            return found;
        }
        public async Task<Job> Get(int id) =>
            await db.Jobs.FindAsync(id);
    }
}
