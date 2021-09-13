using PermIT.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static PermIT.Data.Services.User;

namespace PermIT.Data.Services
{
    partial class User
    {
        public UserStatus SyncStatus { get; set; }
        public string SyncStatusToString
        {
            get; set;
        }
    }
    public enum UserStatus
    {
        [EnumStringAttribute("Add")]
        Add = 0,
        [EnumStringAttribute("Change")]
        Change = 1,
        [EnumStringAttribute("Delete")]
        Delete = 2,
        [EnumStringAttribute("Enable")]
        Enable = 3,
        [EnumStringAttribute("Disable")]
        Disable = 4,
    }
    public class EnumStringAttribute : Attribute
    {
        public EnumStringAttribute(string stringValue)
        {
            this.stringValue = stringValue;
        }
        private string stringValue;
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }



    public static class EnumExtensions
    {
        public static string GetStringValue(this Enum value)
        {
            Type type = value.GetType();
            FieldInfo fieldInfo = type.GetField(value.ToString());
            // Get the stringvalue attributes  
            EnumStringAttribute[] attribs = fieldInfo.GetCustomAttributes(
                    typeof(EnumStringAttribute), false) as EnumStringAttribute[];
            // Return the first if there was a match.  
            return attribs.Length > 0 ? attribs[0].StringValue : null;
        }
    }
}