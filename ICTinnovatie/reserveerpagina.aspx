<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reserveerpagina.aspx.cs" Inherits="ICTinnovatie.reserveerpagina" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="naam" DataValueField="ID" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ICTinnovatieConnectionString %>" SelectCommand="SELECT * FROM [citytbl] ORDER BY [naam]"></asp:SqlDataSource>
        </div>
        <asp:Button ID="Button1" runat="server" Text="kies stad" OnClick="Button1_Click" />
        <div>
            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        </div>
    </form>
</body>
</html>
