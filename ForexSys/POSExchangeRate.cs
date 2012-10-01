using System;
using System.Windows.Forms;

namespace ForexSys {
    public partial class POSExchangeRate : Form {
        public POSExchangeRate() {
            InitializeComponent();
        }

        private void POSExchangeRate_Load(object sender, EventArgs e) {
            // TODO: This line of code loads data into the 'fOREXSYSDataSet.ExchangeT' table. You can move, or remove it, as needed.
            exchangeTTableAdapter.Fill(fOREXSYSDataSet.ExchangeT);
            // TODO: This line of code loads data into the 'fOREXSYSDataSet.EXCHANGE' table. You can move, or remove it, as needed.
        }
    }
}