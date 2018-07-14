using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Web.Script.Services;
using System.Web.Services;
using System.Threading;
using LitJson;
using Newtonsoft.Json;
using System.Web;
using System.Text;
using CarlosAg.ExcelXmlWriter;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using System.Globalization;
using JPV_Portal.Modelo.Datos;
using JPV_Portal.Modelo.Negocio;
using JPV_Portal.Modelo.Ayudante;
using JPV_Portal.CORE;
using JPV_Portal.Reportes_Excel;

namespace JPV_Portal.Portal.Controllers
{
    /// <summary>
    /// Descripción breve de Ctrl_Salida_Mercancia
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class Ctrl_Salida_Mercancia : System.Web.Services.WebService
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
            Cls_Mdl_Entrada_Mercancia Obj_Capturado = new Cls_Mdl_Entrada_Mercancia();
            String Json_Resultado = String.Empty;
            DataTable Dt_Registros = new DataTable();
            try
            {
                Dt_Registros = Controlador.Consulta_Entrada(new Cls_Mdl_Entrada_Mercancia(), Obj_Capturado);
                if (Dt_Registros != null)
                {
                    Obj_Respuesta.Mensaje = "ok";
                    Obj_Respuesta.Estatus = true;

                    var result = from Fila in Dt_Registros.AsEnumerable()
                                 select new[] {
                                     Fila.Field<String>("Proveedor").Trim(),
                                     Fila.Field<Decimal>("Toneladas").ToString(),
                                     Fila.Field<String>("Estatus").Trim(),
                                     Fila.Field<String>("Producto").Trim(),
                                     Fila.Field<Decimal>("Cajas").ToString(),
                                     Fila.Field<String>("Notas").ToString(),/* == null ? " " : Field.<String>("Notas").Trim(),*/
                                     Fila.Field<String>("Fecha_Creo").Trim(),
                                     Fila.Field<int>("Entrada_ID").ToString(),
                                     Fila.Field<Decimal>("Cantidad_Descontar").ToString(),
                                 };
                    Obj_Respuesta.data = JsonConvert.SerializeObject(result, Formatting.None);
                }
            }
            catch (Exception Ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "Consultar Usuario[" + Ex.Message + "]";
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Obj_Respuesta);
            }
            return Json_Resultado;
        }
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
        public String Historial(String Datos)
        {
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Respuesta Obj_Respuesta = new Respuesta();
            Cls_Mdl_Entrada_Mercancia Obj_Capturado = new Cls_Mdl_Entrada_Mercancia();
            String Json_Resultado = String.Empty;
            DataTable Dt_Registros = new DataTable();
            try
            {
                Obj_Capturado = JsonConvert.DeserializeObject<Cls_Mdl_Entrada_Mercancia>(Datos);
                Dt_Registros = Controlador.Consulta_Entrada(new Cls_Mdl_Entrada_Mercancia(), Obj_Capturado);
                if (Dt_Registros != null)
                {
                    Obj_Respuesta.Mensaje = "ok";
                    Obj_Respuesta.Estatus = true;

                    var result = from Fila in Dt_Registros.AsEnumerable()
                                 select new[] {
                                     Fila.Field<String>("Proveedor").Trim(),
                                     Fila.Field<String>("Producto").ToString(),
                                     Fila.Field<Decimal>("Cajas").ToString(),
                                     Fila.Field<Decimal>("Toneladas").ToString(),
                                     Fila.Field<String>("Notas").ToString(),/* == null ? " " : Field.<String>("Notas").Trim(),*/
                                     Fila.Field<String>("Fecha_Creo").Trim(),
                                     Fila.Field<String>("Estatus").ToString()
                                 };
                    Obj_Respuesta.data = JsonConvert.SerializeObject(result, Formatting.None);
                }
            }
            catch (Exception Ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "Consultar Usuario[" + Ex.Message + "]";
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Obj_Respuesta);
            }
            return Json_Resultado;
        }
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
        public void Cargar_Cmb_Clientes()
        {
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Respuesta Obj_Respuesta = new Respuesta();
            String Json_Resultado = String.Empty;
            List<Cls_Select2> Lista_Select = new List<Cls_Select2>();
            DataTable Dt_Registros = new DataTable();
            try
            {
                String Str_Q = String.Empty;
                NameValueCollection nvc = Context.Request.Form;
                Dt_Registros = Controlador.Consulta_Cliente_Salida();
                if (!String.IsNullOrEmpty(nvc["q"]))
                    Str_Q = nvc["q"].ToString().Trim();

                var Datos = from Fila in Dt_Registros.AsEnumerable()
                            orderby Fila.Field<int>("Cliente_ID") ascending
                            select new Cls_Select2
                            {
                                id = Fila.Field<int>("Cliente_ID"),
                                text = Fila.Field<String>("Nombre"),

                            };

                foreach (var p in Datos)
                    Lista_Select.Add((Cls_Select2)p);

                Json_Resultado = JsonMapper.ToJson(Lista_Select);
                Context.Response.Write(Json_Resultado);

            }
            catch (Exception Ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "Consultar Producto [" + Ex.Message + "]";
            }

        }
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
        public void Cargar_Cmb_Producto()
        {
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Respuesta Obj_Respuesta = new Respuesta();
            Cls_Mdl_Entrada_Mercancia Obj_Capturado = new Cls_Mdl_Entrada_Mercancia();
            String Json_Resultado = String.Empty;
            List<Cls_Select2> Lista_Select = new List<Cls_Select2>();
            DataTable Dt_Registros = new DataTable();
            try
            {
                String Str_Q = String.Empty;
                NameValueCollection nvc = Context.Request.Form;
                Dt_Registros = Controlador.Consulta_Entrada(new Cls_Mdl_Entrada_Mercancia(), Obj_Capturado);
                if (!String.IsNullOrEmpty(nvc["q"]))
                    Str_Q = nvc["q"].ToString().Trim();

                var Datos = from Fila in Dt_Registros.AsEnumerable()
                            orderby Fila.Field<int>("Entrada_ID") ascending
                            select new Cls_Select2
                            {
                                id = Fila.Field<int>("Entrada_ID"),
                                text = Fila.Field<String>("Proveedor_Producto"),
                                tag = Fila.Field<String>("Producto"),
                                tag1 = Fila.Field<String>("Proveedor"),
                                tag2 = Fila.Field<Decimal>("Cantidad_Descontar"),
                                tag3 = Fila.Field<String>("Fecha_Creo"),
                                tag4 = Fila.Field<String>("Chofer")

                            };

                foreach (var p in Datos)
                    Lista_Select.Add((Cls_Select2)p);

                Json_Resultado = JsonMapper.ToJson(Lista_Select);
                Context.Response.Write(Json_Resultado);

            }
            catch (Exception Ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "Consultar Producto [" + Ex.Message + "]";
            }

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
        public String Guardar(String Datos, String Lista)
        {
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Cls_Mdl_Salida_Mercancia Obj_Capturado = new Cls_Mdl_Salida_Mercancia();
            List<Cls_Mdl_Salida_Mercancia> Lista_Productos = new List<Cls_Mdl_Salida_Mercancia>();
            JsonSerializerSettings Configuracion_Json = new JsonSerializerSettings();
            Respuesta Obj_Respuesta = new Respuesta();
            String Str_Respuesta = String.Empty;
            try
            {
                Obj_Capturado = JsonConvert.DeserializeObject<Cls_Mdl_Salida_Mercancia>(Datos);
                Lista_Productos = JsonConvert.DeserializeObject<List<Cls_Mdl_Salida_Mercancia>>(Lista);

                Obj_Capturado.Usuario_Registro = Sessiones.Nombre;

                if (Controlador.Guardar_Salida(Obj_Capturado, Lista_Productos))
                {
                    Archivos_Excel(Obj_Capturado, Lista_Productos);
                    Obj_Respuesta.Estatus = true;
                    Obj_Respuesta.Mensaje = "Registro exitoso.";
                    Obj_Respuesta.Registros = "Productos_Traspaso.xlsx";
                }
            }
            catch (Exception ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "Guardar Cliente[" + ex.Message + "]";
            }
            finally
            {
                Str_Respuesta = JsonConvert.SerializeObject(Obj_Respuesta, Formatting.Indented, Configuracion_Json);
            }
            return Str_Respuesta;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Archivos_Excel
        ///DESCRIPCIÓN:     FUNCION PARA EXPORTAR A EXCEL
        ///PARAMETROS:      N/A
        ///CREO:            MARÍA CHANTAL ORIGEL SEGURA
        ///FECHA_CREO:      24 OCTUBRE 2017
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String Archivos_Excel(Cls_Mdl_Salida_Mercancia Objeto, List<Cls_Mdl_Salida_Mercancia> Lista)
        {
            Cls_Mdl_Salida_Mercancia Obj_Negocio = new Cls_Mdl_Salida_Mercancia();
            Respuesta Obj_Resp = new Respuesta();
            String Json_Resultado = String.Empty;
            DataTable Dt_Registros = new DataTable();
            Obj_Resp.Registros = "{}";

            try
            {

                //if (Dt_Registros.Rows.Count > 0)
                //{
                    Workbook Book_Reporte = new Workbook();
                    String Ruta = HttpContext.Current.Server.MapPath("~") + "\\Temporal";
                    String ruta_plantilla = System.AppDomain.CurrentDomain.BaseDirectory + "Plantillas\\Productos_Traspaso.xlsx";
                    String nombre_archivo = "Productos_Traspaso.xlsx";
                    String ruta_almacenamiento = Ruta + "\\" + nombre_archivo;

                    Rpt_Excel Obj_Reporte = new Rpt_Excel(ruta_plantilla, ruta_almacenamiento);
                    Obj_Reporte.Salida_Mercancia_Reporte(Objeto, Lista);

                    if (!Directory.Exists(Ruta))
                        Directory.CreateDirectory(Ruta);

                    HttpContext.Current.ApplicationInstance.Response.Clear();
                    HttpContext.Current.ApplicationInstance.Response.Buffer = true;
                    HttpContext.Current.ApplicationInstance.Response.ContentType = "application/vnd.ms-excel";
                    HttpContext.Current.ApplicationInstance.Response.AddHeader("Content-Disposition", "attachment; filename=" + nombre_archivo);
                    HttpContext.Current.ApplicationInstance.Response.Charset = "UTF-8";
                    HttpContext.Current.ApplicationInstance.Response.ContentEncoding = Encoding.Default;
                    HttpContext.Current.ApplicationInstance.Response.WriteFile(ruta_almacenamiento);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();

                    Obj_Resp.Registros = "Productos_Traspaso.xls";
                    Obj_Resp.Estatus = true;
                //}
                //else
                //{
                //    Obj_Resp.Mensaje = "No hay datos que mostrar.";
                //    Obj_Resp.Estatus = false;
                //}
            }
            catch (Exception Ex)
            {
                Obj_Resp.Estatus = false;
                Obj_Resp.Mensaje = "Archivos_Excel[" + Ex.Message + "]";
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Obj_Resp);
            }
            return Json_Resultado;
        }
    }
}
