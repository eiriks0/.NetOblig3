<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="Obligatorisk3.Users" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >

    
    <div>
    
        <br />
        <br />
        <br />
    
        <asp:Label ID="Label_welcome" runat="server" Text="Velkommen til din test "></asp:Label>
        <br />
        <br />
        <span class="auto-style1"><strong>Spørsmål:<br />
        <br />
        <asp:Image ID="Image1" runat="server" />
        </strong>
        </span>
        <br />
        <br />
        <asp:Panel ID="Panel1" runat="server">
            <span class="auto-style1">
            <asp:Label ID="Label1" runat="server" CssClass="auto-style2" Text="Label"></asp:Label>
            <br />
            <br />
            <asp:RadioButton ID="RadioButton1" runat="server" CssClass="auto-style2" GroupName="Answers" />
            <br />
            <br />
            <asp:RadioButton ID="RadioButton2" runat="server" GroupName="Answers" />
            <br />
            <br />
            <asp:RadioButton ID="RadioButton3" runat="server" GroupName="Answers" />
            <br />
            <br />
            <asp:RadioButton ID="RadioButton4" runat="server" GroupName="Answers" />
            </span>
        </asp:Panel>
        <br />
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Neste spørsmål" />
        <br />
        <br />
        <br />
        <br />
        <br />
        
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Button ID="B_Logout" runat="server" OnClick="B_Logout_Click" Text="Logg ut" />
    
    </div>
    
     </asp:Content>