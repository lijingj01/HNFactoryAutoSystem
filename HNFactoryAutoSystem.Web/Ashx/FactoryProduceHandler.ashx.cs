using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HNFactoryAutoSystem.Web.Ashx
{
    /// <summary>
    /// FactoryProduceHandler 的摘要说明
    /// </summary>
    public class FactoryProduceHandler : BaseAshx, IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string strReturnJson = string.Empty;
            string strAction = context.Request.Params["Action"];
            switch (strAction)
            {
                case "GetFactoryLineProduceLog":
                    //获取工厂的生产线的生产数据
                    strReturnJson = GetFactoryLineProduceLog(context);
                    break;
                default: break;
            }


            context.Response.Write(strReturnJson);
        }

        private string GetFactoryLineProduceLog(HttpContext context)
        {
            string strJson = string.Empty;
            string strLineId = context.Request.Params["LineId"];
            Data.DataLogHelper logHelper = new Data.DataLogHelper();
            Data.DeviceProduceLogCollection items = logHelper.GetDeviceProduceLogCollection(strLineId);

            Data.Result<Data.DeviceProduceLogCollection> result = Data.ResultUtil.Success<Data.DeviceProduceLogCollection>(items);

            strJson = result.ToJsonString();
            return strJson;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}