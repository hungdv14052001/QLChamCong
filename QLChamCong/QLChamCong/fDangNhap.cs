﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QLChamCong
{
    public partial class fDangNhap : Form
    {
        string str = @"Data Source=DESKTOP-LICKT66;Initial Catalog=QLChamCong;Integrated Security=True";
        SqlConnection con;
        SqlCommand com = new SqlCommand();
        SqlDataAdapter ada = new SqlDataAdapter();
        DataTable dtA = new DataTable();
        DataTable dtM = new DataTable();

        Thread th;

        public void openHomePage(object obj)
        {
            Application.Run(new fHomePageA());
        }
        public fDangNhap()
        {
            InitializeComponent();
            con = new SqlConnection(str);
            con.Open();
        }

        private void fDangNhap_Load(object sender, EventArgs e)
        {
            com = con.CreateCommand();
            com.CommandText = "select * from tblAdmin";
            ada.SelectCommand = com;
            ada.Fill(dtA);
            com = con.CreateCommand();
            com.CommandText = "select * from tblNhanVien";
            ada.SelectCommand = com;
            ada.Fill(dtM);
        }
        public void AdminDangNhap(string username, string password)
        {
            bool DangNhap = false;
            foreach(DataRow r in dtA.Rows)
            {
                if (username.Equals(r["UsernameA"]) && password.Equals(r["PasswordA"].ToString()))
                {
                    DangNhap = true;
                }
            }
            if (DangNhap)
            {
                this.Close();
                th = new Thread(openHomePage);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
            else
            {
                lbThongBao.Text = "Tài khoản hoặc mật khẩu không chính xác!";
            }
        }
        public void MemberDangNhap(string username, string password)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Texts;
            string password = txtPassword.Texts;
            if (username.Length == 0 || password.Length == 0)
            {
                lbThongBao.Text = "Vui lòng nhập đầy đủ thông tin đăng nhập";
                return;
            }
            bool isDangNhap = cbAdmin.Checked;
            if (isDangNhap)
            {
                AdminDangNhap(username, password);
            }
            else
            {
                MemberDangNhap(username, password);
            }
        }

        private void cbAdmin_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAdmin.Checked)
            {
                lbTieuDe.Text = "Admin Login";
            }
            else
            {
                lbTieuDe.Text = "Member Login";
            }
        }
    }
}
