using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.UI;

namespace Inthros.AspNet.Views
{
    public class ForEachView : ActivityView, IActivityViewContainer
    {
        public ForEachView(string activityId)
            : base(activityId)
        {
            this.Body = EmptyActivityView.Instance;
        }

        public string Values { get; set; }

        public string Argument { get; set; }

        public ActivityView Body { get; private set; }

        protected override string CssClass
        {
            get
            {
                return "ww-foreach";
            }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.AddAttribute("class", "header");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.RenderBeginTag(HtmlTextWriterTag.Label);
            writer.Write("Foreach");
            writer.RenderEndTag();

            writer.AddAttribute("class", "ww-code");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.WriteEncodedText(this.Argument);
            writer.RenderEndTag();

            writer.RenderBeginTag(HtmlTextWriterTag.Label);
            writer.Write("in");
            writer.RenderEndTag();

            writer.AddAttribute("class", "ww-code");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.WriteEncodedText(this.Values);
            writer.RenderEndTag();

            writer.RenderEndTag();

            writer.RenderBeginTag(HtmlTextWriterTag.Label);
            writer.Write("Body");
            writer.RenderEndTag();

            writer.AddAttribute("class", "body");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            this.Body.Render(writer);
            writer.RenderEndTag();
        }

        void IActivityViewContainer.Add(ActivityView view)
        {
            this.Body = view;
        }

        IReadOnlyCollection<ActivityView> IActivityViewContainer.ActivityViews
        {
            get { return new ReadOnlyCollection<ActivityView>(new[] { this.Body }); }
        }
    }
}
