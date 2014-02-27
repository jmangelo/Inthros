using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.UI;

namespace Inthros.AspNet.Views
{
    public class IfView : ActivityView, IActivityViewContainer
    {
        public IfView(string activityId)
            : base(activityId)
        {
            this.Then = EmptyActivityView.Instance;
            this.Else = EmptyActivityView.Instance;
        }

        public string Condition { get; set; }

        public ActivityView Then { get; set; }

        public ActivityView Else { get; set; }

        protected override string CssClass
        {
            get { return "ww-if"; }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.Label);
            writer.Write("Condition");
            writer.RenderEndTag();

            writer.AddAttribute("class", "condition ww-code");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.WriteEncodedText(this.Condition);
            writer.RenderEndTag();

            writer.AddAttribute("class", "container");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.AddAttribute("class", "then");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.RenderBeginTag(HtmlTextWriterTag.Label);
            writer.Write("Then");
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            this.Then.Render(writer);
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();

            writer.AddAttribute("class", "container");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.AddAttribute("class", "else");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.RenderBeginTag(HtmlTextWriterTag.Label);
            writer.Write("Else");
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            this.Else.Render(writer);
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();
        }

        void IActivityViewContainer.Add(ActivityView view)
        {
            if (this.Then == EmptyActivityView.Instance)
            {
                this.Then = view;
            }
            else if (this.Else == EmptyActivityView.Instance)
            {
                this.Else = view;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        IReadOnlyCollection<ActivityView> IActivityViewContainer.ActivityViews
        {
            get { return new ReadOnlyCollection<ActivityView>(new[] { this.Then, this.Else }); }
        }
    }
}