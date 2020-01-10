using HNFactoryAutoSystem.Business;
using HNFactoryAutoSystem.Data;
using HNFactoryAutoSystem.Data.ProcessData;
using HNFactoryAutoSystem.Test;
using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Windows.Forms;

namespace HNFactoryAutoSystem
{
    public partial class Form1 : Form
    {
        private WebServiceHost host;
        public Form1()
        {
            InitializeComponent();
            LoadDBList();
        }

        private void LoadDBList()
        {
            //测试加载数据库信息
            //DataHelper dataHelper = new DataHelper();
            //string strProcessId = "HPIO-2019-3000";
            ////TeExProcessCollection infos = dataHelper.GetTeExProcesses(strProcessId);

            //TeExProcess exProcess = dataHelper.GetTeExProcess(strProcessId, 1);

            //StringBuilder strText = new StringBuilder();
            //foreach (ProcessStep info in exProcess.Steps)
            //{
            //    strText.AppendLine(info.StepTitle);
            //}
            //textBox1.Text = strText.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void OpenHost()
        {
            try
            {
                PersonInfoQueryServices service = new PersonInfoQueryServices();
                Uri baseAddress = new Uri("http://localhost:7788/");
                //Uri baseAddress2 = new Uri("https://localhost:7789/");

                if (host == null)
                {
                    host = new WebServiceHost(service, baseAddress);

                    //using (ServiceHost _serviceHost = new ServiceHost(service, baseAddress))//或者：WebServiceHost _serviceHost = new WebServiceHost(typeof(PersonInfoQueryServices), baseAddress);
                    //{
                    //如果不设置MaxBufferSize,当传输的数据特别大的时候，很容易出现“提示:413 Request Entity Too Large”错误信息,最大设置为20M
                    WebHttpBinding binding = new WebHttpBinding
                    {
                        TransferMode = TransferMode.Buffered,
                        MaxBufferSize = 2147483647,
                        MaxReceivedMessageSize = 2147483647,
                        MaxBufferPoolSize = 2147483647,
                        ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max,
                        Security = { Mode = WebHttpSecurityMode.None }
                    };
                    host.AddServiceEndpoint(typeof(IPersonInfoQuery), binding, baseAddress);

                    //建立一个服务元数据行为对象
                    //ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                    //服务元数据行为有一个属性HttpGetEnabled，我们要把它设为true ，这样，客户端就可用HTTP GET的方法从服务端下载元数据了
                    //smb.HttpsGetEnabled = false;
                    //把这个服务元数据行为对象添加到宿主ServiceHost 对象的行为集合中去。
                    //host.Description.Behaviors.Add(smb);

                    //_serviceHost.Opened += delegate
                    //{
                    //    this.label1.Text = "Web服务已开启...";
                    //};
                    host.Open();
                    this.label1.Text = "Service Running";
                    //this.label1.Text += "输入任意键关闭程序！";

                    //_serviceHost.Close();
                    //}
                }
            }
            catch (Exception ex)
            {
                this.label1.Text = string.Format("Web服务开启失败：{0}\r\n{1}", ex.Message, ex.StackTrace);
            }
        }

        private void StartHost()
        {

            //使用代码绑定
            //Uri tcpa = new Uri("net.tcp://localhost:8090/Service1");
            //Uri baseAddress = new Uri("http://localhost:8090/Service1");
            //host = new ServiceHost(typeof(HNFactoryAutoService.Service1), baseAddress);
            //ServiceMetadataBehavior mBehave = new ServiceMetadataBehavior();
            //NetTcpBinding tcpb = new NetTcpBinding();

            //WebHttpBinding binding = new WebHttpBinding();
            //ServiceEndpoint endpoint = host.AddServiceEndpoint(typeof(HNFactoryAutoService.Service1), binding, baseAddress);

            //WebHttpBehavior httpBehavior = new WebHttpBehavior();

            //endpoint.Behaviors.Add(httpBehavior);

            //host.Opened += delegate {

            //    Console.WriteLine("Hosted successfully.");

            //};
            //host.Description.Behaviors.Add(mBehave);
            ////, "mex"
            //WebHttpBinding binding = new WebHttpBinding();
            //host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexTcpBinding(),"");
            //host.AddServiceEndpoint(typeof(HNFactoryAutoService.IService1), tcpb, tcpa);

            //host.Open();
            //this.btnStart.Enabled = false;
            //this.label1.Text = "Service Running";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //StartHost();
            OpenHost();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (host != null)
            {
                host.Close();
                host = null;
                this.label1.Text = "Service Closed";
                this.btnStart.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessBusiness processBusiness = new ProcessBusiness();

                ProcessBusiness.DeviceAction deviceAction = ProcessDeviceAction;

                string strAssemblyLineId = "G01-PL-01";
                string strCreateUser = "Test";

                processBusiness.StartProcess(strAssemblyLineId, strCreateUser, deviceAction);

                MessageBox.Show("流程完成！");
            }
            catch(Exception ex)
            {
                textBox1.Text = ex.Message;
            }
        }

        private bool ProcessDeviceAction(string strDeviceId
                , string strSonserId
                , SysHelper.Enums.SenserStatusType senserStatusType
                , SysHelper.Enums.DeviceParameterType parameterType
                , decimal deSetValue)
        {
            return true;
        }
    }
}
