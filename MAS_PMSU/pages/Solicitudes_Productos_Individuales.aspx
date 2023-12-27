<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/principal.Master" CodeBehind="Solicitudes_Productos_Individuales.aspx.vb" Inherits="MAS_PMSU.Solicitudes_Productos_Individuales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="divgrid" runat="server" style="margin-top: 50px; padding: 1rem;">
        <div class="row">
            <div class="col-lg-12">
                <h1 class="page-header">Solicitude de Requisición</h1>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Detalle de la solicitud
                    </div>

                    <div class="panel-body">
                        <div>
                            <asp:GridView runat="server" AutoGenerateColumns="false" ID="GridView1" 
                                        CellPadding="4" 
                                        ForeColor="#333333" 
                                        Width="100%"
                                        GridLines="None" 
                                        AllowPaging="True" 
                                        CssClass="table table-striped table-bordered table-hover" 
                                        Font-Size="Small" 
                                        OnPageIndexChanging="OnPaging" 
                                        PageSize="20"
                                        ViewStateMode="Inherit">
                                <Columns>
                                    <asp:BoundField DataField="Descripcion_bien" HeaderText="Descripcion" />
                                    <asp:BoundField DataField="Numero_Catalago" HeaderText="No. Catalogo" />
                                    <asp:BoundField DataField="Unidad_Medida" HeaderText="Unidad de Medida" />
                                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad Solicitada"/>
                                </Columns>
                                <AlternatingRowStyle BackColor="White" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                <EmptyDataTemplate>Agrega los bienes a solicitar</EmptyDataTemplate>
                                <EditRowStyle BackColor="#7C6F57" />
                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#90d0f5" Font-Bold="True" ForeColor="#035787" />
                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="20" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#E3EAEB" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                <SortedAscendingHeaderStyle BackColor="#246B61" />
                                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                <SortedDescendingHeaderStyle BackColor="#15524A" />
                            </asp:GridView>
                        </div>

                        <div>
                            <div class="row">
                                <div class="col-lg-2">
                                    <asp:Label ID="lblcanreg" runat="server" Text="Cantidad de Registros: "></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-2">
                                    <asp:Button ID="btnExcel" runat="server" Text="Exportar a Excel" CssClass="btn btn-primary" OnClick="btnExcel_Click"/>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>