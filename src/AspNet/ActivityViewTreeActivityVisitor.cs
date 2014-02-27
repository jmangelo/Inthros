using System;
using System.Activities;
using Inthros.AspNet.Builders;
using Inthros.AspNet.Views;
using Inthros.Core.Traversing;
using Inthros.Core.Utilities;

namespace Inthros.AspNet
{
    internal sealed class ActivityViewTreeActivityVisitor : ActivityVisitor
    {
        public ActivityViewTree CreateActivityViewTree(Activity root)
        {
            if (root == null)
            {
                throw new ArgumentNullException("root");
            }

            this.ViewTree = new ActivityViewTree();

            this.Visit(root);

            return this.ViewTree;
        }

        protected override void Visit(object activity)
        {
            if (activity != null)
            {
                var viewBuilder = ActivityViewBuilderProvider.Get(activity.GetType());

                if (viewBuilder != null)
                {
                    var activityView = viewBuilder.Build(activity);

                    this.AddActivityViewToTree(activityView);
                }

                base.Visit(activity);
            }
        }

        private void AddActivityViewToTree(ActivityView view)
        {
            if (this.ViewTree.Root == null)
            {
                this.ViewTree.Root = view;
            }
            else
            {
                string parentId = ObjectIdManager.GetId(this.CurrentParent);

                var parentView = this.ViewTree.FindActivityView(parentId) as IActivityViewContainer;

                if (parentView != null)
                {
                    parentView.Add(view);
                }
            }
        }

        private ActivityViewTree ViewTree { get; set; }
    }
}
