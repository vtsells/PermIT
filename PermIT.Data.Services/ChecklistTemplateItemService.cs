using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermIT.Data.Services
{
    public class ChecklistTemplateItemService : Service
    {
        public IEnumerable<ChecklistTemplateItem_AsSelectList_Result> AsSelectList(int checklistTemplateId) =>
           db.ChecklistTemplateItem_AsSelectList(checklistTemplateId);
        public async Task<ChecklistTemplateItem> Add(ChecklistTemplateItem model)
        {
            db.ChecklistTemplateItems.Add(model);
            await db.SaveChangesAsync();
            return model;
        }
        public async Task<ChecklistTemplateItem>Edit(ChecklistTemplateItem model)
        {
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return model;
        }
        public async Task<ChecklistTemplateItem> Delete(int id)
        {
            var found = await db.ChecklistTemplateItems.FindAsync(id);
            db.ChecklistTemplateItems.Remove(found);
            await db.SaveChangesAsync();
            return found;
        }
        public async Task<ChecklistTemplateItem> Get(int id) =>
             await db.ChecklistTemplateItems.FindAsync(id);
        public async Task<IEnumerable<ChecklistTemplateItem>> GetMany(int checklistTemplateId) =>
             await db.ChecklistTemplateItems.Where(m => m.ChecklistTemplateId == checklistTemplateId).ToListAsync();

    }
}
