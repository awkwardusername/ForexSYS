using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;

namespace ForexSys {

	public partial class POSLogin : Form {

		public POSLogin() {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e) {
            textBox_Username.Clear();
            textBox_Password.Clear();
        }

        private void button1_Click(object sender, EventArgs e) {
	        //this.Dispose();
	        const string SQLCOMM = "SELECT EMPLOYEE.EMPNO, EMPLOYEE.EMP_LNAME+', '+EMPLOYEE.EMP_FNAME+ ' '+ " +
	                               "EMPLOYEE.EMP_MNAME, EMPLOYEE.EMP_LEVEL, COMPANY.CMP_NAME, COMPANY.CMP_WEBSITE," +
	                               "COMPANY.CMP_ID, EMPLOYEE.WORK_BRANCH, BRANCH.BRH_ADDRESS + ', ' + BRANCH.BRH_CITY " +
	                               "FROM EMPLOYEE INNER JOIN " +
	                               "BRANCH ON EMPLOYEE.WORK_BRANCH = BRANCH.BRH_ID INNER JOIN " +
	                               "COMPANY ON BRANCH.HEAD_CMPY = COMPANY.CMP_ID " +
	                               "WHERE USERNAME=@uName AND PASSWORD=@pWord";
	        using (
                var connection =
                    new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=FOREXSYS;Integrated Security=True")) {
                connection.Open();
                using (var login = new SqlCommand(SQLCOMM, connection)) {
                    login.Parameters.Add(new SqlParameter("uName", textBox_Username.Text));
                    login.Parameters.Add(new SqlParameter("pWord", textBox_Password.Text));

                    SqlDataReader reader = login.ExecuteReader();
                    if (reader.HasRows) {
                        var posform = new POSForm(reader);

                        posform.Show();
                        Hide();
                    }
                    else {
                        MessageBox.Show("Invalid Username/Password!");
                    }
                }
            }
        }

		private void POSLogin_Load(object sender, EventArgs e) {
			StatusMe.pingServer();
			Status.Text = StatusMe.statusMe;
		}

		

		private void toolStripStatusLabel1_Click(object sender, EventArgs e) {
			POSConnect connect = new POSConnect();
			connect.Show();
		}
	}
}