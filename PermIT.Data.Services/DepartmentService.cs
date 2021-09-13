using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermIT.Data.Services
{
    public class DepartmentService : Service
    {
        public IEnumerable<Department_AsSelectList_Result> AsSelectList() =>
           db.Department_AsSelectList();
        public async Task<Department> Add(Department model)
        {
            db.Departments.Add(model);
            await db.SaveChangesAsync();
            return model;
        }
        public async Task<Department>Edit(Department model)
        {
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return model;
        }
        public async Task<Department> Delete(int id)
        {
            var found = await db.Departments.FindAsync(id);
            db.Departments.Remove(found);
            await db.SaveChangesAsync();
            return found;
        }
        public async Task<Department> Get(int id) =>
            await db.Departments.FindAsync(id);
    }
}
