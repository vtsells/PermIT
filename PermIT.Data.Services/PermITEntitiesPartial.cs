using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermIT.Data.Services
{
    public partial class PermITEntities
    {
        public void OnContextCreated()
        {
            this.Configuration.ProxyCreationEnabled = false;
        }
    }
}
