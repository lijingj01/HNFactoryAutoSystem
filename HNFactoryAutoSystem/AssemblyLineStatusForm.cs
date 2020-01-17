using HNFactoryAutoSystem.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HNFactoryAutoSystem
{
    public partial class AssemblyLineStatusForm : Form
    {
        /// <summary>
        /// 生产线编号
        /// </summary>
        public string AssemblyLineId { get; set; }
        public AssemblyLineStatusForm()
        {
            InitializeComponent();

        }

        public AssemblyLineStatusForm(string strAssLineId) : this()
        {
            this.AssemblyLineId = strAssLineId;
            Bind();
        }

        private void Bind()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(AssemblyLineId))
                {
                    //通过生产线获取对应的所有设备信息和参数
                    DataLogHelper logHelper = new DataLogHelper();
                    DeviceProduceLogCollection logs = logHelper.GetDeviceProduceLogCollection(AssemblyLineId);


                    //加载进列表
                    foreach(DeviceProduceLog log in logs)
                    {
                        int index = this.dvLogList.Rows.Add();
                        this.dvLogList.Rows[index].Cells["AssemblyLineTitle"].Value = log.AssemblyLineTitle;
                        this.dvLogList.Rows[index].Cells["ProcessTitle"].Value = log.ProcessTitle;
                        this.dvLogList.Rows[index].Cells["SensorName"].Value = log.SensorName;
                        this.dvLogList.Rows[index].Cells["DeviceName"].Value = log.DeviceName;
                        this.dvLogList.Rows[index].Cells["Created"].Value = log.Created;
                        this.dvLogList.Rows[index].Cells["DeviceStatus"].Value = SysHelper.Enums.EnumHelper.GetDeviceActionTypeString(log.DeviceStatus);
                        this.dvLogList.Rows[index].Cells["SensorStatus"].Value = SysHelper.Enums.EnumHelper.GetSenserStatusTypeString(log.SensorStatus);
                        this.dvLogList.Rows[index].Cells["ParType"].Value = SysHelper.Enums.EnumHelper.GetDeviceParameterTypeString(log.ParType);
                        this.dvLogList.Rows[index].Cells["ParUnit"].Value = log.ParUnit;
                        this.dvLogList.Rows[index].Cells["ParValue"].Value = log.ParValue;
                    }
                }


            }catch(Exception ex)
            {

            }
        }
    }
}
