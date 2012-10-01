using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ForexSys {
    public partial class POSForm : Form {
        private readonly int _brhID;
        private readonly int _cmpID;
        private readonly int _empID;
        private readonly Logger _log = new Logger();
        private readonly string _userlevel = "";
        private readonly int _userlvl;
        private decimal _converted;
        private List<string> _currencyIDList;

        private List<int> _customerIDList;
        private List<string> _customerNameList;
        private decimal _inputAmt;

        public POSForm(SqlDataReader reader) {
            InitializeComponent();

            while (reader.Read()) {
                _empID = reader.GetInt32(0);
                l_empname.Text = reader.GetString(1);
                _userlevel = reader.GetString(2);
                l_compName.Text = reader.GetString(3);
                linkLabel1.Text = reader.GetString(4);
                _cmpID = reader.GetInt32(5);
                _brhID = reader.GetInt32(6);
                l_branch.Text = reader.GetString(7);
            }
            l_empno.Text = "" + _empID;
            _userlvl = int.Parse(_userlevel);
            _log.Log(l_empno.Text, 'L');
            initComboBox();
            comboBox_custID.DataSource = _customerIDList;
            comboBox_custName.DataSource = _customerNameList;
            comboBox_currID.DataSource = _currencyIDList;
        }

        private void initComboBox() {
            _customerIDList = new List<int>();
            _customerNameList = new List<string>();
            _currencyIDList = new List<string>();
            using (
                var conn =
                    new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=FOREXSYS;Integrated Security=True")) {
                conn.Open();
                using (
                    var comm = new SqlCommand("SELECT [CustomerID],[Customer Name] FROM [FOREXSYS].[dbo].[Customers]",
                                              conn)) {
                    SqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read()) {
                        _customerIDList.Add(reader.GetInt32(0));
                        _customerNameList.Add(reader.GetString(1));
                    }
                }
            }
            using (
                var conn =
                    new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=FOREXSYS;Integrated Security=True")) {
                conn.Open();
                using (var comm = new SqlCommand("SELECT [CURRENCY_ID] FROM [FOREXSYS].[dbo].[EXCHANGE]", conn)) {
                    SqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read()) {
                        _currencyIDList.Add(reader.GetString(0));
                    }
                }
            }
        }

        private void POSForm_FormClosing(object sender, FormClosingEventArgs e) {
            _log.Log(l_empno.Text, 'O');
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e) {
            comboBox_currID.Text = "";
            comboBox_custID.Text = "";
            comboBox_custName.Text = "";
            numericUpDown1.ResetText();
            textBox_converted.Text = "";
            textBox_exchRate.Text = "";
        }

        private void POSForm_Load(object sender, EventArgs e) {}

        private void comboBox_currID_SelectedIndexChanged(object sender, EventArgs e) {
            using (
                var connection =
                    new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=FOREXSYS;Integrated Security=True")) {
                connection.Open();
                using (
                    var command = new SqlCommand("SELECT CURR_EXCHANGE_RATE FROM EXCHANGE WHERE CURRENCY_ID=@currID",
                                                 connection)) {
                    command.Parameters.AddWithValue("@currID", comboBox_currID.Text);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows) {
                        while (reader.Read()) {
                            textBox_exchRate.Text = "" + reader.GetSqlDecimal(0);
                        }
                    }
                }
            }
        }

        private void comboBox_custName_SelectedIndexChanged(object sender, EventArgs e) {
            comboBox_custID.SelectedIndex = comboBox_custName.SelectedIndex;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
            _inputAmt = Convert.ToDecimal(textBox_exchRate.Text);
            _converted = (numericUpDown1.Value*_inputAmt);

            textBox_converted.Text = "" + _converted;
        }

        private void button1_Click(object sender, EventArgs e) {
            using (
                var connection =
                    new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=FOREXSYS;Integrated Security=True")) {
                connection.Open();
                string insert = "INSERT INTO [FOREXSYS].[dbo].[TRANSACTION]" +
                                " ([TRAN_AMOUNT]" +
                                " ,[CURRENCY]" +
                                " ,[CNVRT_AMNT]" +
                                ",[EMP_TELLER]" +
                                ",[CUSTOMER]" +
                                ",[TRANSACT_DATE]" +
                                ",[RATE])" +
                                " VALUES " +
                                "  (@tranAmount, @currID ,@cnverAmt,@empID ,@custID, GETDATE(), @exchrate)";

                using (var insertme = new SqlCommand(insert, connection)) {
                    insertme.Parameters.AddWithValue("@tranAmount", _inputAmt);
                    insertme.Parameters.AddWithValue("@currID", comboBox_currID.Text);
                    insertme.Parameters.AddWithValue("@cnverAmt", _converted);
                    insertme.Parameters.AddWithValue("@empID", l_empno.Text);
                    insertme.Parameters.AddWithValue("@custID", comboBox_custID.Text);
                    insertme.Parameters.AddWithValue("@exchrate", Convert.ToDecimal(textBox_exchRate.Text));
                    insertme.ExecuteNonQuery();
                }
                MessageBox.Show("Transaction Processed!");
            }
            using (
                var conn =
                    new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=FOREXSYS;Integrated Security=True")) {
                int transID = 0;
                conn.Open();
                using (var sqlcom = new SqlCommand("SELECT MAX([TRAN_ID]) FROM [FOREXSYS].[dbo].[TRANSACTION]", conn)) {
                    SqlDataReader reader = sqlcom.ExecuteReader();
                    while (reader.Read()) {
                        transID = reader.GetInt32(0);
                    }
                }
                _log.Log(l_empno.Text, transID + "");
            }
        }

        private void logToolStripMenuItem_Click(object sender, EventArgs e) {
            if (_userlvl >= 4) {
                var log = new POSLog();
                log.Show();
            }
            else {
                MessageBox.Show("Insufficient Access Privileges. Contact your Admin for details.");
            }
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e) {
            if (_userlvl >= 2) {
                var viewer = new POSTableViewer(_userlvl, _cmpID, _brhID, _empID);
                viewer.Show();
            }
            else {
                MessageBox.Show("Insufficient Access Privileges. Contact your Admin for details.");
            }
        }

        private void editRatesToolStripMenuItem_Click(object sender, EventArgs e) {
            if (_userlvl >= 2) {
                var editor = new POSTableEditor(_userlvl, _cmpID, _brhID);
                editor.Show();
            }
            else {
                MessageBox.Show("Insufficient Access Privileges. Contact your Admin for details.");
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e) {
            if (_userlvl >= 2) {
                var addForm = new POSAddForm(_cmpID, _userlvl, _empID, _brhID);
                addForm.Show();
            }
            else {
                MessageBox.Show("Insufficient Access Privileges. Contact your Admin for details.");
            }
        }

        private void label15_Click(object sender, EventArgs e) {
            if (
                MessageBox.Show(this, "Do you want to log out of the system?", "Logout", MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Question) == DialogResult.OK) {
                Hide();
                _log.Log(l_empno.Text, 'O');
                var login = new POSLogin();
                login.Show();
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e) {
            if (
                MessageBox.Show(this, "Do you want to exit application?", "Quit!", MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Warning) == DialogResult.OK) {
                Application.Exit();
            }
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e) {
            if (
                MessageBox.Show(this, "Do you want to log out of the system?", "Logout", MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Question) == DialogResult.OK) {
                Hide();
                _log.Log(l_empno.Text, 'O');
                var login = new POSLogin();
                login.Show();
            }
        }
    }
}