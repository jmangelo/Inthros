using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.UI;

namespace Inthros.AspNet.Views
{
    public class PickBranchView : ActivityView, IActivityViewContainer
    {
        public PickBranchView(string activityId)
            : base(activityId)
        {
            this.Trigger = EmptyActivityView.Instance;
            this.Action = EmptyActivityView.Instance;
        }

        public ActivityView Trigger { get; set; }

        public ActivityView Action { get; set; }

        protected override string CssClass
        {
            get
            {
                return "ww-pickbranch";
            }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.Label);
            writer.Write("Trigger");
            writer.RenderEndTag();

            writer.AddAttribute("class", "trigger");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            this.Trigger.Render(writer);
            writer.RenderEndTag();

            writer.RenderBeginTag(HtmlTextWriterTag.Label);
            writer.Write("Action");
            writer.RenderEndTag();

            writer.AddAttribute("class", "action");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            this.Action.Render(writer);
            writer.RenderEndTag();
        }

        void IActivityViewContainer.Add(ActivityView view)
        {
            if (this.Trigger == EmptyActivityView.Instance)
            {
                this.Trigger = view;
            }
            else if (this.Action == EmptyActivityView.Instance)
            {
                this.Action = view;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        IReadOnlyCollection<ActivityView> IActivityViewContainer.ActivityViews
        {
            get { return new ReadOnlyCollection<ActivityView>(new[] { this.Trigger, this.Action }); }
        }
    }
}
