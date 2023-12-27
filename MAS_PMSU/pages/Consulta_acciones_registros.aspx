<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/principal.Master" CodeBehind="Consulta_acciones_registros.aspx.vb" Inherits="MAS_PMSU.Consulta_acciones_registros" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="divgrid" runat="server" style="margin-top: 50px; padding: 1rem;">
        <asp:Label ID="lbluser" runat="server"></asp:Label>
        <div class="row">
            <div class="col-lg-12">
                <h1 class="page-header">Consultar Inventario</h1>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Consultar producto del inventario
                    </div>

                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-3">
                                <div class="form-group">
                                    <label>Sede:</label>
                                    <asp:DropDownList CssClass="form-control" ID="BsqSede" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Bsq_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-lg-3">
                                <div class="form-group">
                                    <label>Departamento:</label>
                                    <asp:DropDownList CssClass="form-control" ID="BsqDepto" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Bsq_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-lg-3">
                                <div class="form-group">
                                    <label>Producto:</label>
                                    <asp:DropDownList CssClass="form-control" ID="BsqProd" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Bsq_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-lg-3">
                                <div class="form-group">
                                    <label>Tipo:</label>
                                    <asp:DropDownList CssClass="form-control" ID="BsqTipo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Bsq_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>

                        </div>
                        
                        <div class="form-group">
                            <div class="row">

                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <label>Fecha desde:</label>
                                        <asp:TextBox ID="txtFecha1" CssClass="form-control"  Enabled="false" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Bsq_SelectedIndexChanged"></asp:TextBox>
                                        <asp:LinkButton ID="Btncalen1" runat="server" CssClass="btn btn-primary" Text="Calendario" CausesValidation="false">&nbsp;Calendario</asp:LinkButton>
                                        <asp:Calendar ID="calFecha1" CssClass="table" Width="200px" Height="200px" runat="server" Visible="false" TargetControlID="txtFecha" OnSelectionChanged="CalFecha1_OnSelectionChanged" Format="yyyy/MM/dd" Enabled="false" AutoPostBack="True"></asp:Calendar>
                                    </div>
                                </div>

                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <label>Fecha hasta:</label>
                                        <asp:TextBox ID="txtFecha2" CssClass="form-control" Enabled="false" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Bsq_SelectedIndexChanged"></asp:TextBox>
                                        <asp:LinkButton ID="Btncalen2" runat="server" CssClass="btn btn-primary" Text="Calendario" CausesValidation="false">&nbsp;Calendario</asp:LinkButton>
                                        <asp:Calendar ID="calFecha2" CssClass="table" Width="200px" Height="200px" runat="server" Visible="false" TargetControlID="txtFecha" OnSelectionChanged="CalFecha2_OnSelectionChanged" Format="yyyy/MM/dd" Enabled="false" AutoPostBack="True"></asp:Calendar>
                                    </div>
                                </div>

                                
                                <div class="col-lg-4">
                                    <label>Busqueda por descripción:</label>

                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Buscar por la caracteristica del producto" autocomplete="off"  AutoPostBack="True" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                                
                                    <asp:Button ID="btnSearch" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="txtSearch_TextChanged"/>
                                </div>

                            </div>
                        </div>

                        <div class="row">
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <asp:LinkButton ID="btnRest" runat="server" CssClass="btn btn-warning" Text="Restablecer Filtro" CausesValidation="false">&nbsp;Restablecer Filtro</asp:LinkButton>
                                </div>
                            </div>
                        </div>

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
                                        PageSize="20">
                                <Columns>
                                    <asp:BoundField DataField="ID" HeaderText="ID" />
                                    <asp:BoundField DataField="Producto" HeaderText="Producto" />
                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                    <asp:BoundField DataField="Sede" HeaderText="Sede"/>
                                    <asp:BoundField DataField="Departamento" HeaderText="Departamento" />
                                    <asp:BoundField DataField="Factura" HeaderText="Factura" />
                                    <asp:BoundField DataField="Requisicion" HeaderText="Requisicion"/>
                                    <asp:BoundField DataField="Proveedor" HeaderText="Proveedor" />
                                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                    <asp:BoundField DataField="Precio" HeaderText="Precio" />
                                    <asp:BoundField DataField="Total" HeaderText="Total" />
                                    <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                                    <asp:TemplateField HeaderText="Editar">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEditar" CssClass="btn btn-warning" runat="server"
                                                CommandArgument='<%# Eval("ID") %>' OnClick="btnEditar_Click" ImageUrl="~/imagenes/editar.png" Height="30" Width="40" ImageAlign="Middle" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Eliminar">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEliminar" CssClass="btn btn-danger" runat="server" 
                                                CommandArgument='<%# Eval("ID").ToString() + "-" + Eval("Cantidad").ToString() + "-" + Eval("Producto").ToString() + "-" + Eval("Tipo").ToString() %>' OnClick="btnEliminar_Click" ImageUrl="~/imagenes/borrar.png" Height="30" Width="40" ImageAlign="Middle" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <AlternatingRowStyle BackColor="White" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                <EmptyDataTemplate>
                                    ¡No hay registros con esas propiedades!
                                </EmptyDataTemplate>
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
