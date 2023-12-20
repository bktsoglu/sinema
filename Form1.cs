using System;
using System.ComponentModel;
using System.Data;
using System.Drawing.Imaging;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace sinemaOtomasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnKoltukIptal_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            formSalonEkle formSalonEkle = new formSalonEkle();
            formSalonEkle.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            formFilmEkle formFilmEkle = new formFilmEkle();
            formFilmEkle.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            formSeansEkle formSeansEkle = new formSeansEkle();
            formSeansEkle.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            formSeanslar formSeanslar = new formSeanslar();
            formSeanslar.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            formSatislar formSatislar = new formSatislar();
            formSatislar.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            

            #region film bilgilerini doldur
            DataTable dt = new DataTable();
            dt = veri.select("select * from film where filmAdi='" + comboBox1.SelectedItem + "'");
            label2.Text = "Film Adý= " + dt.Rows[0][1];
            label3.Text = "Film Türü= " + dt.Rows[0][2];
            label4.Text = "Vizyon Tarihi= " + dt.Rows[0][3];
            label5.Text = "Yönetmen= " + dt.Rows[0][4];
            label6.Text = "Süre= " + dt.Rows[0][5];
            label7.Text = "Oyuncular= " + dt.Rows[0][6];
            var enviroment = System.Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(enviroment).Parent.Parent.FullName;
            pictureBox1.Image = new Bitmap(projectDirectory + @"\filmAfis\" + dt.Rows[0][7]);

            //salon adlarýný getir
            comboBox2.Items.Clear();
            DataTable salonDt = new DataTable();
            salonDt = veri.select("select salonAdi from seans where filmAdi='"+comboBox1.SelectedItem + "'");
            for (int i = 0; i < salonDt.Rows.Count; i++)
            {
                comboBox2.Items.Add(salonDt.Rows[i].Field<string>("salonAdi"));
            }

           
            #endregion
            /*
            Button button2 = new Button();
            groupBox5.Controls.Add(button2);
            button2.Text = "1";
            button2.Size = new Size(45, 45);
            button2.ForeColor = Color.Black;
            button2.BackColor = Color.White;*/
        }
        public int ucret = 0;
        public List<int> koltukNo = new List<int>();
        private void NewButton_Click(object? sender, EventArgs e)
        {
            

            Button button = (sender as Button);
            
            if (button.BackColor == Color.DarkOrange)
            {
                button.BackColor = Color.White;
                if (ucret != 0)
                    ucret -= 150;
                label23.Text="Ücret= " +ucret.ToString();
                koltukNo.Remove(int.Parse(button.Text));
                //return;
            }
            
            else if (button.BackColor == Color.White)
            {
                ucret += 150;
                label23.Text = "Ücret= " + ucret.ToString();
                button.BackColor = Color.DarkOrange;
                koltukNo.Add(int.Parse(button.Text));
                //return;
            }
            textBox4.Text = "";
            foreach (var item in koltukNo)
            {
                textBox4.Text += item.ToString() + ",";
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*
            //salon adlarýný getir
            DataTable salonDt = new DataTable();
            salonDt = veri.select("select salonAdi from salon");
            for (int i = 0; i < salonDt.Rows.Count; i++)
            {
                comboBox2.Items.Add(salonDt.Rows[i].Field<string>("salonAdi"));
            }*/

            //film adlarýný getir
            DataTable filmDt = new DataTable();
            filmDt = veri.select("select filmAdi from film");
            for (int i = 0; i < filmDt.Rows.Count; i++)
            {
                comboBox1.Items.Add(filmDt.Rows[i].Field<string>("filmAdi"));
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string koltuklar = "";
            foreach (var item in koltukNo)
            {
                koltuklar+=item.ToString()+",";
            }

            string komut = veri.komut("Insert into satis(filmAdi,salonAdi,tarih,seans,adSoyad,telefon,koltukNo,ucret)" +
                "Values('" + comboBox1.SelectedItem+ "','" + comboBox2.SelectedItem+ "','" + comboBox4.SelectedItem + "','" + comboBox3.SelectedItem + "','" + textBox1.Text + "','" + textBox2.Text + "','"+ koltuklar + "',"+ucret+")");
            if (komut == "")
            {
                MessageBox.Show("Satýþ Baþarýyla Gerçekleþti!", "Bilgi");
                comboBox3_SelectedIndexChanged(this, EventArgs.Empty);
            }
            else
                MessageBox.Show(komut, "Hata");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //tarih getir
            comboBox4.Items.Clear();
            DataTable tarihDt = new DataTable();
            tarihDt = veri.select("select tarih from seans where filmAdi='" + comboBox1.SelectedItem + "' and salonAdi='" + comboBox2.SelectedItem + "'");
            for (int i = 0; i < tarihDt.Rows.Count; i++)
            {
                comboBox4.Items.Add(tarihDt.Rows[i].Field<DateTime>("tarih").ToString("dd.MM.yyyy"));
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //satýþ tablosundaki verilere göre koltuk renkleri ve týklanabilirliði ayarla
            #region koltuk butonlarý oluþtur
           // string[] koltuklar;
            List<string> koltuklarr = new List<string>();
            string doluKoltuk = "select koltukNo from satis where filmAdi='" + comboBox1.SelectedItem + "' and salonAdi='" + comboBox2.SelectedItem + "' and  (tarih = DateValue('"+comboBox4.SelectedItem+"')) and seans='"+comboBox3.SelectedItem+"'";
            DataTable dtKoltuk= veri.select(doluKoltuk);
            for (int z = 0; z < dtKoltuk.Rows.Count; z++)
            {
                string[] seanslar = dtKoltuk.Rows[z].Field<string>("koltukNo").Split(',');
                for (int s = 0; s < seanslar.Length; s++)
                {
                    if(seanslar[s]!="")
                    {
                        koltuklarr.Add(seanslar[s]);
                        comboIptalKoltukNo.Items.Add(seanslar[s]);
                    }
                }
            }
            textBox4.Text = "";
            koltukNo.Clear();
            ucret = 0;
            label23.Text = "Ücret= ";
            groupBox5.Controls.Clear();
            int biletId = 0;
            int x = 6, y = 18;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Button newButton = new Button();
                    groupBox5.Controls.Add(newButton);
                    biletId++;
                    newButton.Text = biletId.ToString();
                    newButton.Location = new Point(x, y);
                    newButton.Size = new Size(45, 45);
                    newButton.ForeColor = Color.Black;
                    newButton.BackColor = Color.White;
                    for (int k = 0; k < koltuklarr.Count; k++)
                    {
                        if (koltuklarr[k] == biletId.ToString())
                        { 
                            newButton.BackColor = Color.Red;
                            newButton.Enabled = false;
                            break;
                        }
                        else
                            newButton.BackColor = Color.White;

                    }

                    //newButton.BackColor = Color.White;
                    newButton.Click += new System.EventHandler(this.NewButton_Click);

                    x = x + 50;
                }
                x = 6;
                y = y + 50;
            }

            
            #endregion
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            //seanslarý getir
            comboBox3.Items.Clear();
            DataTable seansDt = new DataTable();
            seansDt = veri.select("select seanslar from seans where filmAdi='" + comboBox1.SelectedItem + "' and salonAdi='" + comboBox2.SelectedItem + "' and  (tarih = DateValue('"+comboBox4.SelectedItem+"'))");
            for (int i = 0; i < seansDt.Rows.Count; i++)
            {
                string[] seanslar = seansDt.Rows[i].Field<string>("seanslar").Split(',');
                for (int j = 0; j < seanslar.Length; j++)
                {

                    comboBox3.Items.Add(seanslar[j]);
                }
            }
        }
    }
}