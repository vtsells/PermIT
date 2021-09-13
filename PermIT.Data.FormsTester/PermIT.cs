using ITMS.ActiveDirectory;
using MetroFramework.Controls;
using MetroFramework.Forms;
using PermIT.Data.Services;
using PermIT.Data.Services.Actions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PermIT.Data.FormsTester
{
    public partial class PermIT : MetroForm
    {
        public PermIT()
        {
            InitializeComponent();
            updateLists();


        }
        private void updateLists()
        {
            using (var applications = new ApplicationService())
            using (var users = new UserService())
            using (var jobs = new JobService())
            using (var checklists = new ChecklistService())
            using (var departments = new DepartmentService())
            {
                userList.Items.Clear();

                foreach (var user in users.AsSelectList(true))
                {
                    userList.Items.Add(new ListViewItem() { Tag = user.value, Text = user.text });
                }
                applicationList.Items.Clear();

                foreach (var item in applications.AsSelectList())
                {
                    applicationList.Items.Add(new ListViewItem() { Text = item.text, Tag = item.value });
                }

                jobList.Items.Clear();
                jobUserSelect.Items.Clear();
                foreach (var item in jobs.AsSelectList())
                {
                    jobList.Items.Add(new ListViewItem() { Text = item.text, Tag = item.value });
                    jobUserSelect.Items.Add(new ListViewItem() { Text = item.text, Tag = item.value });
                }
                jobUserSelect.Columns[0].Width = -1;
               checklistList.Items.Clear();

                foreach (var item in checklists.AsSelectList())
                {
                    checklistList.Items.Add(new ListViewItem() { Text = item.text, Tag = item.value });
                }
                departmentList.Items.Clear();

                foreach (var item in departments.AsSelectList())
                {
                    departmentList.Items.Add(new ListViewItem() { Text = item.text, Tag = item.value });
                }

            }
        }
        private async void addUserBtn_Click(object sender, EventArgs e)
        {
            using(var users = new UserActions())
            {
                await users.AddUserWJob(new User()
                {
                    FirstName = userFirstName.Text,
                    LastName = userLastName.Text
                }, int.Parse(jobUserSelect.SelectedItems[0].Tag.ToString()));

            }
            updateLists ();
        }


        private async void addApplicationBtn_Click(object sender, EventArgs e)
        {
            using (var service = new ApplicationService())
            {

                await service.Add(new Services.Application()
                {
                    Name=applicationName.Text
                });

            }
            updateLists();
        }


        private async void userDeleteBtn_Click(object sender, EventArgs e)
        {
            using (var service = new UserService())
            {

                var selected =int.Parse(userList.SelectedItems[0].Tag.ToString());
               await service.Delete(selected);
            }
            updateLists();
        }

        private async void deleteApplicationBtn_Click(object sender, EventArgs e)
        {
            using (var service = new ApplicationService())
            {

                var selected = int.Parse(applicationList.SelectedItems[0].Tag.ToString());
                await service.Delete(selected);
            }
            updateLists();
        }

        private async void addJobBtn_Click(object sender, EventArgs e)
        {
            using (var jobs = new JobService())
            {
                await jobs.Add(new Job()
                {
                    Name=jobName.Text
                });

            }
            updateLists();
        }

        private async void deleteJobBtn_Click(object sender, EventArgs e)
        {
            using (var service = new JobService())
            {

                var selected = int.Parse(jobList.SelectedItems[0].Tag.ToString());
                await service.Delete(selected);
            }
            updateLists();
        }

        private async void addChecklistBtn_Click(object sender, EventArgs e)
        {
            using (var checklists = new ChecklistService())
            {
                await checklists.Add(new Checklist()
                {
                    Name = checklistName.Text
                });

            }
            updateLists();
        }

        private async void deleteChecklistBtn_Click(object sender, EventArgs e)
        {
            using (var service = new ChecklistService())
            {

                var selected = int.Parse(checklistList.SelectedItems[0].Tag.ToString());
                await service.Delete(selected);
            }
            updateLists();
        }

        private async void addDepartmentBtn_Click(object sender, EventArgs e)
        {
            using (var departments = new DepartmentService())
            {
                await departments.Add(new Department()
                {
                    Name = departmentName.Text
                });

            }
            updateLists();
        }

        private async void deleteDepartmentBtn_Click(object sender, EventArgs e)
        {
            using (var service = new DepartmentService())
            {

                var selected = int.Parse(departmentList.SelectedItems[0].Tag.ToString());
                await service.Delete(selected);
            }
            updateLists();
        }
    }
}
