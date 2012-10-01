using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ForexSys {
	public class StatusMe {
		private static string _statusMe;

		public static string statusMe {
			get { return _statusMe; }
			set { _statusMe = value; }
		}

		public static void pingServer() {
			using(
				var connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=FOREXSYS;Integrated Security=True")) {
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
