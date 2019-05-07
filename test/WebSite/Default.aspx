<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <style type="text/css">
        .style1
        {
            height: 332px;
        }
        .style2
        {
            width: 87px;
        }
        .style3
        {
            height: 332px;
            width: 87px;
        }
        .style4
        {
            width: 566px;
        }
        .style5
        {
            height: 332px;
            width: 566px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="width:100%; height: 249px;">
            <tr>
                <td>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="TempletSimple.aspx?ObjectTypeFullName=Hong.ChildSafeSystem.Module.Team">Team Looking</asp:HyperLink>
                    <br />
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                    <br />
                    <asp:TextBox ID="TextBox1" runat="server" Height="48px" Width="155px"></asp:TextBox>
                    <br />
                    <table style="width:100%;">
                        <tr>
                            <td class="style2">
                                &nbsp;</td>
                            <td class="style4">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style3">
                            </td>
                            <td class="style5">
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                <br />
                                <asp:Image ID="Image1_adfads" runat="server" Height="100%" />
                            </td>
                            <td class="style1">
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                &nbsp;</td>
                            <td class="style4">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
