﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.UI;

namespace Inthros.AspNet.Views
{
    public class DoWhileView : ActivityView, IActivityViewContainer
    {
        public DoWhileView(string activityId)
            : base(activityId)
        {
            this.Body = EmptyActivityView.Instance;
        }

        public ActivityView Body { get; private set; }

        public string Condition { get; set; }

        protected override string CssClass
        {
            get
            {
                return "ww-dowhile";
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
            writer.Write("Condition");
            writer.RenderEndTag();

            writer.AddAttribute("class", "condition ww-code");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.WriteEncodedText(this.Condition);
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
