<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Portal/MasterPage.Master" CodeBehind="Frm_Salida_Mercancia.aspx.cs" Inherits="JPV_Portal.Portal.Frm_Salida_Mercancia" %>



<asp:Content ID="Head" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <script src="../JS/Js_Salida_Mercancia.js"></script>
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
        <input type="hidden" id="add" />
        <input type="hidden" id="txt_cantidad_entrada" />
        <input type="hidden" id="txt_cliente" />
        <input type="hidden" id="txt_cliente_id" />
        <input type="hidden" id="txt_producto_id" />
        <input type="hidden" id="txt_producto" />
        <input type="hidden" id="txt_proveedor_producto" />
        <input type="hidden" id="txt_proveedor" />
        <input type="hidden" id="txt_chofer" />

        <div class="row" id="cont-alta">
            <div class="form-group row">
                <label for="example-text-input" class="col-1 col-form-label">*Cliente</label>
                <div class="col-11">
                    <select id="cmb_cliente" class="form-control" aria-describedby="basic-addon1"></select>
                </div>
            </div>
            <div class="form-group row">
                <label for="example-text-input" class="col-1 col-form-label">*Producto</label>
                <div class="col-5">
                    <select id="cmb_producto" class="form-control" aria-describedby="basic-addon1"></select>
                </div>
                <label for="example-text-input" class="col-1 col-form-label">*Cantidad</label>
                <div class="col-5">
                    <input type="text" id="txt_cantidad" class="form-control" placeholder="# Cajas" aria-describedby="basic-addon1" />
                </div>
               <%--  <label for="example-text-input" class="col-1 col-form-label">*Precio</label>
                <div class="col-2">
                    <input type="text" id="txt_precio" class="form-control" placeholder="Precio por caja" aria-describedby="basic-addon1" />
                </div>--%>
            </div>
          <br/>
            <div class="form-group row">
                <div class="col-md-11"></div>
                <div class="col-lg-1">
                    <button id="btn_agregar" type="button" class="btn btn-success btn_my_class" title="Cargar" style="border-radius: 5px 5px">
                        <i class="glyphicon glyphicon-upload"></i>&nbsp;&nbsp;Agregar
                    </button>
                </div>
            </div>
        </div>
          <hr/>

        <div class="row" id="Reg-Datos">
            <div class="col-xs-12 col-md-12">
                <table id="Tbl_Registros" class="display compact table-bordered dt-responsive nowrap" cellspacing="0" width="100%"></table>
            </div>
        </div>

    </div>

</asp:Content>
