using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Inthros.AspNet.Views
{
    public class TransactionScopeView : ActivityView, IActivityViewContainer
    {
        public TransactionScopeView(string activityId)
            : base(activityId)
        {
            this.Body = EmptyActivityView.Instance;
        }

        public ActivityView Body { get; private set; }

        protected override string CssClass
        {
            get
            {
                return "ww-transactionscope";
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
