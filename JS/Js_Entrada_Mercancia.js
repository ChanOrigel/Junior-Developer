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
        cargar_tabla();

        $('#Txt_Inicio').datepicker({
            uiLibrary: 'bootstrap4'
        });
        $('#Txt_Fin').datepicker({
            uiLibrary: 'bootstrap4'
        });
        //$('#Txt_Inicio').datetimepicker({
        //    locale: 'es',
        //    format: "DD/MM/YYYY"
        //});
        //$('#Txt_Fin').datetimepicker({
        //    locale: 'es',
        //    format: "DD/MM/YYYY"
        //    //format: "YYYY/MM/DD"
        //});

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

    $('#btn_excel').on('click', function (e) {
        e.preventDefault();
        habilitar_controles('Mostrar');
        var modal = $('#Modal_Excel');
        modal.modal({ backdrop: 'static', keyboard: false });

        crear_mascaras_file_control();
        document.getElementById('Fl_Ruta_Excel').addEventListener('change', Excel_Seleccionado, false);
    });

    $('#Btn_Leer_Excel').on('click', function (e) {
        e.preventDefault();
        if ($('#Fl_Ruta_Excel').val() != null && $('#Fl_Ruta_Excel').val() != undefined && $('#Fl_Ruta_Excel').val() != "") {
            //$('#Btn_Leer_Excel').attr({ disabled: true });
            Leer_Excel();
            $('#Flag').val('1');
        } else {
            mostrar_mensaje('Advertencia', 'Selecciona un archivo a cargar.');
        }
    });

    $('#btn_historial').on('click', function (e) {
        e.preventDefault();
        var modal = $('#Modal_Historial');
        modal.modal({ backdrop: 'static', keyboard: false });
    });


    $('#btn_consultar').on('click', function (e) {
        e.preventDefault();
        Historial();
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
/// Función que consulta y carga los registros para una tabla.
/// </summary>
/// <usuario_creo>MARÍA CHANTAL ORIGEL SEGURA</usuario_creo>
/// <fecha_creo>24 OCTUBRE 2017</fecha_creo>
function cargar_tabla() {
    var registros = "{}";
    try {

        $.ajax({
            url: 'Controllers/Ctrl_Entrada_Mercancia.asmx/Consultar',
            method: 'POST',
            cache: false,
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (data) {
                if (data.d != undefined && data.d != null) {
                    var res = eval("(" + data.d + ")");
                    registros = res.Registros;
                    Llenar_Grid_Registros(registros);

                }
            }
        });
    } catch (e) {
        //asignar_modal("", e);
        //jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }

}
/// <summary>
/// Función que consulta y carga los registros.
/// </summary>
/// <usuario_creo>MARÍA CHANTAL ORIGEL SEGURA</usuario_creo>
/// <fecha_creo>24 OCTUBRE 2017</fecha_creo>
function Llenar_Grid_Registros(registros) {
    try {

        $('#Tbl_Registros').bootstrapTable('destroy');
        $('#Tbl_Registros').bootstrapTable({
            data: JSON.parse(registros),
            method: 'POST',
            height: 400,
            striped: true,
            pagination: true,
            pageSize: 50,
            pageList: [10, 25, 50, 100, 200],
            search: true,
            showColumns: false,
            showRefresh: false,
            minimumCountColumns: 2,
            clickToSelect: true,
            columns: [
                { field: 'Producto', title: 'Producto', align: 'left', valign: 'center', sortable: true, clickToSelect: false },
                { field: 'Cantidad_Descontar', title: 'Cajas', align: 'left', valign: 'center', sortable: true, clickToSelect: false },
                { field: 'Proveedor', title: 'Proveedor', align: 'left', valign: 'center', sortable: true, clickToSelect: false },
                { field: 'Chofer', title: 'Chofer', align: 'left', valign: 'center', sortable: true, clickToSelect: false },
                { field: 'Notas', title: 'Notas', align: 'left', valign: 'center', sortable: true, clickToSelect: false },
                { field: 'Fecha_Creo', title: 'Fecha ', align: 'left', valign: 'center', sortable: true, clickToSelect: false },
                {
                    field: 'Modificar', title: '', align: 'center', formatter: function (index, value, row) {
                        return '<button type="button" class="btn-primary btn-sm" title="Modificar"><i class="glyphicon glyphicon-edit"></i></button>'
                        //return '<div><a class="remove ml10" id="' + index + '" href="javascript:void(0)"><i class="glyphicon glyphicon-remove"></i></a></div>';
                    }
                },
                { title: "Proveedor", visible: false },
                { field: 'Entrada_ID', visible: false },
                { field: 'Estatus', visible: false },
                { title: "Toneladas", visible: false },

            ],
            onClickCell: function (field, value, row, $element) {
                if (field == 'Modificar') {
                    $('#txt_proveedor').val(row.Proveedor);
                    $('#txt_toneladas').val(row.Toneladas);
                    $('#txt_producto').val(row.Producto);
                    $('#txt_cajas').val(row.Cantidad_Descontar);
                    $('#txt_cajas_anterior').val(row.Cantidad_Descontar);
                    $('#txt_notas').val(row.Notas);
                    $('#txt_id').val(row.Entrada_ID);
                    $('#txt_cantidad_descontar').val(row.Cantidad_Descontar);
                    $('#txt_chofer').val(row.Chofer);

                    habilitar_controles("Modificar");

                    //$("#cmb_proveedor").select2({
                    //    data: [{text: row.Proveedor }]
                    //})
                    //$('#cmb_proveedor').val(row.Material_ID).trigger('change');
                }
            },
           
        });
    } catch (e) {
        //asignar_modal("", e);
        //jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }

}
/// <summary>
/// FUNCION PARA CARGAR LOS REGISTROS
/// </summary>
//function cargar_tabla() {
//    var registros = "{}";
//    $.ajax({
//        url: 'Controllers/Ctrl_Entrada_Mercancia.asmx/Consultar',
//        method: 'POST',
//        cache: false,
//        async: true,
//        responsive: true,
//        contentType: "application/json; charset=utf-8",
//        dataType: 'json',
//        success: function (data) {
//            if (data.d != undefined && data.d != null) {
//                var res = eval("(" + data.d + ")");
//                registros = res.data;
//            }
//            oTable = $('#Tbl_Registros').DataTable({
//                destroy: true,
//                data: JSON.parse(registros),
//                lengthMenu: [10, 25, 50, 75, 100],
//                columns: [
//                    { title: "Proveedor" },
//                    { title: "Toneladas", visible: false },
//                    { title: "Estatus", visible: false },
//                    { title: "Producto" },
//                    { title: "Cajas",},
//                    { title: "Notas" },
//                    { title: "Fecha" },
//                    { title: "Entrada_ID", visible:false },
//                    { title: "Cantidad", visible: false },
//                    { title: "Modificar", align: 'center' }
                    //$('#txt_proveedor').val(res[0]);
                    //$('#txt_toneladas').val(res[1]);
                    //$('#txt_producto').val(res[3]);
                    //$('#txt_cajas').val(res[4]);
                    //$('#txt_cajas_anterior').val(res[4]);
                    //$('#txt_notas').val(res[5]);
                    //$('#txt_id').val(res[7]);
                    //$('#txt_cantidad_descontar').val(res[8]);
//                ],
//                columnDefs: [
//                   {
//                       render: function (data, type, row) {
//                           return '<button type="button" class="btn-primary btn-sm" title="Modificar" onclick="Cargar_Informacion(' + "'" + row + "'" + ')"><i class="glyphicon glyphicon-edit"></i></button>'
//                       },
//                       targets: 9
//                   }
//                ]
//            });
//        }
//    });
//}
/// <summary>
/// Función que ejecuta el alta de los registros
/// </summary>
function Ope_Alta() {
    var Obj_usuario = new Object();

    try {
        Obj_usuario.Proveedor = $('#txt_proveedor').val();
        Obj_usuario.Proveedor_ID = $('#txt_proveedor_id').val();
        //Obj_usuario.Producto = "Jitomate";
        Obj_usuario.Producto = $('#txt_producto').val();
        Obj_usuario.Chofer = $('#txt_chofer').val();

        Obj_usuario.Proveedor_Producto = $('#txt_proveedor').val() + " " + $('#txt_producto').val();
        Obj_usuario.Cantidad_Descontar = $('#txt_cajas').val();
        Obj_usuario.Toneladas = $('#txt_toneladas').val();
        Obj_usuario.Cajas = $('#txt_cajas').val();
        Obj_usuario.Notas = $('#txt_notas').val();
        if ($('#txt_cajas').val() > 0)
            Obj_usuario.Estatus = "Almacen";
        else
            Obj_usuario.Estatus = "Agotado";

        $.ajax({
            url: 'Controllers/Ctrl_Entrada_Mercancia.asmx/Ope_Alta',
            data: "{'Datos':'" + JSON.stringify(Obj_usuario) + "'}",
            type: 'POST',
            cache: false,
            async: true,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (Resultado) {
                var res = eval("(" + Resultado.d + ")");
                //if (res.Estatus) {

                //    asignar_modal("Correcto", res.Mensaje);
                //    jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
                    estado_inicial();
                    //lis.modal('success', 'Success Alert & Notification');
                //}
                //else {
                //    asignar_modal("Advertencia", res.Mensaje);
                //    jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
                //}
            }
        });
    } catch (e) {
        //asignar_modal("", e);
        //jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}
/// <summary>
/// Función que ejecuta el alta de los registros
/// </summary>
function Ope_Modificar() {
    var Obj_usuario = new Object();
    try {

        Obj_usuario.Entrada_ID = $('#txt_id').val();
        Obj_usuario.Proveedor = $('#txt_proveedor').val();
        Obj_usuario.Proveedor_ID = $('#txt_proveedor_id').val();
        Obj_usuario.Chofer = $('#txt_chofer').val();
        Obj_usuario.Producto = $('#txt_producto').val();
        Obj_usuario.Proveedor_Producto = $('#txt_proveedor').val() + " " + $('#txt_producto').val();
        Obj_usuario.Toneladas = $('#txt_toneladas').val();
        Obj_usuario.Cajas = $('#txt_cajas').val();
        var catalogo=0
        //Por si modifica las cajas que entraron
        if ($('#txt_cajas_anterior').val()*1 > $('#txt_cajas').val()*1) //si la cantidad anterior es mayor a la modificada
        {
            var a = $('#txt_cajas_anterior').val();
            var ar = $('#txt_cantidad_descontar').val();

            var descontadas = $('#txt_cajas_anterior').val() * 1 - $('#txt_cantidad_descontar').val()*1;//Cantidad descontadas
            if ($('#txt_cajas').val() <= descontadas) //Si la cantidad modificada es menor a la descontada
            {
                Obj_usuario.Estatus = "Agotado";
                catalogo = descontadas - $('#txt_cajas').val()*1;
            }
            else {
                var anterior = $('#txt_cajas_anterior').val();
                var modificadas = $('#txt_cajas').val();
                var En_almacen = $('#txt_cantidad_descontar').val();

                catalogo = modificadas * 1 - anterior * 1;//se descuenta del catalogo

                var Total = En_almacen*1 - (anterior*1 - modificadas*1);

                if (Total > 0)
                {
                    Obj_usuario.Cantidad_Descontar = Total;
                    Obj_usuario.Estatus = "Almacen";
                }
                else
                    Obj_usuario.Estatus = "Agotado";


            }

        } else if ($('#txt_cajas_anterior').val()*1 < $('#txt_cajas').val()*1) //si la cantidad es menor a la modificada
        {
                var anterior = $('#txt_cajas_anterior').val();
                var modificadas = $('#txt_cajas').val();
                var En_almacen = $('#txt_cantidad_descontar').val();

                catalogo = modificadas * 1 - anterior * 1;//se suma del catalogo

                var Total = En_almacen * 1 + (modificadas * 1 - anterior * 1);
                Obj_usuario.Cantidad_Descontar = Total;

                if (Total > 0)
                    Obj_usuario.Estatus = "Almacen";
                else 
                    Obj_usuario.Estatus = "Agotado";

        }
        Obj_usuario.Notas = $('#txt_notas').val();
        Obj_usuario.Catalogo_Agregar_Quitar = catalogo;

        $.ajax({
            url: 'Controllers/Ctrl_Entrada_Mercancia.asmx/Ope_Modificar',
            data: "{'Datos':'" + JSON.stringify(Obj_usuario) + "'}",
            type: 'POST',
            cache: false,
            async: true,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (Resultado) {
                //var res = eval("(" + Resultado.d + ")");
                //if (res.Estatus) {
                //    asignar_modal("Correcto", res.Mensaje);
                //    jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
                    estado_inicial();
                //}
                //else {
                //    asignar_modal("Advertencia", res.Mensaje);
                //    jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
                //}
            }
        });
    } catch (e) {
        //asignar_modal("", e);
        //jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}
/// <summary>
/// FUNCION PARA VISUALIZAR LA CAPTURA DEL PROVEEDOR
/// </summary>
function Proveedor_Registrado() {
    //$('#Registrado').attr({ disabled: false });
    $('#No_Registrado').css({ display: 'none' });
    $('#Registrado').css({ display: 'inline-block' });
    $('#Registrado').css({ display: 'Block' });
}
/// <summary>
/// FUNCION PARA VISUALIZAR LA CAPTURA DEL PROVEEDOR
/// </summary>
function Proveedor() {
    //$('#No_Registrado').attr({ disabled: false });
    $('#Registrado').css({ display: 'none' });
    $('#No_Registrado').css({ display: 'inline-block' });
    $('#No_Registrado').css({ display: 'Block' });
}
/// <summary>
/// FUNCION PARA TRAER LA INFORMACIÓN AL COMBO
/// </summary>
function Cargar_Cmb_Proveedor() {
    try {
        $('#cmb_proveedor').select2({
            theme: "classic",
            language: "es",
            placeholder: 'Seleccione',
            allowClear: true,
            minimumInputLength: 0,
            tags: false,
            tokenSeparators: [','],
            ajax: {
                url: 'Controllers/Ctrl_Entrada_Mercancia.asmx/Cargar_Cmb_Proveedor',
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

        $('#cmb_proveedor').on("select2:select", function (evt) {
            $('#txt_proveedor').val(evt.params.data.text);
            $('#txt_proveedor_id').val(evt.params.data.id);

        });

        $("#cmb_proveedor").on("select2:unselecting instead", function (e) {
            $('#txt_proveedor').val('');
            $('#txt_proveedor_id').val('');
        });
    } catch (e) {
        //asignar_modal("", e);
        //jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
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
                url: 'Controllers/Ctrl_Entrada_Mercancia.asmx/Cargar_Cmb_Producto',
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
            $('#txt_producto').val(evt.params.data.text);
            $('#txt_producto_id').val(evt.params.data.id);

        });

        $("#cmb_producto").on("select2:unselecting instead", function (e) {
            $('#txt_producto').val('');
            $('#txt_producto_id').val('');
        });
    } catch (e) {
        //asignar_modal("", e);
        //jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}
/// Función que consulta y carga los registros.
/// </summary>
/// <usuario_creo>MARÍA CHANTAL ORIGEL SEGURA</usuario_creo>
/// <fecha_creo>24 OCTUBRE 2017</fecha_creo>
function llenar_grid_historial() {
    try {
        var registros = "{}";

        $('#Tbl_Historial').bootstrapTable('destroy');
        $('#Tbl_Historial').bootstrapTable({
            data: JSON.parse(registros),
            method: 'POST',
            height: 500,
            striped: true,
            pagination: true,
            pageSize: 50,
            pageList: [10, 25, 50, 100, 200],
            search: true,
            showColumns: false,
            showRefresh: false,
            minimumCountColumns: 2,
            clickToSelect: true,
            columns: [
                { field: 'Proveedor', title: 'Proveedor', align: 'left', valign: 'center', sortable: true, clickToSelect: false },
                { field: 'Chofer', title: 'Chofer', align: 'left', valign: 'center', sortable: true, clickToSelect: false },
                { field: 'Producto', title: 'Producto', align: 'left', valign: 'center', sortable: true, clickToSelect: false },
                { field: 'Cajas', title: 'Cajas', align: 'left', valign: 'center', sortable: true, clickToSelect: false },
                { field: 'Notas', title: 'Notas', align: 'left', valign: 'center', sortable: true, clickToSelect: false },
                { field: 'Fecha_Creo', title: 'Fecha', align: 'left', valign: 'center', sortable: true, clickToSelect: false },
                { field: 'Estatus', title: 'Estatus', align: 'left', valign: 'center', sortable: true, clickToSelect: false },

            ],
        });
    } catch (e) {
        asignar_modal("", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}
// <summary>
/// Función que consulta y carga los registros.
/// </summary>
/// <usuario_creo>MARÍA CHANTAL ORIGEL SEGURA</usuario_creo>
/// <fecha_creo>24 OCTUBRE 2017</fecha_creo>
function Historial() {
    var registros = "{}";
    try {
            var Obj_usuario = new Object();

        Obj_usuario.Fecha_Inicio = $('#Txt_Inicio').val();
        Obj_usuario.Fecha_Fin = $('#Txt_Fin').val();

        $.ajax({
            url: 'Controllers/Ctrl_Entrada_Mercancia.asmx/Historial',
            data: "{'Datos':'" + JSON.stringify(Obj_usuario) + "'}",
            method: 'POST',
            cache: false,
            async: true,
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (data) {
                if (data.d != undefined && data.d != null) {
                    var res = eval("(" + data.d + ")");
                    row = JSON.parse(res.Registros);
                    llenar_grid_historial();
                    if (row != null) {
                        if (row.length > 0) {
                            for (var i = 0; i < row.length; i++) {
                                var benef = $('#Tbl_Historial').bootstrapTable('getData');
                                var no_ben = benef.length + 1;
                                $('#Tbl_Historial').bootstrapTable('insertRow', {
                                    index: no_ben,
                                    row: {
                                        Proveedor: row[i].Proveedor,
                                        Producto: row[i].Producto,
                                        Fecha_Creo: row[i].Fecha_Creo,
                                        Cajas: row[i].Cajas,
                                        Chofer: row[i].Chofer,
                                        Notas: row[i].Notas,
                                        Estatus: row[i].Estatus,
                                        ID: no_ben,
                                    }
                                });
                            }

                        } else {
                            llenar_grid_historial();
                        }
                    }
                }
            }
        });

    } catch (e) {
        asignar_modal("", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}
/// <summary>
/// FUNCION PARA CARGAR LOS REGISTROS
/// </summary>
//function Historial() {
//    var registros = "{}";
//    var Obj_usuario = new Object();

//    try {
//        Obj_usuario.Fecha_Inicio = $('#Txt_Inicio').val();
//        Obj_usuario.Fecha_Fin = $('#Txt_Fin').val();
//        $.ajax({
//            url: 'Controllers/Ctrl_Entrada_Mercancia.asmx/Historial',
//            data: "{'Datos':'" + JSON.stringify(Obj_usuario) + "'}",
//            method: 'POST',
//            cache: false,
//            async: true,
//            responsive: true,
//            contentType: "application/json; charset=utf-8",
//            dataType: 'json',
//            success: function (data) {
//                if (data.d != undefined && data.d != null) {
//                    var res = eval("(" + data.d + ")");
//                    registros = res.data;
//                }
//                oTable = $('#Tbl_Historial').DataTable({
//                    destroy: true,
//                    data: JSON.parse(registros),
//                    lengthMenu: [10, 25, 50, 75, 100],
//                    columns: [
//                        { title: "Proveedor" },
//                        { title: "Producto" },
//                        { title: "Cajas", },
//                        { title: "Toneladas", },
//                        { title: "Notas" },
//                        { title: "Fecha" },
//                        { title: "Estatus", },
//                    ],
               
//                });
//            }
//        });
//    } catch (e) {
//        //asignar_modal("", e);
//        //jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
//    }
//}
/*======================================SECCIÓN DE CARGA EXCEL=========================*/
/// <summary>
/// Función para generar la mascara del control de subir archivos.
/// </summary>
/// <usuario_creo>MARÍA CHANTAL ORIGEL SEGURA</usuario_creo>
/// <fecha_creo>24 OCTUBRE 2017</fecha_creo>
function crear_mascaras_file_control() {

    var wrapper2 = $('<div/>').css({ height: 0, width: 0, 'overflow': 'hidden' });
    var fileInputEXCEL = $('#Fl_Ruta_Excel:file').wrap(wrapper2);

    fileInputEXCEL.change(function () {
        $this = $(this);

        // If the selection is empty, reset it
        if ($this.val().length == 0) {
            $('#Btn_Mascara_Excel').text("Seleccionar");

        } else {

            $('#Btn_Mascara_Excel').text($this.val());
        }
    });

    // When your fake button is clicked, simulate a click of the file button
    $('#Btn_Mascara_Excel').click(function (e) {
        e.stopPropagation();
        e.preventDefault();
        fileInputEXCEL.trigger('click');
    }).show();

    $('#Btn_Mascara_Excel').hover(function () {
        $(this).css({
            'background-color': '#060b4c',
            color: '#fff',
            'font-weight': 'bold'
        });
    }, function () {
        $(this).css({
            //'background-color': '#92b231',
            'background-color': '#060b4c',
            color: '#fff',
            'font-weight': 'normal'
        });
    });

}
/// <summary>
/// funcion para validar el tipo de archivo excel
/// </summary>
/// <usuario_creo>MARÍA CHANTAL ORIGEL SEGURA</usuario_creo>
/// <fecha_creo>24 OCTUBRE 2017</fecha_creo>
function Excel_Seleccionado(evt) {
    evt.preventDefault();
    xls = new FormData();
    var nombre_archivo = "";
    var Arc = null;
    var files = evt.target.files; // FileList object
    for (var i = 0, f; f = files[i]; i++) {
        if (f.type == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" || f.type == "application/vnd.ms-excel") {
            xls.append('file' + i, files[i]);
            //obtemos la ruta con el nombre
            Arc = f.name.split('/');
            nombre_archivo = Arc[Arc.length - 1];
            $('#Hf_Ruta_Excel').val(nombre_archivo);
            Cargar_Archivos(xls);
        }
        else {
            $('#Fl_Ruta_Excel').val('');
            mostrar_mensaje('Información', 'Solo se aceptan documentos con extensión xls o  xlsx.');
        }
    }
}
/// <summary>
/// Función para guardar el archivo de excel
/// </summary>
/// <usuario_creo>MARÍA CHANTAL ORIGEL SEGURA</usuario_creo>
/// <fecha_creo>24 OCTUBRE 2017</fecha_creo>
function Cargar_Archivos(_formdata) {
    try {
        $.ajax({
            url: "../../FileUploadHandler.ashx",
            type: "POST",
            data: _formdata,
            dataType: "multipart/form-data",
            cache: false,
            contentType: false,
            processData: false,
            async: true,
            success: function (result) {
                //Cerrar_Ventana_Espera();

            },
            complete: function () {
                setTimeout(function () {
                    Cerrar_Ventana_Espera();
                }, 5000);
            }
        });
    } catch (e) {
        mostrar_mensaje('Información técnica', e);
    }
}
/// <summary>
/// Función para leer el excel
/// </summary>
/// <usuario_creo>MARÍA CHANTAL ORIGEL SEGURA</usuario_creo>
/// <fecha_creo>24 OCTUBRE 2017</fecha_creo>
function Leer_Excel() {
    var registros_detalle = "{}";
    $('#Hf_Reg_Tbl').val('')
    $('#Txt_Registros_Detalle').val('{}')

    habilitar_controles('Ocultar');
    try {
        Abrir_Ventana_Espera();
        $.ajax({
            url: 'Controllers/Ctrl_Entrada_Mercancia.asmx/Leer_Excel',
            data: "{'ruta':'" + $('#Hf_Ruta_Excel').val() + "'}",
            method: 'POST',
            cache: false,
            async: true,
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (data) {
                if (data.d != undefined && data.d != null) {
                    var res = eval("(" + data.d + ")");
                    registros = res.Registros;
                    $('#Hf_Reg_Tbl').val(res.Tabla_Registros);
                    Llenar_Grid(registros);
                    Cerrar_Ventana_Espera();
                    //asignar_modal("Informe Técnico", res.Mensaje);
                    //jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
                    if (res.Estatus == false) {
                        Cerrar_Ventana_Espera();
                        asignar_modal("Informe Técnico", res.Mensaje);
                        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
                    }
                    else {
                        Cerrar_Ventana_Espera();
                        //if (res.Mensaje != "")
                        //    mostrar_mensaje('Observaciones:', res.Mensaje);
                    }
                }
            }
        });
        Cerrar_Ventana_Espera();

    } catch (e) {
        asignar_modal("Informe Técnico", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}
/// <summary>
/// FUNCION PARA CARGAR LOS REGISTROS
/// </summary>
function Llenar_Grid(registros) {

    try {
        oTable = $('#Tbl_Excel').DataTable({
            destroy: true,
            data: JSON.parse(registros),
            lengthMenu: [10, 25, 50, 75, 100],
            columns: [
                { title: "Proveedor" },
                { title: "Producto" },
                { title: "Cantidad", },
                { title: "Usuario_Creo", visible: false },
                { title: "Proveedor_Producto", visible: false },
            ],
            columnDefs: [
                {
                   
                }
            ]
        });

    } catch (e) {
        //asignar_modal("", e);
        //jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}
/// <summary>
/// Función para guardar los datos del excel
/// </summary>
/// <usuario_creo>MARÍA CHANTAL ORIGEL SEGURA</usuario_creo>
/// <fecha_creo>24 OCTUBRE 2017</fecha_creo>
function Guardar_Excel() {
    var reg = $('#Hf_Reg_Tbl').val();
    var registros_detalle = $('#Txt_Registros_Detalle').val();

    if ($('#Hf_Reg_Tbl').val() == null || $('#Hf_Reg_Tbl').val() == undefined || $('#Hf_Reg_Tbl').val() == "" || $('#Hf_Reg_Tbl').val() == "{}") {
        bootbox.confirm({
            title: 'Advertencia',
            message: 'No se ha cargado ningun registro',
            callback: function (result) { }
        });
    }
    else {
        try {
            $.ajax({
                url: 'Controllers/Ctrl_Entrada_Mercancia.asmx/Guardar_Excel',
                data: "{'Datos':'" + reg + "'}",
                type: 'POST',
                async: true,
                cache: false,
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (Resultado) {
                    var res = eval("(" + Resultado.d + ")");
                    if (res.Estatus) {
                        estado_inicial();
                        asignar_modal("Correcto", res.Mensaje);
                        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
                    }
                    else {
                        asignar_modal("Advertencia", res.Mensaje);
                        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
                    }
                }
            });
            $('#Hf_Reg_Tbl').val('');

        } catch (e) {
            asignar_modal("Informe Técnico", e);
            jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
        }
    }
}
/// <summary>
/// Abre el modal de espera.
/// </summary>
/// <usuario_creo>MARÍA CHANTAL ORIGEL SEGURA</usuario_creo>
/// <fecha_creo>24 OCTUBRE 2017</fecha_creo>
function Abrir_Ventana_Espera() {
    var pleaseWaitDiv = $('#Ventana_Espera');
    pleaseWaitDiv.modal({ backdrop: 'static', keyboard: false });
}
/// <summary>
/// Cierra el modal de espera.
/// </summary>
/// <usuario_creo>MARÍA CHANTAL ORIGEL SEGURA</usuario_creo>
/// <fecha_creo>24 OCTUBRE 2017</fecha_creo>
function Cerrar_Ventana_Espera() {
    var pleaseWaitDiv = $('#Ventana_Espera');
    pleaseWaitDiv.modal('hide');
}
/*====================================== GENERALES =====================================*/
/// <summary>
/// FUNCION PARA ESTABLECER LA PAGINA CON LA CONFIGURACION INICIAL
/// </summary>
function estado_inicial() {
    try {
        habilitar_controles('Inicio');
        cargar_tabla();
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
    $('#txt_proveedor').val(res[0]);
    //$('#txt_proveedor_id').val(res[1]);
    $('#txt_toneladas').val(res[1]);
    $('#txt_producto').val(res[3]);
    $('#txt_cajas').val(res[4]);
    $('#txt_cajas_anterior').val(res[4]);
    $('#txt_notas').val(res[5]);
    $('#txt_id').val(res[7]);
    $('#txt_cantidad_descontar').val(res[8]);
    $Correo = res[1];

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
        $('#txt_notas').attr({ disabled: !Estatus });
        if ($('#txt_proveedor').val() == '' || $('#txt_proveedor').val() == undefined || $('#txt_proveedor').val() == null) {
            output.Estatus = false;
            output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>PROVEEDOR</strong>.</span><br />';
        }
        if ($('#txt_producto').val() == '' || $('#txt_producto').val() == undefined || $('#txt_producto').val() == null) {
            output.Estatus = false;
            output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>PRODUCTO</strong>.</span><br />';
        }
        if ($('#txt_toneladas').val() == '' || $('#txt_toneladas').val() == undefined || $('#txt_toneladas').val() == null) {
            output.Estatus = false;
            output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>EL PESO EN TONELADAS</strong>.</span><br />';
        }
        if ($('#txt_cajas').val() == '' || $('#txt_cajas').val() == undefined || $('#txt_cajas').val() == null) {
            output.Estatus = false;
            output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>LAS CAJAS</strong>.</span><br />';
        }

    } catch (e) {
        //asignar_modal("Informe Técnico", e);
        //jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
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
            $('#li-excel').css({ display: 'inline-block' });
            $('#li-historial').css({ display: 'inline-block' });
            $('#li-guardar').css({ display: 'none' });
            $('#li-cancelar').css({ display: 'none' });
            $('#cont-alta').css({ display: 'none' });
            $('#Reg-Datos').css({ display: 'Block' });
            break;
        case "Nuevo":
            Estatus = true;
            $('#li-nuevo').css({ display: 'none' });
            $('#li-excel').css({ display: 'none' });
            $('#li-historial').css({ display: 'none' });
            $('#li-guardar').css({ display: 'inline-block' });
            $('#li-cancelar').css({ display: 'inline-block' });
            $('#cont-alta').css({ display: 'Block' });
            $('#Reg-Datos').css({ display: 'none' });
            Cargar_Cmb_Proveedor();
            Cargar_Cmb_Producto();
            break;
        case "Modificar":
            Estatus = true;
            $('#li-nuevo').css({ display: 'none' });
            $('#li-excel').css({ display: 'none' });
            $('#li-historial').css({ display: 'none' });
            $('#li-guardar').css({ display: 'inline-block' });
            $('#li-cancelar').css({ display: 'inline-block' });
            $('#cont-alta').css({ display: 'Block' });
            $('#Reg-Datos').css({ display: 'none' });
            Cargar_Cmb_Proveedor();
            Cargar_Cmb_Producto();
            break;
        case "Ocultar":
            Estatus = false;
            $('#Btn_Leer_Excel').css({ display: 'none' });
            $('#Btn_Mascara_Excel').css({ display: 'none' });
            $('#Etiqueta').css({ display: 'none' });
            $('#Fl_Ruta_Excel').val("");
            

            break;
        case "Mostrar":
            Estatus = false;
            $('#Btn_Leer_Excel').css({ display: 'inline-block' });
            $('#Btn_Mascara_Excel').css({ display: 'inline-block' });
            $('#Btn_Mascara_Excel').text("Seleccionar");
            $('#Etiqueta').css({ display: 'inline-block' });

            if ($('#Flag').val() == '1')
            {
                var table = $('#Tbl_Excel').DataTable();
                table.destroy();
                $('#Tbl_Excel').empty(); // empty in case the columns change

                $('#Flag').val('');
            }
            
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
    $('#Btn_Mascara_Excel').text("Seleccionar");
    $('#Fl_Ruta_Excel').val('');
    $('#txt_notas').val('');

}
/// <summary>
/// formato_conceptos
/// </summary>
/// <usuario_creo>MARÍA CHANTAL ORIGEL SEGURA</usuario_creo>
/// <fecha_creo>24 OCTUBRE 2017</fecha_creo>
function formato_conceptos(row) {
    var $row = $('<span style="font-family:Century Gothic;font-size:12px;"><i class="fa fa-tag" style="color:#000;"></i>&nbsp;' + row.text + '</span>');

    var _salida = '<span>' +
    '<i class="fa fa-tag" style="color:#0060aa;"></i>&nbsp;' + row.text +
    '</span>';

   // _salida += '<span>' +
   //'<i class="glyphicon glyphicon-option-vertical" style="color:#0060aa;"></i>&nbsp;' + row.tag +
   //'</span>';

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
