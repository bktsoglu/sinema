using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace sinemaOtomasyonu
{
    public partial class formSalonEkle : Form
    {
        public formSalonEkle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string komut = veri.komut("Insert into salon(salonAdi)Values('" + textBox1.Text + "')");
            if (komut == "")
                MessageBox.Show("Salon Başarıyla Eklendi!", "Tebrikler");
            else
                MessageBox.Show(komut, "Hata");

        }
    }
}
