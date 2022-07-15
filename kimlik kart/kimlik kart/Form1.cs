using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.IO;


namespace kimlik_kart
{
    public partial class Form1 : Form
    {
        static string conString = "Data Source=DESKTOP-8E8HBS2\\SQLEXPRESS;Initial Catalog=kimlikkart;Integrated Security=True";
        SqlConnection connect = new SqlConnection(conString);
        SqlDataReader okuyucu;     
        public void ogrencigetir()
        {
            connect.Open();
            string kayit = "select * from ogrenci";
            SqlCommand komut = new SqlCommand(kayit, connect);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            connect.Close();
        }
        public void verisil(int id)
        {
            string sil = "Delete from ogrenci where id =@id";
            SqlCommand komut = new SqlCommand(sil, connect);
            connect.Open();
            komut.Parameters.AddWithValue("@id", id);
            komut.ExecuteNonQuery();
            connect.Close();
            MessageBox.Show("Kayıt Silindi");
        }
        public Form1()
        {
            InitializeComponent();
            ogrencigetir();
        }
        int i = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();
                string kayit = "insert into ogrenci (tc,adsoyad,telefon,okulno,sınıf) values(@tc,@adsoyad,@telefon,@okulno,@sınıf)";
                SqlCommand komut = new SqlCommand(kayit, connect);
                komut.Parameters.AddWithValue("@tc", textBox1.Text);
                komut.Parameters.AddWithValue("@adsoyad", textBox2.Text);
                komut.Parameters.AddWithValue("@telefon", textBox3.Text);
                komut.Parameters.AddWithValue("@okulno", textBox4.Text);
                komut.Parameters.AddWithValue("@sınıf", textBox5.Text);
                

                komut.ExecuteNonQuery();
                connect.Close();
                MessageBox.Show("Kayıt Eklendi");
                ogrencigetir();

                for (int i = 0; i < Controls.Count; i++)
                {
                    if (Controls[i] is TextBox)
                    {
                        Controls[i].Text = "";
                    }
                }
            }

            catch (Exception hata)
            {
                MessageBox.Show("Hata Meydana Geldi" + hata.Message);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow drow in dataGridView1.SelectedRows)
            {
                int id = Convert.ToInt32(drow.Cells[0].Value);
                verisil(id);
                ogrencigetir();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string sql = @"update ogrenci set  tc=@tc, adsoyad=@adsoyad, telefon=@telefon,okulno=@okulno, sınıf=@sınıf  where id=@id";

            SqlCommand ekle = new SqlCommand(sql, connect);
            ekle.Parameters.AddWithValue("@tc", textBox1.Text);
            ekle.Parameters.AddWithValue("@adsoyad", textBox2.Text);
            ekle.Parameters.AddWithValue("@telefon", textBox3.Text);
            ekle.Parameters.AddWithValue("@okulno", textBox4.Text);
            ekle.Parameters.AddWithValue("@sınıf", textBox5.Text);          
            ekle.Parameters.AddWithValue("@id", dataGridView1.Rows[i].Cells[0].Value);

            connect.Open();
            ekle.ExecuteNonQuery();
            connect.Close();
            MessageBox.Show("Kayıt Güncellendi");
            ogrencigetir();
        }

        int id;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = Convert.ToInt32((dataGridView1.CurrentRow.Cells[0].Value));
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[rowIndex];
            id = int.Parse(row.Cells[0].Value.ToString());
            SqlCommand komut = new SqlCommand("select * from ogrenci where id=@id", connect);
            komut.Parameters.AddWithValue("@id", id);
            try
            {
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }
                okuyucu = komut.ExecuteReader();
                okuyucu.Read();
                textBox1.Text = okuyucu["tc"].ToString();
                textBox2.Text = okuyucu["adsoyad"].ToString();
                textBox3.Text = okuyucu["telefon"].ToString();
                textBox4.Text = okuyucu["okulno"].ToString();
                textBox5.Text = okuyucu["sınıf"].ToString();
                
                connect.Close();
                okuyucu.Close();
            }
            catch
            {

            }
        }
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            i = e.RowIndex;
            textBox1.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();          
        }
        public static string tc,adsoyad,telefon,okulno,Sınıf, Resim;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
           if (textBox1.Text == ""|| textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" /*|| textBox6.Text == ""*/)
            {
                MessageBox.Show("öğrenci secin","HATa",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                tc = textBox1.Text;
                adsoyad = textBox2.Text;
                telefon = textBox3.Text;
                okulno = textBox4.Text;
                Sınıf = textBox5.Text;             
                new Form3().ShowDialog();
            }
        }                
    }
}
