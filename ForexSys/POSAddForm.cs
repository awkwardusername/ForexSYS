using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ForexSys {
    public partial class POSAddForm : Form {
        private readonly int _compyID;
        private readonly List<int> _level2 = new List<int>();
        private readonly List<int> _level3 = new List<int>();
        private readonly List<int> _level4 = new List<int>();
        private readonly Logger _log = new Logger();

        private readonly int _userlevel;
	    private List<string> _branchLoc;
        private List<int> _branchVal;
        private int _branchValint2;
	    private List<int> _managerID;
        private List<string> _managerName;

        public POSAddForm(int company, int level, int empID, int brhID) {
            InitializeComponent();
            _level2.Add(1);
            _level3.Add(1);
            _level3.Add(2);
            _level4.Add(1);
            _level4.Add(2);
            _level4.Add(3);
            _level4.Add(4);
            _compyID = company;
	        _userlevel = level;
            initManager();
        }

        private void initManager() {
            _managerID = new List<int>();
            _managerName = new List<string>();
            using (
                var conn =
                    new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=FOREXSYS;Integrated Security=True")) {
                conn.Open();
                using (
                    var comm = new SqlCommand("SELECT [Manager],[EmpID] FROM [FOREXSYS].[dbo].[EmployeesNoCompany]",
                                              conn)) {
                    SqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read()) {
                        _managerName.Add(reader.GetString(0));
                        _managerID.Add(reader.GetInt32(1));
                    }
                }
            }
            coCB_manager.DataSource = _managerName;

            switch (_userlevel) {
                case 2:
                    eCB_Level.DataSource = _level2;
                    break;
                case 3:
                    eCB_Level.DataSource = _level3;
                    break;
                case 4:
                    eCB_Level.DataSource = _level4;
                    break;
                default:
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            using (
                var connect =
                    new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=FOREXSYS;Integrated Security=True")) {
                string dbTable = "";
                string dbColumn = "";
                string dbValues = "";
                bool flag = false;
                connect.Open();

                switch (tabControl1.SelectedIndex) {
                    case 0:
                        dbColumn =
                            "[CUST_FNAME],[CUST_LNAME],[CUST_MI],[CUST_ADDRESS],[CUST_CITY],[CUST_EMAIL],[CUST_CONTACT],[CUST_ZIP]";
                        dbTable = "[FOREXSYS].[dbo].[CUSTOMER]";
                        dbValues =
                            "@custFname, @custLname, @custMI, @custAddress, @custCity, @custEmail, @custContact, @custZip";
                        break;
                    case 1:
                        if (_userlevel >= 3) {
                            dbColumn = "[BRH_ADDRESS],[BRH_CITY],[BRH_ZIP],[HEAD_CMPY],[BRH_CONTACT]";
                            dbTable = "[FOREXSYS].[dbo].[BRANCH]";
                            dbValues = "@brhAddress, @brhCity, @brhZip, @headCmpy, @brhContact";
                        }
                        break;
                    case 2:
                        if (_userlevel >= 4) {
                            flag = true;
                        }

                        break;
                    case 3:
                        if (_userlevel == 3) {
                            dbColumn =
                                "[EMP_FNAME],[EMP_LNAME],[EMP_MNAME],[EMP_ADDRESS],[EMP_CITY],[EMP_ZIP],[EMP_EMAIL],[EMP_CNTCTNO],[USERNAME],[PASSWORD],[EMP_LEVEL],[WORK_BRANCH]";
                            dbTable = "[FOREXSYS].[dbo].[EMPLOYEE]";
                            dbValues =
                                "@empFname, @empLname, @empMI, @empAddress, @empCity, @empZip, @empEmail, @empContact, @username, @password, @empLevel, @workbranch";
                        }
                        break;
                    default:
                        break;
                }
                string queryBuilder = "INSERT INTO " + dbTable + " (" + dbColumn + ") VALUES (" + dbValues + ")";
                using (var sqlexec = new SqlCommand(queryBuilder, connect)) {
                    switch (tabControl1.SelectedIndex) {
                        case 0:
                            //dbValues = "(@custFname, @custLname, @custMI, @custAddress, @custCity, @custEmail, @custContact, @custZip)";
                            sqlexec.Parameters.AddWithValue("@custFname", CtextBox_custFname.Text);
                            sqlexec.Parameters.AddWithValue("@custLname", CtextBox_custLname.Text);
                            sqlexec.Parameters.AddWithValue("@custMI", CtextBox_custMI.Text);
                            sqlexec.Parameters.AddWithValue("@custAddress", CtextBox_Address.Text);
                            sqlexec.Parameters.AddWithValue("@custCity", CtextBox_City.Text);
                            sqlexec.Parameters.AddWithValue("@custEmail", CtextBox_email.Text);
                            sqlexec.Parameters.AddWithValue("@custContact", CtextBox_contact.Text);
                            sqlexec.Parameters.AddWithValue("@custZip", CtextBox_Zip.Text);
                            break;
                        case 1: //dbValues = "@brhAddress, @brhCity, @brhZip, @headCmpy, @brhContact
                            sqlexec.Parameters.AddWithValue("@brhAddress", BtextBox_Address.Text);
                            sqlexec.Parameters.AddWithValue("@brhCity", BtextBox_City.Text);
                            sqlexec.Parameters.AddWithValue("@brhZip", BtextBox_Zip.Text);
                            sqlexec.Parameters.AddWithValue("@headCmpy", _compyID);
                            sqlexec.Parameters.AddWithValue("@brhContact", BtextBox_contact.Text);
                            break;
                        case 3:
                            //dbValues = "@empFname, @empLname, @empMI, @empAddress, @empCity, @empZip, @empEmail, @empContact, @username, @password, @empLevel, @workbranch";
                            sqlexec.Parameters.AddWithValue("@empFname", eTB_Fname.Text);
                            sqlexec.Parameters.AddWithValue("@empLname", eTB_Lname.Text);
                            sqlexec.Parameters.AddWithValue("@empMI", eTB_MI.Text);
                            sqlexec.Parameters.AddWithValue("@empAddress", eTB_Address.Text);
                            sqlexec.Parameters.AddWithValue("@empCity", eTB_City.Text);
                            sqlexec.Parameters.AddWithValue("@empZip", eTB_Zip.Text);
                            sqlexec.Parameters.AddWithValue("@empEmail", eTB_mail.Text);
                            sqlexec.Parameters.AddWithValue("@empContact", eTB_contact.Text);
                            sqlexec.Parameters.AddWithValue("@username", eTB_username.Text);
                            sqlexec.Parameters.AddWithValue("@password", eTB_Password.Text);
                            sqlexec.Parameters.AddWithValue("@empLevel", eCB_Level.SelectedText);
                            sqlexec.Parameters.AddWithValue("@workbranch", _branchValint2);
                            break;
                        default:
                            break;
                    }
                    if (!flag) {
                        try {
                            sqlexec.ExecuteNonQuery();
                        }
                        catch {
                            MessageBox.Show("Insufficient Access Privileges. Contact your Admin for details.");
                        }
                    }
                }
                if (flag) {
	                const string sqlCompany = "BEGIN TRANSACTION; " +
	                                          "DECLARE @cmpID int; " +
	                                          "DECLARE @brhID int; " +
	                                          "INSERT INTO [FOREXSYS].[dbo].[COMPANY] ([CMP_NAME],[CMP_ADDRESS],[CMP_CITY],[CMP_ZIP],[CMP_EMAIL],[CMP_WEBSITE],[CMP_CONTACT]) " +
	                                          "VALUES (@cmpName, @cmpAddress, @cmpCity, @cmpZip, @cmpEmail, @cmpWebsite, @cmpContact); " +
	                                          "SELECT @cmpID = scope_identity(); " +
	                                          "INSERT INTO [FOREXSYS].[dbo].[COMPANY_MANAGER] VALUES (@cmpID, @mgrID); " +
	                                          "INSERT INTO [FOREXSYS].[dbo].[BRANCH] ([BRH_ADDRESS],[BRH_CITY],[BRH_ZIP],[HEAD_CMPY]) VALUES (@cmpAddress, @cmpCity, @cmpZip, @cmpID);" +
	                                          "SELECT @brhID = scope_identity();" +
	                                          "UPDATE [FOREXSYS].[dbo].[EMPLOYEE] SET [EMP_LEVEL]=3, [WORK_BRANCH]=@brhID WHERE [EMPNO]=@mgrID;" +
	                                          "COMMIT; ";
	                //[BRH_ID],[BRH_ADDRESS],[BRH_CITY],[BRH_ZIP],[HEAD_CMPY],[BRH_CONTACT]
                    using (var sql2 = new SqlCommand(sqlCompany, connect)) {
                        sql2.Parameters.AddWithValue("@mgrID", _managerID[_managerName.IndexOf(coCB_manager.Text)]);
                        sql2.Parameters.AddWithValue("@cmpName", coTB_name.Text);
                        sql2.Parameters.AddWithValue("@cmpAddress", coTB_Address.Text);
                        sql2.Parameters.AddWithValue("@cmpCity", coTB_city.Text);
                        sql2.Parameters.AddWithValue("@cmpZip", coTB_zip.Text);
                        sql2.Parameters.AddWithValue("@cmpEmail", coTB_email.Text);
                        sql2.Parameters.AddWithValue("@cmpWebsite", coTB_Website.Text);
                        sql2.Parameters.AddWithValue("@cmpContact", coTB_contact.Text);
                        sql2.ExecuteNonQuery();
                    }
                }
            }
            MessageBox.Show("Record Committed to Database");
        }

        private void POSAddForm_Load(object sender, EventArgs e) {}

        private void tabControl1_Selected(object sender, TabControlEventArgs e) {
            _branchLoc = new List<string>();
            _branchVal = new List<int>();
            if (tabControl1.SelectedIndex == 3) {
                using (
                    var conn =
                        new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=FOREXSYS;Integrated Security=True")) {
                    conn.Open();
                    using (
                        var command =
                            new SqlCommand(
                                "SELECT [Branch Location],[BRH_ID] FROM [FOREXSYS].[dbo].[Company Branches] where [CompanyID]=@compyID",
                                conn)) {
                        command.Parameters.AddWithValue("@compyID", _compyID);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows) {
                            while (reader.Read()) {
                                _branchVal.Add(reader.GetInt32(1));
                                _branchLoc.Add(reader.GetString(0));
                            }
                        }
                        else {
                            MessageBox.Show("Invalid Operation!");
                        }
                        eCB_Work.DataSource = _branchLoc;
                    }
                }
            }
        }

        private void comboBox_Work_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                _branchValint2 = _branchVal[eCB_Work.SelectedIndex];
            }
            catch {
                // do nothing
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            _log.ClearForm(this);
        }
    }
}