using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ForexSys {
    public partial class POSLog : Form {
        public POSLog() {
            InitializeComponent();
            var dTable = new DataTable();
            var bSource = new BindingSource();
            using (
                var connection =
                    new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=FOREXSYS;Integrated Security=True")) {
                connection.Open();
                var dAdapter =
                    new SqlDataAdapter("SELECT [LOG_DATE],[EMP_ID],[TRANSACT_ID],[CMPNY_ID],[BRANCH_ID],[TRANSACT_TYPR] FROM [FOREXSYS].[dbo].[LOG]", connection);
                new SqlCommandBuilder(dAdapter);
                dAdapter.Fill(dTable);
                bSource.DataSource = dTable;
                dataGridView1.DataSource = bSource;
            }
        }
    }
}