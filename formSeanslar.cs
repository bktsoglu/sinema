using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace sinemaOtomasyonu
{
    public partial class formSeanslar : Form
    {
        public formSeanslar()
        {
            InitializeComponent();
        }

        private void formSeanslar_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = veri.select("select * from seans");
            dataGridView1.DataSource = dt;
        }
    }
}
