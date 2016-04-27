<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registrering.aspx.cs" Inherits="Obligatorisk3.Registrering" MasterPageFile="~/Site.Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >

    <div id="login-content">
        <div class="row">
            <div class="col-md-7">
                <h4>Registrer</h4>
                <table class="auto-style1">
                    <tr>
                        <td class="auto-style12">Brukernavn:</td>
                        <td class="auto-style13">
                            <asp:TextBox ID="TextBoxUN" CssClass="form-control" runat="server"></asp:TextBox>
                        </td>
                        <td class="auto-style14">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxUN" ErrorMessage="Vennligst skriv inn brukernavn" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style12">E-post:</td>
                        <td class="auto-style13">
                            <asp:TextBox ID="TextBoxEmail" CssClass="form-control" runat="server"></asp:TextBox>
                        </td>
                        <td class="auto-style14">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxEmail" ErrorMessage="Vennligst fyll inn E-mail" ForeColor="Red"></asp:RequiredFieldValidator>
                            <br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBoxEmail" ErrorMessage="E-mail må være riktig" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style12">Passord:</td>
                        <td class="auto-style16">
                            <asp:TextBox ID="TextBoxPass" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                        <td class="auto-style14">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBoxPass" ErrorMessage="Vennligst skriv inn passord" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style12">Bekreft passord:</td>
                        <td class="auto-style16">
                            <asp:TextBox ID="TextBoxRPass" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox></td>
                        <td class="auto-style14">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBoxRPass" ErrorMessage="Vennligst bekreft passord" ForeColor="Red"></asp:RequiredFieldValidator>
                            <br />
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBoxPass" ControlToValidate="TextBoxRPass" ErrorMessage="Passordene må være like" ForeColor="Red"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style4">&nbsp;</td>
                        <td class="auto-style6">
                            <br />
                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" OnClick="Button1_Click" Text="Registrer" />
                            <input id="Reset1" class="auto-style10 btn btn-danger" type="reset" value="Reset" /></td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </div>
            <div class="col-md-5">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4>Har du bruker allerede?</h4>
                            </div>
                            <div class="panel-body">
                                <p>Hva gjør du her? <asp:HyperLink runat="server" NavigateUrl="~/Login.aspx">Logg inn!</asp:HyperLink></p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        
                    </div>
                </div>
            </div>
        </div>
    </div>
   </asp:Content>
