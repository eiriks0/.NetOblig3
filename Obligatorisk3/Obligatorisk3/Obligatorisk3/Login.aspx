<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Obligatorisk3.Login" MasterPageFile="~/Site.Master" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >
    
    <div id="login-content" class="auto-style1">
        <div class="row">
            <div class="col-md-6 col-md-offset-1">
                <h4>Vennligst logg inn for å fortsette</h4>
                <div runat="server" id="UsernamePasswordAlert" style="display: none;" class="alert alert-danger"><strong>Obs!</strong> Feil brukernavn/passord</div>
                <table class="auto-style2">
                    <tr>
                        <td class="auto-style4">Brukernavn:</td>
                        <td class="auto-style5">
                            <asp:TextBox TabIndex="1" ID="TextBoxUserName" runat="server" style="text-align: left" CssClass="form-control" Width="180px"></asp:TextBox>
                        </td>
                        <td class="auto-style9">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxUserName" CssClass="auto-style7" ErrorMessage="Vennligst skriv inn brukernavn" ForeColor="Red" style="text-align: right"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Passord:</td>
                        <td class="auto-style5">
                            <asp:TextBox TabIndex="2" ID="TextBoxPassword" runat="server" CssClass="form-control" TextMode="Password" Width="180px"></asp:TextBox>
                        </td>
                        <td class="auto-style9">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxPassword" CssClass="auto-style7" ErrorMessage="Vennligst skriv inn passord" ForeColor="Red" style="text-align: right"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style3">&nbsp;</td>
                        <td class="auto-style5">
                            <br />
                            <asp:Button ID="Button_Login" TabIndex="3" runat="server" CssClass="auto-style10 btn btn-primary" OnClick="Button_Login_Click" Text="Logg inn" />
                        </td>
                        <td class="auto-style9">&nbsp;</td>
                    </tr>
                </table>
            </div>
            <div class="col-md-5">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4>Logg inn!</h4>
                            </div>
                            <div class="panel-body">
                                <p>Vi lagrer informasjon om din poengscore. Hvis du ønsker å bruke denne tjenesten,
                                    <asp:HyperLink ID="HyperLink2" runat="server" CssClass="auto-style7" NavigateUrl="~/Registrering.aspx">så må du registrere deg!</asp:HyperLink>
                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        
                    </div>
                </div>
            </div>
        </div>
    
    </div>

    <script type="text/javascript">
        var loginBtn = document.getElementById('log-in-navbar-btn');
        loginBtn.addEventListener("click", function (e) {
            location.replace('/Login');
            e.preventDefault();
        });
    </script>

    </asp:Content>

