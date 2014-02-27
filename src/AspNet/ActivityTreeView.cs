using System.Activities;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inthros.AspNet
{
    [ToolboxData(@"<{0}:ActivityTreeView runat=""server""> </{0}:ActivityTreeView>")]
    public sealed class ActivityTreeView : WebControl
    {
        public ActivityTreeView()
            : base(HtmlTextWriterTag.Div)
        {

        }

        public override string ID
        {
            get
            {
                this.EnsureID();

                return base.ID;
            }
            set
            {
                base.ID = value;
            }
        }

        public Activity ActivityTree { get; set; }

        private ActivityViewTree ActivityViewTree { get; set; }

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            if (this.ActivityTree != null)
            {
                var activityViewTreeActivityVisitor = new ActivityViewTreeActivityVisitor();

                this.ActivityViewTree = activityViewTreeActivityVisitor.CreateActivityViewTree(this.ActivityTree);
            }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (this.ActivityViewTree != null)
            {
                this.ActivityViewTree.Render(writer);
            }
        }
    }
}