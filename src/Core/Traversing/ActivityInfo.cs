using System;

namespace Inthros.Core.Traversing
{
    public sealed class ActivityInfo
    {
        public ActivityInfo(object activity)
        {
            if (activity == null)
            {
                throw new ArgumentNullException("activity");
            }

            this.Activity = activity;
        }

        public ActivityInfo(object activity, object parentActivity, string parentPropertyName)
            : this(activity)
        {
            if (parentActivity == null)
            {
                throw new ArgumentNullException("parentActivity");
            }

            if (parentPropertyName == null)
            {
                throw new ArgumentNullException("parentPropertyName");
            }

            this.ParentActivity = parentActivity;
            this.ParentPropertyName = parentPropertyName;
        }

        public object Activity { get; private set; }

        public object ParentActivity { get; private set; }

        public string ParentPropertyName { get; private set; }
    }
}