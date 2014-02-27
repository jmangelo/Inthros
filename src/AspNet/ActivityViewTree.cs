using System.Web.UI;
using Inthros.AspNet.Views;

namespace Inthros.AspNet
{
    internal sealed class ActivityViewTree
    {
        public ActivityView Root { get; set; }

        public ActivityView FindActivityView(string activityId)
        {
            if (this.Root == null)
            {
                return null;
            }

            return this.FindActivityView(this.Root, activityId);
        }

        public void Render(HtmlTextWriter writer)
        {
            if (this.Root != null)
            {
                this.Root.Render(writer);
            }
        }

        private ActivityView FindActivityView(ActivityView activityView, string activityId)
        {
            if (activityView.ActivityId == activityId)
            {
                return activityView;
            }

            var container = activityView as IActivityViewContainer;

            if (container != null)
            {
                foreach (var childView in container.ActivityViews)
                {
                    var targetView = this.FindActivityView(childView, activityId);

                    if (targetView != null)
                    {
                        return targetView;
                    }
                }
            }

            return null;
        }
    }
}