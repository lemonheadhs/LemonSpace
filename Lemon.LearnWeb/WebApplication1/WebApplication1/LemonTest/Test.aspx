<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="WebApplication1.LemonTest.Test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Label runat="server" ><%= DateTime.Now.ToString() %></asp:Label>
        </ContentTemplate>
        <Triggers>
        <asp:AsyncPostBackTrigger  ControlID="Button1" EventName="Click"/>
        </Triggers>
    </asp:UpdatePanel>
        <asp:Button ID="Button1" runat="server" Text="Button" />

    </div>
    
    </form>
</body>
</html>
