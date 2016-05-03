<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Manager.aspx.cs" Inherits="Obligatorisk3.Manager" MasterPageFile="~/Site.Master" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >

    <div>
    
        <br />
        <br />
    
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSourceRegistration" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" ShowHeaderWhenEmpty="True" Width="650px">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                <asp:BoundField DataField="Score" HeaderText="Score" SortExpression="Score" />
            </Columns>
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
        </asp:GridView>
        <asp:SqlDataSource
            ID="SqlDataSourceRegistration"
            runat="server"
            ConnectionString="<%$ ConnectionStrings:RegistrationConnectionString %>"
            SelectCommand="
                SELECT UserData.UserId AS InternalUserId, UserData.ID, UserData.UserName, Email, Highscores.Score
                FROM UserData
                LEFT JOIN Highscores ON
                UserData.UserId = Highscores.UserId
            "
            ></asp:SqlDataSource>
    
    </div>
 
     </asp:Content>