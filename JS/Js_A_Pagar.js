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

        var $table = $('#Tbl_Registros');
        $(function () {
            $table.on('click-row.bs.table', function (e, row, $element) {
                $('.success').removeClass('success');
                $($element).addClass('success');
            });
        });
        $('#Txt_Fecha_Inicial').datepicker({
            uiLibrary: 'bootstrap4'
        });
        $('#Txt_Fecha_Final').datepicker({
            uiLibrary: 'bootstrap4'
        });
        Cargar_Cmb_Cliente();
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
    $('#btn_consultar').on('click', function (e) {
        e.preventDefault();
        $('#Tbl_Cajas').bootstrapTable('destroy');
        $('#Tbl_Bodegas').bootstrapTable('destroy');
        cargar_tabla();
    });
    $('#btn_bodegas').on('click', function (e) {
        e.preventDefault();
        $('#Tbl_Cajas').bootstrapTable('destroy');
        $('#Tbl_Registros').bootstrapTable('destroy');
        cargar_tabla_bodegas();
    });
    $('#btn_cajas').on('click', function (e) {
        e.preventDefault();
        $('#Tbl_Registros').bootstrapTable('destroy');
        $('#Tbl_Bodegas').bootstrapTable('destroy');
        cargar_tabla_cajas();
    });
    $('#btn_historial').on('click', function (e) {
        e.preventDefault();
        Historial_Abonos()
    });
    $('#btn_historial_cajas').on('click', function (e) {
        e.preventDefault();
        Historial_Cajas_Entregadas()
    });
    $('#btn_corte').on('click', function (e) {
        e.preventDefault();
        Ventas_Fechas()
    });
    //setInterval('cargar_tabla()', 9000);
    //$('#Txt_Fecha_Inicial').datepicker({
    //    beforeShowDay: function (date) {
    //        var fecha = fecha.date;
    //        if (highlight) {
    //            return [true, "event", 'Tooltip text'];
    //        } else {
    //            return [true, '', ''];
    //        }
    //    }
    //});
    //$('#Txt_Fecha_Inicial').data("DateTimePicker").date(fecha.getDate() + '/' + (fecha.getMonth() + 1) + '/' + (fecha.getFullYear()));
    //$('#Txt_Fecha_Final').data("DateTimePicker").date(fecha.getDate() + '/' + (fecha.getMonth() + 1) + '/' + (fecha.getFullYear()));



}

/*====================================== OPERACIONES =====================================*/
/// <summary>
/// Función que obtener los parametros
/// </summary>
/// <usuario_creo>MARÍA CHANTAL ORIGEL SEGURA</usuario_creo>
/// <fecha_creo>24 OCTUBRE 2017</fecha_creo>
function Obtener_Parametros() {
    var Obj_Param = null;
    try {
        Obj_Param = new Object();

        Obj_Param.Fecha_Inicio = $('#Txt_Fecha_Inicial').val();
        Obj_Param.Fecha_Fin = $('#Txt_Fecha_Final').val();
        Obj_Param.Folio = $('#Txt_Folio').val();
        Obj_Param.Cliente = $('#txt_cliente_').val();

    } catch (e) {
        asignar_modal("Informe Técnico", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
    return Obj_Param;
}
/// <summary>
/// FUNCION PARA CARGAR LOS REGISTROS
/// </summary>
function cargar_tabla() {
    var registros = "{}";

    var Obj_Param = Obtener_Parametros();

    $.ajax({
        url: 'Controllers/Ctrl_A_Pagar.asmx/Consultar_Cuentas_Pendientes',
        data: "{'Parametros':'" + JSON.stringify(Obj_Param) + "'}",
        method: 'POST',
        cache: false,
        async: true,
        responsive: true,
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (data) {
            if (data.d != undefined && data.d != null) {
                var res = eval("(" + data.d + ")");
                row = JSON.parse(res.Registros);
                if (row != null) {
                    if (row.length > 0) {
                        llenar_grid();
                        for (var i = 0; i < row.length; i++) {
                            var benef = $('#Tbl_Registros').bootstrapTable('getData');
                            var no_ben = benef.length + 1;
                            $('#Tbl_Registros').bootstrapTable('insertRow', {
                                index: no_ben,
                                row: {
                                    Venta_ID: row[i].Venta_ID,
                                    Cliente: row[i].Cliente,
                                    Total_Vendido: row[i].Total_Vendido,
                                    Folio: row[i].Folio,
                                    Fecha_Creo: row[i].Fecha_Creo,
                                    Operacion: no_ben,
                                    ID: no_ben,
                                }
                            });
                        }

                    } else {
                        llenar_grid();
                    }
                }
            }
        }
    });
}
/// </summary>
/// <usuario_creo>David Herrera Rincon</usuario_creo>
/// <fecha_creo>21 Junio 2016</fecha_creo>
function llenar_grid() {
    
        var registros = "{}";

        $('#Tbl_Registros').bootstrapTable('destroy');
        $('#Tbl_Registros').bootstrapTable({
            data: JSON.parse(registros),
            method: 'POST',
            height: 350,
            striped: true,
            pagination: true,
            pageSize: 5,
            pageList: [5],
            search: false,
            showColumns: false,
            showRefresh: false,
            minimumCountColumns: 2,
            clickToSelect: true,
            columns: [
                { field: 'Folio', title: 'Folio', align: 'left', valign: 'center', sortable: true, clickToSelect: false },

                { field: 'Cliente', title: 'Cliente', align: 'left', valign: 'center', sortable: true, clickToSelect: false },
                { field: 'Fecha_Creo', title: 'Fecha', align: 'left', valign: 'center', sortable: true, clickToSelect: false },
                 {
                     field: 'Total_Vendido', title: 'Total Restante', align: 'right', valign: 'center', sortable: true, clickToSelect: false, formatter: function (value, row, index) { return accounting.formatMoney(value); }
                 },
                 {
                     field: 'Abonar', title: 'Abonar', align: 'center', formatter: function (index, value, row) {
                         return '<div><a class="remove ml10" id="' + index + '" href="javascript:void(0)"><i class="fa fa-star-half"></i></a></div>';
                     }
                 },
            {
                field: 'Operacion_Eliminar', title: 'Remover', align: 'center', formatter: function (index, value, row) {
                    return '<div><a class="remove ml10" id="' + index + '" href="javascript:void(0)"><i class="fa fa-minus-circle"></i></a></div>';
                }
            },

             { field: 'Venta_ID', title: 'Venta_ID', visible: false },
                { field: 'ID', title: 'ID', visible: false },

            ],
            onClickCell: function (field, value, row, $element) {
                $("#txt_venta_id").val(row.Venta_ID)
                $("#txt_folio_id").val(row.Folio)
                $("#txt_restante").val(row.Total_Vendido)
                $("#txt_abono").val(row.Total_Vendido)
                $("#txt_cliente").val(row.Cliente)
                Consultar_Cajas();

                if (field == 'Operacion_Eliminar') {
                    bootbox.confirm({
                        title: "Eliminar",
                        message: "¿Esta seguro de Eliminar la cuenta?",
                        buttons: {
                            cancel: {
                                label: '<i class="fa fa-times"></i> Cancel'
                            },
                            confirm: {
                                label: '<i class="fa fa-check"></i> Confirm'
                            }
                        },
                        callback: function (result) {
                            if (result)
                            {
                                //if ($("#txt_cantidad").val() * 1 > $("#txt_cajas").val() * 1)
                                //    Obj_Param.Estatus = "Pendiente";
                                //else
                                //    Obj_Param.Estatus = "Recibido";

                                //Obj_Param.Pagado = $("#txt_cantidad").val() * 1;
                                //Obj_Param.Cantidad = $("#txt_cajas").val() * 1;
                                //Obj_Param.Caja_ID = $("#txt_caja_id").val();
                                //Obj_Param.Descripcion = $("#txt_descripcion").val();
                                //Obj_Param.Cliente = $("#txt_clientes").val();
                                //Obj_Param.Folio = $("#txt_folio_id").val();
                                //Obj_Param.Importe = $("#txt_deposito_deuda").val();
                                Modificar_Estatus("Cancelado");
                            }
                            cargar_tabla();

                        }
                    });

                }

                if (field == 'Pagar') {
                    bootbox.confirm({
                        title: "Confirmar",
                        message: "Pagar cuenta total",
                        buttons: {
                            cancel: {
                                label: '<i class="fa fa-times"></i> Cancel'
                            },
                            confirm: {
                                label: '<i class="fa fa-check"></i> Confirm'
                            }
                        },
                        callback: function (result) {
                            if (result) 
                                Modificar_Estatus("Pagado");
                            cargar_tabla();
                        }
                    });

                }

                if (field == 'Abonar') {
                    var modal = $('#Modal');
                    modal.modal({ backdrop: 'static', keyboard: false });
                }
            }
        });
}
/// <usuario_creo>David Herrera Rincon</usuario_creo>
/// <fecha_creo>21 Junio 2016</fecha_creo>
/// <summary>
function Modificar_Estatus(Estatus) {
    Obj_Param = new Object();

    try {
        Obj_Param.Estatus = Estatus;
        Obj_Param.Venta_ID = $("#txt_venta_id").val().replace(/[, $]+/g, '');
        Obj_Param.Cliente = $("#txt_cliente").val().replace(/[, $]+/g, '');
        Obj_Param.Pagado = $("#txt_restante").val().replace(/[, $]+/g, '');
        Obj_Param.Abonado = $("#txt_abono").val().replace(/[, $]+/g, '');
        Obj_Param.Folio = $("#txt_folio_id").val().replace(/[, $]+/g, '');

        $.ajax({
            url: 'Controllers/Ctrl_A_Pagar.asmx/Modificar_Estatus',
            data: "{'Parametros':'" + JSON.stringify(Obj_Param) + "'}",
            type: 'POST',
            async: false,
            cache: false,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (Resultado) {
                var res = eval("(" + Resultado.d + ")");
                if (res.Estatus) {
                    cargar_tabla();
                }
                else {
                    asignar_modal("Informe Técnico", e);
                    jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
                }
            }
        });
        cargar_tabla();

    } catch (e) {
        asignar_modal("Informe Técnico", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}
/// <summary>
/// FUNCION PARA TRAER LA INFORMACIÓN AL COMBO
/// </summary>
function Cargar_Cmb_Cliente() {
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
                url: 'Controllers/Ctrl_Ventas.asmx/Cargar_Cmb_Cliente',
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

        $('#cmb_cliente').on("select2:select", function (evt) {
            $('#txt_cliente_').val(evt.params.data.text);

        });

        $("#cmb_cliente").on("select2:unselecting instead", function (e) {
            $('#txt_cliente_').val('');
        });
    } catch (e) {
        asignar_modal("Informe Técnico", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}
/// <summary>
/// FUNCION PARA CARGAR LOS REGISTROS
/// </summary>
function Consultar_Cajas() {
    var registros = "{}";

    $("#txt_abono").formatCurrency();

    Obj_Param = new Object();

    Obj_Param.Cliente = $('#txt_cliente').val();

    $.ajax({
        url: 'Controllers/Ctrl_A_Pagar.asmx/Consultar_Cajas',
        data: "{'Parametros':'" + JSON.stringify(Obj_Param) + "'}",
        method: 'POST',
        cache: false,
        async: true,
        responsive: true,
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (data) {
            if (data.d != undefined && data.d != null) {
                var res = eval("(" + data.d + ")");
                row = JSON.parse(res.Registros);
                if (row != null) {
                    if (row.length > 0) {
                        $("#txt_cajas_deuda").val(row[0].Cantidad);
                    } 
                }
            }
        }
    });
}
/// <summary>
/// FUNCION PARA CARGAR LOS REGISTROS
/// </summary>
function cargar_tabla_cajas() {
    var registros = "{}";

    var Obj_Param = Obtener_Parametros();

    $.ajax({
        url: 'Controllers/Ctrl_A_Pagar.asmx/Consultar_Tabla_Cajas',
        data: "{'Parametros':'" + JSON.stringify(Obj_Param) + "'}",
        method: 'POST',
        cache: false,
        async: true,
        responsive: true,
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (data) {
            if (data.d != undefined && data.d != null) {
                var res = eval("(" + data.d + ")");
                row = JSON.parse(res.Registros);
                if (row != null) {
                    if (row.length > 0) {
                        llenar_grid_cajas();
                        for (var i = 0; i < row.length; i++) {
                            var benef = $('#Tbl_Cajas').bootstrapTable('getData');
                            var no_ben = benef.length + 1;
                            $('#Tbl_Cajas').bootstrapTable('insertRow', {
                                index: no_ben,
                                row: {
                                    Caja_ID: row[i].Caja_ID,
                                    Cliente: row[i].Cliente,
                                    Cantidad: row[i].Cantidad,
                                    Folio: row[i].Folio,
                                    Tipo_Caja: row[i].Tipo_Caja,
                                    Fecha_Creo: row[i].Fecha_Creo,
                                    Operacion: no_ben,
                                    ID: no_ben,
                                }
                            });
                        }

                    } else {
                        llenar_grid_cajas();
                    }
                }
            }
        }
    });
}
/// </summary>
/// <usuario_creo>David Herrera Rincon</usuario_creo>
/// <fecha_creo>21 Junio 2016</fecha_creo>
function llenar_grid_cajas() {

    var registros = "{}";

    $('#Tbl_Cajas').bootstrapTable('destroy');
    $('#Tbl_Cajas').bootstrapTable({
        data: JSON.parse(registros),
        method: 'POST',
        height: 350,
        striped: true,
        pagination: true,
        pageSize: 5,
        pageList: [5],
        search: false,
        showColumns: false,
        showRefresh: false,
        minimumCountColumns: 2,
        clickToSelect: true,
        columns: [
            //{ field: 'Folio', title: 'Folio', align: 'left', valign: 'center', sortable: true, clickToSelect: false },
            { field: 'Cliente', title: 'Cliente', align: 'left', valign: 'center', sortable: true, clickToSelect: false },
            //{ field: 'Fecha_Creo', title: 'Fecha', align: 'left', valign: 'center', sortable: true, clickToSelect: false },
            { field: 'Tipo_Caja', title: 'Descripción', align: 'left', valign: 'center', sortable: true, clickToSelect: false },
            { field: 'Cantidad', title: 'Cantidad', align: 'left', valign: 'center', sortable: true, clickToSelect: false },

             {
                 field: 'Abonar', title: 'Abonar', align: 'center', formatter: function (index, value, row) {
                     return '<div><a class="remove ml10" id="' + index + '" href="javascript:void(0)"><i class="fa fa-star-half"></i></a></div>';
                 }
             },

        //{
        //    field: 'Operacion_Eliminar', title: 'Remover', align: 'center', formatter: function (index, value, row) {
        //        return '<div><a class="remove ml10" id="' + index + '" href="javascript:void(0)"><i class="fa fa-minus-circle"></i></a></div>';
        //    }
        //},

         { field: 'Caja_ID', title: 'Caja_ID', visible: false },
            { field: 'ID', title: 'ID', visible: false },

        ],
        onClickCell: function (field, value, row, $element) {
            $("#txt_caja_id").val(row.Caja_ID)
            //$("#txt_folio_id").val(row.Folio)
            $("#txt_clientes").val(row.Cliente)
            $("#txt_cantidad").val(row.Cantidad)
            $("#txt_descripcion").val(row.Tipo_Caja)
            $("#txt_cajas").val(row.Cantidad)

            Consultar_Deposito();

            if (field == 'Abonar') {
                var modal = $('#Modal_Cajas');
                modal.modal({ backdrop: 'static', keyboard: false });
            }
        }
    });
}
/// <summary>
/// FUNCION PARA CARGAR LOS REGISTROS
/// </summary>
function cargar_tabla_bodegas() {
    var registros = "{}";

    var Obj_Param = Obtener_Parametros();

    $.ajax({
        url: 'Controllers/Ctrl_A_Pagar.asmx/Consultar_Tabla_Bodegas',
        data: "{'Parametros':'" + JSON.stringify(Obj_Param) + "'}",
        method: 'POST',
        cache: false,
        async: true,
        responsive: true,
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (data) {
            if (data.d != undefined && data.d != null) {
                var res = eval("(" + data.d + ")");
                row = JSON.parse(res.Registros);
                if (row != null) {
                    if (row.length > 0) {
                        llenar_grid_bodegas();
                        for (var i = 0; i < row.length; i++) {
                            var benef = $('#Tbl_Bodegas').bootstrapTable('getData');
                            var no_ben = benef.length + 1;
                            $('#Tbl_Bodegas').bootstrapTable('insertRow', {
                                index: no_ben,
                                row: {
                                    Bodega_ID: row[i].Bodega_ID,
                                    Nombre: row[i].Nombre,
                                    Cantidad_Cajas: row[i].Cantidad_Cajas,
                                    Fecha_Creo: row[i].Fecha_Creo,
                                    Operacion: no_ben,
                                    ID: no_ben,
                                }
                            });
                        }

                    } else {
                        llenar_grid_bodegas();
                    }
                }
            }
        }
    });
}
/// </summary>
/// <usuario_creo>David Herrera Rincon</usuario_creo>
/// <fecha_creo>21 Junio 2016</fecha_creo>
function llenar_grid_bodegas() {

    var registros = "{}";

    $('#Tbl_Bodegas').bootstrapTable('destroy');
    $('#Tbl_Bodegas').bootstrapTable({
        data: JSON.parse(registros),
        method: 'POST',
        height: 350,
        striped: true,
        pagination: true,
        pageSize: 5,
        pageList: [5],
        search: false,
        showColumns: false,
        showRefresh: false,
        minimumCountColumns: 2,
        clickToSelect: true,
        columns: [
            { field: 'Nombre', title: 'Nombre', align: 'left', valign: 'center', sortable: true, clickToSelect: false },
            { field: 'Cantidad_Cajas', title: 'Cantidad_Cajas', align: 'left', valign: 'center', sortable: true, clickToSelect: false },
            { field: 'Fecha_Creo', title: 'Fecha', align: 'left', valign: 'center', sortable: true, clickToSelect: false },
             {
                 field: 'Abonar', title: 'Abonar', align: 'center', formatter: function (index, value, row) {
                     return '<div><a class="remove ml10" id="' + index + '" href="javascript:void(0)"><i class="fa fa-star-half"></i></a></div>';
                 }
             },
      
        //{
        //    field: 'Operacion_Eliminar', title: 'Remover', align: 'center', formatter: function (index, value, row) {
        //        return '<div><a class="remove ml10" id="' + index + '" href="javascript:void(0)"><i class="fa fa-minus-circle"></i></a></div>';
        //    }
        //},

         { field: 'Bodega_ID', title: 'Bodega_ID', visible: false },
            { field: 'ID', title: 'ID', visible: false },

        ],
        onClickCell: function (field, value, row, $element) {
            $("#txt_caja_id").val(row.Bodega_ID)
            $("#txt_client").val(row.Nombre)
            $("#txt_cantidad_caja").val(row.Cantidad_Cajas)
            $("#txt_cantidad_c").val(row.Cantidad_Cajas)

            //Consultar_Deposito();

            if (field == 'Abonar') {
                var modal = $('#Modal_Bodegas');
                modal.modal({ backdrop: 'static', keyboard: false });
            }
        }
    });
}
/// <summary>
/// FUNCION PARA CARGAR LOS REGISTROS
/// </summary>
function Consultar_Deposito() {
    var registros = "{}";

    Obj_Param = new Object();

    Obj_Param.Cliente = $('#txt_clientes').val();

    $.ajax({
        url: 'Controllers/Ctrl_A_Pagar.asmx/Consultar_Deposito',
        data: "{'Parametros':'" + JSON.stringify(Obj_Param) + "'}",
        method: 'POST',
        cache: false,
        async: true,
        responsive: true,
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (data) {
            if (data.d != undefined && data.d != null) {
                var res = eval("(" + data.d + ")");
                row = JSON.parse(res.Registros);
                if (row != null) {
                    if (row.length > 0) {
                        $("#txt_deposito_deuda").val(row[0].Importe_Cajas).formatCurrency();
                    }
                }
            }
        }
    });
}
/// <usuario_creo>David Herrera Rincon</usuario_creo>
/// <fecha_creo>21 Junio 2016</fecha_creo>
/// <summary>
function Recepcion_Cajas(Flag) {
    Obj_Param = new Object();

    try {
        if ($("#txt_cantidad").val() * 1 > $("#txt_cajas").val() * 1)
            Obj_Param.Estatus = "Pendiente";
        else
            Obj_Param.Estatus = "Recibido";

        Obj_Param.Flag = Flag;
        Obj_Param.Pagado = $("#txt_cantidad").val() * 1;
        Obj_Param.Restante = $("#txt_cantidad").val() * 1 - $("#txt_cajas").val() * 1;
        Obj_Param.Cantidad = $("#txt_cajas").val() * 1;
        Obj_Param.Caja_ID = $("#txt_caja_id").val();
        Obj_Param.Descripcion = $("#txt_descripcion").val();
        Obj_Param.Cliente = $("#txt_clientes").val();
        //Obj_Param.Folio = $("#txt_folio_id").val();
        Obj_Param.Importe = $("#txt_deposito_deuda").val().replace(/[, $]+/g, '');

        $.ajax({
            url: 'Controllers/Ctrl_A_Pagar.asmx/Recepcion_Cajas',
            data: "{'Parametros':'" + JSON.stringify(Obj_Param) + "'}",
            type: 'POST',
            async: false,
            cache: false,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (Resultado) {
                var res = eval("(" + Resultado.d + ")");
                if (res.Estatus) {
                    cargar_tabla_cajas();
                }
                else {
                    asignar_modal("Informe Técnico", e);
                    jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
                }
            }
        });
        cargar_tabla_cajas();

    } catch (e) {
        asignar_modal("Informe Técnico", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}
/// <usuario_creo>David Herrera Rincon</usuario_creo>
/// <fecha_creo>21 Junio 2016</fecha_creo>
/// <summary>
function Pagar_Cajas() {
    Obj_Param = new Object();

    try {
        if ($("#txt_cantidad_caja").val() * 1 > $("#txt_cantidad_c").val() * 1)
            Obj_Param.Estatus = "Pendiente";
        else
            Obj_Param.Estatus = "Recibido";

        Obj_Param.Cantidad = $("#txt_cantidad_caja").val() * 1 - $("#txt_cantidad_c").val() * 1;
        Obj_Param.Bodega_ID = $("#txt_caja_id").val();
        Obj_Param.Importe = $("#txt_importe_cajas").val();
        Obj_Param.Cliente = $("#txt_client").val();

        $.ajax({
            url: 'Controllers/Ctrl_A_Pagar.asmx/Pagar_Cajas_Bodegas',
            data: "{'Parametros':'" + JSON.stringify(Obj_Param) + "'}",
            type: 'POST',
            async: false,
            cache: false,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (Resultado) {
                var res = eval("(" + Resultado.d + ")");
                if (res.Estatus) {
                    cargar_tabla_bodegas();
                }
                else {
                    asignar_modal("Informe Técnico", e);
                    jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
                }
            }
        });
        cargar_tabla_bodegas();
        limpiar_controles();
    } catch (e) {
        asignar_modal("Informe Técnico", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}
/// <usuario_creo>David Herrera Rincon</usuario_creo>
/// <fecha_creo>21 Junio 2016</fecha_creo>
/// <summary>
function Historial_Abonos() {
    Obj_Param = new Object();

    try {
        var Obj_Param = Obtener_Parametros();

        $.ajax({
            url: 'Controllers/Ctrl_A_Pagar.asmx/Historial_Abonos',
            data: "{'filtros':'" + JSON.stringify(Obj_Param) + "'}",
            method: 'POST',
            cache: false,
            async: true,
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (data) {
                if (data.d != undefined && data.d != null) {
                    var res = eval("(" + data.d + ")");
                    if (res.Estatus) {
                        registros = res.Registros;
                        window.open("../../Temporal/Historial_Abonos.xlsx", "FW9", 'toolbar=0,directories=0,menubar=0,status=0,scrollbars=0,resizable=1,width=500,height=100');
                    }
                    else
                    asignar_modal("Informe Técnico", e);
                    jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
                }
            },
            complete: function () {
                window.open("../../Temporal/Historial_Abonos.xlsx", "FW9", 'toolbar=0,directories=0,menubar=0,status=0,scrollbars=0,resizable=1,width=500,height=100');
            }

        });
    } catch (e) {
        asignar_modal("Informe Técnico", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}
/// <usuario_creo>David Herrera Rincon</usuario_creo>
/// <fecha_creo>21 Junio 2016</fecha_creo>
/// <summary>
function Historial_Cajas_Entregadas() {
    Obj_Param = new Object();

    try {
        var Obj_Param = Obtener_Parametros();

        $.ajax({
            url: 'Controllers/Ctrl_A_Pagar.asmx/Historial_Cajas_Entregadas',
            data: "{'filtros':'" + JSON.stringify(Obj_Param) + "'}",
            method: 'POST',
            cache: false,
            async: true,
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (data) {
                if (data.d != undefined && data.d != null) {
                    var res = eval("(" + data.d + ")");
                    if (res.Estatus) {
                        registros = res.Registros;
                        window.open("../../Temporal/Historial_Cajas_Entregadas.xlsx", "FW9", 'toolbar=0,directories=0,menubar=0,status=0,scrollbars=0,resizable=1,width=500,height=100');
                    }
                    else
                        asignar_modal("Informe Técnico", e);
                    jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
                }
            },
            complete: function () {
                window.open("../../Temporal/Historial_Cajas_Entregadas.xlsx", "FW9", 'toolbar=0,directories=0,menubar=0,status=0,scrollbars=0,resizable=1,width=500,height=100');
            }

        });
    } catch (e) {
        asignar_modal("Informe Técnico", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}
/// <usuario_creo>David Herrera Rincon</usuario_creo>
/// <fecha_creo>21 Junio 2016</fecha_creo>
/// <summary>
function Ventas_Fechas() {
    Obj_Param = new Object();

    try {
        var Obj_Param = Obtener_Parametros();

        $.ajax({
            url: 'Controllers/Ctrl_A_Pagar.asmx/Ventas_Fechas',
            data: "{'filtros':'" + JSON.stringify(Obj_Param) + "'}",
            method: 'POST',
            cache: false,
            async: true,
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (data) {
                if (data.d != undefined && data.d != null) {
                    var res = eval("(" + data.d + ")");
                    if (res.Estatus) {
                        registros = res.Registros;
                        window.open("../../Temporal/Ventas.xlsx", "FW9", 'toolbar=0,directories=0,menubar=0,status=0,scrollbars=0,resizable=1,width=500,height=100');
                    }
                    else
                        asignar_modal("Informe Técnico", e);
                    jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
                }
            },
            complete: function () {
                window.open("../../Temporal/Ventas.xlsx", "FW9", 'toolbar=0,directories=0,menubar=0,status=0,scrollbars=0,resizable=1,width=500,height=100');
            }

        });
    } catch (e) {
        asignar_modal("Informe Técnico", e);
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
/// FUNCION PARA VALIDAR LOS DATOS REQUERIDOS
/// </summary>
function validar_datos() {
    var output = new Object();
    output.Estatus = true;
    output.Mensaje = '';

    try {
        if ($('#txt_abono').val() == '' || $('#txt_abono').val() == undefined || $('#txt_abono').val() == null) {
            output.Estatus = false;
            output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>EL MONTO DEL ABONO</strong>.</span><br />';
       
            $('#txt_abono').popModal({
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
        else
        {
            Modificar_Estatus('Abonar');
        }
        limpiar_controles();
    } 
    catch (e) {
        asignar_modal("Informe Técnico", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}
/// <summary>
/// FUNCION PARA VALIDAR LOS DATOS REQUERIDOS
/// </summary>
function validar_datos_cajas() {
    var output = new Object();
    output.Estatus = true;
    output.Mensaje = '';

    try {
        if ($('#txt_cajas').val() == '' || $('#txt_cajas').val() == undefined || $('#txt_cajas').val() == null) {
            output.Estatus = false;
            output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>LA CANTIDAD DE CAJAS A RECIBIR</strong>.</span><br />';

            $('#txt_cajas').popModal({
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
        else {
            Recepcion_Cajas();
            cargar_tabla_cajas();

        }
        limpiar_controles();
    }
    catch (e) {
        asignar_modal("Informe Técnico", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
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
/// <usuario_creo>MARÍA CHANTAL ORIGEL SEGURA</usuario_creo>
/// <fecha_creo>24 OCTUBRE 2017</fecha_creo>
function formato_conceptos(row) {
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
    $('#title').append('<span class="glyphicon glyphicon-option-vertical"></span> ' + titulo);
    $('#Ml_boby').append(mensaje);
}
