using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace HNFactoryAutoService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface IPlcDataService
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: 在此添加您的服务操作
        [OperationContract]
        [WebInvoke(Method = "GET",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "GetStudentById/Id={Id}")]
        Student GetStudentById(string Id);

        [OperationContract]
        [WebInvoke(Method = "GET",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "GetStudentList")]
        IList<Student> GetStudentList();

        #region 针对PLC数据的写入接口
        [OperationContract]
        [WebInvoke(Method = "GET",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "RequestPlcDataToDB/strJsonText={strJsonText}")]
        void RequestPlcDataToDB(string strJsonText);
        #endregion

    }

    // 使用下面示例中说明的数据约定将复合类型添加到服务操作。
    // 可以将 XSD 文件添加到项目中。在生成项目后，可以通过命名空间“HNFactoryAutoService.ContractType”直接使用其中定义的数据类型。
    [DataContract]
    public class CompositeType
    {
        private bool boolValue = true;
        private string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }

    [DataContract]
    public class Student
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }

    public class UserList
    {
        private static readonly UserList _Instance = new UserList();
        private UserList() { }
        public static UserList Instance { get { return _Instance; } }
        public IList<Student> Users { get { return _Users; } }
        private IList<Student> _Users = new List<Student> {
            new Student { Id = 1, Name = "张三" },
            new Student { Id = 2, Name = "李四" },
            new Student { Id = 3, Name = "王五" } };
    }

}
