
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITMS.ActiveDirectory
{
    public class Context : IDisposable
    {
        private bool disposedValue;

        // private PrincipalContext ADUserContext { get; }
        private DirectoryContext ADContext { get; }

        public Context(string domainName)
        {
            //ADUserContext = new PrincipalContext(ContextType.Domain, domainName);
 
            ADContext = new DirectoryContext(DirectoryContextType.Forest,domainName);
        }
        #region Utilities
        private DirectoryEntry GetADObject(string filter)
        {
            DirectoryEntry de = new DirectoryEntry(GetCurrentDomainPath());
            var ds = new DirectorySearcher(de);
            ds.Filter = filter;
            return ds.FindOne()?.GetDirectoryEntry();
        }
        private SearchResultCollection GetADObjects(string filter)
        {
            DirectoryEntry de = new DirectoryEntry(GetCurrentDomainPath());

            var ds = new DirectorySearcher(de);
            ds.Filter = filter;
            var all = ds.FindAll();
            return all;
        }
        private string GetCurrentConfigurationDomainPath()
        {
          //  DirectoryEntry de = new DirectoryEntry("LDAP://RootDSE");
            ////foreach(PropertyValueCollection prop in de.Properties)
            ////{
            ////    MessageBox.Show(prop.PropertyName+"_"+prop.Value);
            ////}
            ////  DirectoryEntry de = new DirectoryEntry("LDAP://configuration."+ADContext.Name);
            ////  MessageBox.Show(de.Properties["configurationNamingContext"][0].ToString());
            //   return "LDAP://" + de.Properties["configurationNamingContext"][0].ToString();
            var name = "LDAP://CN=Configuration";
            foreach (var s in ADContext.Name.Split('.'))
            {
                name += ",DC=" + s;
            }
          //  MessageBox.Show("LDAP://" + de.Properties["configurationNamingContext"][0].ToString() + "\n" + name);
            return name;
        }
        private string GetCurrentDomainPath()
        {
            // DirectoryEntry de = new DirectoryEntry("LDAP://RootDSE");
            //return "LDAP://" + de.Properties["defaultNamingContext"][0].ToString();
            return "LDAP://" + ADContext.Name;
        }
        #endregion
        #region Queries
        public DirectoryEntry GetOneUser(string samName) =>
            GetADObject( "(samAccountName=" + samName + ")");
        public DirectoryEntry GetOneUser(string firstName, string lastName) =>
            GetADObject("(&(givenName=" + firstName + ")(sn=" + lastName + "))");
        public DirectoryEntry GetOneObjectBySID(string sid) =>
            GetADObject("(objectSID=" + sid + ")");
        public SearchResultCollection GetAllUsers() =>
            GetADObjects("(sAMAccountType=805306368)");
        public List<DirectoryEntry> GetAllMembersOf(string group)
        {
            DirectoryEntry de = new DirectoryEntry(GetCurrentDomainPath());
            var ds = new DirectorySearcher(de);
            ds.Filter = "(sAMAccountType=805306368)";
            ds.PropertiesToLoad.Add("memberOf");
            ds.PropertiesToLoad.Add("name");
            var all = ds.FindAll();
            var found = new List<DirectoryEntry>();
            foreach(SearchResult result in all)
            {
                foreach(string prop in result.Properties["memberOf"])
                {
                    if (prop.Contains("CN="+group))
                    {
                        found.Add(result.GetDirectoryEntry());
                        break;
                    }
                }
            }
            return found;
        }
        public SearchResultCollection GetAllComputers() =>
            GetADObjects("(objectCategory=computer)");
        public SearchResultCollection GetAllGroups() =>
            GetADObjects("(objectCategory=group)");
        public SearchResultCollection GetAllGroupsWMail() =>
            GetADObjects("(&(objectCategory=group)"+"(mail=*))");
        public SearchResultCollection GetWQuery(string query) =>
            GetADObjects(query);
        public ActiveDirectorySubnetCollection GetADSiteIPs(string siteName) =>
            ActiveDirectorySite.FindByName(ADContext,siteName).Subnets;
        public IEnumerable<string> GetOfficeLocations()
        {
            var users = GetAllUsers();
            var locations = new List<string>();
            foreach (SearchResult user in users)
            {
                var userLocation = user.GetDirectoryEntry().Properties[ADProperties.physicalDeliveryOfficeName.ToString()].Value?.ToString();
                if (!locations.Any(m => m == userLocation) && userLocation?.Length>0)
                {
                    locations.Add(userLocation);
                }
               
            }
            return locations.OrderBy(m => m);
        }
        public SearchResultCollection GetAllPrinters() =>
            GetADObjects("(objectCategory=printQueue)");
        public List<ActiveDirectorySite> GetADSites()
        {
           // MessageBox.Show(GetCurrentConfigurationDomainPath());
            DirectoryEntry de = new DirectoryEntry(GetCurrentConfigurationDomainPath());
            var ds = new DirectorySearcher(de);
            ds.Filter = "(objectClass=site)";
            var sites = new List<ActiveDirectorySite>();
            var results = ds.FindAll();
            foreach (SearchResult sr in results)
            {
               // Using the index zero(0) is required!
              //Console.WriteLine(sr.Properties["name"][0].ToString());
              //  var subnets = ActiveDirectorySite.FindByName(ADContext, sr.Properties["name"][0].ToString()).Subnets;
              //  if (subnets.Count > 0)
              //  {
              //      Console.WriteLine(subnets[0]);
              //  }
                sites.Add(ActiveDirectorySite.FindByName(ADContext, sr.Properties["name"][0].ToString()));
            }
            return sites;
        }
        #endregion
        #region Edit Properties
        public static string GetProperty(ADProperties property, SearchResult searchResult) =>
            GetProperty(property, searchResult.GetDirectoryEntry());

        public static string GetProperty(ADProperties property, DirectoryEntry entry) =>
            (entry.Properties[property.ToString()].Value == null) ?
            null : entry.Properties[property.ToString()].Value.ToString();
        public static string GetProperty(string property, SearchResult searchResult) =>
            GetProperty(property, searchResult.GetDirectoryEntry());

        public static string GetProperty(string property, DirectoryEntry entry) =>
            (entry.Properties[property.ToString()].Value == null) ?
            null : entry.Properties[property.ToString()].Value.ToString();

        public static void EditUserProperty(ADProperties property, string value, SearchResult searchResult) =>
            EditUserProperty(property, value, searchResult.GetDirectoryEntry());
        public static void EditUserProperty(ADProperties property, string value, DirectoryEntry entry)
        {
            entry.Properties[property.ToString()].Value = value;
            entry.CommitChanges();
        }
        #endregion
        //public SearchResultCollection GetAllComputers()
        //{
        //    DirectoryEntry de = new DirectoryEntry(GetCurrentDomainPath());
        //    var ds = new DirectorySearcher(de);
        //    ds.Asynchronous = true;
        //    ds.Filter = "(objectClass=computer)";
        //    //var computers= new List<DirectoryEntry>();

        //    //var results = ds.FindAll();
        //    ////foreach(var s in results[0].Properties.PropertyNames)
        //    ////{
        //    ////    Console.WriteLine(s);
        //    //// }

        //    //foreach (SearchResult sr in results)
        //    //{
        //    //   Console.WriteLine(sr.Properties[ComputerProperties.name.ToString()][0].ToString());

        //    //    /*var buffer = sr.Properties[ComputerProperties.objectsid.ToString()][0] as Byte[];
        //    //    var s = new SecurityIdentifier(buffer, 0);
        //    //    Console.WriteLine(s.ToString() + "__");*/
        //    //    computers.Add(sr.GetDirectoryEntry());

        //    //}
        //    return ds.FindAll();
        //}

        #region Enums
        public enum ADProperties
        {
            /// <summary>
            /// Account name
            /// </summary>
            samaccountname,
            cn,
            sn,
            whencreated,
            dnshostname,
            lastlogon,
            samaccounttype,
            objectguid,
            lastlogontimestamp,
            operatingsystemversion,
            name,
            objectsid,
            logoncount,
            whenchanged,
            objectcategory,
            useraccountcontrol,
            distinguishedname,
            objectclass,
            memberof,
            adspath,
            operatingsystem,
            operatingsystemservicepack,
            /// <summary>
            /// User's manager
            /// </summary>
            manager,
            /// <summary>
            /// User's title
            /// </summary>
            title,
            /// <summary>
            /// User's phone number
            /// </summary>
            telephoneNumber,
            /// <summary>
            /// User's fax number
            /// </summary>
            facsimileTelephoneNumber,
            /// <summary>
            /// User's mobile number
            /// </summary>
            mobile,
            /// <summary>
            /// User's branch location
            /// </summary>
            physicalDeliveryOfficeName,
            /// <summary>
            /// User's P.O. Box
            /// </summary>
            postOfficeBox,
            /// <summary>
            /// User's street address
            /// </summary>
            streetAddress,
            /// <summary>
            /// User's city
            /// </summary>
            l,
            /// <summary>
            /// User's state
            /// </summary>
            st,
            /// <summary>
            /// User's zip code
            /// </summary>
            postalCode,
            /// <summary>
            /// User's country
            /// </summary>
            co,
            /// <summary>
            /// User's SID
            /// </summary>
            objectSID,
            givenName,
            initials,
            description,
            mail,
            wWWHomePage,
            userPrincipalName,
            homeDirectory,
            homeDrive,
            scriptPath,
            profilePath,
            homePhone,
            pager,
            ipPhone,
            info,
            department,
            company,
            msExchRecipientTypeDetails
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~Context()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
