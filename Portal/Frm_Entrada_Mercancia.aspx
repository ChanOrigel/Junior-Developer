<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Portal/MasterPage.Master" CodeBehind="Frm_Entrada_Mercancia.aspx.cs" Inherits="JPV_Portal.Portal.Frm_Entrada_Mercancia" %>



<asp:Content ID="Head" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <script src="../JS/Js_Entrada_Mercancia.js"></script>
</asp:Content>

<asp:Content ID="RioGrande" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <section class="container br-nav">
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
    </section>
    <hr />
    <div class="container">
        <input type="hidden" id="txt_id" />
        <input type="hidden" id="txt_cantidad_descontar" />
        <input type="hidden" id="txt_cajas_anterior" />

        <input type="hidden" id="txt_proveedor_id" />
        <input type="hidden" id="txt_producto_id" />
        <input type="hidden" id="txt_producto" />
        <div class="row" id="Reg-Datos">
            <div class="col-xs-12 col-md-12">
                <table id="Tbl_Registros" class="display compact table-bordered dt-responsive nowrap" cellspacing="0" width="100%"></table>
            </div>
        </div>
        <div class="row" id="cont-alta">
            <div class="form-group row">
                <div class="form-check form-check-inline">
                    <label class="form-check-label">
                        <input class="form-check-input" type="radio" name="Tipo" id="chk_Regis" value="Registrado" checked="checked" onclick="Proveedor_Registrado();" />
                        Proveedor Registrado
                    </label>
                </div>
                <div class="form-check form-check-inline">
                    <label class="form-check-label">
                        <input class="form-check-input" type="radio" name="Tipo" id="chk_No_Regis" value="No_Registrado" onclick="Proveedor();" />
                        Proveedor No Registrado
                    </label>
                </div>
            </div>
            <hr />
            <%-- Proveedor registrado y no registrado --%>
            <div class="form-group row">
                <label for="example-text-input" class="col-2 col-form-label">*Proveedor</label>
                <div class="col-4" id="Registrado">
                    <select id="cmb_proveedor" class="form-control" aria-describedby="basic-addon1"></select>
                </div>
                <div class="col-4" id="No_Registrado" style="display: none">
                    <input type="text" id="txt_proveedor" class="form-control" placeholder="Proveedor No Registrado" aria-describedby="basic-addon1" />
                </div>
                 <label for="example-text-input" class="col-1 col-form-label">*Chofer</label>
                <div class="col-4">
                    <input type="text" id="txt_chofer" class="form-control" placeholder="Chofer" aria-describedby="basic-addon1" />
                </div>
            </div>

            <%-- fin proveedor --%>

            <div class="form-group row">
                <label for="example-text-input" class="col-2 col-form-label">*Producto</label>
                <div class="col-4">
                    <select id="cmb_producto" class="form-control" aria-describedby="basic-addon1"></select>
                </div>

                <label for="example-text-input" class="col-1 col-form-label">Peso</label>
                <div class="col-2">
                    <input type="text" id="txt_toneladas" class="form-control" placeholder="kg." aria-describedby="basic-addon1" />
                </div>

                <label for="example-text-input" class="col-1 col-form-label">Cajas</label>
                <div class="col-2">
                    <input type="text" id="txt_cajas" class="form-control" placeholder="# Cajas" aria-describedby="basic-addon1" />
                </div>
            </div>

            <div class="form-group row">
                <label for="example-text-input" class="col-2 col-form-label">Notas</label>
                <div class="col-10">
                    <textarea id="txt_notas" class="form-control" placeholder="Anotaciones de la mercancía" aria-describedby="basic-addon1"></textarea>

                </div>
            </div>
        </div>


        <div id="Modal_Excel" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">

                        <h4 class="modal-title">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Cargar Mercancía por medio de Excel</h4>

                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <br />
                    <div class="form-group row">
                        <div class="col-md-1">
                        </div>
                        <label for="example-text-input" class="col-3 col-form-label" id="Etiqueta">Seleccionar Excel</label>
                        <div class="col-4">
                            <button id="Btn_Mascara_Excel" type="button" class="btn btn-primary btn_my_class" style="width: 100%; text-align: center; cursor: pointer; border-radius: 7px 7px"></button>
                            <input id="Fl_Ruta_Excel" style="width: 100%;" type="file" name="file" accept=".xls,.xlsx" />
                            <input type="hidden" id="Flag" />
                            <input type="hidden" id="Hf_Reg_Tbl" />
                            <input type="hidden" id="Txt_Registros_Detalle" />
                            <input type="hidden" id="Hf_Ruta_Excel" />
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-lg-2">
                            <button id="Btn_Leer_Excel" type="button" class="btn btn-success btn_my_class" title="Cargar" style="border-radius: 5px 5px">
                                <i class="glyphicon glyphicon-upload"></i>&nbsp;&nbsp;Cargar
                            </button>
                        </div>
                    </div>
                    <div class="row" data-bind="visible: hasTI">
                        <div class="col-md-1">
                        </div>
                        <div class="col-xs-10 col-md-10">
                            <table id="Tbl_Excel" class="display compact table-bordered dt-responsive nowrap" cellspacing="0" width="100%"></table>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-dismiss="modal" onclick="Guardar_Excel()">Guardar</button>
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <div id="Modal_Historial" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">

                        <h4 class="modal-title">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;Historial de Entradas</h4>

                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <br />
                    <div class="form-group row">
                        <div class="col-md-1">
                        </div>
                        <label for="example-text-input" class="col-2 col-form-label">Fecha Inicio</label>
                        <div class="col-3">
                            <input id="Txt_Inicio" class="glyphicon glyphicon-calendar" style="width: 100%;" type="text" />
                        </div>
                        <label for="example-text-input" class="col-2 col-form-label">Fecha Fin</label>
                        <div class="col-3">
                            <input id="Txt_Fin" class="glyphicon glyphicon-calendar" style="width: 100%;" type="text" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-md-10"></div>
                        <div class="col-lg-1">
                            <button id="btn_consultar" type="button" class="btn btn-success btn_my_class" title="Cargar" style="border-radius: 5px 5px">
                                <i class="glyphicon glyphicon-upload"></i>&nbsp;&nbsp;Consultar
                            </button>
                        </div>
                    </div>
                    <hr />

                    <div class="row" data-bind="visible: hasTI">
                        <div class="col-md-1">
                        </div>
                        <div class="col-xs-10 col-md-10">
                            <table id="Tbl_Historial" class="display compact table-bordered dt-responsive nowrap" cellspacing="0" width="100%"></table>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
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
