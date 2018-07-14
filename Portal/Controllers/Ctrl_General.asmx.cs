using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.Services;
using CarlosAg.ExcelXmlWriter;
using System.Web.Script.Services;
using LitJson;
using Newtonsoft.Json;
using JPV_Portal.Modelo.Datos;
using JPV_Portal.Modelo.Negocio;
using JPV_Portal.Modelo.Ayudante;
using JPV_Portal.ReportesExcel;
using JPV_Portal.CORE;

namespace JPV_Portal.Portal.Controllers
{
    /// <summary>
    /// Descripción breve de Ctrl_General
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class Ctrl_General : System.Web.Services.WebService
    {

        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consultar
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
        public void Proveedores()
        {
            String Json_Resultado = String.Empty;
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            List<Cls_Select2> Lista_Select = new List<Cls_Select2>();
            DataTable Dt_Registros = new DataTable();
            try
            {
                String q = String.Empty;
                NameValueCollection nvc = Context.Request.Form;
                if (!String.IsNullOrEmpty(nvc["q"]))
                    q = nvc["q"].ToString().Trim();
                Dt_Registros = Controlador.Consulta_Proveedores(new Cls_Mdl_Proveedor());
                var result = from Fila in Dt_Registros.AsEnumerable()
                             select new Cls_Select2
                             {
                                 id = Fila.Field<int>("Proveedor_ID"),
                                 text = Fila.Field<String>("Nombre").ToString().Trim()
                             };
                foreach (var p in result)
                    Lista_Select.Add((Cls_Select2)p);

                Json_Resultado = JsonMapper.ToJson(Lista_Select);
                Context.Response.Write(Json_Resultado);
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.StackTrace);
            }
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consultar
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
        public void Cajas()
        {
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            List<Cls_Select2> Lista_Select = new List<Cls_Select2>();
            DataTable Dt_Registros = new DataTable();
            String Json_Resultado = String.Empty;
            try
            {
                String q = String.Empty;
                NameValueCollection nvc = Context.Request.Form;
                if (!String.IsNullOrEmpty(nvc["q"]))
                    q = nvc["q"].ToString().Trim();
                Dt_Registros = Controlador.Consulta_Cajas(new Cls_Mdl_Cajas());
                var result = from Fila in Dt_Registros.AsEnumerable()
                             select new Cls_Select2
                             {
                                 id = Fila.Field<int>("Caja_ID"),
                                 text = Fila.Field<String>("Descripcion").ToString().Trim()
                             };
                foreach (var p in result)
                    Lista_Select.Add((Cls_Select2)p);

                Json_Resultado = JsonMapper.ToJson(Lista_Select);
                Context.Response.Write(Json_Resultado);
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.StackTrace);
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
        public String Alta_Guardar(String Datos)
        {
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Cls_Ctrl_Operaciones Obj_Negocio = new Cls_Ctrl_Operaciones();
            Respuesta Obj_Respuesta = new Respuesta();
            Cls_Mdl_A_Pagar Obj_Capturado = new Cls_Mdl_A_Pagar();
            String Json_Resultado = String.Empty;
            DataTable Dt_Registros = new DataTable();
            DataTable Dt_Parametros = new DataTable();

            try
            {
                Obj_Capturado = JsonConvert.DeserializeObject<Cls_Mdl_A_Pagar>(Datos);
                Obj_Capturado.Usuario_Registro = Sessiones.Nombre;

                if (Controlador.Guardar_Entrada_Cajas(Obj_Capturado))
                {
                    Obj_Respuesta.Mensaje = "ok";
                    Obj_Respuesta.Estatus = true;
                }
            }
            catch (Exception Ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "Consultar Cliente [" + Ex.Message + "]";
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Obj_Respuesta);
            }
            return Json_Resultado;
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
        public String Archivos_Excel()
        {
            Cls_Mdl_A_Pagar Obj_Negocio = new Cls_Mdl_A_Pagar();
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Respuesta Obj_Respuesta = new Respuesta();
            String Json_Resultado = String.Empty;
            DataTable Dt_Registros = new DataTable();
            Obj_Respuesta.Registros = "{}";

            try
            {
                Dt_Registros = Controlador.Historial_Cajas();

                if (Dt_Registros.Rows.Count > 0)
                {
                    Workbook Book_Reporte = new Workbook();
                    String Ruta = HttpContext.Current.Server.MapPath("~") + "\\Temporal";
                    String ruta_plantilla = System.AppDomain.CurrentDomain.BaseDirectory + "Plantillas\\Historial_Cajas.xlsx";
                    String nombre_archivo = "Historial_Cajas.xlsx";
                    String ruta_almacenamiento = Ruta + "\\" + nombre_archivo;

                    Rpt_Inventarios Obj_Reporte = new Rpt_Inventarios(ruta_plantilla, ruta_almacenamiento, Dt_Registros);
                    Obj_Reporte.Historial_Cajas();

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

                    Obj_Respuesta.Registros = "Historial_Cajas.xlsx";
                    Obj_Respuesta.Estatus = true;
                }
                else
                {
                    Obj_Respuesta.Mensaje = "No hay datos que mostrar.";
                    Obj_Respuesta.Estatus = false;
                }
            }
            catch (Exception Ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "Archivos_Excel[" + Ex.Message + "]";
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Obj_Respuesta);
            }
            return Json_Resultado;
        }
    }
}
