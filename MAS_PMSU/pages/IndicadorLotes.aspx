<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/principal.Master" CodeBehind="IndicadorLotes.aspx.vb" Inherits="MAS_PMSU.IndicadorLotes" %>
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
                   TABLERO DE INDICADORES DE LOTES
                </div>
                <div class="panel-body">
                    <ul class="nav nav-pills">
                        <li class="active"><a href="#Graficos" data-toggle="tab">Graficos</a>
                        </li>
                       <%-- <li><a href="#Datos" data-toggle="tab">Datos</a>
                        </li>--%>
                    </ul>
                    <br />
                    <br/>
                    <div class="tab-content">   
                        <div class="tab-pane fade in active" id="Graficos">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="embed-responsive embed-responsive-16by9">

                                        <iframe width="800" height="600" src="C:\Users\se7en\OneDrive\Desktop\Indicadores Multiplicadores.pbix" frameborder="0" allowfullscreen="true"></iframe>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
     <%--<div class="row">
        <div class="col-lg-12">
            
            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-warning" Text="Exportar Datos"><span class="glyphicon glyphicon-save"></span>&nbsp;Exportar Datos</asp:LinkButton>
        </div>
   </div>--%>
</asp:Content>