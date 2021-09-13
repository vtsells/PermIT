using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermIT.Data.Services.Actions
{
    class ChecklistActions : Action
    {
        public async Task AddChecklistsFromJob(int userId,int jobId)
        {
            var templates = await this.checklistTemplates.GetByJob(jobId);
            foreach (var template in templates.Where(m=>m.OnRemove==false))
            {
                //var template = await this.checklistTemplates.Get(templateId);
                var checklist = await this.checklists.Add(new Checklist()
                {
                    Name = template.Name
                });
               // var rel = await this.users.AddChecklistUser(new ChecklistUser() { ChecklistId=checklist.Id,UserId=userId});
                var itemTemplates = await this.checklistTemplateItems.GetMany(template.Id);
                foreach (var itemTemplate in itemTemplates)
                {
                    var checklistItem = await this.checklistItems.Add(new ChecklistItem()
                    {
                        Name = itemTemplate.Name,
                        ChecklistId = checklist.Id
                    });
                }
            }
        }
    }
}
