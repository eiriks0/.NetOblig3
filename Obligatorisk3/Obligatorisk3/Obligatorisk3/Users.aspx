<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="Obligatorisk3.Users" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >

    
    <div class="row">
        <div class="col-md-9">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <h4>Spørsmål (<asp:Label runat="server" ID="QuestionCounter"></asp:Label>)</h4>
                </div>
                <div runat="server" id="PanelProgress" class="panel-progress">
                    <div runat="server" id="PanelProgressbar" style="width: 0%;" class="panel-progressbar"></div>
                </div>
                <div class="panel-body">
                    <div class="col-md-8">
                        <p class="question-title">
                            <asp:Label runat="server" ID="QuestionText"></asp:Label>
                        </p>
                        <asp:RadioButtonList runat="server" ID="Answers"></asp:RadioButtonList>
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-success" OnClick="Button1_Click" Text="Neste spørsmål" />
                    </div>
                    <div class="col-md-4">
                        <asp:Image ID="TrafficQuestionImage" CssClass="question-image" runat="server" />
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="panel panel-info">
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