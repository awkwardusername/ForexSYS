using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ForexSys {
    public partial class POSTableViewer : Form {
        private readonly int _branchId;
        private readonly int _cmpId;
        private readonly int _emploId;

        private readonly string[] _items = {
                                              "Customers", "Employees", "Branches", "Companies", "Exchange Rates",
                                              "Transactions"
                                          };

        private readonly int _userlevel;

        public POSTableViewer(int level, int compID, int brhID, int empID) {
            InitializeComponent();
            comboBox1.DataSource = _items;
            _userlevel = level;
            _cmpId = compID;
            _branchId = brhID;
            _emploId = empID;
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e) {
            var dTable = new DataTable();
            var bSource = new BindingSource();

            using (
                var connection =
                    new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=FOREXSYS;Integrated Security=True")) {
                connection.Open();
                var dAdapter = new SqlDataAdapter();
                new SqlCommandBuilder();

                switch (comboBox1.SelectedIndex) {
                    case 0:
                        if (_userlevel >= 2) {
                            dAdapter =
                                new SqlDataAdapter(
                                    "SELECT [CustomerID],[Customer Name],[Complete Address],[Zip],[Email Address],[Contact No.] FROM [FOREXSYS].[dbo].[Customer Details]",
                                    connection);
                        }
                        break;
                    case 1:
                        if (_userlevel >= 4) {
                            dAdapter =
                                new SqlDataAdapter(
                                    "SELECT dbo.EMPLOYEE.EMPNO AS EmployeeID, dbo.EMPLOYEE.EMP_LNAME + ', ' + dbo.EMPLOYEE.EMP_FNAME + ' ' + dbo.EMPLOYEE.EMP_MNAME AS [Employee Name], dbo.EMPLOYEE.EMP_ADDRESS + ', ' + dbo.EMPLOYEE.EMP_CITY AS [Complete Address], dbo.EMPLOYEE.EMP_ZIP AS Zip, dbo.EMPLOYEE.EMP_EMAIL AS [Email Address],dbo.EMPLOYEE.EMP_CNTCTNO AS [Contact No.], dbo.EMPLOYEE.EMP_LEVEL AS [Employee Level], dbo.COMPANY.CMP_NAME AS [Work Company] FROM dbo.BRANCH INNER JOIN dbo.EMPLOYEE ON dbo.BRANCH.BRH_ID = dbo.EMPLOYEE.WORK_BRANCH INNER JOIN dbo.COMPANY ON dbo.BRANCH.HEAD_CMPY = dbo.COMPANY.CMP_ID",
                                    connection);
                        }
                        else if (_userlevel == 3) {
                            dAdapter =
                                new SqlDataAdapter(
                                    "SELECT dbo.EMPLOYEE.EMPNO AS EmployeeID, dbo.EMPLOYEE.EMP_LNAME + ', ' + dbo.EMPLOYEE.EMP_FNAME + ' ' + dbo.EMPLOYEE.EMP_MNAME AS [Employee Name], dbo.EMPLOYEE.EMP_ADDRESS + ', ' + dbo.EMPLOYEE.EMP_CITY AS [Complete Address], dbo.EMPLOYEE.EMP_ZIP AS Zip, dbo.EMPLOYEE.EMP_EMAIL AS [Email Address],dbo.EMPLOYEE.EMP_CNTCTNO AS [Contact No.], dbo.EMPLOYEE.EMP_LEVEL AS [Employee Level], dbo.COMPANY.CMP_NAME AS [Work Company] FROM dbo.BRANCH INNER JOIN dbo.EMPLOYEE ON dbo.BRANCH.BRH_ID = dbo.EMPLOYEE.WORK_BRANCH INNER JOIN dbo.COMPANY ON dbo.BRANCH.HEAD_CMPY = dbo.COMPANY.CMP_ID WHERE dbo.EMPLOYEE.WORK_BRANCH=" +
                                    _cmpId + "", connection);
                        }
                        else if (_userlevel == 2) {
                            dAdapter =
                                new SqlDataAdapter(
                                    "SELECT dbo.EMPLOYEE.EMPNO AS EmployeeID, dbo.EMPLOYEE.EMP_LNAME + ', ' + dbo.EMPLOYEE.EMP_FNAME + ' ' + dbo.EMPLOYEE.EMP_MNAME AS [Employee Name], dbo.EMPLOYEE.EMP_ADDRESS + ', ' + dbo.EMPLOYEE.EMP_CITY AS [Complete Address], dbo.EMPLOYEE.EMP_ZIP AS Zip, dbo.EMPLOYEE.EMP_EMAIL AS [Email Address],dbo.EMPLOYEE.EMP_CNTCTNO AS [Contact No.] FROM dbo.BRANCH INNER JOIN dbo.EMPLOYEE ON dbo.BRANCH.BRH_ID = dbo.EMPLOYEE.WORK_BRANCH INNER JOIN dbo.COMPANY ON dbo.BRANCH.HEAD_CMPY = dbo.COMPANY.CMP_ID WHERE dbo.EMPLOYEE.WORK_BRANCH=" +
                                    _branchId + "", connection);
                        }
                        break;
                    case 2:
                        if (_userlevel == 3) {
                            dAdapter =
                                new SqlDataAdapter(
                                    "SELECT DISTINCT dbo.COMPANY.CMP_ID AS CompanyID, dbo.COMPANY.CMP_NAME AS [Head Company], dbo.BRANCH.BRH_ADDRESS + ', ' + dbo.BRANCH.BRH_CITY AS [Branch Location], dbo.BRANCH.BRH_ZIP AS Zip, dbo.BRANCH.BRH_CONTACT AS [Contact No.], dbo.COMPANY.CMP_EMAIL AS [Company Email], dbo.BRANCH.BRH_ID FROM dbo.BRANCH INNER JOIN dbo.COMPANY ON dbo.BRANCH.HEAD_CMPY = dbo.COMPANY.CMP_ID  WHERE dbo.COMPANY.CMP_ID=" +
                                    _cmpId + " ORDER BY 'Head Company'", connection);
                        }
                        else if (_userlevel >= 4) {
                            dAdapter =
                                new SqlDataAdapter(
                                    "SELECT DISTINCT dbo.COMPANY.CMP_ID AS CompanyID, dbo.COMPANY.CMP_NAME AS [Head Company], dbo.BRANCH.BRH_ADDRESS + ', ' + dbo.BRANCH.BRH_CITY AS [Branch Location], dbo.BRANCH.BRH_ZIP AS Zip, dbo.BRANCH.BRH_CONTACT AS [Contact No.], dbo.COMPANY.CMP_EMAIL AS [Company Email], dbo.BRANCH.BRH_ID FROM dbo.BRANCH INNER JOIN dbo.COMPANY ON dbo.BRANCH.HEAD_CMPY = dbo.COMPANY.CMP_ID  ORDER BY 'Head Company'",
                                    connection);
                        }
                        break;
                    case 3:
                        if (_userlevel >= 3) {
                            dAdapter =
                                new SqlDataAdapter(
                                    "SELECT [CompanyID],[Company Name]      ,[Location],[Zip]      ,[E-Mail],[WebSite],[Manager] FROM [FOREXSYS].[dbo].[Companies]",
                                    connection);
                        }
                        break;
                    case 4:
                        dAdapter =
                            new SqlDataAdapter(
                                "SELECT [Currency Name],[Currency ID],[Current Exchange Rate] FROM [FOREXSYS].[dbo].[Exchange Rates]",
                                connection);
                        break;
                    case 5:
                        if (_userlevel == 1) {
                            dAdapter =
                                new SqlDataAdapter(
                                    "SELECT dbo.[TRANSACTION].TRAN_ID as TransactionID, dbo.[TRANSACTION].TRANSACT_DATE as [Date of Transaction], dbo.CUSTOMER.CUST_LNAME + ', ' + dbo.CUSTOMER.CUST_FNAME + ' ' + dbo.CUSTOMER.CUST_MI as [Customer Name], dbo.[TRANSACTION].TRAN_AMOUNT as [Input Amount], dbo.[TRANSACTION].CURRENCY as [Currency], dbo.[TRANSACTION].CNVRT_AMNT as [Peso Equivalent], dbo.EMPLOYEE.EMP_LNAME +', ' + dbo.EMPLOYEE.EMP_FNAME + ' ' + dbo.EMPLOYEE.EMP_MNAME as [Responsible Teller], dbo.BRANCH.BRH_ADDRESS +', ' + dbo.BRANCH.BRH_CITY as [Branch Location], dbo.COMPANY.CMP_NAME as [Company] FROM dbo.[TRANSACTION] INNER JOIN dbo.EMPLOYEE ON dbo.[TRANSACTION].EMP_TELLER = dbo.EMPLOYEE.EMPNO INNER JOIN dbo.BRANCH ON dbo.EMPLOYEE.WORK_BRANCH = dbo.BRANCH.BRH_ID INNER JOIN dbo.COMPANY ON dbo.BRANCH.HEAD_CMPY = dbo.COMPANY.CMP_ID INNER JOIN dbo.CUSTOMER ON dbo.[TRANSACTION].CUSTOMER = dbo.CUSTOMER.CUST_ID WHERE dbo.EMPLOYEE.EMPNO=" +
                                    _emploId + "", connection);
                        }
                        else if (_userlevel == 2) {
                            dAdapter =
                                new SqlDataAdapter(
                                    "SELECT dbo.[TRANSACTION].TRAN_ID as TransactionID, dbo.[TRANSACTION].TRANSACT_DATE as [Date of Transaction], dbo.CUSTOMER.CUST_LNAME + ', ' + dbo.CUSTOMER.CUST_FNAME + ' ' + dbo.CUSTOMER.CUST_MI as [Customer Name], dbo.[TRANSACTION].TRAN_AMOUNT as [Input Amount], dbo.[TRANSACTION].CURRENCY as [Currency], dbo.[TRANSACTION].CNVRT_AMNT as [Peso Equivalent], dbo.EMPLOYEE.EMP_LNAME +', ' + dbo.EMPLOYEE.EMP_FNAME + ' ' + dbo.EMPLOYEE.EMP_MNAME as [Responsible Teller], dbo.BRANCH.BRH_ADDRESS +', ' + dbo.BRANCH.BRH_CITY as [Branch Location], dbo.COMPANY.CMP_NAME as [Company] FROM dbo.[TRANSACTION] INNER JOIN dbo.EMPLOYEE ON dbo.[TRANSACTION].EMP_TELLER = dbo.EMPLOYEE.EMPNO INNER JOIN dbo.BRANCH ON dbo.EMPLOYEE.WORK_BRANCH = dbo.BRANCH.BRH_ID INNER JOIN dbo.COMPANY ON dbo.BRANCH.HEAD_CMPY = dbo.COMPANY.CMP_ID INNER JOIN dbo.CUSTOMER ON dbo.[TRANSACTION].CUSTOMER = dbo.CUSTOMER.CUST_ID WHERE dbo.BRANCH.BRH_ID=" +
                                    _branchId + "", connection);
                        }
                        else if (_userlevel == 3) {
                            dAdapter =
                                new SqlDataAdapter(
                                    "SELECT dbo.[TRANSACTION].TRAN_ID as TransactionID, dbo.[TRANSACTION].TRANSACT_DATE as [Date of Transaction], dbo.CUSTOMER.CUST_LNAME + ', ' + dbo.CUSTOMER.CUST_FNAME + ' ' + dbo.CUSTOMER.CUST_MI as [Customer Name], dbo.[TRANSACTION].TRAN_AMOUNT as [Input Amount], dbo.[TRANSACTION].CURRENCY as [Currency], dbo.[TRANSACTION].CNVRT_AMNT as [Peso Equivalent], dbo.EMPLOYEE.EMP_LNAME +', ' + dbo.EMPLOYEE.EMP_FNAME + ' ' + dbo.EMPLOYEE.EMP_MNAME as [Responsible Teller], dbo.BRANCH.BRH_ADDRESS +', ' + dbo.BRANCH.BRH_CITY as [Branch Location], dbo.COMPANY.CMP_NAME as [Company] FROM dbo.[TRANSACTION] INNER JOIN dbo.EMPLOYEE ON dbo.[TRANSACTION].EMP_TELLER = dbo.EMPLOYEE.EMPNO INNER JOIN dbo.BRANCH ON dbo.EMPLOYEE.WORK_BRANCH = dbo.BRANCH.BRH_ID INNER JOIN dbo.COMPANY ON dbo.BRANCH.HEAD_CMPY = dbo.COMPANY.CMP_ID INNER JOIN dbo.CUSTOMER ON dbo.[TRANSACTION].CUSTOMER = dbo.CUSTOMER.CUST_ID WHERE dbo.COMPANY.CMP_ID=" +
                                    _cmpId + "", connection);
                        }
                        else if (_userlevel >= 4) {
                            dAdapter =
                                new SqlDataAdapter(
                                    "SELECT dbo.[TRANSACTION].TRAN_ID as TransactionID, dbo.[TRANSACTION].TRANSACT_DATE as [Date of Transaction], dbo.CUSTOMER.CUST_LNAME + ', ' + dbo.CUSTOMER.CUST_FNAME + ' ' + dbo.CUSTOMER.CUST_MI as [Customer Name], dbo.[TRANSACTION].TRAN_AMOUNT as [Input Amount], dbo.[TRANSACTION].CURRENCY as [Currency], dbo.[TRANSACTION].CNVRT_AMNT as [Peso Equivalent], dbo.EMPLOYEE.EMP_LNAME +', ' + dbo.EMPLOYEE.EMP_FNAME + ' ' + dbo.EMPLOYEE.EMP_MNAME as [Responsible Teller], dbo.BRANCH.BRH_ADDRESS +', ' + dbo.BRANCH.BRH_CITY as [Branch Location], dbo.COMPANY.CMP_NAME as [Company] FROM dbo.[TRANSACTION] INNER JOIN dbo.EMPLOYEE ON dbo.[TRANSACTION].EMP_TELLER = dbo.EMPLOYEE.EMPNO INNER JOIN dbo.BRANCH ON dbo.EMPLOYEE.WORK_BRANCH = dbo.BRANCH.BRH_ID INNER JOIN dbo.COMPANY ON dbo.BRANCH.HEAD_CMPY = dbo.COMPANY.CMP_ID INNER JOIN dbo.CUSTOMER ON dbo.[TRANSACTION].CUSTOMER = dbo.CUSTOMER.CUST_ID",
                                    connection);
                        }
                        break;
                    default:
                        break;
                }

                try {
                    new SqlCommandBuilder(dAdapter);
                    dAdapter.Fill(dTable);
                    bSource.DataSource = dTable;
                    dataGridView1.DataSource = bSource;
                }
                catch {
                    MessageBox.Show("Insufficient Privileges to show Table. Contact your Admin for details");
                }
            }
        }
    }
}