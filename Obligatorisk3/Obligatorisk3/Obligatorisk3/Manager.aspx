<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Manager.aspx.cs" Inherits="Obligatorisk3.Manager" MasterPageFile="~/Site.Master" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >

    <div id="ManagerAdmin" runat="server" style="display: none;" class="row">
        <div class="col-md-12">
            <h3>Administratorområde</h3>
            <p class="text-muted">For vanlige dødelige vil denne siden se relativt kjedelig ut. <br /> Kun en liste med highscorene? Du som er admin har tilgang til mer informasjon enn som så...</p>
        </div>
        <div class="col-md-12">
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSourceRegistration" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" ShowHeaderWhenEmpty="True" Width="100%">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="Intern BrukerID" SortExpression="ID" />
                    <asp:BoundField DataField="UserName" HeaderText="Brukernavn" SortExpression="UserName" />
                    <asp:BoundField DataField="IsAdmin" HeaderText="Administrator" SortExpression="IsAdmin" />
                    <asp:BoundField DataField="Email" HeaderText="E-post" SortExpression="Email" />
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
                    SELECT UserData.ID, UserData.UserName, UserData.IsAdmin, Email, Highscores.Score
                    FROM UserData
                    LEFT JOIN Highscores ON
                    UserData.UserId = Highscores.UserId
                "
                >
            </asp:SqlDataSource>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <h3>Dine highscores</h3>
            <p class="text-muted">Under ser du en liste over dine highscores sortert på en dato. <br /> Denne datoen blir generert hver gang du lagrer poengsummen din.</p>
        </div>
        <div class="col-md-12">
            <asp:GridView ID="HighscoresGridView" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="HighscoresDataSource" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" ShowHeaderWhenEmpty="True" Width="100%">
                <Columns>
                    <asp:BoundField DataField="Score" HeaderText="Score" SortExpression="Score" />
                    <asp:BoundField DataField="CreatedDate" HeaderText="Dato" SortExpression="CreatedDate" />
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
                ID="HighscoresDataSource"
                runat="server"
                ConnectionString="<%$ ConnectionStrings:RegistrationConnectionString %>"
                >
            </asp:SqlDataSource>
        </div>
    </div>
 
     </asp:Content>