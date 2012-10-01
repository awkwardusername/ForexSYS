using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ForexSys {
    public partial class POSTableEditor : Form {
        private readonly int brhID;
        private readonly int cmpID;

        private readonly SqlConnection connection =
            new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=FOREXSYS;Integrated Security=True");

        private readonly string[] _items = {"Exchange Rate", "Employees", "Branch", "Company", "Customer"};

        private readonly int _userlevel;
        private BindingSource _bSource;
        private SqlCommandBuilder _cBuilder;
        private SqlDataAdapter _dAdapter;
        private DataTable _dTable;

        public POSTableEditor(int level, int compID, int branchID) {
            InitializeComponent();
            comboBox1.DataSource = _items;
            _userlevel = level;
            cmpID = compID;
            brhID = branchID;
        }

	    public SqlCommandBuilder cBuilder {
		    get { return _cBuilder; }
		    set { _cBuilder = value; }
	    }

	    private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e) {
            if (connection.State == ConnectionState.Closed) {
                connection.Open();
            }

            _bSource = new BindingSource();
            _dTable = new DataTable();
            _dAdapter = new SqlDataAdapter();
            cBuilder = new SqlCommandBuilder();

            switch (comboBox1.SelectedIndex) {
                case 0:
                    if (_userlevel >= 3) {
                        _dAdapter =
                            new SqlDataAdapter(
                                "SELECT [Currency ID],[Exchange Rate],[Currency Description] FROM [FOREXSYS].[dbo].[ExchangeT]",
                                connection);
                    }
                    break;
                case 1:
                    if (_userlevel >= 3) {
                        _dAdapter =
                            new SqlDataAdapter(
                                "SELECT  dbo.EMPLOYEE.EMP_LNAME AS Lastname, dbo.EMPLOYEE.EMP_FNAME AS Firstname, dbo.EMPLOYEE.EMP_MNAME AS [Middle Initial], dbo.EMPLOYEE.EMP_ADDRESS AS [Street Address], dbo.EMPLOYEE.EMP_CITY AS City, dbo.EMPLOYEE.EMP_ZIP AS Zip, dbo.EMPLOYEE.EMP_EMAIL AS [Email Address], dbo.EMPLOYEE.EMP_CNTCTNO AS [Contact No.] FROM         dbo.EMPLOYEE INNER JOIN  dbo.BRANCH ON dbo.EMPLOYEE.WORK_BRANCH = dbo.BRANCH.BRH_ID INNER JOIN dbo.COMPANY ON dbo.BRANCH.HEAD_CMPY = dbo.COMPANY.CMP_ID where dbo.COMPANY.CMP_ID=" +
                                cmpID + "", connection);
                    }
                    else if (_userlevel == 2) {
                        _dAdapter =
                            new SqlDataAdapter(
                                "SELECT dbo.EMPLOYEE.EMP_LNAME AS Lastname, dbo.EMPLOYEE.EMP_FNAME as Firstname, dbo.EMPLOYEE.EMP_MNAME as [Middle Initial], dbo.EMPLOYEE.EMP_ADDRESS as Address, dbo.EMPLOYEE.EMP_CITY AS City, dbo.EMPLOYEE.EMP_ZIP AS Zip, dbo.EMPLOYEE.EMP_EMAIL AS [Email Address],dbo.EMPLOYEE.EMP_CNTCTNO AS [Contact No.]FROM [FOREXSYS].[dbo].[EMPLOYEE] WHERE dbo.EMPLOYEE.WORK_BRANCH=" +
                                brhID + "", connection);
                    }
                    break;
                case 2:
                    if (_userlevel >= 3) {
                        _dAdapter =
                            new SqlDataAdapter(
                                "SELECT [BranchID],[Address],[City],[Zip],[CompanyID],[Contact No.] FROM [FOREXSYS].[dbo].[BranchT] where [CompanyID]=" +
                                cmpID, connection);
                    }
                    break;
                case 3:
                    if (_userlevel >= 4) {
                        _dAdapter =
                            new SqlDataAdapter(
                                "SELECT     CMP_ID AS CompanyID, CMP_NAME AS [Company Name], CMP_ADDRESS AS Address, CMP_CITY AS City, CMP_ZIP AS Zip, CMP_EMAIL AS [E-Mail],CMP_WEBSITE AS WebSite FROM         dbo.COMPANY",
                                connection);
                    }

                    break;
                case 4:
                    _dAdapter =
                        new SqlDataAdapter(
                            "SELECT [CustomerID],[Firstname],[Lastname],[MI],[Address],[City],[EMail],[Contact No.],[Zip] FROM [FOREXSYS].[dbo].[CustomerT]",
                            connection);
                    break;
                default:
                    break;
            }
            try {
                _dAdapter.Fill(_dTable);
                _bSource.DataSource = _dTable;
                dataGridView1.DataSource = _bSource;
            }
            catch {
                MessageBox.Show("Insufficient Privileges to edit Table. Contact your Admin for details");
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            if (connection.State == ConnectionState.Open) {
                if (
                    MessageBox.Show(this, "Do you want to commit changes to database?", "Commit to Database",
                                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) {
                    cBuilder = new SqlCommandBuilder(_dAdapter);
                    _dAdapter.Update(_dTable);
                    connection.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            if (connection.State == ConnectionState.Open) {
                connection.Close();
            }
            Hide();
        }
    }
}