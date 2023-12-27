<%@ Page Title="Ingresar Producto" Language="vb" AutoEventWireup="false" MasterPageFile="~/principal.Master" CodeBehind="Proveeduria_Agregar_Inventario2.aspx.vb" Inherits="MAS_PMSU.Proveeduria_Agregar_Inventario2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:connIHMA %>" ProviderName="<%$ ConnectionStrings:connIHMA.ProviderName %>"></asp:SqlDataSource>
<div class="row" style="margin-top: 50px; padding: 1rem;">
        <div class="col-lg-12">
            <h1 class="page-header">Ingresar nuevo producto a Inventario</h1>
        </div>
    </div>
       <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Datos del Producto
                    </div>

                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <label>1. ID del Producto:</label>
                                    <asp:TextBox CssClass="form-control" ID="ProdID" runat="server" placeholder="############" Enabled="false"></asp:TextBox>
                                </div>

                                <div class="form-group">
                                    <label>2. Nombre del Producto:</label>
                                   
                                    <asp:DropDownList CssClass="form-control" ID="Prodname" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Prodname_SelectedIndexChanged">
                                        <asp:ListItem Value="10">Otro producto...</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblInputProdname" runat="server" Text="Ingresa el nombre del nuevo producto:" Visible="false"></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtInputProdname" runat="server" Enabled="False" Visible="false" placeholder="Nombre del nuevo producto"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="validadorProdname" runat="server"
                                                                    ControlToValidate="txtInputProdname"
                                                                    ErrorMessage="*El campo no puede estar vacío"
                                                                    Forecolor="Red" 
                                                                    Enabled="false"/>
                                    <asp:Label ID="lbladver1" runat="server" Text="El producto ingresado ya esta en la lista de productos" Visible="false" ForeColor="Red"></asp:Label>
                                </div>

                                <div class="form-group">
                                    <label>3. Descripción del Producto:</label>
                                    <asp:TextBox CssClass="form-control" ID="Proddesc" runat="server" placeholder="Ingrese una breve descripción del producto" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="validatorProddesc" runat="server"
                                                                    ControlToValidate="Proddesc"
                                                                    ErrorMessage="*El campo no puede estar vacío"
                                                                    Forecolor="Red"/>
                                </div>

                                <div class="form-group">
                                    <label>4. Sede:</label>
                                    <asp:DropDownList CssClass="form-control" ID="DropDownList2" runat="server" DataSourceID="" AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group">
                                    <label>5. Asignado al Departamento:</label>
                                    <asp:DropDownList CssClass="form-control" ID="Proddepto" runat="server" DataSourceID="" AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                </div>

                                
                                
                                <div class="form-group">
                                    <label>6. N° de Factura:</label>
                                    <asp:TextBox CssClass="form-control" ID="Prodfact" runat="server" placeholder="Ingrese el número de la factura del producto"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                    ControlToValidate="Prodfact"
                                                                    ErrorMessage="*El campo no puede estar vacío"
                                                                    Forecolor="Red"/>
                                    <asp:RegularExpressionValidator id="RegularExpressionValidator1" 
                                                                    ControlToValidate="Prodfact"
                                                                    ValidationExpression="^[0-9]{1,10}$"
                                                                    Display="Static"
                                                                    ErrorMessage="*La factura debe contener solo números."
                                                                    EnableClientScript="False" 
                                                                    Forecolor="Red"
                                                                    runat="server"/>
                                </div>

                                <div class="form-group">
                                    <label>7. N° Requisición:</label>
                                    <asp:TextBox CssClass="form-control" ID="Prodrequi" runat="server" placeholder="Ingrese el número de requisición del producto" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                    ControlToValidate="Prodrequi"
                                                                    ErrorMessage="*El campo no puede estar vacío"
                                                                    Forecolor="Red"/>
                                    <asp:RegularExpressionValidator id="RegularExpressionValidator2" 
                                                                     ControlToValidate="Prodrequi"
                                                                     ValidationExpression="^[0-9]{1,10}$"
                                                                     Display="Static"
                                                                     ErrorMessage="*La requisición debe contener solo números."
                                                                     EnableClientScript="False" 
                                                                     Forecolor="Red"
                                                                     runat="server"/>
                                </div>

                                <div class="form-group">
                                    <label>8. Proveedor:</label>
                                    <asp:DropDownList CssClass="form-control" ID="Prodprov" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Prodprov_SelectedIndexChanged">
                                        <asp:ListItem Value="10">Otro proveedor</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblInputProdprov" runat="server" Text="Ingresa el nombre del nuevo Proveedor:" Visible="false"></asp:Label>
                                    <asp:TextBox CssClass="form-control" ID="txtInputProdprov" runat="server" placeholder="Nombre del nuevo proveedor" Enabled="False" Visible="false"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="validatorProdprov" runat="server"
                                                                    ControlToValidate="txtInputProdprov"
                                                                    ErrorMessage="*El campo no puede estar vacío"
                                                                    Forecolor="Red" 
                                                                    Enabled="false"/>
                                    <asp:Label ID="lbladver2" runat="server" Text="El proveedor ingresado ya esta en la lista de proveedores" Visible="false" ForeColor="Red"></asp:Label>
                                </div>

                                <div class="form-group">
                                    <label>9. Cantidad:</label>
                                    <asp:TextBox CssClass="form-control" ID="Prodcant" runat="server" placeholder="Ingrese la cantidad del producto" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                                    ControlToValidate="Prodcant"
                                                                    ErrorMessage="*El campo no puede estar vacío"
                                                                    Forecolor="Red"/>
                                    <asp:RegularExpressionValidator id="RegularExpressionValidator3" 
                                                                     ControlToValidate="Prodcant"
                                                                     ValidationExpression="^[0-9]{1,10}$"
                                                                     Display="Static"
                                                                     ErrorMessage="*La cantidad debe contener solo números."
                                                                     EnableClientScript="False" 
                                                                     Forecolor="Red"
                                                                     runat="server"/>
                                </div>

                                <div class="form-group">
                                    <label>10. Precio Unitario:</label>
                                    <asp:TextBox CssClass="form-control" ID="Prodpreuni" runat="server" placeholder="Ingrese el precio unitario del producto" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                                    ControlToValidate="Prodpreuni"
                                                                    ErrorMessage="*El campo no puede estar vacío"
                                                                    Forecolor="Red"/>
                                    <asp:RegularExpressionValidator id="RegularExpressionValidator4" 
                                                                     ControlToValidate="Prodpreuni"
                                                                     ValidationExpression="^[0-9]{1,10}$"
                                                                     Display="Static"
                                                                     ErrorMessage="*El precio debe contener solo números."
                                                                     EnableClientScript="False" 
                                                                     Forecolor="Red"
                                                                     runat="server"/>
                                </div>

                                <div class="form-group">
                                    <label>11. Tipo:</label>
                                    <asp:DropDownList CssClass="form-control" ID="DropDownList1" runat="server" DataSourceID="" AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group">
                                    <label>12. Fecha:</label>
                                    <asp:TextBox ID="txtFecha" CssClass="form-control"  Enabled="false" runat="server"></asp:TextBox>
                                    <asp:LinkButton ID="Btncalen" runat="server" CssClass="btn btn-primary" Text="Calendario" CausesValidation="false">&nbsp;Calendario</asp:LinkButton>
                                    <asp:Calendar ID="calFecha" CssClass="table" Width="200px" Height="200px" runat="server" Visible="false" TargetControlID="txtFecha" OnSelectionChanged="CalFecha_OnSelectionChanged" Format="yyyy/MM/dd" Enabled="false" AutoPostBack="True"></asp:Calendar>
                                </div>

                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6 col-md-offset-2">
                                <asp:LinkButton ID="Btn_save" runat="server" CssClass="btn btn-success" Text="Guardar" OnClick="Validar">&nbsp;Guardar</asp:LinkButton>
                                <asp:LinkButton ID="Btn_cancel" runat="server" CssClass="btn btn-warning" Text="Cancelar" CausesValidation="false" OnClick="Btn_cancel_Click">&nbsp;Cancelar</asp:LinkButton>
                            </div>
                        </div>

                    </div>

                </div>

            </div>

        </div>

</asp:Content>
