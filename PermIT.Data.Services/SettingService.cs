using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermIT.Data.Services
{
    public class SettingService : Service
    {
        public async Task<string> Get(string setting) =>
           (await db.Settings.Where(m => m.Name == setting).FirstOrDefaultAsync()).Value;

        public async Task<Setting> Update(string name, string value)
        {
            var found =await db.Settings.Where(m => m.Name == name).FirstOrDefaultAsync();
            found.Value = value;
            db.Entry(found).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return found;
        }

    }
}
