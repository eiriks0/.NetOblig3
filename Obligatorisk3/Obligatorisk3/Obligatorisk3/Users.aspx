<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="Obligatorisk3.Users" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >

    
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <h4>Spørsmål <span class="pull-right">(<asp:Label runat="server" ID="QuestionCounter"></asp:Label>)</span></h4>
                </div>
                <div class="progress">
                    <div runat="server" class="progress-bar" id="PanelProgressbar" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 0%">
                        <span class="sr-only"></span>
                    </div>
                </div>
                <div id="QuestionBody" class="panel-body">
                    <div class="row question-wrapper">
                        <div id="ResultsWrapper" class="col-md-12" runat="server" style="display: none;">
                            <div class="row">
                                <div class="col-md-12">
                                    <h4>Poengsum: <span runat="server" id="TheScore"></span></h4>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-md-6" runat="server">
                                    <h4 id="WrongResultsHeader">Du svarte feil på disse spørsmålene:</h4>
                                    <div id="WrongResults" runat="server"></div>
                                </div>
                                <div class="col-md-6">
                                    <h4 id="CorrectResultsHeader">Du svarte riktig på disse spørsmålene:</h4>
                                    <div id="CorrectResults" runat="server"></div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-8">
                            <p class="question-title">
                                <asp:Label runat="server" ID="QuestionText"></asp:Label>
                            </p>
                            <asp:RadioButtonList runat="server" ID="Answers"></asp:RadioButtonList>
                            <asp:Panel runat="server" ID="Panel1"></asp:Panel>
                        </div>
                        <div class="col-md-4">
                            <asp:Image ID="TrafficQuestionImage" CssClass="question-image" runat="server" />
                        </div>
                    </div>
                    
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Button ID="NextQuestionButton" runat="server" CssClass="btn btn-success btn-lg" OnClick="B_Next_Question" Text="Neste spørsmål" Visible="true" />
                            <asp:Button ID="RestartButton" runat="server" CssClass="btn btn-success btn-lg" OnClick="B_New_Quiz" Text="Start om igjen?" Visible="false" />
                            <asp:Button ID="SaveScoreButton" runat="server" CssClass="btn btn-primary btn-lg" OnClick="B_Save_Highscore" Text="Lagre poengsum" Visible="false" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $("#MainContent_Button1").enabled = false;
        $(".question-wrapper").hide();
        $(document).ready(function () {
            $(".question-wrapper").slideDown(500);

            $("input[type=radio][name=ctl00\\$MainContent\\$Answers]").change(function () {
                $("#MainContent_Button1").enabled = true;
            });
        });
    </script>
    
    </asp:Content>