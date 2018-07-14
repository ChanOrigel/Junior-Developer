<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Portal/MasterPage.Master" CodeBehind="RioGrande-Proveedores.aspx.cs" Inherits="JPV_Portal.Portal.RioGrande_Proveedores" %>

<asp:Content ID="Head" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <script src="../JS/Proveedor.js?v=1.0.0"></script>
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
    <hr />
    <div class="container">
        <input type="hidden" id="txt_proveedor_id" />
        <div class="row" id="Reg-Datos">
            <div class="col-xs-12 col-md-12">
                <table id="Tbl_Registros" class="display compact table-bordered dt-responsive nowrap" cellspacing="0" width="100%"></table>
            </div>
        </div>
        <div class="row" id="cont-alta" style="display: none;">                        
            <div class="form-group row">
                <label for="example-text-input" class="col-2 col-form-label">*Nombre</label>
                <div class="col-10">
                    <input type="text" id="txt_nombre" class="form-control" placeholder="Nombre" aria-describedby="basic-addon1" />
                </div>
            </div>
            <div class="form-group row">
                <label for="example-text-input" class="col-2 col-form-label">Domicilio/Origen</label>
                <div class="col-10">
                    <input type="text" id="txt_ubicacion" class="form-control" placeholder="Domicilio/Origen" aria-describedby="basic-addon1" />
                </div>
            </div>
            <div class="form-group row">
                <label for="example-text-input" class="col-2 col-form-label">Teléfono</label>
                <div class="col-4">
                    <input type="text" id="txt_telefono" class="form-control" placeholder="Telefono" aria-describedby="basic-addon1" />
                </div>
                <label for="example-text-input" class="col-2 col-form-label">*Tipo Proveedor</label>
                <div class="col-4">
                    <select id="cmb_tipo" class="form-control text-center">
                        <option value="" selected="selected">Seleccione</option>
                        <option value="CAJAS">CAJAS</option>
                        <option value="JITOMATE">JITOMATE</option>
                        <option value="OTRO">OTRO</option>
                    </select>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
