namespace NEO_test_tools
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtAddr = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.txtSplit = new System.Windows.Forms.TextBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.txtPublicKey = new System.Windows.Forms.TextBox();
            this.txtSign = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtOldUTXO = new System.Windows.Forms.TextBox();
            this.listoldUTXOs = new System.Windows.Forms.ListBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(817, 489);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtAddr);
            this.tabPage1.Controls.Add(this.comboBox1);
            this.tabPage1.Controls.Add(this.txtSplit);
            this.tabPage1.Controls.Add(this.txtAmount);
            this.tabPage1.Controls.Add(this.txtResult);
            this.tabPage1.Controls.Add(this.txtPublicKey);
            this.tabPage1.Controls.Add(this.txtSign);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.txtOldUTXO);
            this.tabPage1.Controls.Add(this.listoldUTXOs);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(809, 463);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "UTXO粉碎机";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtAddr
            // 
            this.txtAddr.Location = new System.Drawing.Point(146, 16);
            this.txtAddr.Name = "txtAddr";
            this.txtAddr.Size = new System.Drawing.Size(570, 21);
            this.txtAddr.TabIndex = 19;
            this.txtAddr.Text = "输入地址";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "testnet",
            "mainnet"});
            this.comboBox1.Location = new System.Drawing.Point(19, 16);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 18;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // txtSplit
            // 
            this.txtSplit.Location = new System.Drawing.Point(138, 224);
            this.txtSplit.Name = "txtSplit";
            this.txtSplit.Size = new System.Drawing.Size(100, 21);
            this.txtSplit.TabIndex = 17;
            this.txtSplit.Text = "100";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(19, 224);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(100, 21);
            this.txtAmount.TabIndex = 16;
            this.txtAmount.Text = "1";
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(19, 340);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(697, 48);
            this.txtResult.TabIndex = 15;
            // 
            // txtPublicKey
            // 
            this.txtPublicKey.Location = new System.Drawing.Point(19, 296);
            this.txtPublicKey.Multiline = true;
            this.txtPublicKey.Name = "txtPublicKey";
            this.txtPublicKey.Size = new System.Drawing.Size(697, 38);
            this.txtPublicKey.TabIndex = 14;
            this.txtPublicKey.Text = "填入公钥HEX";
            // 
            // txtSign
            // 
            this.txtSign.Location = new System.Drawing.Point(19, 252);
            this.txtSign.Multiline = true;
            this.txtSign.Name = "txtSign";
            this.txtSign.Size = new System.Drawing.Size(697, 38);
            this.txtSign.TabIndex = 13;
            this.txtSign.Text = "填入私钥HEX";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(19, 412);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "粉碎";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtOldUTXO
            // 
            this.txtOldUTXO.Location = new System.Drawing.Point(19, 122);
            this.txtOldUTXO.Multiline = true;
            this.txtOldUTXO.Name = "txtOldUTXO";
            this.txtOldUTXO.Size = new System.Drawing.Size(697, 96);
            this.txtOldUTXO.TabIndex = 11;
            // 
            // listoldUTXOs
            // 
            this.listoldUTXOs.FormattingEnabled = true;
            this.listoldUTXOs.ItemHeight = 12;
            this.listoldUTXOs.Location = new System.Drawing.Point(19, 40);
            this.listoldUTXOs.Name = "listoldUTXOs";
            this.listoldUTXOs.Size = new System.Drawing.Size(697, 76);
            this.listoldUTXOs.TabIndex = 10;
            this.listoldUTXOs.SelectedIndexChanged += new System.EventHandler(this.listoldUTXOs_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 530);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "NEO调试工具箱";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtAddr;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox txtSplit;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.TextBox txtPublicKey;
        private System.Windows.Forms.TextBox txtSign;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtOldUTXO;
        private System.Windows.Forms.ListBox listoldUTXOs;
    }
}

