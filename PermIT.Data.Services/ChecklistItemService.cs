using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermIT.Data.Services
{
    public class ChecklistItemService : Service
    {
        public IEnumerable<ChecklistItem_AsSelectList_Result> AsSelectList(int checklistId) =>
           db.ChecklistItem_AsSelectList(checklistId);
        public async Task<ChecklistItem> Add(ChecklistItem model)
        {
            db.ChecklistItems.Add(model);
            await db.SaveChangesAsync();
            return model;
        }
        public async Task<ChecklistItem>Edit(ChecklistItem model)
        {
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return model;
        }
        public async Task<ChecklistItem> Delete(int id)
        {
            var found = await db.ChecklistItems.FindAsync(id);
            db.ChecklistItems.Remove(found);
            await db.SaveChangesAsync();
            return found;
        }
        public async Task<ChecklistItem> Get(int id) =>
            await db.ChecklistItems.FindAsync(id);
    }
}
