using System.Collections.Generic;

namespace Inthros.Core.Traversing
{
    public abstract class ActivityVisitor
    {
        protected ActivityVisitor()
        {
            this.CurrentLevel = 0;
            this.ParentActivities = new Stack<object>();
        }

        protected int CurrentLevel { get; private set; }

        protected object CurrentParent
        {
            get { return this.ParentActivities.Count > 0 ? this.ParentActivities.Peek() : null; }
        }

        protected string CurrentParentPropertyName { get; private set; }

        protected virtual void Visit(object activity)
        {
            if (activity != null)
            {
                var compositeDescriptor = ActivityDescriptorProvider.Get(activity.GetType());

                if (compositeDescriptor != null)
                {
                    this.EnterParentActivity(activity);

                    var children = compositeDescriptor.GetChildren(activity);

                    foreach (var activityInfo in children)
                    {
                        try
                        {
                            this.CurrentParentPropertyName = activityInfo.ParentPropertyName;

                            this.Visit(activityInfo.Activity);
                        }
                        finally
                        {
                            this.CurrentParentPropertyName = null;
                        }
                    }

                    this.LeaveParentActivity();
                }
            }
        }

        private Stack<object> ParentActivities { get; set; }

        private void LeaveParentActivity()
        {
            this.CurrentLevel--;
            this.ParentActivities.Pop();
        }

        private void EnterParentActivity(object parent)
        {
            this.CurrentLevel++;
            this.ParentActivities.Push(parent);
        }
    }
}