﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Portal/MasterPage.Master" CodeBehind="Frm_Ventas.aspx.cs" Inherits="JPV_Portal.Portal.Frm_Ventas" %>


<asp:Content ID="Head" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <script src="../JS/Js_Ventas.js"></script>
    <link href="../Recursos/Css/StyleSheet.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="RioGrande" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <%-- <section class="container br-nav">
        <nav class="navbar">
            <div class="d-inline-flex " style="width: 100%;">
                <ul class="nav navbar-nav d-inline-flex">
                    <li class="myClass nav-item">
                        <ul class="list-inline-mb-0">
                            <li class="list-inline-item" id="li-nuevo">
                                <button id="btn_nuevo" type="button" class="btn btn-primary btn_my_class" title="Nuevo"><i class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Nuevo</button></li>
                            <li class="list-inline-item" id="li-excel">
                                <button id="btn_excel" type="button" class="btn btn-primary btn_my_class" title="Cargar Layout"><i class="fa fa-file-excel-o"></i>&nbsp;&nbsp;Cargar Layout</button></li>
                            <li class="list-inline-item" id="li-historial">
                                <button id="btn_historial" type="button" class="btn btn-primary btn_my_class" title="Historial"><i class="fa fa-file-pdf-o"></i>&nbsp;&nbsp;Historial</button></li>
                            <li class="list-inline-item" id="li-guardar" style="display: none">
                                <button id="btn_guardar" type="button" class="btn btn-primary btn_my_class" title="Guardar"><i class="glyphicon glyphicon-floppy-save"></i>&nbsp;&nbsp;Guardar</button></li>
                            <li class="list-inline-item" id="li-cancelar" style="display: none">
                                <button id="btn_cancelar" type="button" class="btn btn-primary btn_my_class" title="Cancelar"><i class="glyphicon glyphicon-remove"></i>&nbsp;&nbsp;Cancelar</button>
                            </li>
                        </ul>
                    </li>
                </ul>
            </div>
        </nav>
    </section>--%>
    <br />
    <div class="container">
        <input type="hidden" id="add" />
        <input type="hidden" id="txt_producto_id" />
        <input type="hidden" id="txt_proveedor_producto" />
        <input type="hidden" id="txt_producto" />
        <input type="hidden" id="txt_cliente_id" />
        <input type="hidden" id="txt_cajas_id" />

        <input type="hidden" id="txt_fecha" />

        <div class="row" id="Reg_Datos">
            <%--<img src="../../Recursos/Imagenes/Logos/reducida.png" />--%>
            <div class="form-group row">
                <label for="example-text-input" class="col-2 col-form-label labeles">Folio</label>
                <div class="col-lg-3">
                    <input type='text' class="form-control labeles" id="txt_folio" disabled="disabled" />
                </div>
                <div class="col-lg-1"></div>
                <label for="example-text-input" class="col-2 col-form-label labeles">Total</label>
                <div class="col-lg-4">
                    <input type="text" id="txt_total" class="form-control labeles" placeholder="$0" disabled="disabled" />
                </div>
            </div>
            <hr />
            <br />
            <div class="col-xs-12 col-md-12">
                <table id="Tbl_Registros" class="display compact table-bordered dt-responsive nowrap" cellspacing="0" width="100%"></table>
            </div>
            <br />
            <%-- <div class="form-group row">
              
            </div>--%>
            <hr />
            <div class="form-group row">
                <div class="col-lg-6">
                    <button id="btn_pagare" type="button" class="btn btn-success btn_pendiente" title="Pagaré"><i class="fa fa-credit-card-alt"></i>&nbsp;&nbsp;Pagaré&nbsp;</button>
                </div>
                <div class="col-lg-6">
                    <button id="Btn_Limpiar_Todo" type="button" class="btn btn-success btn_pendiente" title="Limpiar"><i class="fa fa-eraser"></i>&nbsp;&nbsp;Limpiar&nbsp;</button>
                </div>
            </div>
            <div class="form-group row">
                <div class="col-lg-12">
                    <button id="btn_imprimir" type="button" class="btn btn-info btn_my_imprimir" title="Imprimir"><i class="fa fa-print"></i>&nbsp;&nbsp;</button>
                </div>
            </div>

        </div>

        <div class="row" id="cont_alta">

            <div class="form-group row">
                <div class="col-lg-2"></div>
                <div class="form-check form-check-inline labeles">
                    <label class="form-check-label">
                        <input class="form-check-input radio" type="radio" name="Tipo" id="chk_Regis" value="Registrado" checked="checked" onclick="Proveedor_Registrado();" />
                        Registrado
                    </label>
                </div>
                <div class="form-check form-check-inline">
                    <label class="form-check-label labeles">
                        <input class="form-check-input radio" type="radio" name="Tipo" id="chk_No_Regis" value="No_Registrado" onclick="Proveedor();" />
                        No Registrado
                    </label>
                </div>
            </div>
            <hr />
            <%-- Cliente registrado y no registrado --%>
            <div class="form-group row select">
                <%--<label for="example-text-input" class="col-2 col-form-label labeles1">*Cliente</label>--%>
                <div class="col-12" id="Registrado">
                    <select id="cmb_cliente" class="labeles"></select>
                </div>
                <div class="col-12" id="No_Registrado" style="display: none">
                    <input type="text" id="txt_cliente" class="form-control" placeholder="Cliente No Registrado" aria-describedby="basic-addon1" />
                </div>
            </div>

            <%-- fin Cliente --%>
            <br />

            <div class="form-group row select">
                <%--<label for="example-text-input" class="col-2 col-form-label labeles1">*Producto</label>--%>
                <div class="col-12">
                    <select id="cmb_producto" class="form-control" onkeypress="$('#txt_cantidad').focus().select()" oncuechange="$('#txt_cantidad').focus().select()" onchange="$('#txt_cantidad').focus().select()"></select>
                </div>
            </div>

            <div id="Cantidad">
                    <button id="por" type="button" class="btn btn-info btn_x" title="X"><i ></i>&nbsp;X&nbsp;</button>

                <div class="form-group row">
                    <div class="col-lg-3"></div>
                    <button id="btn_1" type="button" class="btn btn-info btn_principal_ventas" title="1"><i class="glyphicon glyphicon-print"></i>&nbsp;1&nbsp;</button>
                    <button id="btn_2" type="button" class="btn btn-info btn_principal_ventas" title="2"><i class="glyphicon glyphicon-print"></i>&nbsp;2&nbsp;</button>
                    <button id="btn_3" type="button" class="btn btn-info btn_principal_ventas" title="3"><i class="glyphicon glyphicon-print"></i>&nbsp;3&nbsp;</button>
                </div>
                <div class="form-group row">
                    <div class="col-lg-3"></div>
                    <button id="btn_4" type="button" class="btn btn-info btn_principal_ventas" title="4"><i class="glyphicon glyphicon-print"></i>&nbsp;4&nbsp;</button>
                    <button id="btn_5" type="button" class="btn btn-info btn_principal_ventas" title="5"><i class="glyphicon glyphicon-print"></i>&nbsp;5&nbsp;</button>
                    <button id="btn_6" type="button" class="btn btn-info btn_principal_ventas" title="6"><i class="glyphicon glyphicon-print"></i>&nbsp;6&nbsp;</button>
                </div>
                <div class="form-group row">
                    <div class="col-lg-3"></div>
                    <button id="btn_7" type="button" class="btn btn-info btn_principal_ventas" title="7"><i class="glyphicon glyphicon-print"></i>&nbsp;7&nbsp;</button>
                    <button id="btn_8" type="button" class="btn btn-info btn_principal_ventas" title="8"><i class="glyphicon glyphicon-print"></i>&nbsp;8&nbsp;</button>
                    <button id="btn_9" type="button" class="btn btn-info btn_principal_ventas" title="9"><i class="glyphicon glyphicon-print"></i>&nbsp;9&nbsp;</button>
                </div>
                <div class="form-group row">
                    <div class="col-lg-3"></div>
                    <button id="btn_ce" type="button" class="btn btn-info btn_principal_ventas" title="CE"><i class="glyphicon glyphicon-print"></i>&nbsp;CE&nbsp;</button>
                    <button id="btn_0" type="button" class="btn btn-info btn_principal_ventas" title="0"><i class="glyphicon glyphicon-print"></i>&nbsp;0&nbsp;</button>
                    <button id="btn_punto" type="button" class="btn btn-info btn_principal_ventas" title="."><i class="glyphicon glyphicon-print"></i>&nbsp;.&nbsp;</button>
                </div>

            </div>
            <div id="Precio">
                    <button id="por_" type="button" class="btn btn-info btn_x" title="X"><i ></i>&nbsp;X&nbsp;</button>

                <div class="form-group row">
                    <div class="col-lg-3"></div>
                    <button id="btn_1_p" type="button" class="btn btn-info btn_principal_ventas" title="1"><i class="glyphicon glyphicon-print"></i>&nbsp;1&nbsp;</button>
                    <button id="btn_2_p" type="button" class="btn btn-info btn_principal_ventas" title="2"><i class="glyphicon glyphicon-print"></i>&nbsp;2&nbsp;</button>
                    <button id="btn_3_p" type="button" class="btn btn-info btn_principal_ventas" title="3"><i class="glyphicon glyphicon-print"></i>&nbsp;3&nbsp;</button>
                </div>
                <div class="form-group row">
                    <div class="col-lg-3"></div>
                    <button id="btn_4_p" type="button" class="btn btn-info btn_principal_ventas" title="4"><i class="glyphicon glyphicon-print"></i>&nbsp;4&nbsp;</button>
                    <button id="btn_5_p" type="button" class="btn btn-info btn_principal_ventas" title="5"><i class="glyphicon glyphicon-print"></i>&nbsp;5&nbsp;</button>
                    <button id="btn_6_p" type="button" class="btn btn-info btn_principal_ventas" title="6"><i class="glyphicon glyphicon-print"></i>&nbsp;6&nbsp;</button>
                </div>
                <div class="form-group row">
                    <div class="col-lg-3"></div>
                    <button id="btn_7_p" type="button" class="btn btn-info btn_principal_ventas" title="7"><i class="glyphicon glyphicon-print"></i>&nbsp;7&nbsp;</button>
                    <button id="btn_8_p" type="button" class="btn btn-info btn_principal_ventas" title="8"><i class="glyphicon glyphicon-print"></i>&nbsp;8&nbsp;</button>
                    <button id="btn_9_p" type="button" class="btn btn-info btn_principal_ventas" title="9"><i class="glyphicon glyphicon-print"></i>&nbsp;9&nbsp;</button>
                </div>
                <div class="form-group row">
                    <div class="col-lg-3"></div>
                    <button id="btn_ce_p" type="button" class="btn btn-info btn_principal_ventas" title="CE"><i class="glyphicon glyphicon-print"></i>&nbsp;CE&nbsp;</button>
                    <button id="btn_0_p" type="button" class="btn btn-info btn_principal_ventas" title="0"><i class="glyphicon glyphicon-print"></i>&nbsp;0&nbsp;</button>
                    <button id="btn_punto_p" type="button" class="btn btn-info btn_principal_ventas" title="."><i class="glyphicon glyphicon-print"></i>&nbsp;.&nbsp;</button>
                </div>
            </div>

            <div class="form-group row">

                <label for="example-text-input" class="col-2 col-form-label labeles">Cantidad</label>
                <div class="col-4">
                    <input type="text" id="txt_cantidad" class="form-control labeles" placeholder="Cantidad" aria-describedby="basic-addon1" />
                </div>

                <label for="example-text-input" class="col-2 col-form-label labeles">Precio</label>
                <div class="col-4">
                    <input type="text" id="txt_precio" class="form-control labeles" placeholder="Precio" aria-describedby="basic-addon1" />
                </div>
            </div>
            <div class="form-group row">
                <label for="example-text-input" class="col-2 col-form-label labeles">Importe</label>
                <div class="col-4">
                    <input type="text" id="txt_importe" class="form-control labeles" disabled="disabled" placeholder="$0" aria-describedby="basic-addon1" />
                </div>
                <%--<div class="form-group row">--%>
                <div class="col-lg-6">
                    <button id="btn_agregar" type="button" class="btn btn-success btn_my_agregar" title="Agregar" tabindex="4"><i class="fa fa-plus"></i>&nbsp;&nbsp;</button>
                </div>
            </div>
            <%--</div>--%>
        </div>


        <div id="Modal_Cuentas" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">

                        <h4 class="modal-title">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Cuentas Pendientes</h4>

                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <br />
                    <div class="form-group row">
                        <div class="col-md-1">
                        </div>
                        <label for="example-text-input" class="col-3 col-form-label" id="Etiqueta">Cuentas Pendientes</label>
                        <div class="col-4">
                            <input type="text" id="txt_cuentas_pendientes" class="form-control" disabled="disabled" placeholder="$0" aria-describedby="basic-addon1" />
                        </div>

                    </div>
                    <div class="row" data-bind="visible: hasTI">
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <div id="Modal_Cajas" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">

                        <h4 class="modal-title">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;Préstamo de Cajas</h4>

                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <br />
                    <div class="form-group row">
                        <div class="col-md-1"></div>
                        <label for="example-text-input" class="col-1 col-form-label">Cajas</label>
                        <div class="col-2">
                            <select id="cmb_cajas" class="form-control" aria-describedby="basic-addon1"></select>
                            <input type="hidden" id="txt_cajas" />
                        </div>
                        <label for="example-text-input" class="col-1 col-form-label">Cantidad</label>
                        <%--<div class="col-md-1"></div>--%>
                        <div class="col-3">
                            <div class="input-group">
                                <span class="input-group-btn">
                                    <button type="button" class="btn btn-default btn-number" data-type="minus" data-field="txt_cantidad_cajas">-
                                        <span class="glyphicon glyphicon-minus"></span>
                                    </button>
                                </span>
                                <input type="text" id="txt_cantidad_cajas" name="txt_cantidad_cajas" class="form-control input-number" value="0" min="0" max="1000" />
                                <span class="input-group-btn">
                                    <button type="button" class="btn btn-default btn-number" data-type="plus" data-field="txt_cantidad_cajas">+
                                        <span class="glyphicon glyphicon-plus"></span>
                                    </button>
                                </span>
                            </div>

                            <%--<input id="txt_cantidad_cajas" class="form-control input-number" value="0" min="0" max="100" style="width: 100%;" type="text" />--%>
                        </div>
                        <label for="example-text-input" class="col-1 col-form-label">Importe</label>
                        <div class="col-2">
                            <input id="txt_importe_cajas" class="form-control" style="width: 100%;" type="text" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-lg-10"></div>
                        <div class="col-lg-1">
                            <button id="btn_agregar_cajas" type="button" class="btn btn-success btn_my_agregar" title="Agregar" tabindex="4"><i class="fa fa-plus"></i>&nbsp;&nbsp;</button>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-lg-2"></div>
                        <div class="col-xs-8 col-md-8">
                            <table id="Tbl_Cajas" class="display compact table-bordered dt-responsive nowrap" cellspacing="0" width="100%"></table>
                        </div>
                        <div class="col-lg-2"></div>

                    </div>
                    <hr />

                    <div class="row" data-bind="visible: hasTI">
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-dismiss="modal" onclick="Factura()">Continuar</button>
                        <button type="button" class="btn btn-secondary" data-dismiss="modal" onclick="habilitar_controles('Cerrar')">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <div id="Modal_Factura" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">

                        <h4 class="modal-title">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FACTURACIÓN</h4>

                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <br />
                    <div class="form-group row">
                        <div class="col-md-2">
                        </div>
                        <label for="example-text-input" class="col-4 col-form-label">EL CLIENTE REQUIERE FACTURA</label>
                        <div class="col-8">
                            <button type="button" id="Si" class="btn btn-default BOTON" onclick="Imprimir('Si')" data-dismiss="modal">SI</button>
                            <button type="button" id="No" class="btn btn-default BOTON" onclick="Imprimir('No')" data-dismiss="modal">NO</button>
                           <%-- <select id="cmb_factura" class="form-control" aria-describedby="basic-addon1">
                                <option value="NO">NO</option>
                                <option value="SI">SI</option>
                            </select>--%>
                        </div>
                    </div>
                    <div class="row" data-bind="visible: hasTI">
                    </div>
                    <div class="modal-footer">
                        <%--<button type="button" class="btn btn-primary" data-dismiss="modal" >IMPRIMIR</button>--%>
                    </div>
                </div>
            </div>
        </div>
        <div id="Ventana_Espera" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Procesando</h4>
                    </div>
                    <div class="modal-body">
                        <div class="progress">
                            <div class="progress-bar progress-bar-striped active" role="progressbar"
                                aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width: 100%">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="progressBackgroundFilter" class="progressBackgroundFilter">
            <div class="processMessage" id="div_progress">
            </div>
        </div>

    </div>

</asp:Content>

