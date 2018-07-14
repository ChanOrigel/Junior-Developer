/*====================================== VARIABLES =====================================*/
/*====================================== INICIO-CARGA ===================================*/

$(document).on('ready', function () {
    try {
        eventos();
        cargar_proveedores();
        cargar_cajas();
    } catch (e) {
        asignar_modal("Informe Técnico", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
});
/// <summary>
/// FUNCION QUE INICIALIZA LOS MANEJADORES DE EVENTOS.
/// </summary>
function eventos() {
    //$('#cmb_proveedor').on("select2:select", function (evt) {
    //    $('#txt_proveddor_id').val(evt.params.data.id);
    //});
    //$("#cmb_proveedor").on("select2:unselecting instead", function (e) {
    //    $('#txt_proveddor_id').val('');
    //});

    //$('#cmb_cajas').on("select2:select", function (evt) {
    //    $('#txt_caja_id').val(evt.params.data.id);
    //});
    //$("#cmb_cajas").on("select2:unselecting instead", function (e) {
    //    $('#txt_caja_id').val('');
    //});
    $('#btn_historial').on('click', function (e) {
        e.preventDefault();
        Historial_Cajas()
    });
    $('#btn_guardar').on('click', function (e) {
        e.preventDefault();
        var output = validar_datos();
        if (output.Estatus) {
            Ope_Alta();
            limpiar_controles();
            $("#cmb_cajas").select2("val", "Seleccione");
            $("#cmb_proveedor").select2("val", "Seleccione");
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
function cargar_proveedores() {
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
                url: 'Controllers/Ctrl_General.asmx/Proveedores',
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
        asignar_modal("", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}

/// <summary>
/// FUNCION PARA CARGAR LOS REGISTROS
/// </summary>
function cargar_cajas() {
    try {
        $('#cmb_cajas').select2({
            theme: "classic",
            language: "es",
            placeholder: 'Seleccione',
            allowClear: true,
            minimumInputLength: 0,
            tags: false,
            tokenSeparators: [','],
            ajax: {
                url: 'Controllers/Ctrl_General.asmx/Cajas',
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

        $('#cmb_cajas').on("select2:select", function (evt) {
            $('#txt_caja').val(evt.params.data.text);
            $('#txt_caja_id').val(evt.params.data.id);
        });

        $("#cmb_cajas").on("select2:unselecting instead", function (e) {
            $('#txt_caja').val('');
            $('#txt_caja_id').val('');
        });
    } catch (e) {
        asignar_modal("", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}

function formatRepoSelection(repo) {
    return repo.nombre || repo.text;
}
/// <summary>
/// Función que ejecuta el alta de los registros
/// </summary>
function Ope_Alta() {
    var Obj_Capturado = new Object();
    try {
        Obj_Capturado.Proveedor = $('#txt_proveedor').val();
        Obj_Capturado.Cajas = $('#txt_caja').val();
        Obj_Capturado.Cantidad = $('#txt_cantidad').val();
        Obj_Capturado.Costo = $('#txt_costo').val();


        $.ajax({
            url: 'Controllers/Ctrl_General.asmx/Alta_Guardar',
            data: "{'Datos':'" + JSON.stringify(Obj_Capturado) + "'}",
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
/// <usuario_creo>David Herrera Rincon</usuario_creo>
/// <fecha_creo>21 Junio 2016</fecha_creo>
/// <summary>
function Historial_Cajas() {
    Obj_Param = new Object();

    try {

        $.ajax({
            url: 'Controllers/Ctrl_General.asmx/Archivos_Excel',
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
                        window.open("../../Temporal/Historial_Cajas.xlsx", "FW9", 'toolbar=0,directories=0,menubar=0,status=0,scrollbars=0,resizable=1,width=500,height=100');
                    }
                    else
                        mostrar_mensaje("Advertencia", res.Mensaje);
                }
            },
            complete: function () {
                window.open("../../Temporal/Historial_Cajas.xlsx", "FW9", 'toolbar=0,directories=0,menubar=0,status=0,scrollbars=0,resizable=1,width=500,height=100');
            }

        });
    } catch (e) {
        asignar_modal("Informe Técnico", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}
/*====================================== GENERALES =====================================*/

/// <summary>
/// FUNCION PARA VALIDAR LOS DATOS REQUERIDOS
/// </summary>
function validar_datos() {
    var output = new Object();
    output.Estatus = true;
    output.Mensaje = '';
    try {
        if ($('#cmb_proveedor :selected').val() == '' || $('#cmb_proveedor :selected').val() == undefined || $('#cmb_proveedor :selected').val() == null) {
            output.Estatus = false;
            output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>PROVEEDOR</strong>.</span><br />';
        }
        if ($('#cmb_cajas :selected').val() == '' || $('#cmb_cajas :selected').val() == undefined || $('#cmb_cajas :selected').val() == null) {
            output.Estatus = false;
            output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>TIPO DE CAJA</strong>.</span><br />';
        }
        if ($('#txt_cantidad').val() == '' || $('#txt_cantidad').val() == undefined || $('#txt_cantidad').val() == null) {
            output.Estatus = false;
            output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>CANTIDAD</strong>.</span><br />';
        }
        if ($('#txt_costo').val() == '' || $('#txt_costo').val() == undefined || $('#txt_costo').val() == null) {
            output.Estatus = false;
            output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>CANTIDAD</strong>.</span><br />';
        }
    } catch (e) {
        asignar_modal("Informe Técnico", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    } finally {
        return output;
    }
}
/// <summary>
/// FUNCION PARA ESTABLECER LA PAGINA CON LA CONFIGURACION INICIAL
/// </summary>
function estado_inicial() {
    try {
        limpiar_controles();
    } catch (e) {
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
    $('#cantidad').text("0");
}
// <summary>
/// CREAR MODAL MENSAJE
/// </summary>
function asignar_modal(titulo, mensaje) {
    $('#title').text('');
    $('#Ml_boby').text('');
    $('#title').append('<span class="glyphicon glyphicon-option-vertical"></span> ' + titulo);
    $('#Ml_boby').append(mensaje);
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