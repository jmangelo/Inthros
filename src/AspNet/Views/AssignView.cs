using System.Web.UI;

namespace Inthros.AspNet.Views
{
    public class AssignView : ActivityView
    {
        public AssignView(string activityId)
            : base(activityId)
        {
        }

        public string To { get; set; }

        public string Value { get; set; }

        protected override bool Collapsible
        {
            get
            {
                return false;
            }
        }

        protected override string CssClass
        {
            get
            {
                return "ww-assign";
            }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.AddAttribute("class", "ww-code");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.WriteEncodedText(this.To);
            writer.RenderEndTag();

            writer.WriteEncodedText("=");

            writer.AddAttribute("class", "ww-code");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.WriteEncodedText(this.Value);
            writer.RenderEndTag();
        }
    }
}
