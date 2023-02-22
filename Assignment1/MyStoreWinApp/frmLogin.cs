using BussinessObject;
using DataAccess;
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
    public partial class frmLogin : Form
    {

        public IMemberRepository MemberRepository { get; set; }

        public frmLogin()
        {
            MemberRepository = new MemberRepository();
            InitializeComponent();
        }


        private void Login_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            MemberObject member = MemberRepository.getMemberByEmailAndPassword(email, password);
            if (member != null)
            {
                this.Hide();
                if (member.Email.Equals("admin@fstore.com"))
                {
                    frmMemberManagement memberManagement = new frmMemberManagement()
                    {
                        MemberRepository = this.MemberRepository
                    };
                    memberManagement.ShowDialog();
                }
                else
                {
                    frmMemberDetails memberDetail = new frmMemberDetails()
                    {
                        MemberRepository = this.MemberRepository,
                        CurrentMember = member,
                        IsUpdate = true
                    };
                    memberDetail.ShowDialog();
                }
                this.Show();
            }
            else
            {
                MessageBox.Show("Incorect Username or Password!");
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            txtEmail.Text = "admin@fstore.com";
            txtPassword.Text = "admin@@";

        }

        private void Close_Click(object sender, EventArgs e) => Close();

        private void Login_Enter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login_Click(sender, e);
            }
        }

        
    }
}
