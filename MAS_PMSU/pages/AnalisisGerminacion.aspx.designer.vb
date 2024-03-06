'------------------------------------------------------------------------------
' <generado automáticamente>
'     Este código fue generado por una herramienta.
'
'     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
'     se vuelve a generar el código. 
' </generado automáticamente>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Partial Public Class AnalisisGerminacion

    '''<summary>
    '''Control ScriptManager1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ScriptManager1 As Global.System.Web.UI.ScriptManager

    '''<summary>
    '''Control DivGrid.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DivGrid As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Control TxtDepto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtDepto As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control TxtProductorGrid.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtProductorGrid As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control DDL_SelCult.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DDL_SelCult As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control txtciclo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtciclo As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control lblTotalClientes.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblTotalClientes As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control SqlDataSource1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents SqlDataSource1 As Global.System.Web.UI.WebControls.SqlDataSource

    '''<summary>
    '''Control GridDatos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents GridDatos As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''Control LinkButton1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LinkButton1 As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Control LinkButton2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LinkButton2 As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Control DivActaInfo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DivActaInfo As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Control DivActaInfo1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DivActaInfo1 As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Control TextBox1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TextBox1 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtlega.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtlega As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtnum.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtnum As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control Txtcount.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Txtcount As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control TextIdlote2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TextIdlote2 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lblFechaElab.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblFechaElab As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtFechaElab.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtFechaElab As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lblCultivo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblCultivo As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtCultivo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCultivo As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lblVariedad.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblVariedad As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtVariedad.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtVariedad As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lblHibrido.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblHibrido As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtHibrido.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtHibrido As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lblCategoria.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblCategoria As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtCategoria.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCategoria As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lblaño.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblaño As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtaño.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtaño As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lblProductor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblProductor As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtProductor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtProductor As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lblProcedencia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblProcedencia As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtProcedencia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtProcedencia As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lblLoteRegi.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblLoteRegi As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtLoteRegi.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtLoteRegi As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lblciclo2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblciclo2 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control Textciclo2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Textciclo2 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lblDepartamento.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblDepartamento As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtDepartamento.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtDepartamento As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lblMunicipio.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblMunicipio As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtMunicipio.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtMunicipio As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lblLocallidad.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblLocallidad As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtLocallidad.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtLocallidad As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control DivActaInfo2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DivActaInfo2 As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Control RegularExpressionValidator43.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RegularExpressionValidator43 As Global.System.Web.UI.WebControls.RegularExpressionValidator

    '''<summary>
    '''Control lblPesoH.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblPesoH As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtPesoH.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtPesoH As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lblSacos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblSacos As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtSacos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtSacos As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lblEnvase.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblEnvase As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtEnvase.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtEnvase As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control DivActaInfo3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DivActaInfo3 As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Control RegularExpressionValidator1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RegularExpressionValidator1 As Global.System.Web.UI.WebControls.RegularExpressionValidator

    '''<summary>
    '''Control lblPesoInicialPlanta.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblPesoInicialPlanta As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtPesoInicialPlanta.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtPesoInicialPlanta As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control RegularExpressionValidator2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RegularExpressionValidator2 As Global.System.Web.UI.WebControls.RegularExpressionValidator

    '''<summary>
    '''Control lblGranel.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblGranel As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtGranel.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtGranel As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control RegularExpressionValidator3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RegularExpressionValidator3 As Global.System.Web.UI.WebControls.RegularExpressionValidator

    '''<summary>
    '''Control lblEnSacos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblEnSacos As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtEnSacos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtEnSacos As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control RegularExpressionValidator4.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RegularExpressionValidator4 As Global.System.Web.UI.WebControls.RegularExpressionValidator

    '''<summary>
    '''Control lblEnBolsas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblEnBolsas As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtEnBolsas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtEnBolsas As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control RegularExpressionValidator5.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RegularExpressionValidator5 As Global.System.Web.UI.WebControls.RegularExpressionValidator

    '''<summary>
    '''Control lblEnMazorca.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblEnMazorca As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtEnMazorca.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtEnMazorca As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control DivActaInfo4.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DivActaInfo4 As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Control lblFechaRecibo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblFechaRecibo As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtFechaRecibo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtFechaRecibo As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lblFechaMuestreo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblFechaMuestreo As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtFechaMuestreo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtFechaMuestreo As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control RegularExpressionValidator42.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RegularExpressionValidator42 As Global.System.Web.UI.WebControls.RegularExpressionValidator

    '''<summary>
    '''Control lblHumedad.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblHumedad As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtHumedad.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtHumedad As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control RegularExpressionValidator6.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RegularExpressionValidator6 As Global.System.Web.UI.WebControls.RegularExpressionValidator

    '''<summary>
    '''Control lblHumedadF.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblHumedadF As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtHumedadF.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtHumedadF As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lblFecha.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblFecha As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtFechaSiembra.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtFechaSiembra As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lblFechaEval.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblFechaEval As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtFechaEval.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtFechaEval As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control DivActaInfo5.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DivActaInfo5 As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Control RegularExpressionValidator7.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RegularExpressionValidator7 As Global.System.Web.UI.WebControls.RegularExpressionValidator

    '''<summary>
    '''Control lblBolsas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblBolsas As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtBolsas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtBolsas As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control RegularExpressionValidator8.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RegularExpressionValidator8 As Global.System.Web.UI.WebControls.RegularExpressionValidator

    '''<summary>
    '''Control lblOtrosSacos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblOtrosSacos As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtOtrosSacos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtOtrosSacos As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control DDLFase.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DDLFase As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control DDLTamaño.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DDLTamaño As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control RegularExpressionValidator9.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RegularExpressionValidator9 As Global.System.Web.UI.WebControls.RegularExpressionValidator

    '''<summary>
    '''Control lblCantInicial.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblCantInicial As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtCantInicial.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCantInicial As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control RegularExpressionValidator10.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RegularExpressionValidator10 As Global.System.Web.UI.WebControls.RegularExpressionValidator

    '''<summary>
    '''Control lblCantExistente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblCantExistente As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtCantExistente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCantExistente As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control RegularExpressionValidator11.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RegularExpressionValidator11 As Global.System.Web.UI.WebControls.RegularExpressionValidator

    '''<summary>
    '''Control lblCamaraNo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblCamaraNo As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtCamaraNo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCamaraNo As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control RegularExpressionValidator12.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RegularExpressionValidator12 As Global.System.Web.UI.WebControls.RegularExpressionValidator

    '''<summary>
    '''Control lblPerimetro.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblPerimetro As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtPerimetro.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtPerimetro As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control DivActaInfo6.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DivActaInfo6 As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Control RegularExpressionValidator13.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RegularExpressionValidator13 As Global.System.Web.UI.WebControls.RegularExpressionValidator

    '''<summary>
    '''Control lblCERTISEM.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblCERTISEM As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtCERTISEM.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCERTISEM As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lblFechaCERTISEM.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblFechaCERTISEM As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtFechaCERTISEM.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtFechaCERTISEM As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control RegularExpressionValidator14.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RegularExpressionValidator14 As Global.System.Web.UI.WebControls.RegularExpressionValidator

    '''<summary>
    '''Control lblPlanta.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblPlanta As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtPlanta.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtPlanta As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lblFechaPlanta.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblFechaPlanta As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtFechaPlanta.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtFechaPlanta As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control DivActa.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DivActa As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Control TxtID.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtID As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control RegularExpressionValidator15.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RegularExpressionValidator15 As Global.System.Web.UI.WebControls.RegularExpressionValidator

    '''<summary>
    '''Control lblSemillaPura.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblSemillaPura As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtSemillaPura.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtSemillaPura As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control RegularExpressionValidator16.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RegularExpressionValidator16 As Global.System.Web.UI.WebControls.RegularExpressionValidator

    '''<summary>
    '''Control lblSemillaOtroCult.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblSemillaOtroCult As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtSemillaOtroCult.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtSemillaOtroCult As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control RegularExpressionValidator17.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RegularExpressionValidator17 As Global.System.Web.UI.WebControls.RegularExpressionValidator

    '''<summary>
    '''Control lblSemillaMalezas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblSemillaMalezas As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtSemillaMalezas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtSemillaMalezas As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control RegularExpressionValidator18.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RegularExpressionValidator18 As Global.System.Web.UI.WebControls.RegularExpressionValidator

    '''<summary>
    '''Control lblSemillaInerte.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblSemillaInerte As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtSemillaInerte.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtSemillaInerte As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lblmensaje.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblmensaje As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtCam1PlanNorm.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCam1PlanNorm As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCam1PlanAnor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCam1PlanAnor As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCam1SemiMuer.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCam1SemiMuer As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCam1SemiDura.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCam1SemiDura As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCam1Debiles.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCam1Debiles As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCam1Mezcla.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCam1Mezcla As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCam1NoDias.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCam1NoDias As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCam2PlanNorm.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCam2PlanNorm As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCam2PlanAnor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCam2PlanAnor As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCam2SemiMuer.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCam2SemiMuer As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCam2SemiDura.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCam2SemiDura As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCam2Debiles.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCam2Debiles As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCam2Mezcla.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCam2Mezcla As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCam2NoDias.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCam2NoDias As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCam3PlanNorm.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCam3PlanNorm As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCam3PlanAnor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCam3PlanAnor As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCam3SemiMuer.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCam3SemiMuer As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCam3SemiDura.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCam3SemiDura As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCam3Debiles.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCam3Debiles As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCam3Mezcla.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCam3Mezcla As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCam3NoDias.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCam3NoDias As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCam4PlanNorm.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCam4PlanNorm As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCam4PlanAnor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCam4PlanAnor As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCam4SemiMuer.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCam4SemiMuer As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCam4SemiDura.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCam4SemiDura As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCam4Debiles.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCam4Debiles As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCam4Mezcla.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCam4Mezcla As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCam4NoDias.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCam4NoDias As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtTotalPlanNorm.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtTotalPlanNorm As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtTotalPlanAnor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtTotalPlanAnor As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtTotalSemiMuer.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtTotalSemiMuer As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtTotalSemiDura.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtTotalSemiDura As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtTotalDebiles.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtTotalDebiles As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtTotalMezcla.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtTotalMezcla As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtTotalNoDias.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtTotalNoDias As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lblPorcGerm.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblPorcGerm As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtPorcGerm.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtPorcGerm As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lblObserv.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblObserv As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtObserv.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtObserv As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lblRespMuestreo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblRespMuestreo As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtRespMuestreo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtRespMuestreo As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lblRespAnalisis.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblRespAnalisis As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtRespAnalisis.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtRespAnalisis As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control div_nuevo_prod.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents div_nuevo_prod As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Control LabelPDF.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LabelPDF As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control FileUploadPDF.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FileUploadPDF As Global.System.Web.UI.WebControls.FileUpload

    '''<summary>
    '''Control Labelimglote.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Labelimglote As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control FileUploadimglote.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FileUploadimglote As Global.System.Web.UI.WebControls.FileUpload

    '''<summary>
    '''Control Label23.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Label23 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control Label25.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Label25 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control BtnUpload.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnUpload As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control Button4.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Button4 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control Label26.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Label26 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control Button6.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Button6 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control LabelGuardar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LabelGuardar As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control btnGuardarActa.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnGuardarActa As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control BtnImprimir.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnImprimir As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control BtnNuevo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnNuevo As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control Label103.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Label103 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control BConfirm.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BConfirm As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control BBorrarsi.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BBorrarsi As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control BBorrarno.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BBorrarno As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control Label1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Label1 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control Button1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Button1 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control Button2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Button2 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control Button3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Button3 As Global.System.Web.UI.WebControls.Button
End Class
