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
using CarlosAg.ExcelXmlWriter;
using LitJson;
using LibPrintTicket;
using Newtonsoft.Json;
using System.Web;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using System.Globalization;
using JPV_Portal.Reportes_Excel;
using JPV_Portal.Modelo.Datos;
using JPV_Portal.Modelo.Negocio;
using JPV_Portal.Modelo.Ayudante;
using JPV_Portal.ReportesExcel;
using JPV_Portal.CORE;

namespace JPV_Portal.Portal.Controllers
{
    /// <summary>
    /// Descripción breve de Ctrl_A_Pagar
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class Ctrl_A_Pagar : System.Web.Services.WebService
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
        public String Consultar_Cuentas_Pendientes(String Parametros)
        {
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Respuesta Obj_Respuesta = new Respuesta();
            Cls_Mdl_A_Pagar Obj_Capturado = new Cls_Mdl_A_Pagar();
            String Json_Resultado = String.Empty;
            DataTable Dt_Registros = new DataTable();
            try
            {
                Obj_Capturado = JsonConvert.DeserializeObject<Cls_Mdl_A_Pagar>(Parametros);


                Dt_Registros = Controlador.Consultar_Cuentas_Pendientes(Obj_Capturado);
                if (Dt_Registros != null)
                {
                    if (Dt_Registros.Rows.Count > 0)
                        Obj_Respuesta.Registros = JsonConvert.SerializeObject(Dt_Registros, Newtonsoft.Json.Formatting.None);

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
        public String Consultar_Tabla_Cajas(String Parametros)
        {
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Respuesta Obj_Respuesta = new Respuesta();
            Cls_Mdl_A_Pagar Obj_Capturado = new Cls_Mdl_A_Pagar();
            String Json_Resultado = String.Empty;
            DataTable Dt_Registros = new DataTable();
            try
            {
                Obj_Capturado = JsonConvert.DeserializeObject<Cls_Mdl_A_Pagar>(Parametros);


                Dt_Registros = Controlador.Consultar_Tabla_Cajas(Obj_Capturado);
                if (Dt_Registros != null)
                {
                    if (Dt_Registros.Rows.Count > 0)
                        Obj_Respuesta.Registros = JsonConvert.SerializeObject(Dt_Registros, Newtonsoft.Json.Formatting.None);

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
        public String Consultar_Tabla_Bodegas(String Parametros)
        {
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Respuesta Obj_Respuesta = new Respuesta();
            Cls_Mdl_A_Pagar Obj_Capturado = new Cls_Mdl_A_Pagar();
            String Json_Resultado = String.Empty;
            DataTable Dt_Registros = new DataTable();
            try
            {
                Obj_Capturado = JsonConvert.DeserializeObject<Cls_Mdl_A_Pagar>(Parametros);


                Dt_Registros = Controlador.Consultar_Tabla_Bodegas(Obj_Capturado);
                if (Dt_Registros != null)
                {
                    if (Dt_Registros.Rows.Count > 0)
                        Obj_Respuesta.Registros = JsonConvert.SerializeObject(Dt_Registros, Newtonsoft.Json.Formatting.None);

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
        public String Consultar_Cajas(String Parametros)
        {
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Respuesta Obj_Respuesta = new Respuesta();
            Cls_Mdl_A_Pagar Obj_Capturado = new Cls_Mdl_A_Pagar();
            String Json_Resultado = String.Empty;
            DataTable Dt_Registros = new DataTable();
            try
            {
                Obj_Capturado = JsonConvert.DeserializeObject<Cls_Mdl_A_Pagar>(Parametros);


                Dt_Registros = Controlador.Consultar_Cajas(Obj_Capturado);
                if (Dt_Registros != null)
                {
                    if (Dt_Registros.Rows.Count > 0)
                        Obj_Respuesta.Registros = JsonConvert.SerializeObject(Dt_Registros, Newtonsoft.Json.Formatting.None);

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
        public String Consultar_Deposito(String Parametros)
        {
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Respuesta Obj_Respuesta = new Respuesta();
            Cls_Mdl_A_Pagar Obj_Capturado = new Cls_Mdl_A_Pagar();
            String Json_Resultado = String.Empty;
            DataTable Dt_Registros = new DataTable();
            try
            {
                Obj_Capturado = JsonConvert.DeserializeObject<Cls_Mdl_A_Pagar>(Parametros);


                Dt_Registros = Controlador.Consultar_Deposito(Obj_Capturado);
                if (Dt_Registros != null)
                {
                    if (Dt_Registros.Rows.Count > 0)
                        Obj_Respuesta.Registros = JsonConvert.SerializeObject(Dt_Registros, Newtonsoft.Json.Formatting.None);

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
        public String Recepcion_Cajas(String Parametros)
        {
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Cls_Ctrl_Operaciones Obj_Negocio = new Cls_Ctrl_Operaciones();
            Respuesta Obj_Respuesta = new Respuesta();
            Cls_Mdl_A_Pagar Obj_Capturado = new Cls_Mdl_A_Pagar();
            String Json_Resultado = String.Empty;
            DataTable Dt_Registros = new DataTable();
            DataTable Dt_Parametros = new DataTable();
            DataTable Dt_Cajas_Pendientes = new DataTable();

            try
            {
                Obj_Capturado = JsonConvert.DeserializeObject<Cls_Mdl_A_Pagar>(Parametros);
                Dt_Parametros = Obj_Negocio.Consultar_Datos_Fiscales();
                Dt_Cajas_Pendientes = Obj_Negocio.Cajas_Pendientes(Obj_Capturado);

                if (Controlador.Recepcion_Cajas(Obj_Capturado))
                {
                    Obj_Respuesta.Mensaje = "ok";
                    Obj_Respuesta.Estatus = true;
                    Imprimir_Recepcion_Caja(Obj_Capturado, Dt_Parametros, Dt_Cajas_Pendientes);
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
        public String Pagar_Cajas_Bodegas(String Parametros)
        {
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Cls_Ctrl_Operaciones Obj_Negocio = new Cls_Ctrl_Operaciones();
            Respuesta Obj_Respuesta = new Respuesta();
            Cls_Mdl_A_Pagar Obj_Capturado = new Cls_Mdl_A_Pagar();
            String Json_Resultado = String.Empty;
            DataTable Dt_Registros = new DataTable();

            try
            {
                Obj_Capturado = JsonConvert.DeserializeObject<Cls_Mdl_A_Pagar>(Parametros);

                if (Controlador.Pagar_Cajas_Bodegas(Obj_Capturado))
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
        public String Modificar_Estatus(String Parametros)
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
                Obj_Capturado = JsonConvert.DeserializeObject<Cls_Mdl_A_Pagar>(Parametros);
                Dt_Parametros = Obj_Negocio.Consultar_Datos_Fiscales();

                if (Controlador.Modificar_Estatus(Obj_Capturado))
                {
                    Obj_Respuesta.Mensaje = "ok";
                    Obj_Respuesta.Estatus = true;
                    if (Obj_Capturado.Estatus == "Abonar" || Obj_Capturado.Estatus == "Pagado")
                        Imprimir_Ticket(Obj_Capturado, Dt_Parametros);

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
        ///NOMBRE DE LA FUNCIÓN: 
        ///DESCRIPCIÓN:  
        ///PARAMETROS:  
        ///CREO:       
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String Historial_Abonos(String filtros)
        {
            Cls_Mdl_A_Pagar Obj_Negocio = new Cls_Mdl_A_Pagar();
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Respuesta Obj_Respuesta = new Respuesta();
            String Json_Resultado = String.Empty;
            DataTable Dt_Registros = new DataTable();
            Obj_Respuesta.Registros = "{}";

            try
            {
                Obj_Negocio = JsonConvert.DeserializeObject<Cls_Mdl_A_Pagar>(filtros);
                Dt_Registros = Controlador.Historial_Abonos(Obj_Negocio);

                if (Dt_Registros.Rows.Count > 0)
                {
                    Workbook Book_Reporte = new Workbook();
                    String Ruta = HttpContext.Current.Server.MapPath("~") + "\\Temporal";
                    String ruta_plantilla = System.AppDomain.CurrentDomain.BaseDirectory + "Plantillas\\Historial_Abonos.xlsx";
                    String nombre_archivo = "Historial_Abonos.xlsx";
                    String ruta_almacenamiento = Ruta + "\\" + nombre_archivo;

                    Rpt_Inventarios Obj_Reporte = new Rpt_Inventarios(ruta_plantilla, ruta_almacenamiento, Dt_Registros);
                    Obj_Reporte.Historial_Abonos();

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

                    Obj_Respuesta.Registros = "Historial_Abonos.xlsx";
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
                Obj_Respuesta.Mensaje = "Exportar Excel[" + Ex.Message + "]";
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
        ///CREO:       
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String Historial_Cajas_Entregadas(String filtros)
        {
            Cls_Mdl_A_Pagar Obj_Negocio = new Cls_Mdl_A_Pagar();
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Respuesta Obj_Respuesta = new Respuesta();
            String Json_Resultado = String.Empty;
            DataTable Dt_Registros = new DataTable();
            Obj_Respuesta.Registros = "{}";

            try
            {
                Obj_Negocio = JsonConvert.DeserializeObject<Cls_Mdl_A_Pagar>(filtros);
                Dt_Registros = Controlador.Historial_Cajas_Entregadas(Obj_Negocio);

                if (Dt_Registros.Rows.Count > 0)
                {
                    Workbook Book_Reporte = new Workbook();
                    String Ruta = HttpContext.Current.Server.MapPath("~") + "\\Temporal";
                    String ruta_plantilla = System.AppDomain.CurrentDomain.BaseDirectory + "Plantillas\\Historial_Cajas_Entregadas.xlsx";
                    String nombre_archivo = "Historial_Cajas_Entregadas.xlsx";
                    String ruta_almacenamiento = Ruta + "\\" + nombre_archivo;

                    Rpt_Inventarios Obj_Reporte = new Rpt_Inventarios(ruta_plantilla, ruta_almacenamiento, Dt_Registros);
                    Obj_Reporte.Historial_Cajas_Entregadas();

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

                    Obj_Respuesta.Registros = "Historial_Cajas_Entregadas.xlsx";
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
                Obj_Respuesta.Mensaje = "Exportar Excel[" + Ex.Message + "]";
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
        ///CREO:       
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String Ventas_Fechas(String filtros)
        {
            Cls_Mdl_A_Pagar Obj_Negocio = new Cls_Mdl_A_Pagar();
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Respuesta Obj_Respuesta = new Respuesta();
            String Json_Resultado = String.Empty;
            DataTable Dt_Registros = new DataTable();
            DataTable Dt_Abonos = new DataTable();
            DataTable Dt_Detalles = new DataTable();
            Obj_Respuesta.Registros = "{}";

            try
            {
                Obj_Negocio = JsonConvert.DeserializeObject<Cls_Mdl_A_Pagar>(filtros);
                Dt_Registros = Controlador.Ventas_Fechas(Obj_Negocio);
                Dt_Abonos = Controlador.Abonos_Fechas(Obj_Negocio);
                //Dt_Detalles = Controlador.Detalles_Ventas(Obj_Negocio);

                if (Dt_Registros.Rows.Count > 0)
                {
                    Workbook Book_Reporte = new Workbook();
                    String Ruta = HttpContext.Current.Server.MapPath("~") + "\\Temporal";
                    String ruta_plantilla = System.AppDomain.CurrentDomain.BaseDirectory + "Plantillas\\Ventas.xlsx";
                    String nombre_archivo = "Ventas.xlsx";
                    String ruta_almacenamiento = Ruta + "\\" + nombre_archivo;

                    Rpt_Inventarios Obj_Reporte = new Rpt_Inventarios(ruta_plantilla, ruta_almacenamiento, Dt_Registros);
                    Obj_Reporte.Ventas(Dt_Abonos);

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

                    Obj_Respuesta.Registros = "Ventas.xlsx";
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
                Obj_Respuesta.Mensaje = "Exportar Excel[" + Ex.Message + "]";
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Obj_Respuesta);
            }
            return Json_Resultado;
        }
        ////******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: 
        ///DESCRIPCIÓN:
        ///PARAMETROS:  
        ///CREO:       MARIA CHANTAL ORIGEL SEGURA
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************     
        private void Imprimir_Ticket(Cls_Mdl_A_Pagar Reporte_Datos, DataTable Parametros)
        {
            try
            {
                CrearTicket ticket = new CrearTicket();
                //ticket.Imprimir_Logo();
                ticket.TextoIzquierda(" ");
                ticket.TextoCentro("JITOMATES");
                ticket.TextoCentro("'RIO GRANDE'");
                if (Parametros.Rows.Count > 0)
                {
                    //ticket.TextoCentro("" + Parametros.Rows[0]["Domicilio"]);
                    //ticket.TextoCentro("" + Parametros.Rows[0]["RFC"]);
                    //ticket.TextoCentro("TEL." + Parametros.Rows[0]["Telefono"]);
                }
                ticket.TextoIzquierda(" ");
                ticket.TextoDerecha("Folio: " + Reporte_Datos.Folio);
                ticket.TextoIzquierda(" ");
                ticket.TextoIzquierda("Fecha:" + DateTime.Today);
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("Cliente:" + Reporte_Datos.Cliente);
                ticket.TextoIzquierda("");
                ticket.lineasGuio();
                if(Reporte_Datos.Estatus== "Abonar")
                ticket.TextoIzquierda("ABONO: " + Reporte_Datos.Abonado);
                if (Reporte_Datos.Estatus == "Pagado")
                    ticket.TextoIzquierda("PAGO: " + Reporte_Datos.Pagado);
                ticket.lineasIgual();
                ticket.TextoIzquierda(" ");
                if(Reporte_Datos.Estatus== "Abonar")
                    ticket.AgregarTotales("          Le restan: $ ", System.Convert.ToDecimal(Reporte_Datos.Pagado)- System.Convert.ToDecimal(Reporte_Datos.Abonado));
                ticket.TextoIzquierda(" ");
                ticket.TextoCentro("GRACIAS POR SU COMPRA");
                ticket.TextoCentro("VUELVA PRONTO");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");
                ticket.CortaTicket();
                ticket.ImprimirTicket("" + Parametros.Rows[0]["Impresora"]);

            }
            catch (Exception ex)
            {

                //throw new Exception(ex.Message);
            }
        }
        ////******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: 
        ///DESCRIPCIÓN:
        ///PARAMETROS:  
        ///CREO:       MARIA CHANTAL ORIGEL SEGURA
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************     
        private void Imprimir_Recepcion_Caja(Cls_Mdl_A_Pagar Reporte_Datos, DataTable Parametros, DataTable Cajas_Pendientes)
        {
            try
            {
                CrearTicket ticket = new CrearTicket();
                //ticket.Imprimir_Logo();
                //ticket.HeaderImage = picturebox.image;
                ticket.TextoIzquierda(" ");
                ticket.TextoCentro("JITOMATES");
                ticket.TextoCentro("'RIO GRANDE'");
                if (Parametros.Rows.Count > 0)
                {
                    //ticket.TextoCentro("" + Parametros.Rows[0]["Domicilio"]);
                    //ticket.TextoCentro("" + Parametros.Rows[0]["RFC"]);
                    //ticket.TextoCentro("TEL." + Parametros.Rows[0]["Telefono"]);
                }
                ticket.TextoIzquierda(" ");
                //ticket.TextoDerecha("Folio: " + Reporte_Datos.Folio);
                ticket.TextoIzquierda(" ");
                ticket.TextoIzquierda("Fecha:" + DateTime.Today);
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("Cliente:" + Reporte_Datos.Cliente);
                ticket.TextoIzquierda("");
                ticket.lineasGuio();
                ticket.TextoIzquierda("ENTREGO : " + Reporte_Datos.Cantidad + " CAJAS");
                ticket.TextoIzquierda("SE ENTREGÓ A CLIENTE :$" + Reporte_Datos.Importe + " DEL DEPOSITO");
                ticket.TextoIzquierda(" ");
                ticket.TextoIzquierda("CAJAS PENDIENTES : ");
                if (Cajas_Pendientes.Rows.Count > 0)
                {
                    for(var j=0;j< Cajas_Pendientes.Rows.Count;j++)
                    ticket.TextoDerecha(""+Cajas_Pendientes.Rows[j]["Tipo_Caja"] + "    "+ Cajas_Pendientes.Rows[j]["Cantidad"]);
                }
                ticket.lineasIgual();
                ticket.TextoIzquierda(" ");
                ticket.TextoCentro("GRACIAS POR SU COMPRA");
                ticket.TextoCentro("VUELVA PRONTO");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");
                ticket.CortaTicket();
                ticket.ImprimirTicket("" + Parametros.Rows[0]["Impresora"]);

            }
            catch (Exception ex)
            {

                //throw new Exception(ex.Message);
            }
        }

    }
}
