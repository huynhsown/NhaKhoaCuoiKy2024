﻿using NhaKhoaCuoiKy.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NhaKhoaCuoiKy.Views.Employee
{
    public partial class EditDoctor : Form
    {
        Doctor doctor;
        int doctorId;
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );
        public EditDoctor(Doctor doctor, int doctorId)
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            this.doctor = doctor;
            this.doctorId = doctorId;
        }
        public EditDoctor()
        {
            InitializeComponent();
        }

        private void EditDoctor_Load(object sender, EventArgs e)
        {

            try
            {
                DataTable dt = EmployeeHelper.getDoctorByID(doctorId);
                if (dt.Rows.Count == 0)
                {
                    // Decide what to do if no employee is found
                    Close(); // or any other action
                    return;
                }

                DataRow row = dt.Rows[0]; // Get the first row

                string name = row["HoVaTen"].ToString();
                string hocVi = row["HocVi"].ToString();
                string chuyenMon = row["ChuyenMon"].ToString();
                DateTime birth = Convert.ToDateTime(row["NgaySinh"]);
                string phone = row["SoDienThoai"] == DBNull.Value ? "" : row["SoDienThoai"].ToString();
                int homenum = Convert.ToInt32(row["SoNha"]);
                string ward = row["Phuong"].ToString();
                string city = row["ThanhPho"].ToString();
                string gender = row["GioiTinh"].ToString();
                string street = row["TenDuong"].ToString();
                string position = row["ViTri"].ToString();
                DateTime beginwork = Convert.ToDateTime(row["NgayBatDauLamViec"]);
                int salary = Convert.ToInt32(row["TienLuong"]);

                tb_name.Text = name;
                dtp_birth.Value = birth;
                tb_sodienthoai.Text = phone;
                tb_homenum.Text = homenum.ToString();
                tb_ward.Text = ward;
                tb_city.Text = city;
                if (gender.Trim().Equals("nam", StringComparison.OrdinalIgnoreCase))
                {
                    rdb_male.Checked = true;
                }
                else if (gender.Trim().Equals("nu", StringComparison.OrdinalIgnoreCase))
                {
                    rdb_female.Checked = true;
                }
                else
                {
                    rdb_other.Checked = true;
                }
                tb_street.Text = street;
                tb_vitrilamviec.Text = position;
                dtp_beginwork.Value = beginwork;
                tb_tienluong.Text = salary.ToString();
                tb_hocvi.Text = hocVi;
                tb_chuyenmon.Text = chuyenMon;
                // Handling image
                if (row["Anh"] != DBNull.Value)
                {
                    byte[] pic = (byte[])row["Anh"];
                    MemoryStream ms = new MemoryStream(pic);
                    pb_avt.Image = Image.FromStream(ms);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (!verify())
            {
                MessageBox.Show("Dữ liệu thiếu hoặc sai", "Sửa bác sĩ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            try
            {
                string name = tb_name.Text;
                string hocVi = tb_hocvi.Text;
                string chuyenMon = tb_chuyenmon.Text;
                DateTime birth = dtp_birth.Value;
                string phone = tb_sodienthoai.Text;
                int homenum = int.Parse(tb_homenum.Text);
                string ward = tb_ward.Text;
                string city = tb_city.Text;
                string gender = "other";
                string street = tb_street.Text;
                string position = tb_vitrilamviec.Text;
                DateTime beginwork = dtp_beginwork.Value;
                int salary = int.Parse(tb_tienluong.Text);
                if (rdb_male.Checked)
                {
                    gender = "nam";
                }
                else if (rdb_female.Checked)
                {
                    gender = "nu";
                }
                MemoryStream ms = new MemoryStream();
                pb_avt.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] img = ms.ToArray();
                if (EmployeeHelper.updateDoctor(doctorId, name,hocVi,chuyenMon, gender, birth, salary, beginwork, homenum, ward, city, position, img, phone, street))
                {
                    MessageBox.Show("Sửa bác sĩ thành công", "Sửa bác sĩ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    doctor.loadAllDoctor();
                    Close();
                }
                else
                {
                    MessageBox.Show("Sửa bác sĩ thất bại", "Sửa bác sĩ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool verify()
        {
            if (tb_name.Text.Trim().Length == 0
                || dtp_birth.Value.ToString().Trim().Length == 0
                || tb_chuyenmon.Text.Trim().Length == 0
                || tb_hocvi.Text.Trim().Length == 0
                || tb_sodienthoai.Text.Trim().Length == 0
                || tb_homenum.Text.Trim().Length == 0
                || tb_ward.Text.Trim().Length == 0
                || tb_city.Text.Trim().Length == 0
                || tb_street.Text.Trim().Length == 0
                || tb_vitrilamviec.Text.Trim().Length == 0
                || dtp_beginwork.Value.ToString().Trim().Length == 0
                || tb_tienluong.Text.Trim().Length == 0
                || pb_avt.Image.ToString().Trim().Length == 0) return false;


            if (rdb_male.Checked == false && rdb_female.Checked == false && rdb_other.Checked == false) return false;


            //if (tb_discount.BorderThickness == 3
            //    || tb_price.BorderThickness == 3
            //    || tb_title.BorderThickness == 3
            //    || tb_unit.BorderThickness == 3
            //    || tb_warranty.BorderThickness == 3) return false;
            return true;
        }
    }
}
