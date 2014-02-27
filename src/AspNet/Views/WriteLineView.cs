using System.Web.UI;

namespace Inthros.AspNet.Views
{
    public class WriteLineView : ActivityView
    {
        public WriteLineView(string activityId)
            : base(activityId)
        {
        }

        public string Text { get; set; }

        protected override string CssClass
        {
            get
            {
                return "ww-writeLine";
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
            writer.Write("Text");
            writer.RenderEndTag();

            writer.AddAttribute("class", "ww-code");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.WriteEncodedText(this.Text);
            writer.RenderEndTag();
        }
    }
}