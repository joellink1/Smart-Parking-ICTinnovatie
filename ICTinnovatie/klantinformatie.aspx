<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="klantinformatie.aspx.cs" Inherits="ICTinnovatie.klantinformatie" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            <asp:Button ID="Button1" runat="server" Text="wijzig stad" OnClick="Button1_Click"/>
        </div>
        <div>
            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            <asp:Button ID="Button2" runat="server" Text="wijzig parkeergarage" OnClick="Button2_Click"/>
        </div>
        <div>
            <asp:Label ID="Label3" runat="server" Text="Naam"></asp:Label><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="Label4" runat="server" Text="Email-adres"></asp:Label><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="Label5" runat="server" Text="kenteken"></asp:Label><asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="Button3" runat="server" Text="reserveer parkeerplek" OnClick="Button3_Click"/>
        </div>
        <div>
            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        </div>
    </form>
</body>
</html>
