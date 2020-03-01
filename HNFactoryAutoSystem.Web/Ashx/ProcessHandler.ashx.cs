using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HNFactoryAutoSystem.Web.Ashx
{
    /// <summary>
    /// ProcessHandler 的摘要说明
    /// </summary>
    public class ProcessHandler : BaseAshx,IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string strReturnJson = string.Empty;
            string strAction = context.Request.Params["Action"];
            switch (strAction) {
                case "GetExprocessList":
                    //获取主流程下子流程集合
                    strReturnJson = GetExprocessList(context);
                    break;
                case "GetExProcessSteps":
                    //获取子流程的步骤集合
                    strReturnJson = GetExProcessSteps(context);
                    break;
                case "GetExProcessStepPars":
                    //获取步骤对应的参数集合
                    strReturnJson = GetExProcessStepPars(context);
                    break;
                default:break;
            }


            context.Response.Write(strReturnJson);
        }

        private string GetExProcessStepPars(HttpContext context)
        {
            string strJson = string.Empty;
            string strStepId = context.Request.Params["StepId"];
            Data.DataHelper dataHelper = new Data.DataHelper();
            Data.ProcessData.ExProcessStepParsCollection items = dataHelper.GetExProcessStepPars(strStepId);
            strJson = items.ToJsonString();

            return strJson;
        }

        private string GetExProcessSteps(HttpContext context)
        {
            string strJson = string.Empty;
            string strExProcessId = context.Request.Params["ExProcessId"];
            Data.DataHelper dataHelper = new Data.DataHelper();
            Data.ProcessData.ExProcessStepCollection items = dataHelper.GetExProcessSteps(strExProcessId);
            strJson = items.ToJsonString();

            return strJson;
        }

        private string GetExprocessList(HttpContext context)
        {
            string strJson = string.Empty;
            string strProcessId = context.Request.Params["ProcessId"];
            Data.DataHelper dataHelper = new Data.DataHelper();
            Data.ProcessData.TeExProcessCollection items = dataHelper.GetTeExProcessCollection(strProcessId);
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