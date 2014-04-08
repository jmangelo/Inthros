using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Inthros.AspNet.Views
{
    public class CancellationScopeView : ActivityView, IActivityViewContainer
    {
        public CancellationScopeView(string activityId)
            : base(activityId)
        {
            this.Body = EmptyActivityView.Instance;
            this.CancellationHandler = EmptyActivityView.Instance;
        }

        public ActivityView Body { get; private set; }

        public ActivityView CancellationHandler { get; set; }

        protected override string CssClass
        {
            get
            {
                return "ww-cancellationscope";
            }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.Label);
            writer.Write("Body");
            writer.RenderEndTag();

            writer.AddAttribute("class", "body");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            this.Body.Render(writer);
            writer.RenderEndTag();

            writer.RenderBeginTag(HtmlTextWriterTag.Label);
            writer.Write("CancellationHandler");
            writer.RenderEndTag();

            writer.AddAttribute("class", "cancellationhandler");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            this.CancellationHandler.Render(writer);
            writer.RenderEndTag();
        }

        void IActivityViewContainer.Add(ActivityView view)
        {
            if (this.Body == EmptyActivityView.Instance)
            {
                this.Body = view;
            } else if (this.CancellationHandler == EmptyActivityView.Instance)
            {
                this.CancellationHandler = view;
            }
        }

        IReadOnlyCollection<ActivityView> IActivityViewContainer.ActivityViews
        {
            get { return new ReadOnlyCollection<ActivityView>(new[] { this.Body, this.CancellationHandler }); }
        }
    }
}
