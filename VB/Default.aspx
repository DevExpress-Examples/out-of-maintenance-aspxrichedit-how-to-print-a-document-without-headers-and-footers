<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register assembly="DevExpress.Web.ASPxRichEdit.v18.2, Version=18.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxRichEdit" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v18.2, Version=18.2.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script type="text/javascript">
        function onCustomCommandExecuted(s, e) {
            switch (e.commandName) {
                case "Print":
                    var url =  window.location.href + '?print=true';
                    if(ASPx.PDFPluginHelper.IsInstalled()) {
                        var helperFrame = document.createElement('iframe');
                        helperFrame.name = 'printiframe';
                        helperFrame.style.position = 'absolute'
                        helperFrame.style.top = '-1000px'
                        document.body.appendChild(helperFrame);
                        window.frames[helperFrame.name].document.location = url;
                    } else
                        window.open(url);
                    break;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <dx:ASPxRichEdit ID="ASPxRichEdit1" runat="server" WorkDirectory="~\App_Data\WorkDirectory">
            <Settings>
                <Behavior CreateNew="Disabled" Save="Disabled" SaveAs="Disabled" Open="Disabled" />
            </Settings>
            <ClientSideEvents CustomCommandExecuted="onCustomCommandExecuted" />
        </dx:ASPxRichEdit>
    </form>
</body>
</html>