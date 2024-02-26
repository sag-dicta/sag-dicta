<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/principal.Master" CodeBehind="tc_visitaAT.aspx.vb" Inherits="MAS_PMSU.tc_visitaAT" %>






<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header"></h1>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                   TABLERO DE INDICADORES DE REGISTRO DE CAMPO O LIBRO DE CAMPO
                </div>
                <div class="panel-body">
                   
                       <div class="embed-responsive embed-responsive-16by9">

                                        <iframe id="MyIframe" runat="server" width="800" height="600" src="https://app.powerbi.com/view?r=eyJrIjoiOGRiMWE3MGItYmQxNi00MDgyLTk4ZmQtMWJiMDg1Yzc4ZmU2IiwidCI6ImVhOGEzNmMwLTczOGItNGNiNC05MzhjLTY5YTUwNWJiNjg5OCIsImMiOjF9" frameborder="0" allowfullscreen="true"></iframe>
                                    </div>

                </div>
            </div>
        </div>
    </div>
     <div class="row">
        <div class="col-lg-12">
            

             

              </div>
   </div>
</asp:Content>
