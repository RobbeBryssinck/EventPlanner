<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mail.aspx.cs" Inherits="SendMail" %>
<html>
<head id="Head1" runat="server"><title>E-mail test page</title></head>
<body>
    <form id="form1" runat="server">
        Message sender: <asp:textbox id="txtFrom" runat="server" /><br>
        Message recipient: <asp:textbox id="txtTo" runat="server" /><br>
        Message subject: <asp:textbox id="txtSubject" runat="server" /><br>
        Message body:<br/>
        <asp:textbox id="txtBody" runat="server" height="150px" textmode="multiline" /><br>
        <asp:button id="btn_SendMessage" runat="server" onclick="btn_SendMessage_Click" text="Send message" /><br>
        <asp:label id="Label1" runat="server" text="" />
    </form>
</body>
</html>