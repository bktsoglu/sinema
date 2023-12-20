using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace sinemaOtomasyonu
{
    public partial class formFilmEkle : Form
    {
        public formFilmEkle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(open.FileName);
                secilenresim += open.SafeFileName;
            }
        }
        public string secilenresim = "";
        private void button2_Click(object sender, EventArgs e)
        {
            
            var enviroment = System.Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(enviroment).Parent.Parent.FullName;

            //MessageBox.Show(projectDirectory+@"\filmAfis\"+secilenresim, "dir");
            
            string komut = veri.komut("Insert into film(filmAdi,FilmTuru,vizyonTarihi,yonetmen,sure,oyuncular,afis)Values('" + textBox1.Text + "','" + textBox5.Text + "','" + dateTimePicker1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + secilenresim + "')");
            if (komut == "")
            {
                pictureBox1.Image.Save(projectDirectory + @"\filmAfis\" + secilenresim, ImageFormat.Jpeg);
                MessageBox.Show("Film Başarıyla Eklendi!", "Tebrikler");
            }
            else
                MessageBox.Show(komut, "Hata");
            pictureBox1.Image=null;
        }
    }
}
