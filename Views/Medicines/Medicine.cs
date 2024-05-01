using Dapper;
using NhaKhoaCuoiKy.Helpers;
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
        public Medicine()
        {
            InitializeComponent();
        }
        public Medicine(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }
        private void button_themmoi_Click(object sender, EventArgs e)
        {
            newMedicine?.Close();
            newMedicine = new NewMedicine();
            newMedicine.Owner = this;
            newMedicine.Show();
            newMedicine.eventAddMedicine += (s, e) =>
            {
                DynamicParameters p = new DynamicParameters();
                string maThuoc = p.Get<string>("@MaThuoc");
                string tenThuoc = p.Get<string>("@TenThuoc");
                string hDSD = p.Get<string>("@HuongDanSD");
                string thanhPhan = p.Get<string>("@ThanhPhan");
                int giaNhap = p.Get<int>("@GiaNhap");
                int giaBan = p.Get<int>("@GiaBan");
                int soLuong = p.Get<int>("SoLuong");
                string congTy = p.Get<string>("@CongTy");
                data_thuoc.Rows.Add(maThuoc, tenThuoc, hDSD, giaBan, congTy);
            };
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

        private void loadMedicine(DataTable dt)
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
