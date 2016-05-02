<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Results.aspx.cs" Inherits="Obligatorisk3.Results" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >

     <div class="row">
        <div class="col-md-12">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <h4>Resultat <span class="pull-right">(<asp:Label runat="server" ID="QuestionCounter"></asp:Label>)</span></h4>
                </div>
                <div class="progress">
                    <div runat="server" class="progress-bar" id="PanelProgressbar" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 0%">
                        <span class="sr-only"></span>
                    </div>
                </div>
                <div id="QuestionBody" class="panel-body">
                    <div class="row question-wrapper">
                        <div class="col-md-8">
                            <p class="question-title">
                                <asp:Label runat="server" ID="QuestionText"></asp:Label>
                            </p>
                            <asp:RadioButtonList runat="server" ID="Answers"></asp:RadioButtonList>
                        </div>
                        <div class="col-md-4">
                            <asp:Image ID="TrafficQuestionImage" CssClass="question-image" runat="server" />
                        </div>
                    </div>
                    
                    <div class="row">
                        <div class="col-md-12">
                            
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