using System;
using System.Web.UI;
using Sample.Workflows;

namespace Inthros.Samples.AspNet
{
    public partial class Default : Page
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.ActivityTreeView.ActivityTree = new SampleActivity1();
        }
    }
}