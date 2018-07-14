/*====================================== VARIABLES =====================================*/
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
        estado_inicial();

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
        $('#add').val('0');

    });

    $('#btn_cancelar').on('click', function (e) {
        e.preventDefault();
        limpiar_controles();
        $('#cmb_producto').select2("val", "Seleccione");
        $('#cmb_cliente').select2("val", "Seleccione");
        habilitar_controles('Inicio');
        var table = $('#Tbl_Registros').DataTable();
        table.destroy();
        $('#Tbl_Registros').empty(); // empty in case the columns change
    });

    $('#btn_agregar').on('click', function (e) {
        e.preventDefault();
        var output = validar_datos();
        if (output.Estatus) {
            Cargar_Informacion();
            limpiar_agregar();
        }
        else {
            $('#btn_agregar').popModal({
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

    $('#btn_guardar').on('click', function (e) {
        e.preventDefault();
        Guardar();
        var table = $('#Tbl_Registros').DataTable();
        table.destroy();
        $('#Tbl_Registros').empty(); // empty in case the columns change
        limpiar_controles();
        $('#cmb_producto').select2("val", "Seleccione");
        $('#cmb_cliente').select2("val", "Seleccione");
        habilitar_controles('Inicio');
        //setTimeout(5000);
        //window.open("../../Temporal/Productos_Traspaso.xlsx", "FW9", 'toolbar=0,directories=0,menubar=0,status=0,scrollbars=0,resizable=1,width=500,height=100');


    });
}

/*====================================== OPERACIONES =====================================*/
/// <summary>
/// Función que ejecuta el alta de los registros
/// </summary>
function Guardar() {
    var Obj_usuario = new Object();
    var Obj_Param_Lista = new Object();

    try {
        Obj_usuario.Cliente_ID = $('#txt_cliente_id').val();
        Obj_usuario.Cliente = $('#txt_cliente').val();
        Obj_usuario.Estatus = "Pendiente";
        var suma = 0;
        registros = $('#Tbl_Registros').DataTable().rows().data();
        param = new Array();

        if (registros.length > 0) {
        for (var i = 0; i < registros.length; i++) {
            Obj_Param_Lista = new Object();
            Obj_Param_Lista.Proveedor_Producto = registros[i][1];
            Obj_Param_Lista.Producto =  registros[i][2];
            Obj_Param_Lista.Cantidad =  registros[i][3];
            Obj_Param_Lista.Proveedor = registros[i][4];
            Obj_Param_Lista.Entrada_ID = registros[i][5];
            Obj_Param_Lista.Chofer = registros[i][6];

            suma = suma * 1 + registros[i][3] * 1;
            param[i] = Obj_Param_Lista;

           }
        Obj_usuario.Total = suma;

        $.ajax({
            url: 'Controllers/Ctrl_Salida_Mercancia.asmx/Guardar',
            data: "{'Datos':'" + JSON.stringify(Obj_usuario) + "', 'Lista':'" + JSON.stringify(param) + "'}",
            type: 'POST',
            cache: false,
            async: true,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (Resultado) {
                if (data.d != undefined && data.d != null) {
                    var res = eval("(" + data.d + ")");
                    if (res.Estatus) {
                        registros = res.Registros;
                        window.open("../../Temporal/Productos_Traspaso.xlsx", "FW9", 'toolbar=0,directories=0,menubar=0,status=0,scrollbars=0,resizable=1,width=500,height=100');
                    }
                    else
                        mostrar_mensaje("Advertencia", res.Mensaje);
                }
            },
            complete: function () {
                window.open("../../Temporal/Productos_Traspaso.xlsx", "FW9", 'toolbar=0,directories=0,menubar=0,status=0,scrollbars=0,resizable=1,width=500,height=100');
            }
        });
        } else {
            asignar_modal("Advertencia", 'No se ha cargado ningun material a la lista');
            jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });

        }
    } catch (e) {
        asignar_modal("", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}
/// <summary>
/// FUNCION PARA TRAER LA INFORMACIÓN AL COMBO
/// </summary>
function Cargar_Cmb_Clientes() {
    try {
        $('#cmb_cliente').select2({
            theme: "classic",
            language: "es",
            placeholder: 'Seleccione',
            allowClear: true,
            minimumInputLength: 0,
            tags: false,
            tokenSeparators: [','],
            ajax: {
                url: 'Controllers/Ctrl_Salida_Mercancia.asmx/Cargar_Cmb_Clientes',
                cache: "true",
                dataType: 'json',
                cache: "true",
                type: "POST",
                delay: 250,
                cache: true,
                params: {
                    contentType: 'application/json; charset=utf-8'
                },
                quietMillis: 100,
                results: function (data) {
                    return { results: data };
                },
                data: function (params) {
                    return {
                        q: params.term,
                        page: params.page
                    };
                },
                processResults: function (data, page) {
                    return {
                        results: data
                    };
                },
            },
            templateResult: formato_conceptos_solo
        });

        $('#cmb_cliente').on("select2:select", function (evt) {
            $('#txt_cliente').val(evt.params.data.text);
            $('#txt_cliente_id').val(evt.params.data.id);

        });

        $("#cmb_cliente").on("select2:unselecting instead", function (e) {
            $('#txt_cliente').val('');
            $('#txt_cliente_id').val('');
        });
    } catch (e) {
        mostrar_mensaje('Información técnica', e);
    }
}
/// <summary>
/// FUNCION PARA TRAER LA INFORMACIÓN AL COMBO
/// </summary>
function Cargar_Cmb_Producto() {
    try {
        $('#cmb_producto').select2({
            theme: "classic",
            language: "es",
            placeholder: 'Seleccione',
            allowClear: true,
            minimumInputLength: 0,
            tags: false,
            tokenSeparators: [','],
            ajax: {
                url: 'Controllers/Ctrl_Salida_Mercancia.asmx/Cargar_Cmb_Producto',
                cache: "true",
                dataType: 'json',
                cache: "true",
                type: "POST",
                delay: 250,
                cache: true,
                params: {
                    contentType: 'application/json; charset=utf-8'
                },
                quietMillis: 100,
                results: function (data) {
                    return { results: data };
                },
                data: function (params) {
                    return {
                        q: params.term,
                        page: params.page
                    };
                },
                processResults: function (data, page) {
                    return {
                        results: data
                    };
                },
            },
            templateResult: formato_conceptos
        });

        $('#cmb_producto').on("select2:select", function (evt) {
            $('#txt_proveedor_producto').val(evt.params.data.text);
            $('#txt_producto_id').val(evt.params.data.id);
            $('#txt_producto').val(evt.params.data.tag);
            $('#txt_proveedor').val(evt.params.data.tag1);
            $('#txt_cantidad_entrada').val(evt.params.data.tag2);
            $('#txt_cantidad').val(evt.params.data.tag2);
            $('#txt_chofer').val(evt.params.data.tag4);

        });

        $("#cmb_producto").on("select2:unselecting instead", function (e) {
            $('#txt_proveedor_producto').val('');
            $('#txt_producto').val('');
            $('#txt_producto_id').val('');
            $('#txt_chofer').val('');
        });
    } catch (e) {
        mostrar_mensaje('Información técnica', e);
    }
}
/// <summary>
/// FUNCION PARA CARGAR LOS REGISTROS
/// </summary>
function Llenar_Grid() {

    try {
        oTable = $('#Tbl_Registros').DataTable({
            destroy: true,
            //data: JSON.parse(registros),
            lengthMenu: [10, 25, 50, 75, 100],
            columns: [
                { title: "Cliente" },
                { title: "Producto" },
                { title: "Producto", visible:false},
                { title: "Cantidad", },
                { title: "Proveedor", visible: false },
                { title: "Entrada_ID", visible: false },
                { title: "Chofer", visible: false },
                { title: "ID", visible: false },
                { title: "Quitar", align: 'center' }
            ],
            columnDefs: [
               {
                   render: function (data, type, row) {
                       return '<button type="button" class="btn-primary btn-sm" title="Quitar" onclick="Quitar(' + "'" + row + "'" + ')"><i class="glyphicon glyphicon-delete"></i></button>'
                   },
                   targets:8
               }
            ],
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
        habilitar_controles('Inicio');
        Llenar_Grid();
        limpiar_controles();
    } catch (e) {
        asignar_modal("Informe Técnico", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}

/// <summary>
/// FUNCION PARA CARGAR LA INFORMACION DEL REGISTRO
/// </summary>
function Cargar_Informacion() {
    var t = $('#Tbl_Registros').DataTable();
    var i = $('#add').val() * 1 + 1 * 1;

    t.row.add([
            $('#txt_cliente').val(),
            $('#txt_proveedor_producto').val(),
            $('#txt_producto').val(),
            $('#txt_cantidad').val(),
            $('#txt_proveedor').val(),
            $('#txt_producto_id').val(),
            $('#txt_chofer').val(),
            i,

    ]).draw(false);
}
/// <summary>
/// FUNCION PARA ELIMINAR LA INFORMACION DEL REGISTRO
/// </summary>
function Quitar(ID) {

    var table = $('#Tbl_Registros').DataTable();

    try {
        var res = ID.split(",");
        table.row(res[7]).remove().draw();
    //row.remove();

    } catch (e) {
        mostrar_mensaje('Informe Tecnico', e);
    }
}
/// <summary>
/// FUNCION PARA VALIDAR LOS DATOS REQUERIDOS
/// </summary>
function validar_datos() {
    var output = new Object();
    output.Estatus = true;
    output.Mensaje = '';

    try {
        if ($('#txt_cliente').val() == '' || $('#txt_cliente').val() == undefined || $('#txt_cliente').val() == null) {
            output.Estatus = false;
            output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>--CLIENTE</strong>.</span><br />';
        }
        if ($('#txt_producto').val() == '' || $('#txt_producto').val() == undefined || $('#txt_producto').val() == null) {
            output.Estatus = false;
            output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>--PRODUCTO</strong>.</span><br />';
        }
        if ($('#txt_cantidad').val() == '' || $('#txt_cantidad').val() == undefined || $('#txt_cantidad').val() == null) {
            output.Estatus = false;
            output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>--CANTIDAD</strong>.</span><br />';
        }
        //if ($('#txt_precio').val() == '' || $('#txt_precio').val() == undefined || $('#txt_precio').val() == null) {
        //    output.Estatus = false;
        //    output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>--PRECIO</strong>.</span><br />';
        //}
        if ($('#txt_cantidad').val()*1 > $('#txt_cantidad_entrada').val()*1) {
            output.Estatus = false;
            output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>--LA CANTIDAD ES MAYOR A LA QUE SE ENCUENTRA EN EL ALMACEN</strong>.</span><br />';
        }


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
            Estatus = false;
            $('#li-nuevo').css({ display: 'inline-block' });
            $('#li-guardar').css({ display: 'none' });
            $('#li-cancelar').css({ display: 'none' });
            $('#cont-alta').css({ display: 'Block' });
            $('#Reg-Datos').css({ display: 'none' });
            limpiar_controles();

           

            break;
        case "Nuevo":
            Estatus = true;
            Llenar_Grid();
            $('#li-nuevo').css({ display: 'none' });
            $('#li-guardar').css({ display: 'inline-block' });
            $('#li-cancelar').css({ display: 'inline-block' });
            $('#cont-alta').css({ display: 'Block' });
            $('#Reg-Datos').css({ display: 'Block' });
            limpiar_controles();
            Cargar_Cmb_Clientes();
            Cargar_Cmb_Producto();
            break;
    }

    $('#cmb_cliente').attr({ disabled: !Estatus });
    $('#cmb_producto').attr({ disabled: !Estatus });
    $('#btn_agregar').attr({ disabled: !Estatus });
    $('#txt_cantidad').attr({ disabled: !Estatus });
    //$('#txt_precio').attr({ disabled: !Estatus });

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
/// FUNCION PARA LIMPIAR LOS CONTROLES
/// </summary>
function limpiar_agregar(){
    $('#txt_cantidad').val('');
    //$('#txt_precio').val('');
    $('#cmb_producto').select2("val", "Seleccione");
    $('#txt_producto_id').val('');
    $('#txt_producto').val('');
    $('#txt_proveedor_producto').val('');
}
/// <summary>
/// formato_conceptos
/// </summary>
/// <usuario_creo>MARÍA CHANTAL ORIGEL SEGURA</usuario_creo>
/// <fecha_creo>24 OCTUBRE 2017</fecha_creo>
function formato_conceptos(row) {
    var $row = $('<span style="font-family:Century Gothic;font-size:12px;"><i class="fa fa-tag" style="color:#000;"></i>&nbsp;' + row.text + '</span>');

    var _salida = '<span>' +
    '<i class="fa fa-tag" style="color:#0060aa;"></i>&nbsp;' + row.text + ' ' + row.tag4 + ' ----- ' +
    '</span>';
    _salida += '<span>' +
  '<i class="glyphicon glyphicon-option-vertical" style="color:#0060aa;"></i>&nbsp;' + row.tag3 +
  '</span>';

    return $(_salida);
};
function formato_conceptos_solo(row) {
    var $row = $('<span style="font-family:Century Gothic;font-size:12px;"><i class="fa fa-tag" style="color:#000;"></i>&nbsp;' + row.text + '</span>');

    var _salida = '<span>' +
    '<i class="fa fa-tag" style="color:#0060aa;"></i>&nbsp;' + row.text +
    '</span>';
  

    return $(_salida);
};
/// <summary>
/// CREAR MODAL MENSAJE
/// </summary>
function asignar_modal(titulo, mensaje) {
    $('#title').text('');
    $('#Ml_boby').text('');
    $('#title').append(titulo + '<span class="glyphicon glyphicon-option-vertical"></span> ');
    $('#Ml_boby').append(mensaje);
}
