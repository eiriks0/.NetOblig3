<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Obligatorisk3.Login" MasterPageFile="~/Site.Master" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >
    
    <div class="auto-style1">
    
        <strong>
        <br />
        <br />
        <br />
        Login Page<br />
        </strong>
        <table class="auto-style2">
            <tr>
                <td class="auto-style4">Username:</td>
                <td class="auto-style5">
                    <asp:TextBox ID="TextBoxUserName" runat="server" style="text-align: left" Width="180px"></asp:TextBox>
                </td>
                <td class="auto-style9">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxUserName" CssClass="auto-style7" ErrorMessage="Vennligst skriv inn brukernavn" ForeColor="Red" style="text-align: right"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">Password:</td>
                <td class="auto-style5">
                    <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password" Width="180px"></asp:TextBox>
                </td>
                <td class="auto-style9">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxPassword" CssClass="auto-style7" ErrorMessage="Vennligst skriv inn passord" ForeColor="Red" style="text-align: right"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style5">
                    <br />
                    <asp:Button ID="Button_Login" runat="server" CssClass="auto-style10" OnClick="Button_Login_Click" Text="Logg inn" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td class="auto-style9">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style5">
                    <br />
                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="auto-style7" NavigateUrl="~/Registrering.aspx">Registrer ny bruker her</asp:HyperLink>
                    <br />
                </td>
                <td class="auto-style8">&nbsp;</td>
            </tr>
        </table>
    
    </div>
    </asp:Content>

