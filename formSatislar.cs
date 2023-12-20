using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sinemaOtomasyonu
{
    public partial class formSatislar : Form
    {
        public formSatislar()
        {
            InitializeComponent();
        }

        private void formSatislar_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = veri.select("select * from satis");
            dataGridView1.DataSource = dt;
        }
    }
}
