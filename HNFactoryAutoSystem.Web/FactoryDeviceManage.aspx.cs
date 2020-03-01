using HNFactoryAutoSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HNFactoryAutoSystem.Web
{
    public partial class FactoryDeviceManage : BasePage
    {
        public string CardList;
        protected void Page_Load(object sender, EventArgs e)
        {
            PageTitle = "工厂设备管理";
            if (!IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            try
            {

                //加载工厂信息
                Data.DataHelper dataHelper = new Data.DataHelper();
                HNFactoryAutoSystem.Data.FactoryInfoCollection factoryInfos = dataHelper.GetFactoryInfos();


                StringBuilder strHtml = new StringBuilder();
                foreach (FactoryInfo factory in factoryInfos)
                {

                    strHtml.AppendLine("<div class=\"col-xs-6 col-sm-2 pricing-box\">");
                    strHtml.AppendLine("<div class=\"widget-box\">");
                    strHtml.AppendLine("    <div class=\"widget-header header-color-blue\">");
                    strHtml.AppendFormat("        <h5 class=\"bigger lighter\">{0}</h5>", factory.FactoryTitle);
                    strHtml.AppendLine("    </div>");
                    strHtml.AppendLine("  <div class=\"widget-body\"> ");
                    strHtml.AppendLine("    <div class=\"widget-main\">");
                    //strHtml.AppendLine("         <ul class=\"list-unstyled spaced2\">");
                    //strHtml.AppendLine("            <li><i class=\"icon-ok green\"></i>");
                    //strHtml.AppendFormat("             {0}</li>", factory.Parameter1);
                    //strHtml.AppendLine("            <li><i class=\"icon-ok green\"></i>");
                    //strHtml.AppendFormat("            {0}</li>", factory.Parameter2);
                    //strHtml.AppendLine("         </ul>");
                    strHtml.AppendLine("         <hr />");
                    //strHtml.AppendLine("        <div class=\"price\">");
                    //strHtml.AppendFormat("            <small>纯度</small> {0}% ~ {1}%", factory.PurityMin, factory.PuritvMax);
                    //strHtml.AppendLine("        </div>");
                    strHtml.AppendLine("    </div>");
                    strHtml.AppendLine("    <div>");
                    strHtml.AppendFormat("         <a href=\"#\" class=\"btn btn-block btn-primary\" data-code='{0}'>", factory.FactoryId);
                    strHtml.AppendLine("             <i class=\"icon-cogs bigger-110\"></i>");
                    strHtml.AppendLine("            <span>查看生产线</span>");
                    strHtml.AppendLine("        </a>");
                    strHtml.AppendLine("    </div>");
                    strHtml.AppendLine("  </div>");
                    strHtml.AppendLine("</div>");
                    strHtml.AppendLine(" </div>");
                }


                CardList = strHtml.ToString();
            }
            catch(Exception ex)
            {

            }
        }
    }
}