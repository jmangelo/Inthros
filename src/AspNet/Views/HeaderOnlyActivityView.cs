using System.Web.UI;

namespace Inthros.AspNet.Views
{
    public sealed class HeaderOnlyActivityView : ActivityView
    {
        public HeaderOnlyActivityView(string activityId)
            : base(activityId)
        {
        }

        protected override string CssClass
        {
            get
            {
                return "ww-headeronly";
            }
        }

        protected override bool Collapsible
        {
            get { return false; }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
        }
    }
}
