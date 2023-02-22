using BussinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyStoreWinApp
{
    public partial class frmMemberDetails : Form
    {
        public IMemberRepository MemberRepository { get; set; }
        public MemberObject CurrentMember { get; init; }
        public bool IsUpdate { get; set; }
        public bool IsAdmin { get; set; }

        public frmMemberDetails()
        {
            InitializeComponent();
        }

        private void frmMemberDetails_Load(object sender, EventArgs e)
        {
            if (IsUpdate)
            {
                txtID.Text = CurrentMember.MemberID.ToString();
                txtName.Text = CurrentMember.MemberName;
                txtEmail.Text = CurrentMember.Email;
                txtPassword.Text = CurrentMember.Password;
                txtCity.Text = CurrentMember.City;
                txtCountry.Text = CurrentMember.Country;
            }
            else
            {
                txtEmail.ReadOnly = false;
            }

        }

        private void btnCancel_Click(object sender, EventArgs e) => Close();

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsAdmin)
            {
                txtEmail.Enabled = false;
                txtPassword.Enabled = false;
            }
            if (IsUpdate)
            {
                MemberRepository.UpdateMember(new MemberObject()
                {
                    MemberID = CurrentMember.MemberID,
                    MemberName = txtName.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text
                });
                MessageBox.Show("Update successfully", "Update status", MessageBoxButtons.OK);
            }
            else
            {
                int maxID = 0;
                foreach (MemberObject member in MemberRepository.GetListAllMember())
                {
                    if (member.MemberID > maxID)
                    {
                        maxID = member.MemberID;
                    }
                }
                MemberRepository.InsertMember(new MemberObject()
                {
                    MemberID = maxID + 1,
                    MemberName = txtName.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text
                });
                MessageBox.Show("Insert successfully", "Insert status", MessageBoxButtons.OK);
            }

        }
    }
}
