<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="parkinggarages.aspx.cs" Inherits="ICTinnovatie.parkinggarages" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            <asp:Button ID="button1" runat="server" Text="wijzig stad" OnClick="button1_Click"/>
        </div>
        <div>
            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="naam" DataValueField="ID"></asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ICTinnovatieConnectionString %>" SelectCommand="SELECT * FROM [parkinggaragetbls] WHERE ([city_id] = @city_id) ORDER BY [naam]">
                <SelectParameters>
                    <asp:QueryStringParameter Name="city_id" QueryStringField="id" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            
        </div>
        <div>
            <asp:Button ID="Button2" runat="server" Text="kies parkeergarage" OnClick="Button2_Click" />
        </div>
        <div>
            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        </div>
        
    </form>
</body>
</html>
