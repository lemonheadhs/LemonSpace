<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CodeTransform.aspx.cs" 
    Inherits="WebJumony.CodeJumony.CodeTransform"
    ValidateRequest="false"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <style>
        .leftTxt {
            float:left; width:48%; background-color:#E5EFFC; overflow:auto;
        }
        .rigthTxt {
            float:right; width:48%; background-color:#E5EFFC; overflow:auto;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnTransform" OnClick="btnTransform_Click" runat="server" Text="开始转换..." />
        <div>
            <asp:TextBox ID="txtOriginalHtml" TextMode="MultiLine" CssClass="leftTxt" Rows="30" runat="server">
                

            </asp:TextBox>
            <asp:TextBox ID="txtNewHtml" TextMode="MultiLine" CssClass="rigthTxt" Rows="30" runat="server"></asp:TextBox>
        </div>
    </div>
    </form>
</body>
</html>
