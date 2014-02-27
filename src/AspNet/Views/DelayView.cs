using System.Web.UI;

namespace Inthros.AspNet.Views
{
    public class DelayView : ActivityView
    {
        public DelayView(string activityId) : base(activityId)
        {
        }

        public string Duration { get; set; }

        protected override string CssClass
        {
            get
            {
                return "ww-delay";
            }
        }

        protected override bool Collapsible
        {
            get
            {
                return false;
            }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.Label);
            writer.Write("Duration");
            writer.RenderEndTag();

            writer.AddAttribute("class", "ww-code");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.WriteEncodedText(this.Duration);
            writer.RenderEndTag();
        }
    }
}
