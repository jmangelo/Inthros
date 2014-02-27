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
            <div>
                <wf:ActivityTreeView runat="server" ID="ActivityTreeView" />
            </div>
        </form>
        <script src="Scripts/jquery-2.1.0.js"></script>
        <script src="Scripts/inthros.js"></script>
    </body>
</html>