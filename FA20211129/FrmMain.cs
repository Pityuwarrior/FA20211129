using System;
using System.Data.SqlClient;
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
                "Database = zoldseges";
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
            =>FillDGV();
        private void FillDGV() 
        {
            dgv.Rows.Clear();
            using (var c = new SqlConnection(ConnectionString))
            {
                c.Open();
                var command = new SqlCommand(
                    "SELECT datum, nev, egysegAr * mennyiseg" +
                    "FROM zoldseg" +
                    "INNER JOIN eladas ON id = zoldseg" +
                    $"WHERE nev LIKE '{tbKereses.Text}%';", c);
                    var r = command.ExecuteReader();
                while (r.Read())
                {
                    dgv.Rows.Add(
                    r.GetDateTime(0).ToString("yyyy-MM-dd"),
                    r[1],
                    r[2] + " Ft");

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmUjTermek f2 = new FrmUjTermek();
            f2.ShowDialog();
        }

        private void tbKereses_TextChanged(object sender, EventArgs e) 
            => FillDGV();
    }
   
}
