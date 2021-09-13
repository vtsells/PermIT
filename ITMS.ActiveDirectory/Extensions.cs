using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static ITMS.ActiveDirectory.Context;

namespace ITMS.ActiveDirectory
{
    public static class Extensions
    {
        public static string ToReadString(this Byte[] byteArray)
        {
            return  Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
        }
        public static string SID(this DirectoryEntry entry)
        {
            var buffer = entry.Properties[ADProperties.objectSID.ToString()][0] as Byte[];
            return (new SecurityIdentifier(buffer, 0)).ToString();
        }
        public static string SID(this SearchResult searchResult)
        {
            return searchResult.GetDirectoryEntry().SID();
        }
        public async static Task<IPAddress[]> IP(string hostName)
        {
            var ips = await Dns.GetHostEntryAsync(hostName);
            
            return (ips==null)? null : ips.AddressList;
        }
        public async static Task<string> DNS(string ip)
        {
            var names = await Dns.GetHostEntryAsync(ip);
            return (names == null) ? null : names.HostName;
        }
        public static bool Contains(this UserFlags haystack, UserFlags needle)
        {
            return (haystack & needle) == needle;
        }
        public static bool IsAccountDisabled(this DirectoryEntry user)
        {
            const string uac = "userAccountControl";
            if (user.NativeGuid == null) return false;

            if (user.Properties[uac] != null && user.Properties[uac].Value != null)
            {
                var userFlags = (UserFlags)user.Properties[uac].Value;
                return userFlags.Contains(UserFlags.AccountDisabled);
            }

            return false;
        }
    }
    [Flags]
    public enum UserFlags
    {
        Script = 1,                                     // 0x1
        AccountDisabled = 2,                            // 0x2
        HomeDirectoryRequired = 8,                      // 0x8
        AccountLockedOut = 16,                          // 0x10
        PasswordNotRequired = 32,                       // 0x20
        PasswordCannotChange = 64,                      // 0x40
        EncryptedTextPasswordAllowed = 128,             // 0x80
        TempDuplicateAccount = 256,                     // 0x100
        NormalAccount = 512,                            // 0x200
        InterDomainTrustAccount = 2048,                 // 0x800
        WorkstationTrustAccount = 4096,                 // 0x1000
        ServerTrustAccount = 8192,                      // 0x2000
        PasswordDoesNotExpire = 65536,                  // 0x10000 (Also 66048 )
        MnsLogonAccount = 131072,                       // 0x20000
        SmartCardRequired = 262144,                     // 0x40000
        TrustedForDelegation = 524288,                  // 0x80000
        AccountNotDelegated = 1048576,                  // 0x100000
        UseDesKeyOnly = 2097152,                        // 0x200000
        DontRequirePreauth = 4194304,                   // 0x400000
        PasswordExpired = 8388608,                      // 0x800000 (Applicable only in Window 2000 and Window Server 2003)
        TrustedToAuthenticateForDelegation = 16777216,  // 0x1000000
        NoAuthDataRequired = 33554432                   // 0x2000000
    }
}
