using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BT05_DataGridView_4_SQL
{
    public partial class Form1 : Form
    {
        string strcon = @"Server=.; Database=QLSV_SV_L1;integrated security=true";
        DataSet ds = new DataSet();
        //Khai bao doi tuong DataAdapter de su dung cho cac bang du lieu
        SqlDataAdapter adpMonHoc, adpKetqua;

        SqlCommandBuilder cmbMonHoc;
        BindingSource bs = new BindingSource();
        int stt = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void dgvMonHoc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            KhoiTaoCacDoiTuong();
            DocDuLieu();
            MocNoiQuanHe();
          
            
            KhoiTaoBindingSoure();
         
        
            dgvMonHoc.DataSource = bs;
            dgvMonHoc.Columns[3].Visible = false;

            LienKetDieuKhien();
            //Lien ket 
            bdnMonHoc.BindingSource = bs;
        }

        private void GanDuLieu(DataGridViewRow r)
        {
            txtMaMH.Text = r.Cells["colMaMH"].Value.ToString();
            txtTenMH.Text = r.Cells["colTenMH"].Value.ToString();
            txtSoTiet.Text = r.Cells["colSoTiet"].Value.ToString();
        }

        private void LienKetDieuKhien()
        {
            txtMaMH.DataBindings.Add("Text", bs, "MaMH", true);
            txtTenMH.DataBindings.Add("Text", bs, "MaMH", true);
            txtSoTiet.DataBindings.Add("Text", bs, "SoTiet", true);
        }

        private void KhoiTaoBindingSoure()
        {

            bs.DataSource = ds;
            bs.DataMember = "MONHOC";
        }

        private void DocDuLieu()
        {
            adpMonHoc.FillSchema(ds, SchemaType.Source, "MONHOC");
            adpMonHoc.Fill(ds, "MONHOC");

            adpKetqua.FillSchema(ds, SchemaType.Source, "KETQUA");
            adpKetqua.Fill(ds, "KETQUA");
        }

        private void MocNoiQuanHe()
        {
            ds.Relations.Add("FK_MH_KQ", ds.Tables["MONHOC"].Columns["MaMH"], ds.Tables["KETQUA"].Columns["MaMH"], true);

            ds.Relations["FK_MH_KQ"].ChildKeyConstraint.DeleteRule = Rule.None;
          
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaMH.ReadOnly = false;
            bs.AddNew();
            txtMaMH.Focus();

        }

        private void btnKhong_Click(object sender, EventArgs e)
        {
            bs.CancelEdit();
        }

        private void btGhi_Click(object sender, EventArgs e)
        {
            if(txtMaMH.ReadOnly==true)
            {
                DataGridViewRow rsua = dgvMonHoc.SelectedRows[0];
                rsua.Cells[1].Value=txtMaMH.Text;
                rsua.Cells[2].Value = txtSoTiet.Text;
            }
            else
            {
                DataRow newRow = ds.Tables["MONHOC"].NewRow();
                newRow["MaMH"] = txtMaMH.Text;
                newRow["TenMH"] = txtTenMH.Text;
                newRow["SoTiet"] = txtSoTiet.Text;

                ds.Tables["MONHOC"].Rows.Add(newRow);
                txtMaMH.ReadOnly = true;
            }

            adpMonHoc.Update(ds, "MONHOC");
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult tl;
            tl=MessageBox.Show("Ban co muon huy mon hoc nay khong ?(y/n)","Thong bao",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            if(tl== DialogResult.OK)
            {
                DataGridViewRow rhuy = dgvMonHoc.SelectedRows[0];
                dgvMonHoc.Rows.Remove(rhuy);

                dgvMonHoc.Rows[0].Selected = true;
                

            }
        }

        private void KhoiTaoCacDoiTuong()
        {
            adpMonHoc = new SqlDataAdapter("select * from MonHoc ", strcon);
            adpKetqua = new SqlDataAdapter("select * from KetQua ", strcon);

            cmbMonHoc = new SqlCommandBuilder(adpMonHoc);
        }
    }
}
