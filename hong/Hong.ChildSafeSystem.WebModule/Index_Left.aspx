<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index_Left.aspx.cs" Inherits="Index_Left" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body text="#000000">
<form id="form1" runat="server" target="mainFrame">
    <table width="195" border="0" align="center" cellpadding="0" cellspacing="0">
      <tr> 
        <td>&nbsp;</td>
    	    
        <td align="center"><table height="100%" width="100%" border=0>
            <tbody>
              <tr> 
                <td valign=top align=left> <table width=100% height="30" border=0>
                    <tbody>
                      <tr> 
                        <td height="40"><strong><font color="#ff0000">幼儿安全后台</font></strong></td>
                      </tr>
                    </tbody>
                  </table>
                    <asp:panel id="menupanel" runat="server">
                    </asp:panel>
                 <table width="100%" border=1 cellpadding="5" cellspacing="0" bgcolor="#f5f5f5">
                    <tbody>
                      <tr> 
                        <td width="118" height="35"><a href="" target="mainframe"><strong><font color="#ff0000"></font></strong></a></td>
                      </tr>
                      <tr> 
                        <td width="118" height="35"><a href="" target="mainframe"><strong><font color="#ff0000">关于</font></strong></a></td>
                      </tr>
                      <tr> 
                        <td width="118" height="35"><a href="" target="mainframe"><strong><font color="#ff0000"></font></strong></a></td>
                      </tr>
                    </tbody>
                  </table>
                  
                </td>
              </tr>
            </tbody>
          </table> </td>
            <td></td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td height="50" align="center" valign="middle">
        <% if (SchoolsCenter.Singleton.IsLogged(Page))
           {
               %>
               <asp:Button ID="LogoutBn" runat="server" Text="注销" Width="104px" 
                onclick="LogoutBn_Click" />
               <%
           } %>
	    </td>
        <td>&nbsp;</td>
      </tr>
    </table>
</form>
</body>
</html>
