namespace HNFactoryAutoSystem
{
    partial class ServerMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnClosePLCServer = new System.Windows.Forms.Button();
            this.txt_Log = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Port = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_IP = new System.Windows.Forms.TextBox();
            this.btnStartPLCServer = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dvAssLine = new System.Windows.Forms.DataGridView();
            this.AssemblyLineId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FactoryTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProcessId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProcessTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AssemblyLineTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AssStart = new System.Windows.Forms.DataGridViewButtonColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvAssLine)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btnClosePLCServer);
            this.groupBox1.Controls.Add(this.txt_Log);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_Port);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_IP);
            this.groupBox1.Controls.Add(this.btnStartPLCServer);
            this.groupBox1.Location = new System.Drawing.Point(53, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1496, 188);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PLC数据交换服务";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(670, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnClosePLCServer
            // 
            this.btnClosePLCServer.Location = new System.Drawing.Point(456, 117);
            this.btnClosePLCServer.Name = "btnClosePLCServer";
            this.btnClosePLCServer.Size = new System.Drawing.Size(150, 45);
            this.btnClosePLCServer.TabIndex = 6;
            this.btnClosePLCServer.Text = "关闭服务";
            this.btnClosePLCServer.UseVisualStyleBackColor = true;
            this.btnClosePLCServer.Click += new System.EventHandler(this.btnClosePLCServer_Click);
            // 
            // txt_Log
            // 
            this.txt_Log.Location = new System.Drawing.Point(812, 30);
            this.txt_Log.Multiline = true;
            this.txt_Log.Name = "txt_Log";
            this.txt_Log.Size = new System.Drawing.Size(647, 135);
            this.txt_Log.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "端口号";
            // 
            // txt_Port
            // 
            this.txt_Port.Location = new System.Drawing.Point(125, 117);
            this.txt_Port.Name = "txt_Port";
            this.txt_Port.ReadOnly = true;
            this.txt_Port.Size = new System.Drawing.Size(257, 31);
            this.txt_Port.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "IP地址";
            // 
            // txt_IP
            // 
            this.txt_IP.Location = new System.Drawing.Point(125, 58);
            this.txt_IP.Name = "txt_IP";
            this.txt_IP.ReadOnly = true;
            this.txt_IP.Size = new System.Drawing.Size(257, 31);
            this.txt_IP.TabIndex = 1;
            // 
            // btnStartPLCServer
            // 
            this.btnStartPLCServer.Location = new System.Drawing.Point(456, 50);
            this.btnStartPLCServer.Name = "btnStartPLCServer";
            this.btnStartPLCServer.Size = new System.Drawing.Size(150, 43);
            this.btnStartPLCServer.TabIndex = 0;
            this.btnStartPLCServer.Text = "启动服务";
            this.btnStartPLCServer.UseVisualStyleBackColor = true;
            this.btnStartPLCServer.Click += new System.EventHandler(this.btnStartPLCServer_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dvAssLine);
            this.groupBox2.Location = new System.Drawing.Point(53, 258);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1496, 475);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "工艺生产控制";
            // 
            // dvAssLine
            // 
            this.dvAssLine.AllowUserToAddRows = false;
            this.dvAssLine.AllowUserToDeleteRows = false;
            this.dvAssLine.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvAssLine.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AssemblyLineId,
            this.FactoryTitle,
            this.ProcessId,
            this.ProcessTitle,
            this.AssemblyLineTitle,
            this.CurrentStatus,
            this.AssStart});
            this.dvAssLine.Location = new System.Drawing.Point(25, 45);
            this.dvAssLine.Name = "dvAssLine";
            this.dvAssLine.RowHeadersWidth = 72;
            this.dvAssLine.RowTemplate.Height = 33;
            this.dvAssLine.Size = new System.Drawing.Size(1434, 307);
            this.dvAssLine.TabIndex = 0;
            this.dvAssLine.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dvAssLine_CellContentClick);
            // 
            // AssemblyLineId
            // 
            this.AssemblyLineId.HeaderText = "生产线编号";
            this.AssemblyLineId.MinimumWidth = 9;
            this.AssemblyLineId.Name = "AssemblyLineId";
            this.AssemblyLineId.ReadOnly = true;
            this.AssemblyLineId.Width = 175;
            // 
            // FactoryTitle
            // 
            this.FactoryTitle.HeaderText = "所在工厂";
            this.FactoryTitle.MinimumWidth = 9;
            this.FactoryTitle.Name = "FactoryTitle";
            this.FactoryTitle.ReadOnly = true;
            this.FactoryTitle.Width = 175;
            // 
            // ProcessId
            // 
            this.ProcessId.HeaderText = "工艺流程编号";
            this.ProcessId.MinimumWidth = 9;
            this.ProcessId.Name = "ProcessId";
            this.ProcessId.ReadOnly = true;
            this.ProcessId.Width = 175;
            // 
            // ProcessTitle
            // 
            this.ProcessTitle.HeaderText = "工艺流程名称";
            this.ProcessTitle.MinimumWidth = 9;
            this.ProcessTitle.Name = "ProcessTitle";
            this.ProcessTitle.ReadOnly = true;
            this.ProcessTitle.Width = 175;
            // 
            // AssemblyLineTitle
            // 
            this.AssemblyLineTitle.HeaderText = "生产线名称";
            this.AssemblyLineTitle.MinimumWidth = 9;
            this.AssemblyLineTitle.Name = "AssemblyLineTitle";
            this.AssemblyLineTitle.ReadOnly = true;
            this.AssemblyLineTitle.Width = 175;
            // 
            // CurrentStatus
            // 
            this.CurrentStatus.HeaderText = "当前生产状态";
            this.CurrentStatus.MinimumWidth = 9;
            this.CurrentStatus.Name = "CurrentStatus";
            this.CurrentStatus.Width = 175;
            // 
            // AssStart
            // 
            this.AssStart.HeaderText = "生产操作";
            this.AssStart.MinimumWidth = 9;
            this.AssStart.Name = "AssStart";
            this.AssStart.Text = "启动生产";
            this.AssStart.UseColumnTextForButtonValue = true;
            this.AssStart.Width = 175;
            // 
            // ServerMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1605, 774);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServerMainForm";
            this.Text = " ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerMainForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dvAssLine)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnStartPLCServer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Port;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_IP;
        private System.Windows.Forms.TextBox txt_Log;
        private System.Windows.Forms.Button btnClosePLCServer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dvAssLine;
        private System.Windows.Forms.DataGridViewTextBoxColumn AssemblyLineId;
        private System.Windows.Forms.DataGridViewTextBoxColumn FactoryTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProcessId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProcessTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn AssemblyLineTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentStatus;
        private System.Windows.Forms.DataGridViewButtonColumn AssStart;
    }
}