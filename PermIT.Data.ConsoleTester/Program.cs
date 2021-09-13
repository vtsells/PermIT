using PermIT.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PermIT.Data.Services.Actions;
namespace PermIT.Data.ConsoleTester
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var actions = new UserActions())
            {
                var found = await actions.GetUnsycedUsers();
                foreach (var user in found)
                {
                    Console.WriteLine(user.FirstName + " " + user.LastName + " " + user.SID);
                }

            }
        }
    }
}
