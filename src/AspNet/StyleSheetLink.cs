using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Inthros.AspNet
{
    internal class StyleSheetLink : HtmlControl
    {
        public StyleSheetLink()
            : base("link")
        {

        }

        public string Href { get; set; }

        protected override void RenderAttributes(HtmlTextWriter writer)
        {
            base.RenderAttributes(writer);

            if (this.Href != null)
            {
                writer.WriteAttribute("href", this.Href);
            }

            writer.WriteAttribute("type", "text/css");
            writer.WriteAttribute("rel", "stylesheet");
        }
    }
}
