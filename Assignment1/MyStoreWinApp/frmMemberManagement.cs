using BussinessObject;
using DataAccess;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MyStoreWinApp
{
    delegate List<MemberObject> SearchMember(List<MemberObject> list);

    delegate List<MemberObject> FilterMember(List<MemberObject> list);
    public partial class frmMemberManagement : Form
    {
        private bool isDescending = false;
        private SearchMember searchMember;
        private FilterMember filterMember;
        public IMemberRepository MemberRepository { get; set; }
        public frmMemberManagement()
        {
            InitializeComponent();
        }

        private List<String> getAllCity()
        {
            return MemberRepository.GetListAllMember().Select(mem => mem.City).Distinct().ToList();
        }

        private List<String> getAllCountry()
        {
            return MemberRepository.GetListAllMember().Select(mem => mem.Country).Distinct().ToList();
        }

        private void LoadData()
        {
            lvData.Items.Clear();
            ClearDetails();
            List<MemberObject> list = MemberRepository.GetListAllMember();
            list = searchMember(list);
            list = filterMember(list);
            list.Sort((a, b) => a.MemberName.CompareTo(b.MemberName) * (isDescending ? -1 : 1));
            foreach (MemberObject member in list)
            {
                ListViewItem item = new()
                {
                    Text = member.MemberID.ToString(),
                    SubItems = {
                        member.MemberName,
                        member.Email,
                        member.City,
                        member.Country
                    }
                };
                item.Tag = member;
                lvData.Items.Add(item);
            }
        }
        private void ClearDetails()
        {
            txtID.Text = string.Empty;
            txtName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtCountry.Text = string.Empty;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }




        private void txtSearch_TextChanged(object sender, EventArgs e) => LoadData();

        private void btnSort_Click(object sender, EventArgs e)
        {
            isDescending = !isDescending;
            btnSort.Text = isDescending ? "ASC" : "DESC";
            LoadData();
        }

        private void cbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbSearch.SelectedIndex)
            {
                case 0:
                    {
                        searchMember = list =>
                        {
                            int searchID;
                            if (int.TryParse(txtSearch.Text, out searchID))
                            {
                                return MemberRepository.searchMemberByID(searchID, list);
                            }

                            return list;
                        };
                        break;
                    }
                case 1:
                    {
                        searchMember = list => MemberRepository.searchMemberByName(txtSearch.Text, list);
                        break;
                    }
            }

            txtSearch.Text = "";
        }

        private void lvData_SelectedIndexChanged(object sender, EventArgs e)
        {
            var items = lvData.SelectedItems;
            if (items.Count > 0)
            {
                MemberObject member = items[0].Tag as MemberObject;
                txtID.Text = member.MemberID.ToString();
                txtName.Text = member.MemberName;
                txtEmail.Text = member.Email;
                txtCity.Text = member.City;
                txtCountry.Text = member.Country;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
            {
                ClearDetails();
            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(cbFilter.SelectedIndex.ToString());
            cbDetails.Items.Clear();
            List<string> valueList = null;

            switch (cbFilter.SelectedIndex)
            {
                case 0:
                    {
                        valueList = getAllCity();
                        filterMember = list =>
                        {
                            if (cbDetails.SelectedIndex == 0)
                            {
                                return list;
                            }
                            else
                            {
                                return MemberRepository.filterByCity(cbDetails.Text, list);
                            }
                        };
                        break;
                    }
                case 1:
                    {
                        valueList = getAllCountry();
                        filterMember = list =>
                        {
                            if (cbDetails.SelectedIndex == 0)
                            {
                                return list;
                            }
                            else
                            {
                                return MemberRepository.filterByCountry(cbDetails.Text, list);
                            }
                        };
                        break;
                    }
            }
            cbDetails.Items.Add("All");
            if (valueList != null)
            {
                cbDetails.Items.AddRange(valueList.ToArray());
            }

            cbDetails.SelectedIndex = 0;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var items = lvData.SelectedItems;
            if (items.Count > 0)
            {
                MemberObject member = items[0].Tag as MemberObject;
                frmMemberDetails formMemberDetails = new()
                {
                    MemberRepository = this.MemberRepository,
                    CurrentMember = member,
                    IsUpdate = true
                };
                formMemberDetails.ShowDialog();
                items[0].SubItems.Clear();
                items[0].Text = member.MemberID.ToString();
                items[0].SubItems.AddRange(
                    new[] {
                            member.MemberName,
                            member.Email,
                            member.City,
                            member.Country
                    }
                );
            }
        }

        private void frmMemberManagement_Load(object sender, EventArgs e)
        {
            cbSearch.SelectedIndex = 0;
            cbFilter.SelectedIndex = 0;

        }

        private void cbDetails_SelectedIndexChanged(object sender, EventArgs e) => LoadData();

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var items = lvData.SelectedItems;
            if (items.Count > 0)
            {
                MemberObject member = items[0].Tag as MemberObject;
                if (MessageBox.Show($"DELETE {member.MemberName} ?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    MemberRepository.DeleteMember(member.MemberID);
                    lvData.Items.Remove(items[0]);
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmMemberDetails frmMemberDetails = new()
            {
                MemberRepository = this.MemberRepository,
                IsUpdate = false
            };
            frmMemberDetails.ShowDialog();
            LoadData();
            cbSearch_SelectedIndexChanged(null, null);
            cbFilter_SelectedIndexChanged(null, null);
        }
    }
}
