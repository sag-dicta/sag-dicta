Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mime
Imports ClosedXML.Excel
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.[Shared].Json
Imports DocumentFormat.OpenXml.Office.Word
Imports MySql.Data.MySqlClient

Public Class AnalisisGerminacion
    Inherits System.Web.UI.Page
    Dim conn As String = ConfigurationManager.ConnectionStrings("connSAG").ConnectionString
    Dim sentencia As String
    Dim validarflag As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True
        If User.Identity.IsAuthenticated = True Then
            If IsPostBack Then

            Else
                llenarcomboProductor()
                llenarcomboDepto()
                llenarcomboCiclogrid()
                llenagrid()
            End If
        End If
    End Sub
    Protected Sub vaciar(sender As Object, e As EventArgs)
        Response.Redirect(String.Format("~/pages/AnalisisGerminacion.aspx"))
    End Sub
    Private Sub llenarcomboCiclogrid()
        Dim StrCombo As String = "SELECT * FROM sag_ciclo"
        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)

        txtciclo.DataSource = DtCombo
        txtciclo.DataValueField = DtCombo.Columns(0).ToString()
        txtciclo.DataTextField = DtCombo.Columns(1).ToString
        txtciclo.DataBind()
        Dim newitem As New ListItem("Todos", "Todos")
        txtciclo.Items.Insert(0, newitem)
    End Sub
    Private Sub llenarcomboDepto()
        Dim StrCombo As String = "SELECT * FROM tb_departamentos"
        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)

        TxtDepto.DataSource = DtCombo
        TxtDepto.DataValueField = DtCombo.Columns(0).ToString()
        TxtDepto.DataTextField = DtCombo.Columns(2).ToString
        TxtDepto.DataBind()
        Dim newitem As New ListItem("Todos", "Todos")
        TxtDepto.Items.Insert(0, newitem)
    End Sub
    Private Sub llenarcomboProductor()
        Dim StrCombo As String = "SELECT DISTINCT nombre_multiplicador FROM `vista_acta_lote_multi` WHERE 1 = 1 AND fecha_acta IS NOT NULL AND estado_sena = '1' ORDER BY nombre_multiplicador ASC"
        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)

        TxtProductorGrid.DataSource = DtCombo
        TxtProductorGrid.DataValueField = DtCombo.Columns(0).ToString()
        TxtProductorGrid.DataTextField = DtCombo.Columns(0).ToString
        TxtProductorGrid.DataBind()
        Dim newitem As New ListItem("Todos", "Todos")
        TxtProductorGrid.Items.Insert(0, newitem)
    End Sub
    Private Sub llenarcomboProductor2()
        Dim StrCombo As String

        StrCombo = "SELECT DISTINCT nombre_multiplicador FROM vista_acta_lote_multi WHERE estado_sena = '1' AND fecha_acta IS NOT NULL AND departamento = '" & TxtDepto.SelectedItem.Text & "' ORDER BY nombre_multiplicador ASC"

        Dim adaptcombo As New MySqlDataAdapter(StrCombo, conn)
        Dim DtCombo As New DataTable
        adaptcombo.Fill(DtCombo)

        TxtProductorGrid.DataSource = DtCombo
        TxtProductorGrid.DataValueField = DtCombo.Columns(0).ToString()
        TxtProductorGrid.DataTextField = DtCombo.Columns(0).ToString
        TxtProductorGrid.DataBind()
        Dim newitem As New ListItem("Todos", "Todos")
        TxtProductorGrid.Items.Insert(0, newitem)
    End Sub
    Protected Sub TxtDepto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TxtDepto.SelectedIndexChanged
        If TxtDepto.SelectedItem.Text = "Todos" Then
            llenarcomboProductor()
        Else
            llenarcomboProductor2()
        End If
        llenagrid()
    End Sub
    Sub llenagrid()
        Dim cadena As String = "id_acta, nombre_multiplicador, departamento, tipo_cultivo, variedad, lote_registrado, categoria_registrado, ciclo_acta, cantidad_existente, porcentaje_humedad, humedad_final, porcentaje_humedad, peso_inicial_g, PORCENTAJE_GERMINACION, decision"
        Dim c1 As String = ""
        Dim c3 As String = ""
        Dim c4 As String = ""
        Dim c2 As String = ""

        If (TxtProductorGrid.SelectedItem.Text = "Todos") Then
            c1 = " "
        Else
            c1 = "AND  nombre_multiplicador = '" & TxtProductorGrid.SelectedItem.Text & "' "
        End If

        If (TxtDepto.SelectedItem.Text = "Todos") Then
            c2 = " "
        Else
            c2 = "AND  departamento = '" & TxtDepto.SelectedItem.Text & "' "
        End If

        If (DDL_SelCult.SelectedItem.Text = "Todos") Then
            c3 = " "
        Else
            c3 = "AND tipo_cultivo = '" & DDL_SelCult.SelectedItem.Text & "' "
        End If

        If (txtciclo.SelectedItem.Text = "Todos") Then
            c4 = " "
        Else
            c4 = "AND ciclo_acta = '" & txtciclo.SelectedItem.Text & "' "
        End If

        Me.SqlDataSource1.SelectCommand = "SELECT " & cadena & " FROM `vista_acta_lote_multi` WHERE 1 = 1 AND fecha_acta IS NOT NULL AND estado_sena = '1' " & c1 & c3 & c4 & c2 & " ORDER BY id_acta DESC"

        GridDatos.DataBind()
    End Sub
    Protected Sub TxtProductorGrid_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtProductorGrid.SelectedIndexChanged
        llenagrid()
    End Sub
    Protected Sub DDL_SelCult_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DDL_SelCult.SelectedIndexChanged
        llenagrid()
    End Sub
    Protected Sub txtciclo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtciclo.SelectedIndexChanged
        llenagrid()
    End Sub

    Protected Sub SqlDataSource1_Selected(sender As Object, e As SqlDataSourceStatusEventArgs) Handles SqlDataSource1.Selected

        lblTotalClientes.Text = e.AffectedRows.ToString()

    End Sub
    Protected Function SeleccionarItemEnDropDownList(ByVal Prodname As DropDownList, ByVal DtCombo As String)
        For Each item As ListItem In Prodname.Items
            If item.Text = DtCombo Then
                Prodname.SelectedValue = item.Value
                Return True ' Se encontró una coincidencia, devolver verdadero
            End If
        Next
        ' No se encontró ninguna coincidencia
        Return 0
    End Function
    Protected Sub GridDatos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridDatos.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)

        If (e.CommandName = "Editar") Then
            DivGrid.Visible = "false"
            DivActa.Visible = "true"
            DivActaInfo.Visible = "true"
            btnGuardarActa.Visible = True
            BtnNuevo.Visible = True
            btnGuardarActa.Text = "Actualizar"
            txtrespaldito.Text = "Actualizar"
            Dim gvrow As GridViewRow = GridDatos.Rows(index)

            Dim cadena As String = "*"
            Dim Str As String = "SELECT " & cadena & " FROM sag_analisis_germinacion WHERE  ID_2=" & HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString & ""
            Dim adap As New MySqlDataAdapter(Str, conn)
            Dim dt As New DataTable
            adap.Fill(dt)

            If dt.Rows.Count > 0 Then
                TxtID.Text = HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString

                txtFechaElab.Text = If(dt.Rows(0)("decha_elaboracion_g") Is DBNull.Value, String.Empty, DirectCast(dt.Rows(0)("decha_elaboracion_g"), DateTime).ToString("yyyy-MM-dd"))

                txtEnvase.Text = If(dt.Rows(0)("no_envase") Is DBNull.Value, String.Empty, dt.Rows(0)("no_envase").ToString())

                txtPesoInicialPlanta.Text = If(dt.Rows(0)("peso_inicial_g") Is DBNull.Value, String.Empty, dt.Rows(0)("peso_inicial_g").ToString())
                SeleccionarItemEnDropDownList(DDLGranel, dt.Rows(0)("tipo_granel").ToString())

                txtFechaRecibo.Text = If(dt.Rows(0)("fecha_recibo_g") Is DBNull.Value, String.Empty, DirectCast(dt.Rows(0)("fecha_recibo_g"), DateTime).ToString("yyyy-MM-dd"))
                txtFechaMuestreo.Text = If(dt.Rows(0)("fecha_muestreo_g") Is DBNull.Value, String.Empty, DirectCast(dt.Rows(0)("fecha_muestreo_g"), DateTime).ToString("yyyy-MM-dd"))
                txtHumedadF.Text = If(dt.Rows(0)("humedad_final") Is DBNull.Value, String.Empty, dt.Rows(0)("humedad_final").ToString())
                txtFechaEval.Text = If(dt.Rows(0)("fecha_evaluacion_g") Is DBNull.Value, String.Empty, DirectCast(dt.Rows(0)("fecha_evaluacion_g"), DateTime).ToString("yyyy-MM-dd"))

                SeleccionarItemEnDropDownList(DDLEnvasado, dt.Rows(0)("tipo_envase").ToString())
                SeleccionarItemEnDropDownList(DDLFase, dt.Rows(0)("fase_g").ToString())
                SeleccionarItemEnDropDownList(DDLTamañoMaiz, dt.Rows(0)("tamano_maiz").ToString())
                txtCantInicial.Text = If(dt.Rows(0)("cantidad_inicial") Is DBNull.Value, String.Empty, dt.Rows(0)("cantidad_inicial").ToString())
                txtCantExistente.Text = If(dt.Rows(0)("cantidad_existente") Is DBNull.Value, String.Empty, dt.Rows(0)("cantidad_existente").ToString())
                txtCamaraNo.Text = If(dt.Rows(0)("no_camara") Is DBNull.Value, String.Empty, dt.Rows(0)("no_camara").ToString())
                txtPerimetro.Text = If(dt.Rows(0)("perimetro") Is DBNull.Value, String.Empty, dt.Rows(0)("perimetro").ToString())

                txtCERTISEM.Text = If(dt.Rows(0)("certisem") Is DBNull.Value, String.Empty, dt.Rows(0)("certisem").ToString())
                txtFechaCERTISEM.Text = If(dt.Rows(0)("fecha_certisem") Is DBNull.Value, String.Empty, DirectCast(dt.Rows(0)("fecha_certisem"), DateTime).ToString("yyyy-MM-dd"))
                txtPlanta.Text = If(dt.Rows(0)("planta_g") Is DBNull.Value, String.Empty, dt.Rows(0)("planta_g").ToString())
                txtFechaPlanta.Text = If(dt.Rows(0)("fecha_planta_g") Is DBNull.Value, String.Empty, DirectCast(dt.Rows(0)("fecha_planta_g"), DateTime).ToString("yyyy-MM-dd"))

                txtSemillaPura.Text = If(dt.Rows(0)("semilla_pura") Is DBNull.Value, String.Empty, dt.Rows(0)("semilla_pura").ToString())
                txtSemillaOtroCult.Text = If(dt.Rows(0)("semilla_otro_cultivo") Is DBNull.Value, String.Empty, dt.Rows(0)("semilla_otro_cultivo").ToString())
                txtSemillaMalezas.Text = If(dt.Rows(0)("semilla_maleza") Is DBNull.Value, String.Empty, dt.Rows(0)("semilla_maleza").ToString())
                txtSemillaInerte.Text = If(dt.Rows(0)("materia_inerte") Is DBNull.Value, String.Empty, dt.Rows(0)("materia_inerte").ToString())

                txtCam1PlanNorm.Text = If(dt.Rows(0)("plantulas_normales_1") Is DBNull.Value, String.Empty, dt.Rows(0)("plantulas_normales_1").ToString())
                txtCam1PlanAnor.Text = If(dt.Rows(0)("plantulas_anormales_1") Is DBNull.Value, String.Empty, dt.Rows(0)("plantulas_anormales_1").ToString())
                txtCam1SemiMuer.Text = If(dt.Rows(0)("semilla_muerta_1") Is DBNull.Value, String.Empty, dt.Rows(0)("semilla_muerta_1").ToString())
                txtCam1SemiDura.Text = If(dt.Rows(0)("semillas_duras_1") Is DBNull.Value, String.Empty, dt.Rows(0)("semillas_duras_1").ToString())
                txtCam1Debiles.Text = If(dt.Rows(0)("semillas_debiles_1") Is DBNull.Value, String.Empty, dt.Rows(0)("semillas_debiles_1").ToString())
                txtCam1Mezcla.Text = If(dt.Rows(0)("semilla_mezcla_1") Is DBNull.Value, String.Empty, dt.Rows(0)("semilla_mezcla_1").ToString())
                txtCam1NoDias.Text = If(dt.Rows(0)("no_dias_1") Is DBNull.Value, String.Empty, dt.Rows(0)("no_dias_1").ToString())

                txtCam2PlanNorm.Text = If(dt.Rows(0)("plantulas_normales_2") Is DBNull.Value, String.Empty, dt.Rows(0)("plantulas_normales_2").ToString())
                txtCam2PlanAnor.Text = If(dt.Rows(0)("plantulas_anormales_2") Is DBNull.Value, String.Empty, dt.Rows(0)("plantulas_anormales_2").ToString())
                txtCam2SemiMuer.Text = If(dt.Rows(0)("semilla_muerta_2") Is DBNull.Value, String.Empty, dt.Rows(0)("semilla_muerta_2").ToString())
                txtCam2SemiDura.Text = If(dt.Rows(0)("semillas_duras_2") Is DBNull.Value, String.Empty, dt.Rows(0)("semillas_duras_2").ToString())
                txtCam2Debiles.Text = If(dt.Rows(0)("semillas_debiles_2") Is DBNull.Value, String.Empty, dt.Rows(0)("semillas_debiles_2").ToString())
                txtCam2Mezcla.Text = If(dt.Rows(0)("semilla_mezcla_2") Is DBNull.Value, String.Empty, dt.Rows(0)("semilla_mezcla_2").ToString())
                txtCam2NoDias.Text = If(dt.Rows(0)("no_dias_2") Is DBNull.Value, String.Empty, dt.Rows(0)("no_dias_2").ToString())

                txtCam3PlanNorm.Text = If(dt.Rows(0)("plantulas_normales_3") Is DBNull.Value, String.Empty, dt.Rows(0)("plantulas_normales_3").ToString())
                txtCam3PlanAnor.Text = If(dt.Rows(0)("plantulas_anormales_3") Is DBNull.Value, String.Empty, dt.Rows(0)("plantulas_anormales_3").ToString())
                txtCam3SemiMuer.Text = If(dt.Rows(0)("semilla_muerta_3") Is DBNull.Value, String.Empty, dt.Rows(0)("semilla_muerta_3").ToString())
                txtCam3SemiDura.Text = If(dt.Rows(0)("semillas_duras_3") Is DBNull.Value, String.Empty, dt.Rows(0)("semillas_duras_3").ToString())
                txtCam3Debiles.Text = If(dt.Rows(0)("semillas_debiles_3") Is DBNull.Value, String.Empty, dt.Rows(0)("semillas_debiles_3").ToString())
                txtCam3Mezcla.Text = If(dt.Rows(0)("semilla_mezcla_3") Is DBNull.Value, String.Empty, dt.Rows(0)("semilla_mezcla_3").ToString())
                txtCam3NoDias.Text = If(dt.Rows(0)("no_dias_3") Is DBNull.Value, String.Empty, dt.Rows(0)("no_dias_3").ToString())

                txtCam4PlanNorm.Text = If(dt.Rows(0)("plantulas_normales_4") Is DBNull.Value, String.Empty, dt.Rows(0)("plantulas_normales_4").ToString())
                txtCam4PlanAnor.Text = If(dt.Rows(0)("plantulas_anormales_4") Is DBNull.Value, String.Empty, dt.Rows(0)("plantulas_anormales_4").ToString())
                txtCam4SemiMuer.Text = If(dt.Rows(0)("semilla_muerta_4") Is DBNull.Value, String.Empty, dt.Rows(0)("semilla_muerta_4").ToString())
                txtCam4SemiDura.Text = If(dt.Rows(0)("semillas_duras_4") Is DBNull.Value, String.Empty, dt.Rows(0)("semillas_duras_4").ToString())
                txtCam4Debiles.Text = If(dt.Rows(0)("semillas_debiles_4") Is DBNull.Value, String.Empty, dt.Rows(0)("semillas_debiles_4").ToString())
                txtCam4Mezcla.Text = If(dt.Rows(0)("semilla_mezcla_4") Is DBNull.Value, String.Empty, dt.Rows(0)("semilla_mezcla_4").ToString())
                txtCam4NoDias.Text = If(dt.Rows(0)("no_dias_4") Is DBNull.Value, String.Empty, dt.Rows(0)("no_dias_4").ToString())

                txtTotalPlanNorm.Text = If(dt.Rows(0)("plantulas_normales_total") Is DBNull.Value, String.Empty, dt.Rows(0)("plantulas_normales_total").ToString())
                txtTotalPlanAnor.Text = If(dt.Rows(0)("plantulas_anormales_total") Is DBNull.Value, String.Empty, dt.Rows(0)("plantulas_anormales_total").ToString())
                txtTotalSemiMuer.Text = If(dt.Rows(0)("semilla_muerta_total") Is DBNull.Value, String.Empty, dt.Rows(0)("semilla_muerta_total").ToString())
                txtTotalSemiDura.Text = If(dt.Rows(0)("semillas_duras_total") Is DBNull.Value, String.Empty, dt.Rows(0)("semillas_duras_total").ToString())
                txtTotalDebiles.Text = If(dt.Rows(0)("semillas_debiles_total") Is DBNull.Value, String.Empty, dt.Rows(0)("semillas_debiles_total").ToString())
                txtTotalMezcla.Text = If(dt.Rows(0)("semilla_mezcla_total") Is DBNull.Value, String.Empty, dt.Rows(0)("semilla_mezcla_total").ToString())
                txtTotalNoDias.Text = If(dt.Rows(0)("no_dias_total") Is DBNull.Value, String.Empty, dt.Rows(0)("no_dias_total").ToString())

                txtPorcGerm.Text = If(dt.Rows(0)("porcentaje_germnimacion") Is DBNull.Value, String.Empty, dt.Rows(0)("porcentaje_germnimacion").ToString())
                txtObserv.Text = If(dt.Rows(0)("observaciones_g") Is DBNull.Value, String.Empty, dt.Rows(0)("observaciones_g").ToString())
                txtRespMuestreo.Text = If(dt.Rows(0)("responsable_muestreo") Is DBNull.Value, String.Empty, dt.Rows(0)("responsable_muestreo").ToString())
                txtRespAnalisis.Text = If(dt.Rows(0)("responsable_analisis") Is DBNull.Value, String.Empty, dt.Rows(0)("responsable_analisis").ToString())
                SeleccionarItemEnDropDownList(DDL_decision, dt.Rows(0)("decision").ToString())

                llenarCampoLectura(TxtID.Text)
                Verificar()
            Else
                TxtID.Text = HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString

                DivGrid.Visible = "false"
                DivActa.Visible = "true"
                DivActaInfo.Visible = "true"
                btnGuardarActa.Visible = True
                BtnNuevo.Visible = True
                txtrespaldito.Text = "Guardar"
                btnGuardarActa.Text = "Guardar"

                llenarCampoLectura(TxtID.Text)
                Verificar()
            End If

        End If

        If (e.CommandName = "Eliminar") Then
            Dim gvrow As GridViewRow = GridDatos.Rows(index)

            TxtID.Text = HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString


            Label103.Text = "¿Desea eliminar la informacion almacenada que contiene este registro de análisis de germinación?
                      
                    *NOTA: Solo se elimira la informacion que habia registrado el usurio."
            BBorrarsi.Visible = True
            BBorrarno.Visible = True
            BConfirm.Visible = False
            ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)
        End If

        If (e.CommandName = "Imprimir") Then

            Dim gvrow As GridViewRow = GridDatos.Rows(index)
            Dim rptdocument As New ReportDocument
            'nombre de dataset
            Dim ds As New DataSetMultiplicador
            Dim Str As String = "SELECT * FROM vista_acta_lote_multi_germ WHERE ID_acta = @valor"
            Dim adap As New MySqlDataAdapter(Str, conn)
            adap.SelectCommand.Parameters.AddWithValue("@valor", HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString)
            Dim dt As New DataTable

            'nombre de la vista del data set

            adap.Fill(ds, "vista_acta_lote_multi_germ")

            Dim nombre As String

            nombre = "Analisis de Germinación de Semilla " + HttpUtility.HtmlDecode(gvrow.Cells(1).Text).ToString + " " + Today

            rptdocument.Load(Server.MapPath("~/pages/AnalisisGerminacionReport.rpt"))

            rptdocument.SetDataSource(ds)
            Response.Buffer = False


            Response.ClearContent()
            Response.ClearHeaders()

            rptdocument.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, True, nombre)

            Response.End()
            'ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#AdInscrip').modal('show'); });", True)

        End If

        If (e.CommandName = "Subir") Then
            Dim gvrow As GridViewRow = GridDatos.Rows(index)
            TxtID.Text = HttpUtility.HtmlDecode(gvrow.Cells(0).Text).ToString

            div_nuevo_prod.Visible = True
            DivGrid.Visible = False
            DivActa.Visible = False
            DivActaInfo.Visible = False
        End If
    End Sub
    Private Sub llenarCampoLectura(ByVal id As String)
        Dim cadena As String = "fecha_acta, nombre_multiplicador, departamento, municipio, aldea, caserio, no_lote, tipo_cultivo, variedad, categoria_origen, porcentaje_humedad, no_sacos, peso_humedo_QQ, ciclo_acta, lote_registrado, categoria_registrado, tipo_semilla, ano_produ  "
        Dim Str As String = "SELECT " & cadena & " FROM vista_acta_lote_multi WHERE  ID_ACTA=" & id & ""
        Dim adap As New MySqlDataAdapter(Str, conn)
        Dim dt As New DataTable
        adap.Fill(dt)

        Textciclo2.Text = If(dt.Rows(0)("ciclo_acta") Is DBNull.Value, String.Empty, dt.Rows(0)("ciclo_acta").ToString())
        txtFechaSiembra.Text = If(dt.Rows(0)("fecha_acta") Is DBNull.Value, String.Empty, DirectCast(dt.Rows(0)("fecha_acta"), DateTime).ToString("yyyy-MM-dd"))
        txtProductor.Text = If(dt.Rows(0)("nombre_multiplicador") Is DBNull.Value, String.Empty, dt.Rows(0)("nombre_multiplicador").ToString())
        txtCultivo.Text = If(dt.Rows(0)("tipo_cultivo") Is DBNull.Value, String.Empty, dt.Rows(0)("tipo_cultivo").ToString())

        If dt.Rows(0)("tipo_cultivo").ToString() = "Maiz" Then
            DDLTamañoMaiz.Enabled = True
        End If

        txtVariedad.Text = If(dt.Rows(0)("variedad") Is DBNull.Value, String.Empty, dt.Rows(0)("variedad").ToString())
        txtCategoria.Text = If(dt.Rows(0)("categoria_registrado") Is DBNull.Value, String.Empty, dt.Rows(0)("categoria_registrado").ToString())
        txtHumedad.Text = If(dt.Rows(0)("porcentaje_humedad") Is DBNull.Value, String.Empty, dt.Rows(0)("porcentaje_humedad").ToString())
        txtSacos.Text = If(dt.Rows(0)("no_sacos") Is DBNull.Value, String.Empty, dt.Rows(0)("no_sacos").ToString())
        txtPesoH.Text = If(dt.Rows(0)("peso_humedo_QQ") Is DBNull.Value, String.Empty, dt.Rows(0)("peso_humedo_QQ").ToString())
        txtLoteRegi.Text = If(dt.Rows(0)("lote_registrado") Is DBNull.Value, String.Empty, dt.Rows(0)("lote_registrado").ToString())
        If dt.Rows(0)("tipo_semilla").ToString = "Hibrido" Then
            txtHibrido.Text = "Si"
        Else
            txtHibrido.Text = "No"
        End If
        txtaño.Text = If(dt.Rows(0)("ano_produ") Is DBNull.Value, String.Empty, dt.Rows(0)("ano_produ").ToString())
        txtProcedencia.Text = If(dt.Rows(0)("municipio") Is DBNull.Value, String.Empty, dt.Rows(0)("municipio").ToString())
        txtDepartamento.Text = If(dt.Rows(0)("departamento") Is DBNull.Value, String.Empty, dt.Rows(0)("departamento").ToString())
        txtMunicipio.Text = If(dt.Rows(0)("municipio") Is DBNull.Value, String.Empty, dt.Rows(0)("municipio").ToString())
        txtLocallidad.Text = If(dt.Rows(0)("aldea") Is DBNull.Value, String.Empty, dt.Rows(0)("aldea").ToString())

    End Sub
    Protected Sub elminar(sender As Object, e As EventArgs) Handles BBorrarsi.Click
        Dim connectionString As String = conn
        Using connection As New MySqlConnection(connectionString)
            connection.Open()

            Dim query As String = "DELETE FROM sag_analisis_germinacion
                        WHERE
                          id_2 = @id_2;"

            Using cmd As New MySqlCommand(query, connection)

                cmd.Parameters.AddWithValue("@id_2", Convert.ToInt64(TxtID.Text))
                cmd.ExecuteNonQuery()
                connection.Close()
                Response.Redirect(String.Format("~/pages/AnalisisGerminacion.aspx"))
            End Using

        End Using

    End Sub
    Protected Sub PageDropDownList_SelectedIndexChanged(sender As Object, e As EventArgs)
        ' Recupera la fila.
        Dim pagerRow As GridViewRow = GridDatos.BottomPagerRow
        ' Recupera el control DropDownList...
        Dim pageList As DropDownList = CType(pagerRow.Cells(0).FindControl("PageDropDownList"), DropDownList)
        ' Se Establece la propiedad PageIndex para visualizar la página seleccionada...
        GridDatos.PageIndex = pageList.SelectedIndex
        llenagrid()
        'Quita el mensaje de información si lo hubiera...
        'lblInfo.Text = ""
    End Sub

    Protected Sub GridDatos_DataBound(sender As Object, e As EventArgs) Handles GridDatos.DataBound
        If (GridDatos.Rows.Count > 0) Then
            ' Recupera la el PagerRow...
            Dim pagerRow As GridViewRow = GridDatos.BottomPagerRow
            ' Recupera los controles DropDownList y label...
            Dim pageList As DropDownList = CType(pagerRow.Cells(0).FindControl("PageDropDownList"), DropDownList)
            Dim pageLabel As Label = CType(pagerRow.Cells(0).FindControl("CurrentPageLabel"), Label)
            If Not pageList Is Nothing Then
                ' Se crean los valores del DropDownList tomando el número total de páginas...
                Dim i As Integer
                For i = 0 To GridDatos.PageCount - 1
                    ' Se crea un objeto ListItem para representar la  gina...
                    Dim pageNumber As Integer = i + 1
                    Dim item As ListItem = New ListItem(pageNumber.ToString())
                    If i = GridDatos.PageIndex Then
                        item.Selected = True
                    End If
                    ' Se añade el ListItem a la colección de Items del DropDownList...
                    pageList.Items.Add(item)
                Next i
            End If
            If Not pageLabel Is Nothing Then
                ' Calcula el nº de  gina actual...
                Dim currentPage As Integer = GridDatos.PageIndex + 1
                ' Actualiza el Label control con la  gina actual.
                pageLabel.Text = "Página " & currentPage.ToString() & " de " & GridDatos.PageCount.ToString()
            End If
        End If
    End Sub

    Protected Sub btnGuardarActa_Click(sender As Object, e As EventArgs)
        'validarflag = 0
        Verificar()
        If validarflag = 1 Then
            'Funcion para guardar en la BD
            GuardarActa()
        Else
            Label103.Text = "Debe ingresar toda la informacion primero"
            BBorrarsi.Visible = False
            BBorrarno.Visible = False
            BConfirm.Visible = False
            ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)
        End If
    End Sub

    Protected Sub GuardarActa()
        LabelGuardar.Visible = False
        LabelGuardar.Text = ""
        Dim fechaela As Date
        Dim fechaReci As Date
        Dim fechaMues As Date
        Dim fechaEva As Date
        Dim fechaCertim As Date
        Dim fechaPlanta As Date

        If txtrespaldito.Text = "Guardar" Then
            Dim connectionString As String = conn
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "INSERT INTO sag_analisis_germinacion (
                                   decha_elaboracion_g,
                                   no_envase,
                                   peso_inicial_g,
                                   tipo_granel,
                                   fecha_recibo_g,
                                   fecha_muestreo_g,
                                   humedad_final,
                                   fecha_evaluacion_g,
                                   tipo_envase,
                                   fase_g,
                                   tamano_maiz,
                                   cantidad_inicial,
                                   cantidad_existente,
                                   no_camara,
                                   perimetro,
                                   certisem,
                                   fecha_certisem,
                                   planta_g,
                                   fecha_planta_g,
                                   semilla_pura,
                                   semilla_otro_cultivo,
                                   semilla_maleza,
                                   materia_inerte,
                                   plantulas_normales_1,
                                   plantulas_anormales_1,
                                   semilla_muerta_1,
                                   semillas_duras_1,
                                   semillas_debiles_1,
                                   semilla_mezcla_1,
                                   no_dias_1,
                                   plantulas_normales_2,
                                   plantulas_anormales_2,
                                   semilla_muerta_2,
                                   semillas_duras_2,
                                   semillas_debiles_2,
                                   semilla_mezcla_2,
                                   no_dias_2,
                                   plantulas_normales_3,
                                   plantulas_anormales_3,
                                   semilla_muerta_3,
                                   semillas_duras_3,
                                   semillas_debiles_3,
                                   semilla_mezcla_3,
                                   no_dias_3,
                                   plantulas_normales_4,
                                   plantulas_anormales_4,
                                   semilla_muerta_4,
                                   semillas_duras_4,
                                   semillas_debiles_4,
                                   semilla_mezcla_4,
                                   no_dias_4,
                                   plantulas_normales_total,
                                   plantulas_anormales_total,
                                   semilla_muerta_total,
                                   semillas_duras_total,
                                   semillas_debiles_total,
                                   semilla_mezcla_total,
                                   no_dias_total,
                                   porcentaje_germnimacion,
                                   observaciones_g,
                                   responsable_muestreo,
                                   responsable_analisis,
                                   estado,
                                   decision,
                                   id_2) VALUES (
                                   @decha_elaboracion_g,
                                   @no_envase,
                                   @peso_inicial_g,
                                   @tipo_granel,
                                   @fecha_recibo_g,
                                   @fecha_muestreo_g,
                                   @humedad_final,
                                   @fecha_evaluacion_g,
                                   @tipo_envase,
                                   @fase_g,
                                   @tamano_maiz,
                                   @cantidad_inicial,
                                   @cantidad_existente,
                                   @no_camara,
                                   @perimetro,
                                   @certisem,
                                   @fecha_certisem,
                                   @planta_g,
                                   @fecha_planta_g,
                                   @semilla_pura,
                                   @semilla_otro_cultivo,
                                   @semilla_maleza,
                                   @materia_inerte,
                                   @plantulas_normales_1,
                                   @plantulas_anormales_1,
                                   @semilla_muerta_1,
                                   @semillas_duras_1,
                                   @semillas_debiles_1,
                                   @semilla_mezcla_1,
                                   @no_dias_1,
                                   @plantulas_normales_2,
                                   @plantulas_anormales_2,
                                   @semilla_muerta_2,
                                   @semillas_duras_2,
                                   @semillas_debiles_2,
                                   @semilla_mezcla_2,
                                   @no_dias_2,
                                   @plantulas_normales_3,
                                   @plantulas_anormales_3,
                                   @semilla_muerta_3,
                                   @semillas_duras_3,
                                   @semillas_debiles_3,
                                   @semilla_mezcla_3,
                                   @no_dias_3,
                                   @plantulas_normales_4,
                                   @plantulas_anormales_4,
                                   @semilla_muerta_4,
                                   @semillas_duras_4,
                                   @semillas_debiles_4,
                                   @semilla_mezcla_4,
                                   @no_dias_4,
                                   @plantulas_normales_total,
                                   @plantulas_anormales_total,
                                   @semilla_muerta_total,
                                   @semillas_duras_total,
                                   @semillas_debiles_total,
                                   @semilla_mezcla_total,
                                   @no_dias_total,
                                   @porcentaje_germnimacion,
                                   @observaciones_g,
                                   @responsable_muestreo,
                                   @responsable_analisis,
                                   @estado,
                                   @decision,
                                   @id_2
                                   )"
                Using cmd As New MySqlCommand(query, connection)

                    If Date.TryParse(txtFechaElab.Text, fechaela) Then
                        cmd.Parameters.AddWithValue("@decha_elaboracion_g", fechaela.ToString("yyyy-MM-dd"))
                    End If

                    cmd.Parameters.AddWithValue("@no_envase", txtEnvase.Text)

                    cmd.Parameters.AddWithValue("@peso_inicial_g", Convert.ToDecimal(txtPesoInicialPlanta.Text))
                    cmd.Parameters.AddWithValue("@tipo_granel", DDLGranel.SelectedItem.Text)

                    If Date.TryParse(txtFechaRecibo.Text, fechaReci) Then
                        cmd.Parameters.AddWithValue("@fecha_recibo_g", fechaReci.ToString("yyyy-MM-dd"))
                    End If
                    If Date.TryParse(txtFechaMuestreo.Text, fechaMues) Then
                        cmd.Parameters.AddWithValue("@fecha_muestreo_g", fechaMues.ToString("yyyy-MM-dd"))
                    End If
                    cmd.Parameters.AddWithValue("@humedad_final", Convert.ToDecimal(txtHumedadF.Text))
                    If Date.TryParse(txtFechaEval.Text, fechaEva) Then
                        cmd.Parameters.AddWithValue("@fecha_evaluacion_g", fechaEva.ToString("yyyy-MM-dd"))
                    End If

                    cmd.Parameters.AddWithValue("@tipo_envase", DDLEnvasado.SelectedItem.Text)
                    cmd.Parameters.AddWithValue("@fase_g", DDLFase.SelectedItem.Text)
                    cmd.Parameters.AddWithValue("@tamano_maiz", DDLTamañoMaiz.SelectedItem.Text)
                    cmd.Parameters.AddWithValue("@cantidad_inicial", Convert.ToDecimal(txtCantInicial.Text))
                    cmd.Parameters.AddWithValue("@cantidad_existente", Convert.ToDecimal(txtCantExistente.Text))
                    cmd.Parameters.AddWithValue("@no_camara", txtCamaraNo.Text)
                    cmd.Parameters.AddWithValue("@perimetro", Convert.ToDecimal(txtPerimetro.Text))

                    cmd.Parameters.AddWithValue("@certisem", Convert.ToDecimal(txtCERTISEM.Text))
                    If Date.TryParse(txtFechaCERTISEM.Text, fechaCertim) Then
                        cmd.Parameters.AddWithValue("@fecha_certisem", fechaCertim.ToString("yyyy-MM-dd"))
                    End If
                    cmd.Parameters.AddWithValue("@planta_g", txtPlanta.Text)
                    If Date.TryParse(txtFechaPlanta.Text, fechaPlanta) Then
                        cmd.Parameters.AddWithValue("@fecha_planta_g", fechaPlanta.ToString("yyyy-MM-dd"))
                    End If

                    cmd.Parameters.AddWithValue("@semilla_pura", Convert.ToDecimal(txtSemillaPura.Text))
                    cmd.Parameters.AddWithValue("@semilla_otro_cultivo", Convert.ToDecimal(txtSemillaOtroCult.Text))
                    cmd.Parameters.AddWithValue("@semilla_maleza", Convert.ToDecimal(txtSemillaMalezas.Text))
                    cmd.Parameters.AddWithValue("@materia_inerte", Convert.ToDecimal(txtSemillaInerte.Text))

                    cmd.Parameters.AddWithValue("@plantulas_normales_1", Convert.ToInt64(txtCam1PlanNorm.Text))
                    cmd.Parameters.AddWithValue("@plantulas_anormales_1", Convert.ToInt64(txtCam1PlanAnor.Text))
                    cmd.Parameters.AddWithValue("@semilla_muerta_1", Convert.ToInt64(txtCam1SemiMuer.Text))
                    cmd.Parameters.AddWithValue("@semillas_duras_1", Convert.ToInt64(txtCam1SemiDura.Text))
                    cmd.Parameters.AddWithValue("@semillas_debiles_1", Convert.ToInt64(txtCam1Debiles.Text))
                    cmd.Parameters.AddWithValue("@semilla_mezcla_1", Convert.ToInt64(txtCam1Mezcla.Text))
                    cmd.Parameters.AddWithValue("@no_dias_1", Convert.ToInt64(txtCam1NoDias.Text))

                    cmd.Parameters.AddWithValue("@plantulas_normales_2", Convert.ToInt64(txtCam2PlanNorm.Text))
                    cmd.Parameters.AddWithValue("@plantulas_anormales_2", Convert.ToInt64(txtCam2PlanAnor.Text))
                    cmd.Parameters.AddWithValue("@semilla_muerta_2", Convert.ToInt64(txtCam2SemiMuer.Text))
                    cmd.Parameters.AddWithValue("@semillas_duras_2", Convert.ToInt64(txtCam2SemiDura.Text))
                    cmd.Parameters.AddWithValue("@semillas_debiles_2", Convert.ToInt64(txtCam2Debiles.Text))
                    cmd.Parameters.AddWithValue("@semilla_mezcla_2", Convert.ToInt64(txtCam2Mezcla.Text))
                    cmd.Parameters.AddWithValue("@no_dias_2", Convert.ToInt64(txtCam2NoDias.Text))

                    cmd.Parameters.AddWithValue("@plantulas_normales_3", Convert.ToInt64(txtCam3PlanNorm.Text))
                    cmd.Parameters.AddWithValue("@plantulas_anormales_3", Convert.ToInt64(txtCam3PlanAnor.Text))
                    cmd.Parameters.AddWithValue("@semilla_muerta_3", Convert.ToInt64(txtCam3SemiMuer.Text))
                    cmd.Parameters.AddWithValue("@semillas_duras_3", Convert.ToInt64(txtCam3SemiDura.Text))
                    cmd.Parameters.AddWithValue("@semillas_debiles_3", Convert.ToInt64(txtCam3Debiles.Text))
                    cmd.Parameters.AddWithValue("@semilla_mezcla_3", Convert.ToInt64(txtCam3Mezcla.Text))
                    cmd.Parameters.AddWithValue("@no_dias_3", Convert.ToInt64(txtCam3NoDias.Text))

                    cmd.Parameters.AddWithValue("@plantulas_normales_4", Convert.ToInt64(txtCam4PlanNorm.Text))
                    cmd.Parameters.AddWithValue("@plantulas_anormales_4", Convert.ToInt64(txtCam4PlanAnor.Text))
                    cmd.Parameters.AddWithValue("@semilla_muerta_4", Convert.ToInt64(txtCam4SemiMuer.Text))
                    cmd.Parameters.AddWithValue("@semillas_duras_4", Convert.ToInt64(txtCam4SemiDura.Text))
                    cmd.Parameters.AddWithValue("@semillas_debiles_4", Convert.ToInt64(txtCam4Debiles.Text))
                    cmd.Parameters.AddWithValue("@semilla_mezcla_4", Convert.ToInt64(txtCam4Mezcla.Text))
                    cmd.Parameters.AddWithValue("@no_dias_4", Convert.ToInt64(txtCam4NoDias.Text))

                    cmd.Parameters.AddWithValue("@plantulas_normales_total", Convert.ToInt64(txtTotalPlanNorm.Text))
                    cmd.Parameters.AddWithValue("@plantulas_anormales_total", Convert.ToInt64(txtTotalPlanAnor.Text))
                    cmd.Parameters.AddWithValue("@semilla_muerta_total", Convert.ToInt64(txtTotalSemiMuer.Text))
                    cmd.Parameters.AddWithValue("@semillas_duras_total", Convert.ToInt64(txtTotalSemiDura.Text))
                    cmd.Parameters.AddWithValue("@semillas_debiles_total", Convert.ToInt64(txtTotalDebiles.Text))
                    cmd.Parameters.AddWithValue("@semilla_mezcla_total", Convert.ToInt64(txtTotalMezcla.Text))
                    cmd.Parameters.AddWithValue("@no_dias_total", Convert.ToInt64(txtTotalNoDias.Text))

                    cmd.Parameters.AddWithValue("@porcentaje_germnimacion", Convert.ToDecimal(txtPorcGerm.Text))
                    cmd.Parameters.AddWithValue("@observaciones_g", txtObserv.Text)
                    cmd.Parameters.AddWithValue("@responsable_muestreo", txtRespMuestreo.Text)
                    cmd.Parameters.AddWithValue("@responsable_analisis", txtRespAnalisis.Text)
                    cmd.Parameters.AddWithValue("@estado", "1")
                    cmd.Parameters.AddWithValue("@decision", DDL_decision.SelectedItem.Text)
                    cmd.Parameters.AddWithValue("@id_2", Convert.ToInt64(TxtID.Text))

                    cmd.ExecuteNonQuery()
                    connection.Close()

                    Label103.Text = "¡Se ha guardado correctamente el análisis de germinación de muestreo de semilla!"
                    BBorrarsi.Visible = False
                    BBorrarno.Visible = False
                    BConfirm.Visible = True
                    ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)

                    btnGuardarActa.Visible = False
                    BtnImprimir.Visible = False
                    BtnNuevo.Visible = True

                End Using
            End Using
        End If
        If txtrespaldito.Text = "Actualizar" Then
            Dim connectionString As String = conn
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "UPDATE sag_analisis_germinacion
                        SET
                          decha_elaboracion_g = @decha_elaboracion_g,
                          no_envase = @no_envase,
                          peso_inicial_g = @peso_inicial_g,
                          tipo_granel = @tipo_granel,
                          fecha_recibo_g = @fecha_recibo_g,
                          fecha_muestreo_g = @fecha_muestreo_g,
                          humedad_final = @humedad_final,
                          fecha_evaluacion_g = @fecha_evaluacion_g,
                          tipo_envase = @tipo_envase,
                          fase_g = @fase_g,
                          tamano_maiz = @tamano_maiz,
                          cantidad_inicial = @cantidad_inicial,
                          cantidad_existente = @cantidad_existente,
                          no_camara = @no_camara,
                          perimetro = @perimetro,
                          certisem = @certisem,
                          fecha_certisem = @fecha_certisem,
                          planta_g = @planta_g,
                          fecha_planta_g = @fecha_planta_g,
                          semilla_pura = @semilla_pura,
                          semilla_otro_cultivo = @semilla_otro_cultivo,
                          semilla_maleza = @semilla_maleza,
                          materia_inerte = @materia_inerte,
                          plantulas_normales_1 = @plantulas_normales_1,
                          plantulas_anormales_1 = @plantulas_anormales_1,
                          semilla_muerta_1 = @semilla_muerta_1,
                          semillas_duras_1 = @semillas_duras_1,
                          semillas_debiles_1 = @semillas_debiles_1,
                          semilla_mezcla_1 = @semilla_mezcla_1,
                          no_dias_1 = @no_dias_1,
                          plantulas_normales_2 = @plantulas_normales_2,
                          plantulas_anormales_2 = @plantulas_anormales_2,
                          semilla_muerta_2 = @semilla_muerta_2,
                          semillas_duras_2 = @semillas_duras_2,
                          semillas_debiles_2 = @semillas_debiles_2,
                          semilla_mezcla_2 = @semilla_mezcla_2,
                          no_dias_2 = @no_dias_2,
                          plantulas_normales_3 = @plantulas_normales_3,
                          plantulas_anormales_3 = @plantulas_anormales_3,
                          semilla_muerta_3 = @semilla_muerta_3,
                          semillas_duras_3 = @semillas_duras_3,
                          semillas_debiles_3 = @semillas_debiles_3,
                          semilla_mezcla_3 = @semilla_mezcla_3,
                          no_dias_3 = @no_dias_3,
                          plantulas_normales_4 = @plantulas_normales_4,
                          plantulas_anormales_4 = @plantulas_anormales_4,
                          semilla_muerta_4 = @semilla_muerta_4,
                          semillas_duras_4 = @semillas_duras_4,
                          semillas_debiles_4 = @semillas_debiles_4,
                          semilla_mezcla_4 = @semilla_mezcla_4,
                          no_dias_4 = @no_dias_4,
                          plantulas_normales_total = @plantulas_normales_total,
                          plantulas_anormales_total = @plantulas_anormales_total,
                          semilla_muerta_total = @semilla_muerta_total,
                          semillas_duras_total = @semillas_duras_total,
                          semillas_debiles_total = @semillas_debiles_total,
                          semilla_mezcla_total = @semilla_mezcla_total,
                          no_dias_total =  @no_dias_total,
                          porcentaje_germnimacion = @porcentaje_germnimacion,
                          observaciones_g = @observaciones_g,
                          responsable_muestreo = @responsable_muestreo,
                          responsable_analisis = @responsable_analisis,
                          decision = @decision
                        WHERE
                          id_2 = @id_2;"
                Using cmd As New MySqlCommand(query, connection)

                    If Date.TryParse(txtFechaElab.Text, fechaela) Then
                        cmd.Parameters.AddWithValue("@decha_elaboracion_g", fechaela.ToString("yyyy-MM-dd"))
                    End If

                    cmd.Parameters.AddWithValue("@no_envase", txtEnvase.Text)

                    cmd.Parameters.AddWithValue("@peso_inicial_g", Convert.ToDecimal(txtPesoInicialPlanta.Text))
                    cmd.Parameters.AddWithValue("@tipo_granel", DDLGranel.SelectedItem.Text)

                    If Date.TryParse(txtFechaRecibo.Text, fechaReci) Then
                        cmd.Parameters.AddWithValue("@fecha_recibo_g", fechaReci.ToString("yyyy-MM-dd"))
                    End If
                    If Date.TryParse(txtFechaMuestreo.Text, fechaMues) Then
                        cmd.Parameters.AddWithValue("@fecha_muestreo_g", fechaMues.ToString("yyyy-MM-dd"))
                    End If
                    cmd.Parameters.AddWithValue("@humedad_final", Convert.ToDecimal(txtHumedadF.Text))
                    If Date.TryParse(txtFechaEval.Text, fechaEva) Then
                        cmd.Parameters.AddWithValue("@fecha_evaluacion_g", fechaEva.ToString("yyyy-MM-dd"))
                    End If

                    cmd.Parameters.AddWithValue("@tipo_envase", DDLEnvasado.SelectedItem.Text)
                    cmd.Parameters.AddWithValue("@fase_g", DDLFase.SelectedItem.Text)
                    cmd.Parameters.AddWithValue("@tamano_maiz", DDLTamañoMaiz.SelectedItem.Text)
                    cmd.Parameters.AddWithValue("@cantidad_inicial", Convert.ToDecimal(txtCantInicial.Text))
                    cmd.Parameters.AddWithValue("@cantidad_existente", Convert.ToDecimal(txtCantExistente.Text))
                    cmd.Parameters.AddWithValue("@no_camara", txtCamaraNo.Text)
                    cmd.Parameters.AddWithValue("@perimetro", Convert.ToDecimal(txtPerimetro.Text))

                    cmd.Parameters.AddWithValue("@certisem", Convert.ToDecimal(txtCERTISEM.Text))
                    If Date.TryParse(txtFechaCERTISEM.Text, fechaCertim) Then
                        cmd.Parameters.AddWithValue("@fecha_certisem", fechaCertim.ToString("yyyy-MM-dd"))
                    End If
                    cmd.Parameters.AddWithValue("@planta_g", txtPlanta.Text)
                    If Date.TryParse(txtFechaPlanta.Text, fechaPlanta) Then
                        cmd.Parameters.AddWithValue("@fecha_planta_g", fechaPlanta.ToString("yyyy-MM-dd"))
                    End If

                    cmd.Parameters.AddWithValue("@semilla_pura", Convert.ToDecimal(txtSemillaPura.Text))
                    cmd.Parameters.AddWithValue("@semilla_otro_cultivo", Convert.ToDecimal(txtSemillaOtroCult.Text))
                    cmd.Parameters.AddWithValue("@semilla_maleza", Convert.ToDecimal(txtSemillaMalezas.Text))
                    cmd.Parameters.AddWithValue("@materia_inerte", Convert.ToDecimal(txtSemillaInerte.Text))

                    cmd.Parameters.AddWithValue("@plantulas_normales_1", Convert.ToInt64(txtCam1PlanNorm.Text))
                    cmd.Parameters.AddWithValue("@plantulas_anormales_1", Convert.ToInt64(txtCam1PlanAnor.Text))
                    cmd.Parameters.AddWithValue("@semilla_muerta_1", Convert.ToInt64(txtCam1SemiMuer.Text))
                    cmd.Parameters.AddWithValue("@semillas_duras_1", Convert.ToInt64(txtCam1SemiDura.Text))
                    cmd.Parameters.AddWithValue("@semillas_debiles_1", Convert.ToInt64(txtCam1Debiles.Text))
                    cmd.Parameters.AddWithValue("@semilla_mezcla_1", Convert.ToInt64(txtCam1Mezcla.Text))
                    cmd.Parameters.AddWithValue("@no_dias_1", Convert.ToInt64(txtCam1NoDias.Text))

                    cmd.Parameters.AddWithValue("@plantulas_normales_2", Convert.ToInt64(txtCam2PlanNorm.Text))
                    cmd.Parameters.AddWithValue("@plantulas_anormales_2", Convert.ToInt64(txtCam2PlanAnor.Text))
                    cmd.Parameters.AddWithValue("@semilla_muerta_2", Convert.ToInt64(txtCam2SemiMuer.Text))
                    cmd.Parameters.AddWithValue("@semillas_duras_2", Convert.ToInt64(txtCam2SemiDura.Text))
                    cmd.Parameters.AddWithValue("@semillas_debiles_2", Convert.ToInt64(txtCam2Debiles.Text))
                    cmd.Parameters.AddWithValue("@semilla_mezcla_2", Convert.ToInt64(txtCam2Mezcla.Text))
                    cmd.Parameters.AddWithValue("@no_dias_2", Convert.ToInt64(txtCam2NoDias.Text))

                    cmd.Parameters.AddWithValue("@plantulas_normales_3", Convert.ToInt64(txtCam3PlanNorm.Text))
                    cmd.Parameters.AddWithValue("@plantulas_anormales_3", Convert.ToInt64(txtCam3PlanAnor.Text))
                    cmd.Parameters.AddWithValue("@semilla_muerta_3", Convert.ToInt64(txtCam3SemiMuer.Text))
                    cmd.Parameters.AddWithValue("@semillas_duras_3", Convert.ToInt64(txtCam3SemiDura.Text))
                    cmd.Parameters.AddWithValue("@semillas_debiles_3", Convert.ToInt64(txtCam3Debiles.Text))
                    cmd.Parameters.AddWithValue("@semilla_mezcla_3", Convert.ToInt64(txtCam3Mezcla.Text))
                    cmd.Parameters.AddWithValue("@no_dias_3", Convert.ToInt64(txtCam3NoDias.Text))

                    cmd.Parameters.AddWithValue("@plantulas_normales_4", Convert.ToInt64(txtCam4PlanNorm.Text))
                    cmd.Parameters.AddWithValue("@plantulas_anormales_4", Convert.ToInt64(txtCam4PlanAnor.Text))
                    cmd.Parameters.AddWithValue("@semilla_muerta_4", Convert.ToInt64(txtCam4SemiMuer.Text))
                    cmd.Parameters.AddWithValue("@semillas_duras_4", Convert.ToInt64(txtCam4SemiDura.Text))
                    cmd.Parameters.AddWithValue("@semillas_debiles_4", Convert.ToInt64(txtCam4Debiles.Text))
                    cmd.Parameters.AddWithValue("@semilla_mezcla_4", Convert.ToInt64(txtCam4Mezcla.Text))
                    cmd.Parameters.AddWithValue("@no_dias_4", Convert.ToInt64(txtCam4NoDias.Text))

                    cmd.Parameters.AddWithValue("@plantulas_normales_total", Convert.ToInt64(txtTotalPlanNorm.Text))
                    cmd.Parameters.AddWithValue("@plantulas_anormales_total", Convert.ToInt64(txtTotalPlanAnor.Text))
                    cmd.Parameters.AddWithValue("@semilla_muerta_total", Convert.ToInt64(txtTotalSemiMuer.Text))
                    cmd.Parameters.AddWithValue("@semillas_duras_total", Convert.ToInt64(txtTotalSemiDura.Text))
                    cmd.Parameters.AddWithValue("@semillas_debiles_total", Convert.ToInt64(txtTotalDebiles.Text))
                    cmd.Parameters.AddWithValue("@semilla_mezcla_total", Convert.ToInt64(txtTotalMezcla.Text))
                    cmd.Parameters.AddWithValue("@no_dias_total", Convert.ToInt64(txtTotalNoDias.Text))

                    cmd.Parameters.AddWithValue("@porcentaje_germnimacion", Convert.ToDecimal(txtPorcGerm.Text))
                    cmd.Parameters.AddWithValue("@observaciones_g", txtObserv.Text)
                    cmd.Parameters.AddWithValue("@responsable_muestreo", txtRespMuestreo.Text)
                    cmd.Parameters.AddWithValue("@responsable_analisis", txtRespAnalisis.Text)
                    cmd.Parameters.AddWithValue("@decision", DDL_decision.SelectedItem.Text)
                    cmd.Parameters.AddWithValue("@id_2", Convert.ToInt64(TxtID.Text))

                    cmd.ExecuteNonQuery()
                    connection.Close()

                    Label103.Text = "¡Se ha editado correctamente el análisis de germinación de muestreo de semilla!"
                    BBorrarsi.Visible = False
                    BBorrarno.Visible = False
                    BConfirm.Visible = True
                    ClientScript.RegisterStartupScript(Me.GetType(), "JS", "$(function () { $('#DeleteModal').modal('show'); });", True)

                    btnGuardarActa.Visible = False
                    BtnImprimir.Visible = False
                    BtnNuevo.Visible = True

                End Using
            End Using
        End If
    End Sub
    Protected Sub Verificar()

        '1
        If String.IsNullOrEmpty(txtFechaElab.Text) Then
            lblFechaElab.Text = "*"
            validarflag = 0
        Else
            lblFechaElab.Text = ""
            validarflag += 1
        End If
        ''2
        If String.IsNullOrEmpty(txtEnvase.Text) Then
            lblEnvase.Text = "*"
            validarflag = 0
        Else
            lblEnvase.Text = ""
            validarflag += 1
        End If
        ''3
        If String.IsNullOrEmpty(txtPesoInicialPlanta.Text) Then
            lblPesoInicialPlanta.Text = "*"
            validarflag = 0
        Else
            lblPesoInicialPlanta.Text = ""
            validarflag += 1
        End If
        ''4
        If String.IsNullOrEmpty(txtFechaRecibo.Text) Then
            lblFechaRecibo.Text = "*"
            validarflag = 0
        Else
            lblFechaRecibo.Text = ""
            validarflag += 1
        End If
        '5
        If (DDLGranel.SelectedItem.Text = " ") Then
            lblGranel.Text = "*"
            validarflag = 0
        Else
            lblGranel.Text = ""
            validarflag += 1
        End If
        '6
        If String.IsNullOrEmpty(txtFechaMuestreo.Text) Then
            lblFechaMuestreo.Text = "*"
            validarflag = 0
        Else
            lblFechaMuestreo.Text = ""
            validarflag += 1
        End If
        '7
        If String.IsNullOrEmpty(txtHumedadF.Text) Then
            lblHumedadF.Text = "*"
            validarflag = 0
        Else
            lblHumedadF.Text = ""
            validarflag += 1
        End If
        '8
        If String.IsNullOrEmpty(txtFechaEval.Text) Then
            lblFechaEval.Text = "*"
            validarflag = 0
        Else
            lblFechaEval.Text = ""
            validarflag += 1
        End If
        '9
        If (DDLEnvasado.SelectedItem.Text = " ") Then
            lblEnvasado.Text = "*"
            validarflag = 0
        Else
            lblEnvasado.Text = ""
            validarflag += 1
        End If
        '10
        If (DDLFase.SelectedItem.Text = " ") Then
            lblFase.Text = "*"
            validarflag = 0
        Else
            lblFase.Text = ""
            validarflag += 1
        End If
        '11
        If txtCultivo.Text = "Maiz" Then
            If (DDLTamañoMaiz.SelectedItem.Text = " ") Then
                lblTamañoMaiz.Text = "*"
                validarflag = 0
            Else
                lblTamañoMaiz.Text = ""
                validarflag += 1
            End If
        End If
        '12
        If String.IsNullOrEmpty(txtCantInicial.Text) Then
            lblCantInicial.Text = "*"
            validarflag = 0
        Else
            lblCantInicial.Text = ""
            validarflag += 1
        End If
        '13
        If String.IsNullOrEmpty(txtCantExistente.Text) Then
            lblCantExistente.Text = "*"
            validarflag = 0
        Else
            lblCantExistente.Text = ""
            validarflag += 1
        End If
        '14
        If String.IsNullOrEmpty(txtCamaraNo.Text) Then
            lblCamaraNo.Text = "*"
            validarflag = 0
        Else
            lblCamaraNo.Text = ""
            validarflag += 1
        End If
        '15
        If String.IsNullOrEmpty(txtPerimetro.Text) Then
            lblPerimetro.Text = "*"
            validarflag = 0
        Else
            lblPerimetro.Text = ""
            validarflag += 1
        End If
        '16
        If String.IsNullOrEmpty(txtCERTISEM.Text) Then
            lblCERTISEM.Text = "*"
            validarflag = 0
        Else
            lblCERTISEM.Text = ""
            validarflag += 1
        End If
        '17
        If String.IsNullOrEmpty(txtFechaCERTISEM.Text) Then
            lblFechaCERTISEM.Text = "*"
            validarflag = 0
        Else
            lblFechaCERTISEM.Text = ""
            validarflag += 1
        End If
        '18
        If String.IsNullOrEmpty(txtPlanta.Text) Then
            lblPlanta.Text = "*"
            validarflag = 0
        Else
            lblPlanta.Text = ""
            validarflag += 1
        End If
        '19
        If String.IsNullOrEmpty(txtFechaPlanta.Text) Then
            lblFechaPlanta.Text = "*"
            validarflag = 0
        Else
            lblFechaPlanta.Text = ""
            validarflag += 1
        End If
        '20
        If String.IsNullOrEmpty(txtSemillaPura.Text) Then
            lblSemillaPura.Text = "*"
            validarflag = 0
        Else
            lblSemillaPura.Text = ""
            validarflag += 1
        End If
        '21
        If String.IsNullOrEmpty(txtSemillaOtroCult.Text) Then
            lblSemillaOtroCult.Text = "*"
            validarflag = 0
        Else
            lblSemillaOtroCult.Text = ""
            validarflag += 1
        End If
        '22
        If String.IsNullOrEmpty(txtSemillaMalezas.Text) Then
            lblSemillaMalezas.Text = "*"
            validarflag = 0
        Else
            lblSemillaMalezas.Text = ""
            validarflag += 1
        End If
        '23
        If String.IsNullOrEmpty(txtSemillaInerte.Text) Then
            lblSemillaInerte.Text = "*"
            validarflag = 0
        Else
            lblSemillaInerte.Text = ""
            validarflag += 1
        End If
        '24
        If String.IsNullOrEmpty(txtCam1PlanNorm.Text) Or String.IsNullOrEmpty(txtCam1PlanAnor.Text) Or String.IsNullOrEmpty(txtCam1SemiMuer.Text) Or String.IsNullOrEmpty(txtCam1SemiDura.Text) Or String.IsNullOrEmpty(txtCam1Debiles.Text) Or String.IsNullOrEmpty(txtCam1Mezcla.Text) Or String.IsNullOrEmpty(txtCam1NoDias.Text) Or
           String.IsNullOrEmpty(txtCam2PlanNorm.Text) Or String.IsNullOrEmpty(txtCam2PlanAnor.Text) Or String.IsNullOrEmpty(txtCam2SemiMuer.Text) Or String.IsNullOrEmpty(txtCam2SemiDura.Text) Or String.IsNullOrEmpty(txtCam2Debiles.Text) Or String.IsNullOrEmpty(txtCam2Mezcla.Text) Or String.IsNullOrEmpty(txtCam2NoDias.Text) Or
           String.IsNullOrEmpty(txtCam3PlanNorm.Text) Or String.IsNullOrEmpty(txtCam3PlanAnor.Text) Or String.IsNullOrEmpty(txtCam3SemiMuer.Text) Or String.IsNullOrEmpty(txtCam3SemiDura.Text) Or String.IsNullOrEmpty(txtCam3Debiles.Text) Or String.IsNullOrEmpty(txtCam3Mezcla.Text) Or String.IsNullOrEmpty(txtCam3NoDias.Text) Or
           String.IsNullOrEmpty(txtCam4PlanNorm.Text) Or String.IsNullOrEmpty(txtCam4PlanAnor.Text) Or String.IsNullOrEmpty(txtCam4SemiMuer.Text) Or String.IsNullOrEmpty(txtCam4SemiDura.Text) Or String.IsNullOrEmpty(txtCam4Debiles.Text) Or String.IsNullOrEmpty(txtCam4Mezcla.Text) Or String.IsNullOrEmpty(txtCam4NoDias.Text) Then
            lblmensaje.Text = "Seleccione todas las repeticiones."
            validarflag = 0
        Else
            lblmensaje.Text = ""
            validarflag += 1
        End If
        '25
        If String.IsNullOrEmpty(txtObserv.Text) Then
            lblObserv.Text = "*"
            validarflag = 0
        Else
            lblObserv.Text = ""
            validarflag += 1
        End If
        '26
        If String.IsNullOrEmpty(txtRespMuestreo.Text) Then
            lblRespMuestreo.Text = "*"
            validarflag = 0
        Else
            lblRespMuestreo.Text = ""
            validarflag += 1
        End If
        '27
        If String.IsNullOrEmpty(txtRespAnalisis.Text) Then
            lblRespAnalisis.Text = "*"
            validarflag = 0
        Else
            lblRespAnalisis.Text = ""
            validarflag += 1
        End If
        '28
        If (DDL_decision.SelectedItem.Text = " ") Then
            lbldecision.Text = "*"
            validarflag = 0
        Else
            lbldecision.Text = ""
            validarflag += 1
        End If

        If validarflag = 28 Or validarflag = 27 Then
            validarflag = 1
        Else
            validarflag = 0
        End If
    End Sub
    Private Sub exportar()

        Dim cadena As String = "ID_MULTI, NOMBRE_MULTIPLICADOR, REPRESENTANTE_LEGAL, TELEFONO_MULTIPLICADOR, DEPARTAMENTO, MUNICIPIO, ALDEA, CASERIO, ESTADO_MULTI, ID_LOTE, CATEGORIA_ORIGEN, TIPO_CULTIVO, VARIEDAD, PRODUCTOR, NO_LOTE, ESTADO_LOTE, ANO_PRODU, TIPO_SEMILLA, ID_ACTA, FECHA_ACTA, PORCENTAJE_HUMEDAD, ESTADO_SENA, NO_SACOS, PESO_HUMEDO_QQ, PESO_MATERIA_PRIMA_QQ_PORCE_HUMEDAD, SEMILLA_QQ_ORO, SEMILLA_QQ_CONSUMO, SEMILLA_QQ_BASURA, SEMILLA_QQ_TOTAL, OBSERVACIONES, TARA, CICLO_ACTA, PESO_NETO, PESO_LB, LOTE_REGISTRADO, CATEGORIA_REGISTRADO, RENDIMIETO_ORO_PESO, id, decha_elaboracion_g, no_envase, peso_inicial_g, tipo_granel, fecha_recibo_g, fecha_muestreo_g, humedad_final, fecha_evaluacion_g, tipo_envase, fase_g, tamano_maiz, cantidad_inicial, cantidad_existente, no_camara, perimetro, certisem, fecha_certisem, planta_g, fecha_planta_g, semilla_pura, semilla_otro_cultivo, semilla_maleza, materia_inerte, plantulas_normales_1, plantulas_anormales_1, semilla_muerta_1, semillas_duras_1, semillas_debiles_1, semilla_mezcla_1, no_dias_1, plantulas_normales_2, plantulas_anormales_2, semilla_muerta_2, semillas_duras_2, semillas_debiles_2, semilla_mezcla_2, no_dias_2, plantulas_normales_3, plantulas_anormales_3, semilla_muerta_3, semillas_duras_3, semillas_debiles_3, semilla_mezcla_3, no_dias_3, plantulas_normales_4, plantulas_anormales_4, semilla_muerta_4, semillas_duras_4, semillas_debiles_4, semilla_mezcla_4, no_dias_4, plantulas_normales_total, plantulas_anormales_total, semilla_muerta_total, semillas_duras_total, semillas_debiles_total, semilla_mezcla_total, no_dias_total, porcentaje_germnimacion, observaciones_g, responsable_muestreo, responsable_analisis, decision"
        Dim query As String = ""
        Dim c1 As String = ""
        Dim c3 As String = ""
        Dim c4 As String = ""
        Dim c2 As String = ""

        If (TxtProductorGrid.SelectedItem.Text = "Todos") Then
            c1 = " "
        Else
            c1 = "AND  nombre_multiplicador = '" & TxtProductorGrid.SelectedItem.Text & "' "
        End If

        If (TxtDepto.SelectedItem.Text = "Todos") Then
            c2 = " "
        Else
            c2 = "AND  departamento = '" & TxtDepto.SelectedItem.Text & "' "
        End If

        If (DDL_SelCult.SelectedItem.Text = "Todos") Then
            c3 = " "
        Else
            c3 = "AND tipo_cultivo = '" & DDL_SelCult.SelectedItem.Text & "' "
        End If

        If (txtciclo.SelectedItem.Text = "Todos") Then
            c4 = " "
        Else
            c4 = "AND ciclo_acta = '" & txtciclo.SelectedItem.Text & "' "
        End If


        query = "SELECT " & cadena & " FROM `vista_acta_lote_multi_germ` WHERE 1 = 1 AND fecha_acta IS NOT NULL AND estado_sena = '1' " & c1 & c3 & c4 & c2

        Using con As New MySqlConnection(conn)
            Using cmd As New MySqlCommand(query)
                Using sda As New MySqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using ds As New DataSet()
                        sda.Fill(ds)

                        ' Set Name of DataTables.
                        ds.Tables(0).TableName = "Analisis de Germinación"

                        Using wb As New XLWorkbook()
                            ' Add DataTable as Worksheet.
                            wb.Worksheets.Add(ds.Tables(0), "vista_acta_lote_multi_germ")

                            ' Set auto width for all columns based on content.
                            wb.Worksheet(1).Columns().AdjustToContents()

                            ' Export the Excel file.
                            Response.Clear()
                            Response.Buffer = True
                            Response.Charset = ""
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                            Response.AddHeader("content-disposition", "attachment;filename=Analisis_de_Germinación.xlsx")
                            Using MyMemoryStream As New MemoryStream()
                                wb.SaveAs(MyMemoryStream)
                                MyMemoryStream.WriteTo(Response.OutputStream)
                                Response.Flush()
                                Response.End()
                            End Using
                        End Using
                    End Using
                End Using
            End Using
        End Using
    End Sub


    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        exportar()
    End Sub

    Protected Sub CrearIdentificador(d1 As String, m2 As String, a3 As String, c4 As String)
        Dim dep As String = d1
        Dim mun As String = m2
        Dim ald As String = a3
        Dim cas As String = c4

        Dim resultado As String = String.Format("{0}-{1}-{2}-{3}", dep, mun, ald, cas)

        'Textrespaldo.Text = resultado
    End Sub


    Protected Sub GridDatos_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GridDatos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' Obtén los datos de la fila actual
            Dim estimadoProduccion As String = DataBinder.Eval(e.Row.DataItem, "cantidad_existente").ToString()

            ' Encuentra los botones en la fila por índice
            Dim btnEditar As Button = DirectCast(e.Row.Cells(15).Controls(0), Button)
            Dim btnImprimir As Button = DirectCast(e.Row.Cells(17).Controls(0), Button)

            ' Modifica el texto y el color de los botones según la lógica que desees
            If Not String.IsNullOrEmpty(estimadoProduccion) Then
                btnEditar.Visible = True
                btnEditar.Text = "Editar"
                btnEditar.CssClass = "btn btn-primary"
                btnEditar.Style("background-color") = "#007bff"
            Else
                btnEditar.Visible = True
                btnEditar.Text = "Agregar"
            End If

            If Not String.IsNullOrEmpty(estimadoProduccion) Then
                btnImprimir.Visible = True
            Else
                btnImprimir.Visible = False
            End If
        End If
    End Sub

    Protected Sub descargaPDF(sender As Object, e As EventArgs)
        Dim rptdocument As New ReportDocument
        'nombre de dataset
        Dim ds As New DataSetMultiplicador
        Dim Str As String = "SELECT * FROM sag_registro_senasa WHERE nombre_multiplicador = @valor"
        Dim adap As New MySqlDataAdapter(Str, conn)
        adap.SelectCommand.Parameters.AddWithValue("@valor", TxtID.Text)
        Dim dt As New DataTable

        'nombre de la vista del data set

        adap.Fill(ds, "sag_registro_senasa")

        Dim nombre As String

        nombre = " Datos del Multiplicador " + Today

        rptdocument.Load(Server.MapPath("~/pages/AgregarMultiplicadorReport2.rpt"))

        rptdocument.SetDataSource(ds)
        Response.Buffer = False


        Response.ClearContent()
        Response.ClearHeaders()

        rptdocument.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, True, nombre)

        Response.End()
    End Sub

    Protected Sub BConfirm_Click(sender As Object, e As EventArgs)
        Response.Redirect(String.Format("~/pages/AnalisisGerminacion.aspx"))
    End Sub
    Private Function FileUploadToBytes(fileUpload As FileUpload) As Byte()
        Using stream As System.IO.Stream = fileUpload.PostedFile.InputStream
            Dim length As Integer = fileUpload.PostedFile.ContentLength
            Dim bytes As Byte() = New Byte(length - 1) {}
            stream.Read(bytes, 0, length)
            Return bytes
        End Using
    End Function
    Private Function EsExtensionValida(fileName As String) As Boolean
        Dim extension As String = Path.GetExtension(fileName)
        Dim esValida As Boolean = False
        If extension.Equals(".pdf", StringComparison.OrdinalIgnoreCase) Then
            esValida = True
        End If
        Return esValida
    End Function
    Protected Function ValidarFormulario() As Boolean
        Dim esValido As Boolean = True
        LabelPDF.Visible = False

        If Not FileUploadPDF.HasFile OrElse Not EsExtensionValida(FileUploadPDF.FileName) Then
            LabelPDF.Visible = True
            esValido = False
        End If

        Return esValido
    End Function
    Protected Sub BtnUpload_Click(sender As Object, e As EventArgs) Handles BtnUpload.Click

        If ValidarFormulario() Then

            Dim connectionString As String = conn
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim bytesPDF As Byte() = FileUploadToBytes(FileUploadPDF)
                ' Actualizar bytes en la base de datos
                Dim query As String = "UPDATE sag_analisis_germinacion SET germinacion_firmada = @germinacion_firmada WHERE ID_2=" & TxtID.Text & " "
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@germinacion_firmada", bytesPDF)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            Label23.Visible = False
            Label25.Visible = True
            BtnUpload.Visible = False
        Else
            Label23.Visible = True
            Label25.Visible = False
            BtnUpload.Visible = True
        End If

    End Sub
    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Response.Redirect(String.Format("~/pages/AnalisisGerminacion.aspx"))
    End Sub

    Protected Sub LinkButton2_Click(sender As Object, e As EventArgs) Handles LinkButton2.Click
        Response.Redirect(String.Format("~/pages/Analisis_DescArch.aspx"))
    End Sub
    Protected Sub sumaGerminacion()
        Dim noram1, noram2, noram3, noram4, noramT As Integer
        Dim anorm1, anorm2, anorm3, anorm4, anormT As Integer
        Dim muerta1, muerta2, muerta3, muerta4, muertaT As Integer
        Dim dura1, dura2, dura3, dura4, duraT As Integer
        Dim debiles1, debiles2, debiles3, debiles4, debilesT As Integer
        Dim mezcla1, mezcla2, mezcla3, mezcla4, mezclaT As Integer
        Dim dias1, dias2, dias3, dias4, diasT As Integer

        If Integer.TryParse(txtCam1PlanNorm.Text, noram1) Then
            noram1 = txtCam1PlanNorm.Text
        End If
        If Integer.TryParse(txtCam2PlanNorm.Text, noram2) Then
            noram2 = txtCam2PlanNorm.Text
        End If
        If Integer.TryParse(txtCam3PlanNorm.Text, noram3) Then
            noram3 = txtCam3PlanNorm.Text
        End If
        If Integer.TryParse(txtCam4PlanNorm.Text, noram4) Then
            noram4 = txtCam4PlanNorm.Text
        End If

        If Integer.TryParse(txtCam1PlanAnor.Text, anorm1) Then
            anorm1 = txtCam1PlanAnor.Text
        End If
        If Integer.TryParse(txtCam2PlanAnor.Text, anorm2) Then
            anorm2 = txtCam2PlanAnor.Text
        End If
        If Integer.TryParse(txtCam3PlanAnor.Text, anorm3) Then
            anorm3 = txtCam3PlanAnor.Text
        End If
        If Integer.TryParse(txtCam4PlanAnor.Text, anorm4) Then
            anorm4 = txtCam4PlanAnor.Text
        End If

        If Integer.TryParse(txtCam1SemiMuer.Text, muerta1) Then
            muerta1 = txtCam1SemiMuer.Text
        End If
        If Integer.TryParse(txtCam2SemiMuer.Text, muerta2) Then
            muerta2 = txtCam2SemiMuer.Text
        End If
        If Integer.TryParse(txtCam3SemiMuer.Text, muerta3) Then
            muerta3 = txtCam3SemiMuer.Text
        End If
        If Integer.TryParse(txtCam4SemiMuer.Text, muerta4) Then
            muerta4 = txtCam4SemiMuer.Text
        End If

        If Integer.TryParse(txtCam1SemiDura.Text, dura1) Then
            dura1 = txtCam1SemiDura.Text
        End If
        If Integer.TryParse(txtCam2SemiDura.Text, dura2) Then
            dura2 = txtCam2SemiDura.Text
        End If
        If Integer.TryParse(txtCam3SemiDura.Text, dura3) Then
            dura3 = txtCam3SemiDura.Text
        End If
        If Integer.TryParse(txtCam4SemiDura.Text, dura4) Then
            dura4 = txtCam4SemiDura.Text
        End If

        If Integer.TryParse(txtCam1Debiles.Text, debiles1) Then
            debiles1 = txtCam1Debiles.Text
        End If
        If Integer.TryParse(txtCam2Debiles.Text, debiles2) Then
            debiles2 = txtCam2Debiles.Text
        End If
        If Integer.TryParse(txtCam3Debiles.Text, debiles3) Then
            debiles3 = txtCam3Debiles.Text
        End If
        If Integer.TryParse(txtCam4Debiles.Text, debiles4) Then
            debiles4 = txtCam4Debiles.Text
        End If

        If Integer.TryParse(txtCam1Mezcla.Text, mezcla1) Then
            mezcla1 = txtCam1Mezcla.Text
        End If
        If Integer.TryParse(txtCam2Mezcla.Text, mezcla2) Then
            mezcla2 = txtCam2Mezcla.Text
        End If
        If Integer.TryParse(txtCam3Mezcla.Text, mezcla3) Then
            mezcla3 = txtCam3Mezcla.Text
        End If
        If Integer.TryParse(txtCam4Mezcla.Text, mezcla4) Then
            mezcla4 = txtCam4Mezcla.Text
        End If

        If Integer.TryParse(txtCam1NoDias.Text, dias1) Then
            dias1 = txtCam1NoDias.Text
        End If
        If Integer.TryParse(txtCam2NoDias.Text, dias2) Then
            dias2 = txtCam2NoDias.Text
        End If
        If Integer.TryParse(txtCam3NoDias.Text, dias3) Then
            dias3 = txtCam3NoDias.Text
        End If
        If Integer.TryParse(txtCam4NoDias.Text, dias4) Then
            dias4 = txtCam4NoDias.Text
        End If

        noramT = (noram1 + noram2 + noram3 + noram4) / (4)
        anormT = (anorm1 + anorm2 + anorm3 + anorm4) / (4)
        muertaT = (muerta1 + muerta2 + muerta3 + muerta4) / (4)
        duraT = (dura1 + dura2 + dura3 + dura4) / (4)
        debilesT = (debiles1 + debiles2 + debiles3 + debiles4) / (4)
        mezclaT = (mezcla1 + mezcla2 + mezcla3 + mezcla4) / (4)
        diasT = (dias1 + dias2 + dias3 + dias4) / (4)

        txtTotalPlanNorm.Text = noramT.ToString
        txtPorcGerm.Text = noramT.ToString
        txtTotalPlanAnor.Text = anormT.ToString
        txtTotalSemiMuer.Text = muertaT.ToString
        txtTotalSemiDura.Text = duraT.ToString
        txtTotalDebiles.Text = debilesT.ToString
        txtTotalMezcla.Text = mezclaT.ToString
        txtTotalNoDias.Text = diasT.ToString

    End Sub

    Private Sub txtCam1PlanNorm_TextChanged(sender As Object, e As EventArgs) Handles txtCam1PlanNorm.TextChanged
        sumaGerminacion()
    End Sub
    Private Sub txtCam2PlanNorm_TextChanged(sender As Object, e As EventArgs) Handles txtCam2PlanNorm.TextChanged
        sumaGerminacion()
    End Sub
    Private Sub txtCam3PlanNorm_TextChanged(sender As Object, e As EventArgs) Handles txtCam3PlanNorm.TextChanged
        sumaGerminacion()
    End Sub
    Private Sub txtCam4PlanNorm_TextChanged(sender As Object, e As EventArgs) Handles txtCam4PlanNorm.TextChanged
        sumaGerminacion()
    End Sub
    Private Sub TxtCam1PlanAnor_TextChanged(sender As Object, e As EventArgs) Handles txtCam1PlanAnor.TextChanged
        sumaGerminacion()
    End Sub
    Private Sub TxtCam2PanAnor_TextChanged(sender As Object, e As EventArgs) Handles txtCam2PlanAnor.TextChanged
        sumaGerminacion()
    End Sub
    Private Sub TxtCam3PlanAnor_TextChanged(sender As Object, e As EventArgs) Handles txtCam3PlanAnor.TextChanged
        sumaGerminacion()
    End Sub
    Private Sub TxtCam4PlanAnor_TextChanged(sender As Object, e As EventArgs) Handles txtCam4PlanAnor.TextChanged
        sumaGerminacion()
    End Sub
    Private Sub TxtCam1SemiMuer_TextChanged(sender As Object, e As EventArgs) Handles txtCam1SemiMuer.TextChanged
        sumaGerminacion()
    End Sub
    Private Sub TxtCam2SemiMuer_TextChanged(sender As Object, e As EventArgs) Handles txtCam2SemiMuer.TextChanged
        sumaGerminacion()
    End Sub
    Private Sub TxtCam3SemiMuer_TextChanged(sender As Object, e As EventArgs) Handles txtCam3SemiMuer.TextChanged
        sumaGerminacion()
    End Sub
    Private Sub TxtCam4SemiMuer_TextChanged(sender As Object, e As EventArgs) Handles txtCam4SemiMuer.TextChanged
        sumaGerminacion()
    End Sub
    Private Sub TxtCam1SemiDura_TextChanged(sender As Object, e As EventArgs) Handles txtCam1SemiDura.TextChanged
        sumaGerminacion()
    End Sub
    Private Sub TxtCam2SemiDura_TextChanged(sender As Object, e As EventArgs) Handles txtCam2SemiDura.TextChanged
        sumaGerminacion()
    End Sub
    Private Sub TxtCam3SemiDura_TextChanged(sender As Object, e As EventArgs) Handles txtCam3SemiDura.TextChanged
        sumaGerminacion()
    End Sub
    Private Sub TxtCam4SemiDura_TextChanged(sender As Object, e As EventArgs) Handles txtCam4SemiDura.TextChanged
        sumaGerminacion()
    End Sub
    Private Sub TxtCam1Debiles_TextChanged(sender As Object, e As EventArgs) Handles txtCam1Debiles.TextChanged
        sumaGerminacion()
    End Sub
    Private Sub TxtCam2Debiles_TextChanged(sender As Object, e As EventArgs) Handles txtCam2Debiles.TextChanged
        sumaGerminacion()
    End Sub
    Private Sub TxtCam3Debiles_TextChanged(sender As Object, e As EventArgs) Handles txtCam3Debiles.TextChanged
        sumaGerminacion()
    End Sub
    Private Sub TxtCam4Debiles_TextChanged(sender As Object, e As EventArgs) Handles txtCam4Debiles.TextChanged
        sumaGerminacion()
    End Sub
    Private Sub TxtCam1Mezcla_TextChanged(sender As Object, e As EventArgs) Handles txtCam1Mezcla.TextChanged
        sumaGerminacion()
    End Sub
    Private Sub TxtCam2Mezcla_TextChanged(sender As Object, e As EventArgs) Handles txtCam2Mezcla.TextChanged
        sumaGerminacion()
    End Sub
    Private Sub TxtCam3Mezcla_TextChanged(sender As Object, e As EventArgs) Handles txtCam3Mezcla.TextChanged
        sumaGerminacion()
    End Sub
    Private Sub TxtCam4Mezcla_TextChanged(sender As Object, e As EventArgs) Handles txtCam4Mezcla.TextChanged
        sumaGerminacion()
    End Sub
    Private Sub TxtCam1NoDias_TextChanged(sender As Object, e As EventArgs) Handles txtCam1NoDias.TextChanged
        sumaGerminacion()
    End Sub
    Private Sub TxtCam2NoDias_TextChanged(sender As Object, e As EventArgs) Handles txtCam2NoDias.TextChanged
        sumaGerminacion()
    End Sub
    Private Sub TxtCam3NoDias_TextChanged(sender As Object, e As EventArgs) Handles txtCam3NoDias.TextChanged
        sumaGerminacion()
    End Sub
    Private Sub TxtCam4NoDias_TextChanged(sender As Object, e As EventArgs) Handles txtCam4NoDias.TextChanged
        sumaGerminacion()
    End Sub
End Class