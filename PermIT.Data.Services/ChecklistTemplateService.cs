using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermIT.Data.Services
{
    public class ChecklistTemplateService : Service
    {
        public IEnumerable<ChecklistTemplate_AsSelectList_Result> AsSelectList(int jobId) =>
           db.ChecklistTemplate_AsSelectList(jobId,false);
        public async Task<ChecklistTemplate> Add(ChecklistTemplate model)
        {
            db.ChecklistTemplates.Add(model);
            await db.SaveChangesAsync();
            return model;
        }
        public async Task<ChecklistTemplate>Edit(ChecklistTemplate model)
        {
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return model;
        }
        public async Task<ChecklistTemplate> Delete(int id)
        {
            var found = await db.ChecklistTemplates.FindAsync(id);
            db.ChecklistTemplates.Remove(found);
            await db.SaveChangesAsync();
            return found;
        }
        public async Task<ChecklistTemplate> Get(int id) =>
             await db.ChecklistTemplates.FindAsync(id);
        public async Task<IEnumerable<ChecklistTemplate>> GetByJob(int jobId) =>
            await db.ChecklistTemplates.Where(m => m.JobId == jobId).ToListAsync();

    }
}
