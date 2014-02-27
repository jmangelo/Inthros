using System.Web.UI;

namespace Inthros.AspNet.Views
{
    public abstract class ActivityView
    {
        protected ActivityView(string activityId)
        {
            this.ActivityId = activityId;
        }

        public string ActivityId { get; private set; }

        public string ActivityName { get; set; }

        protected virtual string CssClass
        {
            get { return string.Empty; }
        }

        protected virtual bool Collapsible
        {
            get { return true; }
        }

        public virtual void Render(HtmlTextWriter writer)
        {
            // Begin main div
            writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Join(" ", "ww-activity", this.CssClass));
            if (this.Collapsible)
            {
                writer.AddAttribute("data-collapsible", null);
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            // Header div
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "header");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "title");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.WriteEncodedText(this.ActivityName ?? string.Empty);
            writer.RenderEndTag();

            if (this.Collapsible)
            {
                // Expand/Collapse icon
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "icon");
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "fa fa-angle-double-up fa-lg");
                writer.RenderBeginTag(HtmlTextWriterTag.I);
                writer.RenderEndTag();
                writer.RenderEndTag();
            }

            writer.RenderEndTag();

            // Content div
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "content");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            this.RenderContents(writer);

            writer.RenderEndTag();

            if (this.Collapsible)
            {
                // Collapsed div
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "collapsed");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.WriteEncodedText("Double-click to view");
                writer.RenderEndTag();
            }

            // End main div
            writer.RenderEndTag();
        }

        protected abstract void RenderContents(HtmlTextWriter writer);
    }
}
