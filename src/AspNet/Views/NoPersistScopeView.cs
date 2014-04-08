using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Inthros.AspNet.Views
{
    public class NoPersistScopeView : ActivityView, IActivityViewContainer
    {
        public NoPersistScopeView(string activityId)
            : base(activityId)
        {
            this.Body = EmptyActivityView.Instance;
        }

        public ActivityView Body { get; private set; }

        protected override string CssClass
        {
            get
            {
                return "ww-nopersistscope";
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
        }

        void IActivityViewContainer.Add(ActivityView view)
        {
            this.Body = view;
        }

        IReadOnlyCollection<ActivityView> IActivityViewContainer.ActivityViews
        {
            get { return new ReadOnlyCollection<ActivityView>(new[] { this.Body }); }
        }
    }
}
