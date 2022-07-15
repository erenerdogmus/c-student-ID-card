using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;


//using iTextSharp.text;
//using iTextSharp.text.pdf;

namespace kimlik_kart
{
    public partial class Form3 : Form
    {
        public string tc;
        public string adsoyad;
        public string telefon;
        public string okulno;
        public string sınıf;
        public Image resim;     
        public Form3()
        {
            InitializeComponent();
        }
        private void myPrintDocument2_PrintPage(System.Object gönderen, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Bitmap myBitmap1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //pictureBox1.DrawToBitmap(myBitmap1, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            //e.Graphics.DrawImage(myBitmap1, 0, 0);
            //myBitmap1.Dispose();
        }     
        private void Form3_Load(object sender, EventArgs e)
        {
            label1.Text = "TC :" + Form1.tc.ToUpper();
            label2.Text = "AD Soyad :" + Form1.adsoyad.ToUpper();
            label3.Text = "Telefon :" + Form1.telefon.ToUpper();
            label4.Text = "OkulNo : " + Form1.okulno.ToUpper();
            label5.Text = "Sınıf :" + Form1.Sınıf.ToUpper();          
        }
        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            //Print image
            Bitmap bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(bm, new Rectangle(0,0, pictureBox1.Width, pictureBox1.Height));
            e.Graphics.DrawImage(bm, 0, 0);
            bm.Dispose();

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;

            e.Graphics.DrawString(label1.Text, f1, sb, 200, 100);
            e.Graphics.DrawString(label2.Text, f2, sb, 200, 150);
            e.Graphics.DrawString(label3.Text, f3, sb, 200, 200);
            e.Graphics.DrawString(label4.Text, f4, sb, 200, 250);
            e.Graphics.DrawString(label5.Text, f5, sb, 200, 300);
        }
        private void button1_Click(object sender, EventArgs e)
        {
                //Show print dialog
                PrintDialog pd = new PrintDialog();
                PrintDocument doc = new PrintDocument();
                doc.PrintPage += Doc_PrintPage;
                pd.Document = doc;
                if (pd.ShowDialog() == DialogResult.OK)
                    doc.Print();
                ppdDiyalog.ShowDialog(); 
        }    
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Drawing.Printing.PrintDocument myPrintDocument1 = new System.Drawing.Printing.PrintDocument();
            PrintDialog myPrinDialog1 = new PrintDialog();
            myPrintDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(myPrintDocument2_PrintPage);
            myPrinDialog1.Document = myPrintDocument1;
            if (myPrinDialog1.ShowDialog() == DialogResult.OK)
            {
                myPrintDocument1.Print();
            }
            //openFileDialog1.ShowDialog();
            //pictureBox1.ImageLocation = openFileDialog1.FileName;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "JPEG|*.jpg", ValidateNames = true })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                    pictureBox1.Image = Image.FromFile(ofd.FileName);
            }

            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
            textBox1.Text = openFileDialog1.FileName;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }
        Font f1 = new Font("verdane", 12, FontStyle.Bold);
        Font f2 = new Font("Verdane", 15, FontStyle.Bold);
        Font f3 = new Font("Verdane", 15, FontStyle.Bold);
        Font f4 = new Font("Verdane", 15, FontStyle.Bold);
        Font f5 = new Font("Verdane", 15, FontStyle.Bold);      
        SolidBrush sb = new SolidBrush(Color.Black);
        private void pdYazici_PrintPage(object sender, PrintPageEventArgs e)
        {
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;

            e.Graphics.DrawString(label1.Text, f1, sb, 200, 100);
            e.Graphics.DrawString(label2.Text, f2, sb, 200, 150);
            e.Graphics.DrawString(label3.Text, f3, sb, 200, 200);
            e.Graphics.DrawString(label4.Text, f4, sb, 200, 250);
            e.Graphics.DrawString(label5.Text, f5, sb, 200, 300);
        }
    }
}
