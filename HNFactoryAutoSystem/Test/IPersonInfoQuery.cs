using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace HNFactoryAutoSystem.Test
{
    [ServiceContract(Name = "PersonInfoQueryServices")]
    internal interface IPersonInfoQuery
    {
        /// <summary>
        /// 说明：GET请求
        /// WebGet默认请求是GET方式
        /// UriTemplate(URL Routing)的参数名name必须要方法的参数名必须一致（不区分大小写）
        /// RequestFormat规定客户端必须是什么数据格式请求的（JSon或者XML），不设置默认为XML
        /// ResponseFormat规定服务端返回给客户端是以是什么数据格返回的（JSon或者XML）
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "PersonInfoQuery/{name}"
            , BodyStyle = WebMessageBodyStyle.Bare
            , RequestFormat = WebMessageFormat.Json
            , ResponseFormat = WebMessageFormat.Json)]
        User GetScore(string name);

        /// <summary>
        /// 说明：POS请求
        /// WebInvoke请求方式有POST、PUT、DELETE等，所以需要明确指定Method是哪种请求的，这里我们设置POST请求。
        /// 注意：POST情况下，UriTemplate(URL Routing)一般是没有参数（和上面GET的UriTemplate不一样，因为POST参数都通过消息体传送）
        /// RequestFormat规定客户端必须是什么数据格式请求的（JSon或者XML），不设置默认为XML
        /// ResponseFormat规定服务端返回给客户端是以是什么数据格返回的（JSon或者XML）
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST"
            , UriTemplate = "PersonInfoQuery/Info"
            , BodyStyle = WebMessageBodyStyle.Bare
            , RequestFormat = WebMessageFormat.Json
            , ResponseFormat = WebMessageFormat.Json)]
        User GetInfo(Info info);
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class PersonInfoQueryServices : IPersonInfoQuery
    {
        private List<User> UserList = new List<User>();
        /// <summary>
        /// 生成一些测试数据
        /// </summary>
        public PersonInfoQueryServices()
        {
            UserList.Add(new User() { ID = 1, Name = "张三", Age = 18, Score = 98 });
            UserList.Add(new User() { ID = 2, Name = "李四", Age = 20, Score = 80 });
            UserList.Add(new User() { ID = 3, Name = "王二麻子", Age = 25, Score = 59 });
        }
        public User GetInfo(Info info)
        {
            return UserList.FirstOrDefault(n => n.ID == info.ID && n.Name == info.Name);
        }

        public User GetScore(string name)
        {
            return UserList.FirstOrDefault(n => n.Name == name);
        }
    }

    [DataContract]
    public class User
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Age { get; set; }

        [DataMember]
        public int Score { get; set; }
    }

    [DataContract]
    public class Info
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
