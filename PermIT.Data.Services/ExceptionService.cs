using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermIT.Data.Services
{
    public class ExceptionService : Service
    {
        public async Task<Exception> Add(Exception model)
        {
            db.Exceptions.Add(model);
            await db.SaveChangesAsync();
            return model;
        }
        public async Task<Exception>Edit(Exception model)
        {
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return model;
        }
        public async Task<Exception> Delete(int id)
        {
            var found = await db.Exceptions.FindAsync(id);
            db.Exceptions.Remove(found);
            await db.SaveChangesAsync();
            return found;
        }
        public async Task<Exception> Get(int id) =>
            await db.Exceptions.FindAsync(id);
    }
}
