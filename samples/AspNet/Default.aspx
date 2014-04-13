<%@ Page Language="C#" CodeBehind="Default.aspx.cs" Inherits="Inthros.Samples.AspNet.Default" %>

<%@ Register tagPrefix="wf" namespace="Inthros.AspNet" assembly="Inthros.AspNet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <link href="~/Content/inthros.css" rel="stylesheet" />
        <link href="//netdna.bootstrapcdn.com/font-awesome/4.0.3/css/font-awesome.min.css" rel="stylesheet" />
    </head>
    <body>
        <form id="form1" runat="server">
            <asp:ScriptManager runat="server">
                <Scripts>
                    <asp:ScriptReference Name="jquery.js" />
                </Scripts>
            </asp:ScriptManager>
            <div>
                <wf:ActivityTreeView runat="server" ID="ActivityTreeView" />
            </div>
        </form>
    </body>
</html>