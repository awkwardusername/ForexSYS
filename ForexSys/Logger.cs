using System.Data.SqlClient;
using System.Windows.Forms;

namespace ForexSys {
    internal class Logger {
        public void Log(string empID, char x) {
            using (
                var conn =
                    new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=FOREXSYS;Integrated Security=True")) {
                conn.Open();
                using (
                    var sql =
                        new SqlCommand(
                            "INSERT INTO [FOREXSYS].[dbo].[LOG] ([LOG_DATE],[EMP_ID],[TRANSACT_TYPR]) VALUES (GETDATE(), @empID, @chara)",
                            conn)) {
                    sql.Parameters.AddWithValue("@empID", empID);
                    sql.Parameters.AddWithValue("@chara", x);
                    sql.ExecuteNonQuery();
                }
            }
        }

        public void Log(string empID, string transID) {
            using (
                var conn =
                    new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=FOREXSYS;Integrated Security=True")) {
                conn.Open();
                using (
                    var sql =
                        new SqlCommand(
                            "INSERT INTO [FOREXSYS].[dbo].[LOG] ([LOG_DATE],[EMP_ID],[TRANSACT_ID],[TRANSACT_TYPR]) VALUES (GETDATE(), @empID, @transID, 'T')",
                            conn)) {
                    sql.Parameters.AddWithValue("@empID", empID);
                    sql.Parameters.AddWithValue("@transID", transID);
                    sql.ExecuteNonQuery();
                }
            }
        }

        public void ClearForm(Control parent) {
            foreach (Control ctrControl in parent.Controls) {
                //Loop through all controls
                if (ReferenceEquals(ctrControl.GetType(), typeof (TextBox))) {
                    //Check to see if it's a textbox
                    (ctrControl).Text = string.Empty;
                    //If it is then set the text to String.Empty (empty textbox)
                }
                else if (ReferenceEquals(ctrControl.GetType(), typeof (RichTextBox))) {
                    //If its a RichTextBox clear the text
                    (ctrControl).Text = string.Empty;
                }
                else if (ReferenceEquals(ctrControl.GetType(), typeof (ComboBox))) {
                    //Next check if it's a dropdown list
                    ((ComboBox) ctrControl).SelectedIndex = -1;
                    //If it is then set its SelectedIndex to 0
                }
                else if (ReferenceEquals(ctrControl.GetType(), typeof (CheckBox))) {
                    //Next uncheck all checkboxes
                    ((CheckBox) ctrControl).Checked = false;
                }
                else if (ReferenceEquals(ctrControl.GetType(), typeof (RadioButton))) {
                    //Unselect all RadioButtons
                    ((RadioButton) ctrControl).Checked = false;
                }
                if (ctrControl.Controls.Count > 0) {
                    //Call itself to get all other controls in other containers
                    ClearForm(ctrControl);
                }
            }
        }
    }
}