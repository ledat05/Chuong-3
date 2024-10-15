using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BT04_DATAGridView
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form_Load(object sender, EventArgs e)
        {
            ThemDongVaoBan();
            DataGridViewRow r = dgvMonHoc.Rows[2];
            r.Selected = true;
            GanDuLieu(r);
        }

        private void GanDuLieu(DataGridViewRow r)
        {
            txtMaMH.Text = r.Cells["colMaMH"].Value.ToString();
            txtTenMH.Text = r.Cells["colTenMH"].Value.ToString();
            txtSoTiet.Text = r.Cells["colSoTiet"].Value.ToString();
        }

        private void ThemDongVaoBan()
        {
            dgvMonHoc.Rows.Add("01", "Cơ Sở Dữ Liệu", 90);
            dgvMonHoc.Rows.Add("02", "Lập Trình Java Nâng Cao", 90);
            dgvMonHoc.Rows.Add("03", "Pháp Luật", 75);
            dgvMonHoc.Rows.Add("04", "Mạng máy tính", 75);
            dgvMonHoc.Rows.Add("05", "Lập Trình Java Cơ Bản", 90);

        }

        private void dgvMonHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow r = dgvMonHoc.Rows[e.RowIndex];
            GanDuLieu(r);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaMH.ReadOnly = false;
            foreach (Control ctl in this.Controls)
                if (ctl is TextBox)
                    (ctl as TextBox).Clear();
            txtMaMH.Focus();
        }

        private void btnKhong_Click(object sender, EventArgs e)
        {
            GanDuLieu(dgvMonHoc.SelectedRows[0]);
            txtMaMH.ReadOnly = true;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult tl;
            tl = MessageBox.Show("Ban co muon huy mon hoc nay khong(y/n)?", "Huy mon hoc",
                MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(tl==DialogResult.Yes)
            {
                DataGridViewRow rhuy = dgvMonHoc.SelectedRows[0];
                dgvMonHoc.Rows.Remove(rhuy);

                dgvMonHoc.Rows[0].Selected = true;
                GanDuLieu(dgvMonHoc.Rows[0]);
            }
        }

        private void btGhi_Click(object sender, EventArgs e)
        {
            if(txtMaMH.ReadOnly==true)//Ghi khi sua
            {
                DataGridViewRow rsua = dgvMonHoc.SelectedRows[0];

                rsua.Cells[1].Value = txtTenMH.Text;
                rsua.Cells[2].Value = txtSoTiet.Text;
            }
            else
            {
                int stt = dgvMonHoc.Rows.Add(txtMaMH.Text, txtTenMH.Text, txtSoTiet.Text);

                dgvMonHoc.Rows[stt].Selected = true;
                txtMaMH.ReadOnly = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtMaMH_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTenMH_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSoTiet_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
