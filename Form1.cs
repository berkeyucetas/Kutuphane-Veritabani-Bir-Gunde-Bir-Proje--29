using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kütüphane_Veritabanı
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-LQE0JO3;Initial Catalog=kutuphane;Integrated Security=True");
        private void verilerimiGöstter()
        {
            listView1.Items.Clear();
            baglan.Open();
            SqlCommand cmd = new SqlCommand("select * from kitaplar", baglan);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem add = new ListViewItem();
                add.Text = reader["id"].ToString();
                add.SubItems.Add(reader["kitapad"].ToString());
                add.SubItems.Add(reader["yazar"].ToString());
                add.SubItems.Add(reader["yayinevi"].ToString());
                add.SubItems.Add(reader["sayfa"].ToString());
                add.SubItems.Add(reader["kitaptur"].ToString());
                listView1.Items.Add(add);
            }
            baglan.Close();
        }
        private void btnListele_Click(object sender, EventArgs e)
        {
            verilerimiGöstter();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            txtKitap.Text = "";
            txtSayfa.Text = "";
            txtTur.Text = "";
            txtYayin.Text = "";
            txtYazar.Text = "";
            txtId.Text = "";
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];
                txtKitap.Text = selectedItem.Text;
                txtYazar.Text = selectedItem.SubItems[1].Text;
                txtYayin.Text = selectedItem.SubItems[2].Text;                
                txtSayfa.Text = selectedItem.SubItems[3].Text;
                txtTur.Text = selectedItem.SubItems[4].Text;
                txtId.Text = selectedItem.SubItems[5].Text;
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            string Kitap = txtKitap.Text;
            string Sayfa = txtSayfa.Text;
            string Tur = txtTur.Text;
            string Yayin = txtYayin.Text;
            string Yazar = txtYazar.Text;

            string query = "INSERT INTO kitaplar (id, Kitap, Sayfa, Tur, Yayin, Yazar) VALUES (@id, @kitapad, @yazar, @yayinevi, @sayfa, @kitaptur)";

            baglan.Open();
            SqlCommand cmd = new SqlCommand(query, baglan);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@kitapad", Kitap);
            cmd.Parameters.AddWithValue("@yazar", Yazar);
            cmd.Parameters.AddWithValue("@kitaptur", Tur);
            cmd.Parameters.AddWithValue("@yayinevi", Yayin);
            cmd.Parameters.AddWithValue("@sayfa", Sayfa);

            cmd.ExecuteNonQuery();

            baglan.Close();
            verilerimiGöstter();
            txtKitap.Text = "";
            txtSayfa.Text = "";
            txtTur.Text = "";
            txtYayin.Text = "";
            txtYazar.Text = "";
            txtId.Text = "";

        }        
    }
}
