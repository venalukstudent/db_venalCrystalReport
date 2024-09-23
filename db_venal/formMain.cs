using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace db_venal
{
    public partial class formMain : Form
    {
        private MySqlConnection koneksi;
        private MySqlDataAdapter adapter;
        private MySqlCommand perintah;
        private DataSet ds = new DataSet();
        private string alamat, query;

        public formMain()
        {
            alamat = "server=localhost; database=db_mahasiswa; username=root; password=;";
            koneksi = new MySqlConnection(alamat);
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsername.Text != "" && txtPassword.Text != "" && txtNama.Text != "")
                {

                    query = string.Format("insert into db_login  values ('{0}','{1}','{2}','{3}','{4}');", txtID.Text, txtUsername.Text, txtPassword.Text, txtNama.Text, CBLevel.Text);


                    koneksi.Open();
                    perintah = new MySqlCommand(query, koneksi);
                    adapter = new MySqlDataAdapter(perintah);
                    int res = perintah.ExecuteNonQuery();
                    koneksi.Close();
                    if (res == 1)
                    {
                        MessageBox.Show("Insert Data Suksess ...");
                        formMain_Load(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Gagal inser Data . . . ");
                    }
                }
                else
                {
                    MessageBox.Show("Data Tidak lengkap !!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void formMain_Load(object sender, EventArgs e)
        {
            try
            {
                koneksi.Open();
                query = string.Format("select * from db_login");
                perintah = new MySqlCommand(query, koneksi);
                adapter = new MySqlDataAdapter(perintah);
                perintah.ExecuteNonQuery();
                ds.Clear();
                adapter.Fill(ds);
                koneksi.Close();
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Width = 100;
                dataGridView1.Columns[0].HeaderText = "ID Pengguna";
                dataGridView1.Columns[1].Width = 150;
                dataGridView1.Columns[1].HeaderText = "Username";
                dataGridView1.Columns[2].Width = 120;
                dataGridView1.Columns[2].HeaderText = "Password";
                dataGridView1.Columns[3].Width = 120;
                dataGridView1.Columns[3].HeaderText = "Nama Pengguna";
                dataGridView1.Columns[4].Width = 120;
                dataGridView1.Columns[4].HeaderText = "Level";

                txtID.Clear();
                txtNama.Clear();
                txtPassword.Clear();
                txtUsername.Clear();
                txtID.Focus();
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                btnClear.Enabled = false;
                btnSave.Enabled = true;
                btnSearch.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
