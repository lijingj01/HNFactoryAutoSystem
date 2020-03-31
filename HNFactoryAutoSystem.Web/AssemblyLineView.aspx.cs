using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HNFactoryAutoSystem.Web
{
    public partial class AssemblyLineView : BasePage
    {
        public string CardList;
        protected void Page_Load(object sender, EventArgs e)
        {
            PageTitle = "生产线实时状态";
            if (!IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            try
            {
                CardList = string.Empty;

            }
            catch(Exception ex)
            {
                
            }
        }
    }
}