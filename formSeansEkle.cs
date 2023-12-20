using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace sinemaOtomasyonu
{
    public partial class formSeansEkle : Form
    {
        public formSeansEkle()
        {
            InitializeComponent();
        }

        private void formSeansEkle_Load(object sender, EventArgs e)
        {
            //film adlarını getir
            DataTable filmDt = new DataTable();
            filmDt = veri.select("select filmAdi from film");
            for (int i = 0; i < filmDt.Rows.Count; i++)
            {
                comboBox1.Items.Add(filmDt.Rows[i].Field<string>("filmAdi"));
            }

            //salon adlarını getir
            DataTable salonDt = new DataTable();
            salonDt = veri.select("select salonAdi from salon");
            for (int i = 0; i < salonDt.Rows.Count; i++)
            {
                comboBox2.Items.Add(salonDt.Rows[i].Field<string>("salonAdi"));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string seanslar = "";
            if (checkBox3.Checked)
            {
                seanslar += checkBox3.Text+",";
            }
            if (checkBox5.Checked)
            {
                seanslar += checkBox5.Text + ",";
            }
            if (checkBox6.Checked)
            {
                seanslar += checkBox6.Text;
            }
            string komut = veri.komut("Insert into seans(filmAdi,tarih,salonAdi,seanslar)Values('" + comboBox1.Text + "','" + dateTimePicker1.Text + "','" + comboBox2.Text + "','" + seanslar + "')");
            if (komut == "")
            {
                MessageBox.Show("Seans Başarıyla Eklendi!", "Tebrikler");
            }
            else
                MessageBox.Show(komut, "Hata");
        }
    }
}
