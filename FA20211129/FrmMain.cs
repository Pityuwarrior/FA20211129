using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FA20211129
{
    public partial class FrmMain : Form
    {
        public string ConnectionString { get; private set; }
        public FrmMain()
        {
            ConnectionString =
                "Server = (localdb)\\MSSQLLocalDB;" +
                "AttachDBFileName = |DataDirectory|\\zoldseges.mdf";
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FrmMain_Load(object sender, EventArgs e)
            =>FillDGV();
        private void FillDGV() 
        {
            dgv.Rows.Clear();
            using (var c = new SqlConnection((ConnectionString))
            {
                c.Open();
                var r = new SqlCommand(
                    "SELECT datum, nev, egysegAr * mennyiseg"+
                    "FROM zoldseg"+
                    "INNER JOIN eladas ON id = zoldseg"+
                    $"WHERE nev LIKE '{tbKereses.Text}%';",c)
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            FrmUjTermek f2 = new FrmUjTermek();
            f2.ShowDialog();
        }
    }
}
