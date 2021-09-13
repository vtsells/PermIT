using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermIT.Data.Services
{
    public class Service : IDisposable
    {
        public PermITEntities db { get; set; }
        public Service()
        {
            this.db = new PermITEntities();
            this.db.Configuration.ProxyCreationEnabled = false;
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
                db.Dispose();
            }
            //base.Dispose(disposing);
        }
    }
}
