using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ForexSys {
	public partial class POSConnect : Form {
		public POSConnect() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {
			Hide();
			using(var connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=FOREXSYS;Integrated Security=True")) {
				try {
					connection.Open();
					StatusMe.statusMe = "Connected!!";
				} catch(SqlException err) {
					StatusMe.statusMe = "Not Connected!!";
				}

			}
		}
	}
}
