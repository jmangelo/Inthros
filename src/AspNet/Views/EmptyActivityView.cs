using System.Web.UI;

namespace Inthros.AspNet.Views
{
    internal sealed class EmptyActivityView : ActivityView
    {
        public static readonly ActivityView Instance = new EmptyActivityView();

        private EmptyActivityView()
            : base(string.Empty)
        {
        }

        public override void Render(HtmlTextWriter writer)
        {

        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            
        }
    }
}