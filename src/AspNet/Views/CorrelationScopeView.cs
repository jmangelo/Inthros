using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Inthros.AspNet.Views
{
    public class CorrelationScopeView : ActivityView, IActivityViewContainer
    {
        public CorrelationScopeView(string activityId)
            : base(activityId)
        {
            this.Body = EmptyActivityView.Instance;
        }

        public ActivityView Body { get; private set; }

        public string CorrelatesWith { get; set; }

        protected override string CssClass
        {
            get
            {
                return "ww-correlationscope";
            }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.AddAttribute("class", "header");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.RenderBeginTag(HtmlTextWriterTag.Label);
            writer.Write("CorrelatesWith");
            writer.RenderEndTag();

            writer.AddAttribute("class", "ww-code");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.WriteEncodedText(this.CorrelatesWith ?? string.Empty);
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

        void IActivityViewContainer.Add(ActivityView view, string scope)
        {
            this.Body = view;
        }

        IReadOnlyCollection<ActivityView> IActivityViewContainer.ActivityViews
        {
            get { return new ReadOnlyCollection<ActivityView>(new[] { this.Body }); }
        }
    }
}
