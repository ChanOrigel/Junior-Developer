<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Portal/MasterPage.Master" CodeBehind="Frm_A_Pagar.aspx.cs" Inherits="JPV_Portal.Portal.Frm_A_Pagar" %>


<asp:Content ID="Head" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <script src="../Recursos/plugins/accounting.min.js"></script>
    <script src="../JS/Js_A_Pagar.js"></script>

</asp:Content>

<asp:Content ID="RioGrande" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <section class="container br-nav">
        <nav class="navbar">
            <div class="d-inline-flex " style="width: 100%;">
                <ul class="nav navbar-nav d-inline-flex">
                    <li class="myClass nav-item">
                        <ul class="list-inline-mb-0">
                            <li class="list-inline-item" id="li-nuevo">
                                <button id="btn_consultar" type="button" class="btn btn-primary btn_my_class" title="Cuentas Pendientes"><i class="fa fa-credit-card"></i>&nbsp;&nbsp;Cuentas Pend.</button></li>
                            <li class="list-inline-item" id="li-historial">
                                <button id="btn_historial" type="button" class="btn btn-primary btn_my_class" title="Hisorial Abonos"><i class="fa fa-calendar"></i>&nbsp;&nbsp;Historial A.</button></li>
                            <li class="list-inline-item" id="li-bodegas">
                                <button id="btn_bodegas" type="button" class="btn btn-primary btn_my_class" title="Cuentas Pendientes en Bodegas"><i class="fa fa-credit-card"></i>&nbsp;&nbsp;Cuentas Bodegas</button></li>
                            <li class="list-inline-item" id="li-cajas">
                                <button id="btn_cajas" type="button" class="btn btn-warning btn_my_class" title="Recepción Cajas"><i class="fa fa-cube"></i>&nbsp;&nbsp;Cajas</button></li>
                            <li class="list-inline-item" id="li-historial_cajas">
                                <button id="btn_historial_cajas" type="button" class="btn btn-warning btn_my_class" title="Hisorial Entrega de Cajas"><i class="fa fa-calendar"></i>&nbsp;&nbsp;Historial C.</button></li>
                            <li class="list-inline-item" id="li-corte">
                                <button id="btn_corte" type="button" class="btn btn-primary btn_my_class" title="Ventas por rango de fechas"><i class="fa fa-cart-arrow-down"></i>&nbsp;&nbsp;Ventas Reg.</button></li>

                        </ul>
                    </li>
                </ul>
            </div>
        </nav>
    </section>
    <hr />
    <div class="container">
        <input type="hidden" id="txt_cliente_id" />
        <input type="hidden" id="txt_folio_id" />
        <input type="hidden" id="txt_venta_id" />
        <input type="hidden" id="txt_restante" />
        <input type="hidden" id="txt_cliente_" />
        <input type="hidden" id="txt_cantidad_caja" />

        <input type="hidden" id="txt_caja_id" />
        <input type="hidden" id="txt_cantidad" />
        <input type="hidden" id="txt_descripcion" />

        <div id="filtros">
            <div class="col-sm-12 text-left">
                <label for="Txt_No_Proveedor">Filtros de Consulta</label>
            </div>
            <div class="form-group row">
                <label for="example-text-input" class="col-1 col-form-label">Folio</label>
                <div class="col-3">
                    <input type="text" id="Txt_Folio" class="form-control" placeholder="Folio" aria-describedby="basic-addon1" />
                </div>
                <span class="fa fa-calendar">
                    <label for="example-text-input" class="col-1 col-form-label">Inicio</label></span>
                <div class="col-3">
                    <input type="text" id="Txt_Fecha_Inicial" class="form-control" placeholder="Fecha" aria-describedby="basic-addon1" />
                </div>
                <span class="fa fa-calendar">
                    <label for="example-text-input" class="col-1 col-form-label">Final</label></span>
                <div class="col-3">
                    <input type="text" id="Txt_Fecha_Final" class="form-control" placeholder="Fecha" aria-describedby="basic-addon1" />
                </div>
            </div>
            <div class="form-group row">
                <label for="example-text-input" class="col-1 col-form-label">Cliente</label>
                <div class="col-11">
                    <select id="cmb_cliente" class="form-control" aria-describedby="basic-addon1"></select>
                </div>
            </div>
            <br />

        </div>

        <div class="row" id="Reg-Datos">
            <div class="col-xs-12 col-md-12">
                <table id="Tbl_Registros" class="display compact table-bordered dt-responsive nowrap" cellspacing="0" width="100%"></table>
            </div>
        </div>
        <div class="row" id="Reg-Cajas">
            <div class="col-xs-12 col-md-12">
                <table id="Tbl_Cajas" class="display compact table-bordered dt-responsive nowrap" cellspacing="0" width="100%"></table>
            </div>
        </div>
        <div class="row" id="Reg-Bodegas">
            <div class="col-xs-12 col-md-12">
                <table id="Tbl_Bodegas" class="display compact table-bordered dt-responsive nowrap" cellspacing="0" width="100%"></table>
            </div>
        </div>
        <div id="Modal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">

                        <h4 class="modal-title">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Abonar</h4>

                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <br />
                    <div class="form-group row">
                        <div class="col-md-1">
                        </div>
                        <label for="example-text-input" class="col-2 col-form-label" id="Etiquet">El cliente </label>
                        <div class="col-3">
                            <input type="text" id="txt_cliente" class="form-control" disabled="disabled" aria-describedby="basic-addon1" />
                        </div>
                        <label for="example-text-input" class="col-3 col-form-label" id="Etiquete">Cajas Pendientes </label>
                        <div class="col-2">
                            <input type="text" id="txt_cajas_deuda" class="form-control" disabled="disabled" aria-describedby="basic-addon1" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-md-1">
                        </div>
                        <label for="example-text-input" class="col-2 col-form-label" id="Etiqueta">Abonará </label>
                        <div class="col-3">
                            <input type="text" id="txt_abono" class="form-control" placeholder="$0" aria-describedby="basic-addon1" />
                        </div>
                    </div>
                    <div class="row" data-bind="visible: hasTI">
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-dismiss="modal" onclick="validar_datos()">Abonar</button>
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <div id="Modal_Cajas" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">

                        <h4 class="modal-title">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Recepción de Cajas</h4>

                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <br />
                    <div class="form-group row">
                        <div class="col-md-1">
                        </div>
                        <label for="example-text-input" class="col-2 col-form-label" id="Etiquetas">El cliente </label>
                        <div class="col-3">
                            <input type="text" id="txt_clientes" class="form-control" disabled="disabled" aria-describedby="basic-addon1" />
                        </div>
                        <label for="example-text-input" class="col-3 col-form-label" id="Etiquetes">Dejo un Deposito de:</label>
                        <div class="col-2">
                            <input type="text" id="txt_deposito_deuda" class="form-control" aria-describedby="basic-addon1" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-md-1">
                        </div>
                        <label for="example-text-input" class="col-2 col-form-label" id="Etiquetass">Entrega </label>
                        <div class="col-3">
                            <input type="text" id="txt_cajas" class="form-control" placeholder="0" aria-describedby="basic-addon1" />
                        </div>

                    </div>
                    <div class="row" data-bind="visible: hasTI">
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-dismiss="modal" onclick="validar_datos_cajas()">Recibir</button>
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

         <div id="Modal_Bodegas" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">

                        <h4 class="modal-title">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Cuentas Pendientes de Bodegas</h4>

                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <br />
                    <div class="form-group row">
                        <div class="col-md-1">
                        </div>
                        <label for="example-text-input" class="col-2 col-form-label" id="Etiqu">La Bodega </label>
                        <div class="col-3">
                            <input type="text" id="txt_client" class="form-control" disabled="disabled" aria-describedby="basic-addon1" />
                        </div>
                        <label for="example-text-input" class="col-3 col-form-label">Importe:</label>
                        <div class="col-2">
                            <input type="text" id="txt_importe_cajas" class="form-control" aria-describedby="basic-addon1" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-md-1">
                        </div>
                        <label for="example-text-input" class="col-2 col-form-label" >Pagará:</label>
                        <div class="col-3">
                            <input type="text" id="txt_cantidad_c" class="form-control" placeholder="0" aria-describedby="basic-addon1" />&nbsp;&nbsp;Cajas
                        </div>

                    </div>
                    <div class="row" data-bind="visible: hasTI">
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-dismiss="modal" onclick="Pagar_Cajas()">Pagar</button>
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
