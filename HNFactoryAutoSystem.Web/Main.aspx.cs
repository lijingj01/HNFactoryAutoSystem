using HNFactoryAutoSystem.Data.ProcessData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HNFactoryAutoSystem.Web
{
    public partial class Main : BasePage
    {
        public TechnologicalProcessCollection processes { get; set; }
        public string CardList;

        public Main()
        {
            //processes = new TechnologicalProcessCollection();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            try
            {
                Data.DataHelper dataHelper = new Data.DataHelper();

                TechnologicalProcessCollection processes = dataHelper.GetTechnologicalProcesses();

                StringBuilder strHtml = new StringBuilder();
                foreach(TechnologicalProcess process in processes)
                {
                    strHtml.AppendLine("<div class=\"col-md-6\">");
                    strHtml.AppendLine("<div class=\"card\">");// mb-4 shadow-sm
                    strHtml.AppendLine("<div class=\"card-header\">");
                    strHtml.AppendFormat("    <h4 class=\"my-0 font-weight-normal\">{0}</h4>",process.ProcessTitle);
                    strHtml.AppendLine("</div>");
                    strHtml.AppendLine("<div class=\"card-body\">");
                    strHtml.AppendFormat("<h1 class=\"card-title pricing-card-title\"><small class=\"text-muted\">纯度{0}% ~ {1}%</small></h1>"
                                        , process.PurityMin
                                        , process.PuritvMax);
                    strHtml.AppendLine("<ul class=\"list-unstyled mt-3 mb-4\">");
                    strHtml.AppendFormat("    <li>目数：{0}</li>",process.Fineness);
                    strHtml.AppendFormat("    <li>年份：{0}</li>",process.ProcessYear);
                    strHtml.AppendLine("</ul>");
                    strHtml.AppendFormat("<button type=\"button\" class=\"btn btn-lg btn-block btn-outline-primary\" data-code='{0}'>查看详情</button>", process.ProcessId);
                    strHtml.AppendLine(" </div>");
                    strHtml.AppendLine("</div>");
                    strHtml.AppendLine("</div>");
                }

                CardList = strHtml.ToString();
            }
            catch(Exception ex)
            {

            }
        }
    }
}