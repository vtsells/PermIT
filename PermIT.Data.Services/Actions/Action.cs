using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermIT.Data.Services.Actions
{
    public class Action : IDisposable
    {
        public UserService users { get; set; }
        public ApplicationService applications { get; set; }
        public ChecklistItemService checklistItems { get; set; }
        public ChecklistService checklists { get; set; }
        public ChecklistTemplateItemService checklistTemplateItems { get; set; }
        public ChecklistTemplateService checklistTemplates { get; set; }
        public DepartmentService departments { get; set; }
        public ExceptionService exceptions { get; set; }
        public GroupService groups { get; set; }
        public JobService jobs { get; set; }
        public PermissionService permissions { get; set; }
        public Action()
        {
            users = new UserService();
            applications = new ApplicationService();
            checklistItems = new ChecklistItemService();
            checklists = new ChecklistService();
            checklistTemplateItems = new ChecklistTemplateItemService();
            checklistTemplates = new ChecklistTemplateService();
            departments = new DepartmentService();
            exceptions = new ExceptionService();
            groups = new GroupService() ;
            jobs = new JobService();
            permissions = new PermissionService();

        }
        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                users.Dispose();
                applications.Dispose();
                checklistItems.Dispose();
                checklists.Dispose();
                checklistTemplateItems.Dispose();
                checklistTemplates.Dispose();
                departments.Dispose();
                exceptions.Dispose();
                groups.Dispose();
                jobs.Dispose();
                permissions.Dispose();
            }
            //base.Dispose(disposing);
        }
    }
}
