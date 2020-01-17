namespace HNFactoryAutoSystem
{
    partial class AssemblyLineStatusForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAssemblyLineId = new System.Windows.Forms.TextBox();
            this.dvLogList = new System.Windows.Forms.DataGridView();
            this.AssemblyLineTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProcessTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SensorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeviceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Created = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeviceStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SensorStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dvLogList)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "生产线编号";
            // 
            // txtAssemblyLineId
            // 
            this.txtAssemblyLineId.Location = new System.Drawing.Point(204, 27);
            this.txtAssemblyLineId.Name = "txtAssemblyLineId";
            this.txtAssemblyLineId.ReadOnly = true;
            this.txtAssemblyLineId.Size = new System.Drawing.Size(365, 31);
            this.txtAssemblyLineId.TabIndex = 1;
            // 
            // dvLogList
            // 
            this.dvLogList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dvLogList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvLogList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AssemblyLineTitle,
            this.ProcessTitle,
            this.SensorName,
            this.DeviceName,
            this.Created,
            this.DeviceStatus,
            this.SensorStatus,
            this.ParType,
            this.ParValue,
            this.ParUnit});
            this.dvLogList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dvLogList.Location = new System.Drawing.Point(0, 165);
            this.dvLogList.Name = "dvLogList";
            this.dvLogList.RowHeadersWidth = 72;
            this.dvLogList.RowTemplate.Height = 33;
            this.dvLogList.Size = new System.Drawing.Size(2581, 1179);
            this.dvLogList.TabIndex = 2;
            // 
            // AssemblyLineTitle
            // 
            this.AssemblyLineTitle.HeaderText = "生产线名称";
            this.AssemblyLineTitle.MinimumWidth = 9;
            this.AssemblyLineTitle.Name = "AssemblyLineTitle";
            this.AssemblyLineTitle.ReadOnly = true;
            this.AssemblyLineTitle.Width = 156;
            // 
            // ProcessTitle
            // 
            this.ProcessTitle.HeaderText = "工艺名称";
            this.ProcessTitle.MinimumWidth = 9;
            this.ProcessTitle.Name = "ProcessTitle";
            this.ProcessTitle.ReadOnly = true;
            this.ProcessTitle.Width = 135;
            // 
            // SensorName
            // 
            this.SensorName.HeaderText = "传感器名称";
            this.SensorName.MinimumWidth = 9;
            this.SensorName.Name = "SensorName";
            this.SensorName.ReadOnly = true;
            this.SensorName.Width = 156;
            // 
            // DeviceName
            // 
            this.DeviceName.HeaderText = "设备名称";
            this.DeviceName.MinimumWidth = 9;
            this.DeviceName.Name = "DeviceName";
            this.DeviceName.ReadOnly = true;
            this.DeviceName.Width = 135;
            // 
            // Created
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "G";
            dataGridViewCellStyle1.NullValue = null;
            this.Created.DefaultCellStyle = dataGridViewCellStyle1;
            this.Created.HeaderText = "记录时间";
            this.Created.MinimumWidth = 9;
            this.Created.Name = "Created";
            this.Created.ReadOnly = true;
            this.Created.Width = 135;
            // 
            // DeviceStatus
            // 
            this.DeviceStatus.HeaderText = "设备状态";
            this.DeviceStatus.MinimumWidth = 9;
            this.DeviceStatus.Name = "DeviceStatus";
            this.DeviceStatus.ReadOnly = true;
            this.DeviceStatus.Width = 135;
            // 
            // SensorStatus
            // 
            this.SensorStatus.HeaderText = "传感器状态";
            this.SensorStatus.MinimumWidth = 9;
            this.SensorStatus.Name = "SensorStatus";
            this.SensorStatus.ReadOnly = true;
            this.SensorStatus.Width = 156;
            // 
            // ParType
            // 
            this.ParType.HeaderText = "参数类型";
            this.ParType.MinimumWidth = 9;
            this.ParType.Name = "ParType";
            this.ParType.ReadOnly = true;
            this.ParType.Width = 135;
            // 
            // ParValue
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.ParValue.DefaultCellStyle = dataGridViewCellStyle2;
            this.ParValue.HeaderText = "参数数值";
            this.ParValue.MinimumWidth = 9;
            this.ParValue.Name = "ParValue";
            this.ParValue.ReadOnly = true;
            this.ParValue.Width = 135;
            // 
            // ParUnit
            // 
            this.ParUnit.HeaderText = "参数单位";
            this.ParUnit.MinimumWidth = 9;
            this.ParUnit.Name = "ParUnit";
            this.ParUnit.ReadOnly = true;
            this.ParUnit.Width = 135;
            // 
            // AssemblyLineStatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2581, 1344);
            this.Controls.Add(this.dvLogList);
            this.Controls.Add(this.txtAssemblyLineId);
            this.Controls.Add(this.label1);
            this.Name = "AssemblyLineStatusForm";
            this.Text = "生产线设备状态信息";
            ((System.ComponentModel.ISupportInitialize)(this.dvLogList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAssemblyLineId;
        private System.Windows.Forms.DataGridView dvLogList;
        private System.Windows.Forms.DataGridViewTextBoxColumn AssemblyLineTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProcessTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn SensorName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeviceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Created;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeviceStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn SensorStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParUnit;
    }
}