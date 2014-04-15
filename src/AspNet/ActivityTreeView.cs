using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Inthros.AspNet
{
    [ToolboxData(@"<{0}:ActivityTreeView runat=""server""> </{0}:ActivityTreeView>")]
    public sealed class ActivityTreeView : WebControl, IScriptControl
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

        private Activity activityTree;

        public Activity ActivityTree
        {
            get { return this.activityTree; }
            set
            {
                this.activityTree = value;

                if (this.activityTree != null)
                {
                    var activityViewTreeActivityVisitor = new ActivityViewTreeActivityVisitor();

                    this.ActivityViewTree = activityViewTreeActivityVisitor.CreateActivityViewTree(this.ActivityTree);
                }
                else
                {
                    this.ActivityViewTree = null;
                }
            }
        }

        private ActivityViewTree ActivityViewTree { get; set; }

        private ScriptManager ScriptManager { get; set; }

        protected override void OnPreRender(EventArgs e)
        {
            if (!this.DesignMode)
            {
                this.ScriptManager = ScriptManager.GetCurrent(this.Page);

                if (this.ScriptManager == null)
                {
                    throw new InvalidOperationException(
                        "A ScriptManager control must exist on the current page.");
                }

                this.ScriptManager.RegisterScriptControl(this);
            }

            if (this.Page.Header == null)
            {
                throw new InvalidOperationException(
                    "The Page Header must have a runat='server' attribute.");
            }

            if (!this.Page.Header.Controls.Cast<Control>().Any(c => c.ID == WebResourceNames.CssInthros))
            {
                var inthrosCsslink = new StyleSheetLink {
                    ID = WebResourceNames.CssInthros,
                    Href = this.Page.ClientScript.GetWebResourceUrl(typeof(ActivityTreeView), WebResourceNames.CssInthros),
                };

                this.Page.Header.Controls.Add(inthrosCsslink);
            }

            if (!this.Page.Header.Controls.Cast<Control>().Any(c => c.ID == WebResourceNames.CssFontAwesome))
            {
                var fontAwesomeCsslink = new StyleSheetLink
                {
                    ID = WebResourceNames.CssFontAwesome,
                    Href = this.Page.ClientScript.GetWebResourceUrl(typeof(ActivityTreeView), WebResourceNames.CssFontAwesome),
                };

                this.Page.Header.Controls.Add(fontAwesomeCsslink);
            }

            base.OnPreRender(e);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (this.ActivityViewTree != null)
            {
                this.ActivityViewTree.Render(writer);
            }
        }

        IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
        {
            return Enumerable.Empty<ScriptDescriptor>();
        }

        IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
        {
            var jQuery = new ScriptReference
            {
                Name = "jquery.js",
                Assembly = typeof(ActivityTreeView).Assembly.FullName,
            };

            var inthros = new ScriptReference
            {
                Name = "inthros.js",
                Assembly = typeof(ActivityTreeView).Assembly.FullName,
            };

            return new[] { jQuery, inthros };
        }
    }
}