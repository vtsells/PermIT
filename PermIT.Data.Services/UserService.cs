using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermIT.Data.Services
{
    public class UserService : Service
    {
        public IEnumerable<Users_AsSelectList_Result> AsSelectList(bool? isEnabled)
        {
               return db.Users_AsSelectList(isEnabled);

        }
          
        public async Task<User> Add(User model)
        {
            db.Users.Add(model);
            await db.SaveChangesAsync();
            return model;
        }
        public async Task<IEnumerable<User>> AddMany(IEnumerable<User> models)
        {
            foreach (var user in models.Where(m=>m.SyncStatus==UserStatus.Add))
            {

                    db.Users.Add(user);

                   

            }
            foreach(var user in models.Where(m=>m.SyncStatus==UserStatus.Enable || m.SyncStatus == UserStatus.Disable))
            {
                var found = await db.Users.FindAsync(user.Id);
                found.Enabled = user.Enabled;
                db.Entry(found).State = EntityState.Modified;
            }
            foreach (var user in models.Where(m => m.SyncStatus == UserStatus.Delete))
            {

                var found = await db.Users.FindAsync(user.Id);
                db.Users.Remove(found);
            }
            await db.SaveChangesAsync();
            return models;
        }
        public async Task<User> AddUserWJob(User model, int jobId)
        {
            model.Jobs.Add(await db.Jobs.FindAsync(jobId));
            await Add(model);
            return model;
        }
        public async Task<User>Edit(User model)
        {
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return model;
        }
        public async Task<User> Delete(int id)
        {
            var found = await db.Users.FindAsync(id);
            db.Users.Remove(found);
            await db.SaveChangesAsync();
            return found;
        }
        public async Task<User> Get(int id)=>
            await db.Users.FindAsync(id);
        public async Task<IEnumerable<User>> GetAll() =>
            await db.Users.OrderBy(m => m.FirstName).ToListAsync();
        public async Task<IEnumerable<User_IncludeJobs>> GetAllIncludeJobs() =>
    await db.User_IncludeJobs.OrderBy(m => m.FirstName).ToListAsync();
        public async Task SetStatus(int userId, bool status)
        {
            var found = await db.Users.FindAsync(userId);
            found.Enabled = status;
            db.Entry(found).State = EntityState.Modified;
            await db.SaveChangesAsync();

        }

        public async Task AddJobToUser(int userId, int jobId)
        {
            var found = await db.Users.FindAsync(userId);
            var foundJob = await db.Jobs.FindAsync(jobId);
            found.Jobs.Add(foundJob);
            db.Entry(found).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
        //public async Task<GroupUser> AddGroupUser(GroupUser model)
        //{
        //    db.GroupUsers.Add(model);
        //    await db.SaveChangesAsync();
        //    return model;
        //}
        //public async Task<GroupUser> EditGroupUser(GroupUser model)
        //{
        //    db.Entry(model).State = EntityState.Modified;
        //    await db.SaveChangesAsync();
        //    return model;
        //}
        //public async Task<GroupUser> DeleteGroupUser(int id)
        //{
        //    var found = await db.GroupUsers.FindAsync(id);
        //    db.GroupUsers.Remove(found);
        //    await db.SaveChangesAsync();
        //    return found;
        //}
        //public async Task<GroupUser> GetGroupUser(int groupId,int userId) =>
        //    await db.GroupUsers.FindAsync(groupId,userId);
        //public async Task<ApplicationUser> AddApplicationUser(ApplicationUser model)
        //{
        //    db.ApplicationUsers.Add(model);
        //    await db.SaveChangesAsync();
        //    return model;
        //}
        //public async Task<ApplicationUser> EditApplicationUser(ApplicationUser model)
        //{
        //    db.Entry(model).State = EntityState.Modified;
        //    await db.SaveChangesAsync();
        //    return model;
        //}
        //public async Task<ApplicationUser> DeleteApplicationUser(int id)
        //{
        //    var found = await db.ApplicationUsers.FindAsync(id);
        //    db.ApplicationUsers.Remove(found);
        //    await db.SaveChangesAsync();
        //    return found;
        //}
        //public async Task<ApplicationUser> GetApplicationUser(int applicationId, int userId) =>
        //    await db.ApplicationUsers.FindAsync(applicationId, userId);
        //public async Task<ChecklistUser> AddChecklistUser(ChecklistUser model)
        //{
        //    db.ChecklistUsers.Add(model);
        //    await db.SaveChangesAsync();
        //    return model;
        //}
        //public async Task<ChecklistUser> EditChecklistUser(ChecklistUser model)
        //{
        //    db.Entry(model).State = EntityState.Modified;
        //    await db.SaveChangesAsync();
        //    return model;
        //}
        //public async Task<ChecklistUser> DeleteChecklistUser(int id)
        //{
        //    var found = await db.ChecklistUsers.FindAsync(id);
        //    db.ChecklistUsers.Remove(found);
        //    await db.SaveChangesAsync();
        //    return found;
        //}
        //public async Task<ChecklistUser> GetChecklistUser(int checklistId, int userId) =>
        //     await db.ChecklistUsers.FindAsync(checklistId, userId);
        //public async Task<JobUser> AddJobUser(JobUser model)
        //{
        //    db.JobUsers.Add(model);
        //    await db.SaveChangesAsync();
        //    return model;
        //}
        //public async Task<JobUser> EditJobUser(JobUser model)
        //{
        //    db.Entry(model).State = EntityState.Modified;
        //    await db.SaveChangesAsync();
        //    return model;
        //}
        //public async Task<JobUser> DeleteJobUser(int id)
        //{
        //    var found = await db.JobUsers.FindAsync(id);
        //    db.JobUsers.Remove(found);
        //    await db.SaveChangesAsync();
        //    return found;
        //}
        //public async Task<JobUser> GetJobUser(int jobId, int userId) =>
        //    await db.JobUsers.FindAsync(jobId, userId);
        //public async Task<IEnumerable<Job>> GetJobs(int userId)
        //{
        //    var rels = db.JobUsers.Where(m => m.UserId == userId);
        //    var join = await db.Jobs.Join(rels, job => job.Id, rel => rel.JobId, (job, rel) => job).ToListAsync();
        //    return join;
        //}

        //public async Task<DepartmentUser> AddDepartmentUser(DepartmentUser model)
        //{
        //    db.DepartmentUsers.Add(model);
        //    await db.SaveChangesAsync();
        //    return model;
        //}
        //public async Task<DepartmentUser> EditDepartmentUser(DepartmentUser model)
        //{
        //    db.Entry(model).State = EntityState.Modified;
        //    await db.SaveChangesAsync();
        //    return model;
        //}
        //public async Task<DepartmentUser> DeleteDepartmentUser(int id)
        //{
        //    var found = await db.DepartmentUsers.FindAsync(id);
        //    db.DepartmentUsers.Remove(found);
        //    await db.SaveChangesAsync();
        //    return found;
        //}
        //public async Task<DepartmentUser> GetDepartmentUser(int departmentId, int userId) =>
        //    await db.DepartmentUsers.FindAsync(departmentId, userId);
    }
}
