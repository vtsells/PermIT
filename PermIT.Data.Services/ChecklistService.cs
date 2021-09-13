using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermIT.Data.Services
{
    public class ChecklistService : Service
    {
        public IEnumerable<Checklist_AsSelectList_Result> AsSelectList() =>
           db.Checklist_AsSelectList();
        public async Task AddJobChecklistToUser(int userId,int jobId)
        {
            var foundUser = await db.Users.FindAsync(userId);
            var foundJob = await db.Jobs.FindAsync(jobId);
            var foundChecklistTemplate = await db.ChecklistTemplates.Where(m => m.JobId == foundJob.Id && m.OnRemove!=true).ToListAsync();
            foreach(var template in foundChecklistTemplate)
            {
                var checklist = new Checklist()
                {
                    Name = template.Name
                };
                foreach(var item in db.ChecklistTemplateItems.Where(m=>m.ChecklistTemplateId==template.Id))
                {
                    checklist.ChecklistItems.Add(new ChecklistItem()
                    {
                        Name = item.Name
                    });
                }
                foundUser.Checklists.Add(checklist);
            }
            await db.SaveChangesAsync();
        }
        public async Task<Checklist> Add(Checklist model)
        {
            db.Checklists.Add(model);
            await db.SaveChangesAsync();
            return model;
        }
        public async Task<Checklist>Edit(Checklist model)
        {
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return model;
        }
        public async Task<Checklist> Delete(int id)
        {
            var found = await db.Checklists.FindAsync(id);
            db.Checklists.Remove(found);
            await db.SaveChangesAsync();
            return found;
        }
        public async Task<Checklist> Get(int id) =>
            await db.Checklists.FindAsync(id);
    }
}
