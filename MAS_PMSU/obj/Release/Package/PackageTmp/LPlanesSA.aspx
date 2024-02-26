<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/principal.Master" CodeBehind="LPlanesSA.aspx.vb" Inherits="MAS_PMSU.LPlanesSA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="row">
        <div class="col-lg-12">
                    <h1 class="page-header">Planes de Seguridad Alimentaria</h1>
                </div>
     </div>
           <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                      <div class="panel-heading" >
                            Informe POWER BI
                         </div>
                        <div class="panel-body">
                           <form accept-charset="UTF-8" role="form" runat="server">
                               <div class="table-responsive">
                                   <asp:GridView ID="GridDatos" runat="server" CellPadding="4" ForeColor="#333333" Width="100%"
                    GridLines="None" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="DEPARTAMENTO" HeaderText="DEPARTAMENTO" 
                            SortExpression="DEPARTAMENTO" />
                        <asp:BoundField DataField="MUNICIPIO" HeaderText="MUNICIPIO" 
                            SortExpression="MUNICIPIO" />
                        <asp:BoundField DataField="ALDEA" HeaderText="ALDEA" SortExpression="ALDEA" />
                        <asp:BoundField DataField="CASERIO" HeaderText="CASERIO" 
                            SortExpression="CASERIO" />
                        <asp:BoundField DataField="G_JH_JH_CODIGO" HeaderText="CODIGO_HOGAR" 
                            SortExpression="G_JH_JH_CODIGO" />
                        <asp:BoundField DataField="G_JH_JH_NOMBRE" HeaderText="NOMBRES" 
                            SortExpression="G_JH_JH_NOMBRE" />
                        <asp:BoundField DataField="G_JH_JH_APELLIDO" HeaderText="APELLIDOS" 
                            SortExpression="G_JH_JH_APELLIDO" />
                        <asp:BoundField DataField="G_JH_JH_SEXO" HeaderText="SEXO" 
                            SortExpression="G_JH_JH_SEXO" />
                        <asp:ButtonField ButtonType="Image" CommandName="Editar" HeaderText="EDITAR" 
                            ImageUrl="~/images/edit2.png" Text="Editar" Visible="False" />
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
                               </div> 
                               
                           </form>                           
                        </div>
                    </div>
                </div>
                <!-- /.col-lg-12 -->
              
            </div>
</asp:Content>
