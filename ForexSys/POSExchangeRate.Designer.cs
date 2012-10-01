namespace ForexSys {
    partial class POSExchangeRate {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if ( disposing && ( components != null ) ) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.fOREXSYSDataSet = new ForexSys.FOREXSYSDataSet();
            this.exchangeTBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.exchangeTTableAdapter = new ForexSys.FOREXSYSDataSetTableAdapters.ExchangeTTableAdapter();
            this.currencyIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.exchangeRateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.currencyDescriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fOREXSYSDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exchangeTBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Segoe UI Light", 14F);
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(265, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 25);
            this.label3.TabIndex = 12;
            this.label3.Text = "Exchange Rates";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Image = global::ForexSys.Properties.Resources._550px_greenline;
            this.label2.Location = new System.Drawing.Point(-18, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(441, 10);
            this.label2.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Impact", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gainsboro;
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(272, 80);
            this.label1.TabIndex = 10;
            this.label1.Text = "ForexSYS";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.currencyIDDataGridViewTextBoxColumn,
            this.exchangeRateDataGridViewTextBoxColumn,
            this.currencyDescriptionDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.exchangeTBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 109);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(389, 218);
            this.dataGridView1.TabIndex = 13;
            // 
            // fOREXSYSDataSet
            // 
            this.fOREXSYSDataSet.DataSetName = "FOREXSYSDataSet";
            this.fOREXSYSDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // exchangeTBindingSource
            // 
            this.exchangeTBindingSource.DataMember = "ExchangeT";
            this.exchangeTBindingSource.DataSource = this.fOREXSYSDataSet;
            // 
            // exchangeTTableAdapter
            // 
            this.exchangeTTableAdapter.ClearBeforeFill = true;
            // 
            // currencyIDDataGridViewTextBoxColumn
            // 
            this.currencyIDDataGridViewTextBoxColumn.DataPropertyName = "Currency ID";
            this.currencyIDDataGridViewTextBoxColumn.HeaderText = "Currency ID";
            this.currencyIDDataGridViewTextBoxColumn.Name = "currencyIDDataGridViewTextBoxColumn";
            // 
            // exchangeRateDataGridViewTextBoxColumn
            // 
            this.exchangeRateDataGridViewTextBoxColumn.DataPropertyName = "Exchange Rate";
            this.exchangeRateDataGridViewTextBoxColumn.HeaderText = "Exchange Rate";
            this.exchangeRateDataGridViewTextBoxColumn.Name = "exchangeRateDataGridViewTextBoxColumn";
            // 
            // currencyDescriptionDataGridViewTextBoxColumn
            // 
            this.currencyDescriptionDataGridViewTextBoxColumn.DataPropertyName = "Currency Description";
            this.currencyDescriptionDataGridViewTextBoxColumn.HeaderText = "Currency Description";
            this.currencyDescriptionDataGridViewTextBoxColumn.Name = "currencyDescriptionDataGridViewTextBoxColumn";
            this.currencyDescriptionDataGridViewTextBoxColumn.Width = 300;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 334);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(389, 30);
            this.button1.TabIndex = 14;
            this.button1.Text = "Commit Changes";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // POSExchangeRate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 376);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "POSExchangeRate";
            this.Text = "POSExchangeRate";
            this.Load += new System.EventHandler(this.POSExchangeRate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fOREXSYSDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exchangeTBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private FOREXSYSDataSet fOREXSYSDataSet;
        private System.Windows.Forms.BindingSource exchangeTBindingSource;
        private FOREXSYSDataSetTableAdapters.ExchangeTTableAdapter exchangeTTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn currencyIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn exchangeRateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn currencyDescriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button button1;
    }
}