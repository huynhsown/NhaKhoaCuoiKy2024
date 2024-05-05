﻿using Dapper;
using NhaKhoaCuoiKy.Helpers;
using NhaKhoaCuoiKy.Models;
using NhaKhoaCuoiKy.Views.Medicines;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NhaKhoaCuoiKy.Views.Employee.Medicines
{
    public partial class Medicine : Form
    {
        MainForm mainForm;
        NewMedicine newMedicine;
        UserModel userAccount;
        public Medicine()
        {
            InitializeComponent();
        }
        public Medicine(MainForm mainForm, UserModel userAccount)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.userAccount = userAccount;
        }

        void loadForm(Form form)
        {
            FormBackGround formBackGround = new FormBackGround(mainForm);
            try
            {
                using (form)
                {
                    formBackGround.Owner = mainForm;
                    formBackGround.Show();
                    form.Owner = formBackGround;
                    form.ShowDialog();
                    formBackGround.Dispose();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Đã xảy ra lỗi! Vui lòng thử lại.", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_themmoi_Click(object sender, EventArgs e)
        {
            loadForm(new NewMedicine(this));
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            int index = cb_filter.SelectedIndex;
            string filter = tb_filter_search.Text;
            if (index == 0)
            {
                if (tb_filter_search.Text.Trim() == "")
                {
                    MessageBox.Show("Vui lòng nhập mã thuốc", "Tìm kiếm theo mã thuốc", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                loadMedicine(MedicineHelper.getMedicineByID(filter));
            }
            else if (index == 1)
            {
                if (tb_filter_search.Text.Trim() == "")
                {
                    MessageBox.Show("Vui lòng nhập tên thuốc ", "Tìm kiếm theo tên thuốc", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                loadMedicine(MedicineHelper.getMedicineByName(filter));
            }

        }

        public void loadMedicine(DataTable dt)
        {

            data_thuoc.Rows.Clear();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string maThuoc = row["MaThuoc"].ToString();
                    string tenThuoc = row["TenThuoc"].ToString();
                    string hDSD = row["HuongDanSD"].ToString();
                    string thanhPhan = row["ThanhPhan"].ToString();
                    int giaNhap = Convert.ToInt32(row["GiaNhap"]);
                    int giaBan = Convert.ToInt32(row["GiaBan"]);
                    int soLuong = Convert.ToInt32(row["SoLuong"]);
                    string congTy = row["CongTy"].ToString();
                    data_thuoc.Rows.Add(maThuoc, tenThuoc, hDSD, giaBan, congTy);
                }
            }
        }


        private void Medicine_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = MedicineHelper.getAllMedicine();
                loadMedicine(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
