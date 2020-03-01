using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HNFactoryAutoSystem.Web.Ashx
{
    /// <summary>
    /// FactoryHandler 的摘要说明
    /// </summary>
    public class FactoryHandler : BaseAshx, IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string strReturnJson = string.Empty;
            string strAction = context.Request.Params["Action"];
            switch (strAction)
            {
                case "GetFactoryLineList":
                    //获取工厂的生产线集合
                    strReturnJson = GetFactoryLineList(context);
                    break;
                case "GetLineDevices":
                    //获取生产线上的设备集合
                    strReturnJson = GetLineDevices(context);
                    break;
                case "GetDeviceSensors":
                    //获取设备关联的传感器集合
                    strReturnJson = GetDeviceSensors(context);
                    break;
                case "ChangeSensorJoin":
                    //更新传感器的关联信息
                    strReturnJson = ChangeSensorJoin(context);
                    break;
                default: break;
            }


            context.Response.Write(strReturnJson);
        }

        private string ChangeSensorJoin(HttpContext context)
        {
            string strSensorId = context.Request.Params["SensorId"];
            string strDeviceId = context.Request.Params["DeviceId"];
            string strToDeviceId = context.Request.Params["ToDeviceId"];
            Data.DataHelper dataHelper = new Data.DataHelper();

            Data.Result<Data.SensorInfo> result;
            bool isUpdate = dataHelper.UpdateSensorJoinInfo(strSensorId, strDeviceId, strToDeviceId);

            if (isUpdate)
            {
                result = Data.ResultUtil.Success<Data.SensorInfo>(new Data.SensorInfo());
            }
            else
            {
                result = Data.ResultUtil.SystemError<Data.SensorInfo>(new Data.SensorInfo());
            }
            return result.ToJsonString();
        }

        private string GetDeviceSensors(HttpContext context)
        {
            string strJson = string.Empty;
            string strDeviceId = context.Request.Params["DeviceId"];
            Data.DataHelper dataHelper = new Data.DataHelper();
            Data.SensorInfoCollection items = dataHelper.GetSensorInfos(strDeviceId);
            strJson = items.ToJsonString();

            return strJson;
        }

        private string GetLineDevices(HttpContext context)
        {
            string strJson = string.Empty;
            string strLineId = context.Request.Params["LineId"];
            Data.DataHelper dataHelper = new Data.DataHelper();
            Data.DeviceInfoCollection items = dataHelper.GetSmallAssemblyLineDevices(strLineId);
            strJson = items.ToJsonString();

            return strJson;
        }

        private string GetFactoryLineList(HttpContext context)
        {
            string strJson = string.Empty;
            string strFactoryId = context.Request.Params["FactoryId"];
            Data.DataHelper dataHelper = new Data.DataHelper();
            Data.AssemblyLineInfoCollection items = dataHelper.GetAssemblyLineInfoCollection(strFactoryId);
            strJson = items.ToJsonString();

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