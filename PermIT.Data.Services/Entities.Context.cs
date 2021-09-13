﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class PermITEntities : DbContext
    {
        public PermITEntities()
            : base("name=PermITEntities")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<C__RefactorLog> C__RefactorLog { get; set; }
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Checklist> Checklists { get; set; }
        public virtual DbSet<ChecklistItem> ChecklistItems { get; set; }
        public virtual DbSet<ChecklistTemplate> ChecklistTemplates { get; set; }
        public virtual DbSet<ChecklistTemplateItem> ChecklistTemplateItems { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Exception> Exceptions { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Application_List> Application_List { get; set; }
        public virtual DbSet<Checklist_List> Checklist_List { get; set; }
        public virtual DbSet<ChecklistItem_List> ChecklistItem_List { get; set; }
        public virtual DbSet<ChecklistTemplate_List> ChecklistTemplate_List { get; set; }
        public virtual DbSet<ChecklistTemplateItem_List> ChecklistTemplateItem_List { get; set; }
        public virtual DbSet<Department_List> Department_List { get; set; }
        public virtual DbSet<Group_List> Group_List { get; set; }
        public virtual DbSet<Job_List> Job_List { get; set; }
        public virtual DbSet<Permission_List> Permission_List { get; set; }
        public virtual DbSet<User_List> User_List { get; set; }
        public virtual DbSet<User_IncludeJobs> User_IncludeJobs { get; set; }
        public virtual DbSet<ChecklistPerUser> ChecklistPerUsers { get; set; }
    
        public virtual ObjectResult<Application_AsSelectList_Result> Application_AsSelectList()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Application_AsSelectList_Result>("PermITEntities.Application_AsSelectList");
        }
    
        public virtual ObjectResult<Checklist_AsSelectList_Result> Checklist_AsSelectList()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Checklist_AsSelectList_Result>("PermITEntities.Checklist_AsSelectList");
        }
    
        public virtual ObjectResult<ChecklistItem_AsSelectList_Result> ChecklistItem_AsSelectList(Nullable<int> checklistId)
        {
            var checklistIdParameter = checklistId.HasValue ?
                new ObjectParameter("checklistId", checklistId) :
                new ObjectParameter("checklistId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ChecklistItem_AsSelectList_Result>("PermITEntities.ChecklistItem_AsSelectList", checklistIdParameter);
        }
    
        public virtual ObjectResult<ChecklistTemplate_AsSelectList_Result> ChecklistTemplate_AsSelectList(Nullable<int> jobId, Nullable<bool> onRemove)
        {
            var jobIdParameter = jobId.HasValue ?
                new ObjectParameter("jobId", jobId) :
                new ObjectParameter("jobId", typeof(int));
    
            var onRemoveParameter = onRemove.HasValue ?
                new ObjectParameter("onRemove", onRemove) :
                new ObjectParameter("onRemove", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ChecklistTemplate_AsSelectList_Result>("PermITEntities.ChecklistTemplate_AsSelectList", jobIdParameter, onRemoveParameter);
        }
    
        public virtual ObjectResult<ChecklistTemplateItem_AsSelectList_Result> ChecklistTemplateItem_AsSelectList(Nullable<int> checklistTemplateId)
        {
            var checklistTemplateIdParameter = checklistTemplateId.HasValue ?
                new ObjectParameter("checklistTemplateId", checklistTemplateId) :
                new ObjectParameter("checklistTemplateId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ChecklistTemplateItem_AsSelectList_Result>("PermITEntities.ChecklistTemplateItem_AsSelectList", checklistTemplateIdParameter);
        }
    
        public virtual ObjectResult<Department_AsSelectList_Result> Department_AsSelectList()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Department_AsSelectList_Result>("PermITEntities.Department_AsSelectList");
        }
    
        public virtual ObjectResult<Group_AsSelectList_Result> Group_AsSelectList(Nullable<int> applicationId)
        {
            var applicationIdParameter = applicationId.HasValue ?
                new ObjectParameter("applicationId", applicationId) :
                new ObjectParameter("applicationId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Group_AsSelectList_Result>("PermITEntities.Group_AsSelectList", applicationIdParameter);
        }
    
        public virtual ObjectResult<Job_AsSelectList_Result> Job_AsSelectList()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Job_AsSelectList_Result>("PermITEntities.Job_AsSelectList");
        }
    
        public virtual ObjectResult<Permission_AsSelectList_Result> Permission_AsSelectList(Nullable<int> applicationId, Nullable<int> parentId)
        {
            var applicationIdParameter = applicationId.HasValue ?
                new ObjectParameter("applicationId", applicationId) :
                new ObjectParameter("applicationId", typeof(int));
    
            var parentIdParameter = parentId.HasValue ?
                new ObjectParameter("parentId", parentId) :
                new ObjectParameter("parentId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Permission_AsSelectList_Result>("PermITEntities.Permission_AsSelectList", applicationIdParameter, parentIdParameter);
        }
    
        public virtual ObjectResult<User_AsSelectList_Result> User_AsSelectList(Nullable<bool> isEnabled)
        {
            var isEnabledParameter = isEnabled.HasValue ?
                new ObjectParameter("isEnabled", isEnabled) :
                new ObjectParameter("isEnabled", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<User_AsSelectList_Result>("PermITEntities.User_AsSelectList", isEnabledParameter);
        }
    
        public virtual ObjectResult<Users_AsSelectList_Result> Users_AsSelectList(Nullable<bool> isEnabled)
        {
            var isEnabledParameter = isEnabled.HasValue ?
                new ObjectParameter("isEnabled", isEnabled) :
                new ObjectParameter("isEnabled", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Users_AsSelectList_Result>("PermITEntities.Users_AsSelectList", isEnabledParameter);
        }
    }
}
