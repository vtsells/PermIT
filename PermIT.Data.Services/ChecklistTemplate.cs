//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PermIT.Data.Services
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChecklistTemplate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChecklistTemplate()
        {
            this.ChecklistTemplateItems = new HashSet<ChecklistTemplateItem>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> JobId { get; set; }
        public Nullable<bool> OnRemove { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChecklistTemplateItem> ChecklistTemplateItems { get; set; }
        public virtual Job Job { get; set; }
    }
}
