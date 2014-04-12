using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.UI;

namespace Inthros.AspNet.Views
{
    internal sealed class SequenceView : ActivityView, IActivityViewContainer
    {
        public SequenceView(string activityId)
            : base(activityId)
        {
            this.Children = new Collection<ActivityView>();
        }

        public Collection<ActivityView> Children { get; private set; }

        protected override string CssClass
        {
            get
            {
                return "ww-sequence";
            }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            foreach (var child in this.Children)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                child.Render(writer);
                writer.RenderEndTag();
            }
        }

        void IActivityViewContainer.Add(ActivityView view, string scope)
        {
            this.Children.Add(view);
        }

        IReadOnlyCollection<ActivityView> IActivityViewContainer.ActivityViews
        {
            get { return new ReadOnlyCollection<ActivityView>(this.Children); }
        }
    }
}