using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Inthros.AspNet.Views
{
    public class CompensableActivityView : ActivityView, IActivityViewContainer
    {
        public CompensableActivityView(string activityId)
            : base(activityId)
        {
            this.Body = EmptyActivityView.Instance;
            this.CompensationHandler = EmptyActivityView.Instance;
            this.ConfirmationHandler = EmptyActivityView.Instance;
            this.CancellationHandler = EmptyActivityView.Instance;
        }

        public ActivityView Body { get; private set; }

        public ActivityView CompensationHandler { get; set; }

        public ActivityView ConfirmationHandler { get; set; }

        public ActivityView CancellationHandler { get; set; }

        protected override string CssClass
        {
            get
            {
                return "ww-compensableactivity";
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
            writer.Write("CompensationHandler");
            writer.RenderEndTag();

            writer.AddAttribute("class", "compensationhandler");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            this.CompensationHandler.Render(writer);
            writer.RenderEndTag();

            writer.RenderBeginTag(HtmlTextWriterTag.Label);
            writer.Write("ConfirmationHandler");
            writer.RenderEndTag();

            writer.AddAttribute("class", "confirmationhandler");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            this.ConfirmationHandler.Render(writer);
            writer.RenderEndTag();

            writer.RenderBeginTag(HtmlTextWriterTag.Label);
            writer.Write("CancellationHandler");
            writer.RenderEndTag();

            writer.AddAttribute("class", "cancellationhandler");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            this.CancellationHandler.Render(writer);
            writer.RenderEndTag();
        }

        void IActivityViewContainer.Add(ActivityView view, string scope)
        {
            if (scope == "Body")
            {
                this.Body = view;
            }
            else if (scope == "CompensationHandler")
            {
                this.CompensationHandler = view;
            }
            else if (scope == "ConfirmationHandler")
            {
                this.ConfirmationHandler = view;
            }
            else if (scope == "CancellationHandler")
            {
                this.CancellationHandler = view;
            }
        }

        IReadOnlyCollection<ActivityView> IActivityViewContainer.ActivityViews
        {
            get { return new ReadOnlyCollection<ActivityView>(new[] { this.Body, this.CompensationHandler, this.ConfirmationHandler, this.CancellationHandler }); }
        }
    }
}
