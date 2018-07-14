﻿/*====================================== VARIABLES =====================================*/
var oTable;
var $Correo = '';
var dataSet = [{ Menu: "Usuarios", Etiqueta: "Usuarios" },
               { Menu: "Proveedores", Etiqueta: "Proveedores" },
               { Menu: "Reporte Facturas", Etiqueta: "Reporte" }];

$.extend(true, $.fn.dataTable.defaults, {
    "language": {
        "sEmptyTable": "No hay datos en la tabla",
        "lengthMenu": "Mostando _MENU_ Registro(s) por página",
        "zeroRecords": "Nada Encontrado",
        "info": "Mostrando página _PAGE_ de _PAGES_",
        "infoEmpty": "No hay registros disponibles",
        "infoFiltered": "(filtered from _MAX_ total records)",
        "sLoadingRecords": "Cargando ...",
        "sProcessing": "Por favor espere...",
        "oPaginate": {
            "sFirst": "Primero",
            "sPrevious": "Atras",
            "sNext": "Siguiente",
            "sLast": "Ultimo"
        }
    }
});

/*====================================== INICIO-CARGA =====================================*/

$(document).on('ready', function () {
    inicializar_pagina();
});

/// <summary>
/// FUNCION PARA ESTABLECER EL ESTADO INICIAL DE LA PAGINA 
/// </summary>
function inicializar_pagina() {
    try {
        eventos();
        cargar_tabla();
    } catch (e) {
        asignar_modal("Informe Técnico", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}

/// <summary>
/// FUNCION QUE INICIALIZA LOS MANEJADORES DE EVENTOS.
/// </summary>
function eventos() {
    $('#btn_nuevo').on('click', function (e) {
        e.preventDefault();
        limpiar_controles();
        habilitar_controles('Nuevo');
    });
    
    $('#btn_cancelar').on('click', function (e) {
        e.preventDefault();
        limpiar_controles();
        habilitar_controles('Inicio');
    });
    $('#btn_guardar').on('click', function (e) {
        e.preventDefault();

        var output = validar_datos();
        if (output.Estatus) {
            if ($('#txt_id').val() != null && $('#txt_id').val() != undefined && $('#txt_id').val() != '') {
                Ope_Modificar();
            } else {
                Ope_Alta();
            }
        } else {
            $('#btn_guardar').popModal({
                html: "<h6> Datos requeridos </h6> <hr /> " + output.Mensaje + "<div class='popModal_footer'><button type='button' class='btn btn-primary btn-block' data-popmodal-but='ok'>ok</button></div>",
                placement: 'bottomLeft',
                showCloseBut: true,
                onDocumentClickClose: true,
                onDocumentClickClosePrevent: '',
                overflowContent: false,
                inline: true,
                asMenu: false,
                size: '',
                onOkBut: function (event, el) { },
                onCancelBut: function (event, el) { },
                onLoad: function (el) { },
                onClose: function (el) { }
            });
        }
    });
}

/*====================================== OPERACIONES =====================================*/
/// <summary>
/// FUNCION PARA CARGAR LOS REGISTROS
/// </summary>
function cargar_tabla() {
    var registros = "{}";
    $.ajax({
        url: 'Controllers/Ctrl_Productos.asmx/Consultar',
        method: 'POST',
        cache: false,
        async: true,
        responsive: true,
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (data) {
            if (data.d != undefined && data.d != null) {
                var res = eval("(" + data.d + ")");
                registros = res.data;
            }
            oTable = $('#Tbl_Registros').DataTable({
                destroy: true,
                data: JSON.parse(registros),
                lengthMenu: [10, 25, 50, 75, 100],
                columns: [
                    { title: "Descripción" },
                    { title: "Cajas en Almacén", visible: false},
                    { title: "Producto_ID", visible: false },
                    { title: "Modificar", align: 'center' }
                ],

                columnDefs: [
                   {
                       render: function (data, type, row) {
                           return '<button type="button" class="btn-primary btn-sm" title="Modificar" onclick="Cargar_Informacion(' + "'" + row + "'" + ')"><i class="glyphicon glyphicon-edit"></i></button>'
                       },
                       targets: 3
                   }
                ]
            });
        }
    });
}

/// <summary>
/// Función que ejecuta el alta de los registros
/// </summary>
function Ope_Alta() {
    var Obj_usuario = new Object();

    try {
        Obj_usuario.Descripcion = $('#txt_nombre').val();
        Obj_usuario.Cajas_Stock = "0";

        $.ajax({
            url: 'Controllers/Ctrl_Productos.asmx/Ope_Alta',
            data: "{'Datos':'" + JSON.stringify(Obj_usuario) + "'}",
            type: 'POST',
            cache: false,
            async: true,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (Resultado) {
                var res = eval("(" + Resultado.d + ")");
                if (res.Estatus) {

                    asignar_modal("Correcto", res.Mensaje);
                    jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
                    estado_inicial();
                    //lis.modal('success', 'Success Alert & Notification');
                }
                else {
                    asignar_modal("Advertencia", res.Mensaje);
                    jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
                }
            }
        });
    } catch (e) {
        asignar_modal("", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}
/// <summary>
/// Función que ejecuta el alta de los registros
/// </summary>
function Ope_Modificar() {
    var Obj_usuario = new Object();
    try {
        Obj_usuario.Producto_ID = $('#txt_id').val();
        Obj_usuario.Descripcion = $('#txt_nombre').val();
        Obj_usuario.Cajas_Stock = $('#txt_cajas').val();

        $.ajax({
            url: 'Controllers/Ctrl_Productos.asmx/Ope_Modificar',
            data: "{'Datos':'" + JSON.stringify(Obj_usuario) + "'}",
            type: 'POST',
            cache: false,
            async: true,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (Resultado) {
                var res = eval("(" + Resultado.d + ")");
                if (res.Estatus) {
                    asignar_modal("Correcto", res.Mensaje);
                    jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
                    estado_inicial();
                }
                else {
                    asignar_modal("Advertencia", res.Mensaje);
                    jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
                }
            }
        });
    } catch (e) {
        asignar_modal("", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}


/*====================================== GENERALES =====================================*/
/// <summary>
/// FUNCION PARA ESTABLECER LA PAGINA CON LA CONFIGURACION INICIAL
/// </summary>
function estado_inicial() {
    try {
        cargar_tabla();
        habilitar_controles('Inicio');
        limpiar_controles();
    } catch (e) {
        asignar_modal("Informe Técnico", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}

/// <summary>
/// FUNCION PARA CARGAR LA INFORMACION DEL REGISTRO
/// </summary>
function Cargar_Informacion(row) {
    var res = row.split(",");
    $('#txt_nombre').val(res[0]);
    $('#txt_cajas').val(res[1]);
    $('#txt_id').val(res[2]);
    habilitar_controles("Modificar");
}

/// <summary>
/// FUNCION PARA VALIDAR LOS DATOS REQUERIDOS
/// </summary>
function validar_datos() {
    var output = new Object();
    output.Estatus = true;
    output.Mensaje = '';

    try {
        if ($('#txt_nombre').val() == '' || $('#txt_nombre').val() == undefined || $('#txt_nombre').val() == null) {
            output.Estatus = false;
            output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>NOMBRE</strong>.</span><br />';
        }
        //if ($('#txt_password').val() == '' || $('#txt_password').val() == undefined || $('#txt_password').val() == null) {
        //    output.Estatus = false;
        //    output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>CONTRASEÑA</strong>.</span><br />';
        //}
        //if ($('#txt_email').val() == '' || $('#txt_email').val() == undefined || $('#txt_email').val() == null) {
        //    output.Estatus = false;
        //    output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>CORREO ELECTRONICO</strong>.</span><br />';
        //} else {
        //    oTable.column(1)
        //           .data()
        //           .each(function (value, index) {
        //               if (value !== $Correo) {
        //                   if ($('#txt_email').val() == value) {
        //                       output.Estatus = false;
        //                       output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>CORREO ELECTRONICO (asignado)</strong>.</span><br />';
        //                   }
        //               }
        //           });
        //}


        //if ($('#cmb_estatus :selected').val() == '' || $('#cmb_estatus :selected').val() == undefined || $('#cmb_estatus :selected').val() == null) {
        //    output.Estatus = false;
        //    output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>ESTATUS</strong>.</span><br />';
        //}

        //var selValue = $('input[name=Tipo]:checked').val();
        //if (selValue == '' || selValue == undefined || selValue == null) {
        //    output.Estatus = false;
        //    output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>TIPO DE USUARIO</strong>.</span><br />';
        //}


    } catch (e) {
        asignar_modal("Informe Técnico", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    } finally {
        return output;
    }
}

/// <summary>
/// FUNCION QUE HABILITA LOS CONTROLES DE LA PAGINA DE ACUERDO A LA OPERACION A REALIZAR.
/// </summary>
function habilitar_controles(opcion) {
    var Estatus = false;
    switch (opcion) {
        case "Inicio":
            $('#li-nuevo').css({ display: 'inline-block' });
            $('#li-guardar').css({ display: 'none' });
            $('#li-cancelar').css({ display: 'none' });
            $('#cont-alta').css({ display: 'none' });
            $('#Reg-Datos').css({ display: 'Block' });
            break;
        case "Nuevo":
            $('#li-nuevo').css({ display: 'none' });
            $('#li-guardar').css({ display: 'inline-block' });
            $('#li-cancelar').css({ display: 'inline-block' });
            $('#cont-alta').css({ display: 'Block' });
            $('#Reg-Datos').css({ display: 'none' });
            break;
        case "Modificar":
            $('#li-nuevo').css({ display: 'none' });
            $('#li-editar').css({ display: 'none' });
            $('#li-guardar').css({ display: 'inline-block' });
            $('#li-cancelar').css({ display: 'inline-block' });
            $('#cont-alta').css({ display: 'Block' });
            $('#Reg-Datos').css({ display: 'none' });
            break;
    }
}
/// <summary>
/// FUNCION PARA LIMPIAR LOS CONTROLES
/// </summary>
function limpiar_controles() {
    $('input[type=text]').each(function () { $(this).val(''); });
    $('input[type=password]').each(function () { $(this).val(''); });
    $('input[type=hidden]').each(function () { $(this).val(''); });
    $('select').each(function () { $(this).val(''); });
}

/// <summary>
/// CREAR MODAL MENSAJE
/// </summary>
function asignar_modal(titulo, mensaje) {
    $('#title').text('');
    $('#Ml_boby').text('');
    $('#title').append('<span class="glyphicon glyphicon-option-vertical"></span> ' + titulo);
    $('#Ml_boby').append(mensaje);
}
