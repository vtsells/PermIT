using ITMS.ActiveDirectory;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PermIT.Data.Services.User;

namespace PermIT.Data.Services.Actions
{
    public class UserActions : Action
    {
        public async Task AddUserWJob(User model, int jobId)
        {
            var user = await users.AddUserWJob(model,jobId);
            await checklists.AddJobChecklistToUser(user.Id, jobId);
            //  await AssignJob(user.Id, jobId);
        }
        
        public async Task<IEnumerable<User>> GetUnsycedUsers()
        {
            var db = new PermITEntities();
            var domains = await db.Settings.Where(m => m.Name == "Domains").FirstOrDefaultAsync();
            var adQuery = await db.Settings.Where(m => m.Name == "AD Query").FirstOrDefaultAsync();
            var domainsConverted = JArray.Parse(domains.Value).Values<string>();
            var users = new List<User>();
            foreach(var domain in domainsConverted)
            {
                var context = new Context(domain);
                var found = context.GetWQuery(adQuery.Value);
                var dbUsers = await db.Users.ToListAsync();
                var foundAsList = new List<User>();
                foreach(SearchResult user in found)
                {
                    var newUser = new User()
                    {
                        SID = user.SID(),
                        Enabled = !user.GetDirectoryEntry().IsAccountDisabled()
                    };
                    foundAsList.Add(newUser);
                    var isNewUser = !db.Users.Any(m => m.SID == newUser.SID);
                    var statusChanged = false;
                    if (isNewUser)
                    {
                        newUser.SyncStatus = UserStatus.Add;
                        newUser.SyncStatusToString = newUser.SyncStatus.GetStringValue();
                    }
                    if (!isNewUser)
                    {
                        var match = dbUsers.Where(m => m.SID == newUser.SID).FirstOrDefault();
                        if (match.Enabled != newUser.Enabled)
                        {
                            newUser.Id = match.Id;
                            newUser.SyncStatus = (newUser.Enabled==true)?UserStatus.Enable:UserStatus.Disable;
                            newUser.SyncStatusToString = newUser.SyncStatus.GetStringValue();
                            statusChanged = true;
                        }
                    }

                    if (isNewUser || statusChanged)
                    {
                        newUser.FirstName =Context.GetProperty(Context.ADProperties.givenName,user) ;
                        newUser.LastName = Context.GetProperty(Context.ADProperties.sn, user);
                        users.Add(newUser);
                    }
                }
                
                foreach(var user in dbUsers)
                {
                    if (user.Enabled == true)
                    {
                        var isFound = foundAsList.Any(m=>m.SID==user.SID);
                        if (!isFound) {
                            user.SyncStatus = UserStatus.Delete;
                            user.SyncStatusToString = user.SyncStatus.GetStringValue();
                            users.Add(user);
                        }
                    }
                  
                }
            }
            return users.OrderBy(m=>m.FirstName);
        }
        public async Task SyncUsers()
        {
            var unSyncedUsers = await this.GetUnsycedUsers();
            await users.AddMany(unSyncedUsers);
        }

        public async Task AssignJob(int userId, int jobId)
        {
            //var rel = this.users.AddJobUser(new JobUser()
            //{
            //    JobId = jobId,
            //    UserId = userId
            //});
            await users.AddJobToUser(userId, jobId);
            await checklists.AddJobChecklistToUser(userId, jobId);

        }
    }
}
