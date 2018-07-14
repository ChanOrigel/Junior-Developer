<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Portal/MasterPage.Master" CodeBehind="RioGrande-Cajas.aspx.cs" Inherits="JPV_Portal.Portal.RioGrande_Cajas" %>

<asp:Content ID="Head" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <script src="../JS/Cajas.js?v=1.0.0"></script>
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
        <input type="hidden" id="txt_caja_id" />
        <div class="row" id="Reg-Datos">
            <div class="col-xs-12 col-md-12">
                <table id="Tbl_Registros" class="display compact table-bordered dt-responsive nowrap" cellspacing="0" width="100%"></table>
            </div>
        </div>
        <div class="row" id="cont-alta" style="display: none;">
            <h5>En existencia: <span id="cantidad" class="badge badge-info">0</span></h5>
            <hr />
            <div class="form-group row">
                <label for="example-text-input" class="col-2 col-form-label">*Descripción</label>
                <div class="col-10">
                    <input type="text" id="txt_descripcion" class="form-control" placeholder="Descripción" aria-describedby="basic-addon1" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>