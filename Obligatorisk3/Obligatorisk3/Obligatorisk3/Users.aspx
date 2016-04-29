<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="Obligatorisk3.Users" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >

    
    <div class="row">
        <div class="col-md-10">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4>Spørsmål (1/10)</h4>
                </div>
                <div class="panel-body">
                    <asp:Label runat="server" ID="QuestionText"></asp:Label>
                    <asp:Image ID="Image1" runat="server" />
                    <asp:Panel ID="Panel1" runat="server">
                        <asp:RadioButton ID="RadioButton1" runat="server" CssClass="question-block" GroupName="Answers" />
                        <asp:RadioButton ID="RadioButton2" runat="server" CssClass="question-block" GroupName="Answers" />
                        <asp:RadioButton ID="RadioButton3" runat="server" CssClass="question-block" GroupName="Answers" />
                        <asp:RadioButton ID="RadioButton4" runat="server" CssClass="question-block" GroupName="Answers" />
                    </asp:Panel>
                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-success" OnClick="Button1_Click" Text="Neste spørsmål" />
                </div>
            </div>
        </div>
        <div class="col-md-2">
            <div class="panel panel-muted">
                <div class="panel-heading">
                    <h4>Tips:</h4>
                </div>
                <div class="panel-body">
                    <p>Visste du at du kan logge ut og komme tilbake til akkurat samme sted du avsluttet quizet?</p>
                </div>
            </div>
        </div>
    </div>
    
     </asp:Content>