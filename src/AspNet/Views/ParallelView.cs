using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.UI;

namespace Inthros.AspNet.Views
{
    public class ParallelView : ActivityView, IActivityViewContainer
    {
        public ParallelView(string activityId)
            : base(activityId)
        {
            this.Branches = new Collection<ActivityView>();
        }

        public Collection<ActivityView> Branches { get; private set; }

        protected override string CssClass
        {
            get
            {
                return "ww-parallel";
            }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            foreach (var branch in this.Branches)
            {
                branch.Render(writer);
            }
        }

        void IActivityViewContainer.Add(ActivityView view, string scope)
        {
            this.Branches.Add(view);
        }

        IReadOnlyCollection<ActivityView> IActivityViewContainer.ActivityViews
        {
            get { return new ReadOnlyCollection<ActivityView>(this.Branches); }
        }
    }
}