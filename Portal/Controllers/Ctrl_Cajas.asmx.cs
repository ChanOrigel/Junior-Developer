﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using LitJson;
using Newtonsoft.Json;
using JPV_Portal.Modelo.Datos;
using JPV_Portal.Modelo.Negocio;
using JPV_Portal.Modelo.Ayudante;
using JPV_Portal.CORE;

namespace JPV_Portal.Portal.Controllers
{
    /// <summary>
    /// Descripción breve de Ctrl_Cajas
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class Ctrl_Cajas : System.Web.Services.WebService
    {

        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: 
        ///DESCRIPCIÓN:
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String Consultar()
        {
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Respuesta Obj_Respuesta = new Respuesta();
            String Json_Resultado = String.Empty;
            DataTable Dt_Registros = new DataTable();
            try
            {
                Dt_Registros = Controlador.Consulta_Cajas(new Cls_Mdl_Cajas());
                if (Dt_Registros != null)
                {
                    Obj_Respuesta.Mensaje = "ok";
                    Obj_Respuesta.Estatus = true;

                    var result = from Fila in Dt_Registros.AsEnumerable()
                                 select new[] { 
                                     Fila.Field<String>("Descripcion").Trim(), 
                                     Fila.Field<int>("Cantidad").ToString().Trim(),
                                     Fila.Field<int>("Caja_ID").ToString()
                                 };
                    Obj_Respuesta.data = JsonConvert.SerializeObject(result, Formatting.None);
                }
            }
            catch (Exception Ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "Consultar Cajas [" + Ex.Message + "]";
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Obj_Respuesta);
            }
            return Json_Resultado;
        }

        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Ope_Alta
        ///DESCRIPCIÓN:
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String Ope_Alta(String Datos)
        {
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Cls_Mdl_Cajas Obj_Capturado = new Cls_Mdl_Cajas();
            JsonSerializerSettings Configuracion_Json = new JsonSerializerSettings();
            Respuesta Obj_Respuesta = new Respuesta();
            String Str_Respuesta = String.Empty;
            try
            {
                Obj_Capturado = JsonConvert.DeserializeObject<Cls_Mdl_Cajas>(Datos);
                //Obj_Capturado.Caja_ID = Sessiones.Identificador + "|CC-"  + Controlador.MasterCount(new Cls_Mdl_Cajas(), "Identificador", Sessiones.Identificador).ToString();
                Obj_Capturado.Usuario_Registro = Sessiones.Nombre;                
                if (Controlador.MasterRegistro(Obj_Capturado, CORE.MODO_DE_CAPTURA.CAPTURA_ALTA))
                {
                    Obj_Respuesta.Estatus = true;
                    Obj_Respuesta.Mensaje = "Registro exitoso.";
                }
            }
            catch (Exception ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "Guardar Caja [" + ex.Message + "]";
            }
            finally
            {
                Str_Respuesta = JsonConvert.SerializeObject(Obj_Respuesta, Formatting.Indented, Configuracion_Json);
            }
            return Str_Respuesta;
        }

        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Ope_Alta
        ///DESCRIPCIÓN:
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String Ope_Modificar(String Datos)
        {
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Cls_Mdl_Cajas Obj_Capturado = new Cls_Mdl_Cajas();
            JsonSerializerSettings Configuracion_Json = new JsonSerializerSettings();
            Respuesta Obj_Respuesta = new Respuesta();
            String Str_Respuesta = String.Empty;
            try
            {
                Obj_Capturado = JsonConvert.DeserializeObject<Cls_Mdl_Cajas>(Datos);
                Obj_Capturado.Usuario_Modifico = Sessiones.Nombre;
                Obj_Capturado.Fecha_Registro = "GETDATE()";                
                if (Controlador.MasterRegistro(Obj_Capturado, CORE.MODO_DE_CAPTURA.CAPTURA_ACTUALIZA))
                {
                    Obj_Respuesta.Estatus = true;
                    Obj_Respuesta.Mensaje = "Registro actualizado.";
                }
            }
            catch (Exception ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "Actualizar Caja [" + ex.Message + "]";
            }
            finally
            {
                Str_Respuesta = JsonConvert.SerializeObject(Obj_Respuesta, Formatting.Indented, Configuracion_Json);
            }
            return Str_Respuesta;
        }
    }
}
