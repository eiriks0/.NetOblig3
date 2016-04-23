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
        </strong>
        <asp:Label ID="Label1" runat="server" CssClass="auto-style2" Text="Label"></asp:Label>
        <strong>
        <br />
        <br />
        </strong>
        <asp:RadioButton ID="RadioButton1" runat="server" CssClass="auto-style2" />
        </span>
        <br />
        <br />
        <asp:RadioButton ID="RadioButton2" runat="server" />
        <br />
        <br />
        <asp:RadioButton ID="RadioButton3" runat="server" />
        <br />
        <br />
        <asp:RadioButton ID="RadioButton4" runat="server" />
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