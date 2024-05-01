using Guna.UI2.WinForms;
using NhaKhoaCuoiKy.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NhaKhoaCuoiKy.Views.PatientForm
{
    public partial class AddNewRecord : Form
    {
        int patienID;
        PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
        PrintDocument printDocument = new PrintDocument();
        public AddNewRecord(int patienID)
        {
            InitializeComponent();
            this.patienID = patienID;
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dtp_date_ValueChanged(object sender, EventArgs e)
        {
            DateTime today = DateTime.Now;
            DateTime dtp_pick = dtp_date.Value;
            int age = today.Year - dtp_pick.Year;
            tb_age.Text = age.ToString();
        }

        private void AddNewRecord_Load(object sender, EventArgs e)
        {
            panel_patienInfo.Location = new Point(21, 115);
            panel_medicalhistory.Location = new Point(21, 330);
            panel_reason.Location = new Point(21, 755); // Khoảng cách = 20
            panel_diagnose.Location = new Point(21, 1025);
            panel_plan.Location = new Point(21, 1435);
            panel_abstract.Location = new Point(21, 1850);
            loadPatientInfomation();
        }

        private void loadPatientInfomation()
        {
            try
            {
                DataTable dt = PatientHelper.getByID(patienID);
                if (dt.Rows.Count != 1)
                {
                    MessageBox.Show("Không thể thêm bệnh án", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }
                string name = Convert.ToString(dt.Rows[0]["HoVaTen"]).Trim();
                DateTime dob = Convert.ToDateTime(dt.Rows[0]["NgaySinh"]);
                string h_number = dt.Rows[0]["SoNha"].ToString();
                string s_name = dt.Rows[0]["TenDuong"].ToString();
                string ward = dt.Rows[0]["Phuong"].ToString();
                string city = dt.Rows[0]["ThanhPho"].ToString();
                string address = h_number + ", " + s_name + ", " + ward + ", " + city;
                string phone = dt.Rows[0]["SoDienThoai"].ToString();
                string gender = dt.Rows[0]["GioiTinh"].ToString().Trim();
                Image img;
                using (MemoryStream ms = new MemoryStream((byte[])dt.Rows[0]["Anh"]))
                {
                    img = Image.FromStream(ms);
                }
                pb_patientImg.Image = img;
                tb_name.Text = name;
                tb_address.Text = address;
                tb_phone.Text = phone;
                if (gender == "female") rbt_female.Checked = true;
                else if (gender == "male") rbt_male.Checked = true;
                dtp_date.Value = dob;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR::" + ex.Message);
            }
        }

        private void btn_createAppointment_Click(object sender, EventArgs e)
        {
            try
            {
                string DentalDisease = tb_DentalDisease.Text.Trim();
                string OtherDisease = tb_OtherDisease.Text.Trim();
                string Symptoms = tb_Symptoms.Text.Trim();
                string Result = tb_Result.Text.Trim();
                string Diagnosis = tb_Diagnosis.Text.Trim();
                string TreatmentMethod = tb_TreatmentMethod.Text.Trim();
                string NextAppointment = tb_NextAppointment.Text.Trim();
                DateTime now = DateTime.Now;
                if (PatientHelper.addNewRecord(patienID, 1, DentalDisease, OtherDisease, Symptoms, Result, Diagnosis, TreatmentMethod, NextAppointment, now))
                {
                    MessageBox.Show("Thêm bệnh án thành công", "Bệnh án", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Thêm bệnh án thất bại", "Bệnh án", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            Print(panel_main);
        }

        void Print(Panel panel)
        {
            PrinterSettings ps = new PrinterSettings();
            getPrintArea(panel);
            printPreviewDialog.Document = printDocument;
            printDocument.PrintPage += new PrintPageEventHandler(printDocument_print_page);
            printPreviewDialog.ShowDialog();
        }

        void printDocument_print_page(object sender, PrintPageEventArgs e)
        {
            Rectangle pageArea = e.PageBounds;
            e.Graphics.DrawImage(memoryIMG, 89, 600);
        }

        Bitmap memoryIMG;

        void getPrintArea(Panel panel)
        {
            memoryIMG = new Bitmap(panel.Width, panel.Height);
            panel.DrawToBitmap(memoryIMG, new Rectangle(0, 0, panel.Width, panel.Height));

        }


    }
}
