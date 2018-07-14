<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Portal/MasterPage.Master" CodeBehind="RioGrande-In-Cajas.aspx.cs" Inherits="JPV_Portal.Portal.RioGrande_In_Cajas" %>

<asp:Content ID="Head" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <script src="../JS/In_Cajas.js?v=1.0.0"></script>
</asp:Content>


<asp:Content ID="RioGrande" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <section class="container br-nav">
        <nav class="navbar">
            <div class="d-inline-flex " style="width: 100%;">
                <ul class="nav navbar-nav d-inline-flex">
                    <li class="myClass nav-item">
                        <ul class="list-inline-mb-0">
                          <%--  <li class="list-inline-item" id="li-nuevo">
                                <button id="btn_nuevo" type="button" class="btn btn-primary btn_my_class" title="Nuevo"><i class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Nuevo</button></li>--%>
                            <li class="list-inline-item" id="li-guardar">
                                <button id="btn_guardar" type="button" class="btn btn-primary btn_my_class" title="Guardar"><i class="glyphicon glyphicon-floppy-save"></i>&nbsp;&nbsp;Guardar</button></li>
                           <li class="list-inline-item" id="li-historial">
                                <button id="btn_historial" type="button" class="btn btn-primary btn_my_class" title="Historial de compra de cajas"><i class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Historial</button></li>
                           <%-- <li class="list-inline-item" id="li-cancelar" style="display: none">
                                <button id="btn_cancelar" type="button" class="btn btn-primary btn_my_class" title="Cancelar"><i class="glyphicon glyphicon-remove"></i>&nbsp;&nbsp;Cancelar</button>
                            </li>--%>

                        </ul>
                    </li>
                </ul>
            </div>
        </nav>
    </section>
    <hr />
    <div class="container">
        <input type="hidden" id="txt_provedor_id" />
        <input type="hidden" id="txt_caja_id" />
        <input type="hidden" id="txt_caja" />
        <input type="hidden" id="txt_proveedor" />

        <div class="row" id="cont-alta" style="display: block;">
            <div class="form-group row">
                <label for="example-text-input" class="col-1 col-form-label">*Proveedor</label>
                <div class="col-11">
                    <select id="cmb_proveedor" class="form-control"></select>
                </div>
            </div>
            <div class="form-group row">
                <label for="example-text-input" class="col-1 col-form-label">*Cajas</label>
                <div class="col-3">
                    <select id="cmb_cajas" class="form-control"></select>
                </div>
                 <label for="example-text-input" class="col-1 col-form-label">*Cantidad</label>
                <div class="col-3">
                    <input type="text" id="txt_cantidad" class="form-control" placeholder="Cantidad" aria-describedby="basic-addon1" />
                </div>
                  <label for="example-text-input" class="col-1 col-form-label">*Costo</label>
                <div class="col-3">
                    <input type="text" id="txt_costo" class="form-control" placeholder="Costo Total" aria-describedby="basic-addon1" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
