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
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using System.Globalization;
using JPV_Portal.Reportes_Excel;
using JPV_Portal.Modelo.Datos;
using JPV_Portal.Modelo.Negocio;
using JPV_Portal.Modelo.Ayudante;
using JPV_Portal.CORE;

namespace JPV_Portal.Portal.Controllers
{
    /// <summary>
    /// Descripción breve de Ctrl_Ventas
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class Ctrl_Ventas : System.Web.Services.WebService
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
        public void Cargar_Cmb_Cliente()
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
                Dt_Registros = Controlador.Consulta_Clientes(nvc["q"].ToString().Trim());
                if (!String.IsNullOrEmpty(nvc["q"]))
                    Str_Q = nvc["q"].ToString().Trim();

                var Datos = from Fila in Dt_Registros.AsEnumerable()
                            orderby Fila.Field<int>("Cliente_ID") ascending
                            select new Cls_Select2
                            {
                                id = Fila.Field<int>("Cliente_ID"),
                                text = Fila.Field<String>("Nombre"),
                                tag2 = Fila.Field<Decimal>("Cuentas_Pendientes"),
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
        public void Cargar_Cmb_Cajas()
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
                Dt_Registros = Controlador.Cargar_Cajas();
                if (!String.IsNullOrEmpty(nvc["q"]))
                    Str_Q = nvc["q"].ToString().Trim();

                var Datos = from Fila in Dt_Registros.AsEnumerable()
                            orderby Fila.Field<int>("Caja_ID") ascending
                            select new Cls_Select2
                            {
                                id = Fila.Field<int>("Caja_ID"),
                                text = Fila.Field<String>("Descripcion"),
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
            String Json_Resultado = String.Empty;
            List<Cls_Select2> Lista_Select = new List<Cls_Select2>();
            DataTable Dt_Registros = new DataTable();
            try
            {
                String Str_Q = String.Empty;
                NameValueCollection nvc = Context.Request.Form;
                Dt_Registros = Controlador.Consulta_Producto_Entrada(nvc["q"].ToString().Trim());
                if (!String.IsNullOrEmpty(nvc["q"]))
                    Str_Q = nvc["q"].ToString().Trim();

                var Datos = from Fila in Dt_Registros.AsEnumerable()
                            orderby Fila.Field<int>("Entrada_ID") descending
                            select new Cls_Select2
                            {
                                id = Fila.Field<int>("Entrada_ID"),
                                text = Fila.Field<String>("Chofer"),
                                tag = Fila.Field<String>("Producto"),
                                tag1 = Fila.Field<String>("Fecha_Creo"),
                                tag2 = Fila.Field<Decimal>("Cantidad_Descontar"),

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
        ///CREO:       
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String Folio()
        {
            Cls_Ctrl_Operaciones Obj_Negocio = new Cls_Ctrl_Operaciones();
            Respuesta Obj_Respuesta = new Respuesta();
            String Json_Resultado = String.Empty;
            DataTable Dt_Registros = new DataTable();
            Obj_Respuesta.Registros = "{}";

            try
            {
                Dt_Registros = Obj_Negocio.Nuevo_Folio();
                if (Dt_Registros != null)
                {
                    if (Dt_Registros.Rows.Count > 0)
                        Obj_Respuesta.Registros = JsonConvert.SerializeObject(Dt_Registros, Formatting.None);

                    Obj_Respuesta.Mensaje = "ok";
                    Obj_Respuesta.Estatus = true;
                }
            }
            catch (Exception Ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "Folio[" + Ex.Message + "]";
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
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String Imprimir(String Datos, String Items_Lista, String Items_Cajas)
        {
            Cls_Mdl_Ventas Obj_Negocios = new Cls_Mdl_Ventas();
            Cls_Ctrl_Operaciones Obj_Negocio = new Cls_Ctrl_Operaciones();
            List<Cls_Mdl_Ventas> Lista_Materiales = new List<Cls_Mdl_Ventas>();
            List<Cls_Mdl_Ventas> Lista_Cajas = new List<Cls_Mdl_Ventas>();
            Respuesta Obj_Resp = new Respuesta();
            DataTable Solicitud_ID = new DataTable();
            JsonSerializerSettings Configuracion_Json = new JsonSerializerSettings();
            Configuracion_Json.NullValueHandling = NullValueHandling.Ignore;
            String Str_Respuesta;
            CultureInfo ci = new CultureInfo("en-us");
            DataTable Dt_Parametros = new DataTable();

            try
            {
                Obj_Negocios = JsonConvert.DeserializeObject<Cls_Mdl_Ventas>(Datos);
                Lista_Materiales = JsonConvert.DeserializeObject<List<Cls_Mdl_Ventas>>(Items_Lista);
                Lista_Cajas = JsonConvert.DeserializeObject<List<Cls_Mdl_Ventas>>(Items_Cajas);

                Obj_Negocios.Usuario_Registro = Sessiones.Nombre;

                Dt_Parametros = Obj_Negocio.Consultar_Datos_Fiscales();

                if (Obj_Negocio.Guardar_Venta(Obj_Negocios, Lista_Materiales, Lista_Cajas))
                {
                    Obj_Resp.Estatus = true;
                    Obj_Resp.Mensaje = "Registro exitoso.";
                }


                Imprimir_Ticket(Obj_Negocios, Lista_Materiales, Lista_Cajas, Dt_Parametros);

                Obj_Resp.Estatus = true;
                Obj_Resp.Mensaje = "Registro exitoso.";
            }
            catch (Exception ex)
            {
                //Documento.Close();
                Obj_Resp.Estatus = false;
                Obj_Resp.Mensaje = "Guardar[" + ex.Message + "]";
            }
            finally
            {
                Str_Respuesta = JsonConvert.SerializeObject(Obj_Resp, Formatting.Indented, Configuracion_Json);
            }
            return Str_Respuesta;
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
        private void Imprimir_Ticket(Cls_Mdl_Ventas Reporte_Datos, List<Cls_Mdl_Ventas> Detalles, List<Cls_Mdl_Ventas> Cajas, DataTable Parametros)
        {
            try
            {
                for (int j = 0; j < 2; j++)
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
                    ticket.TextoIzquierda("Fecha:" + Reporte_Datos.Fecha + "     " + Reporte_Datos.Dia);
                    ticket.TextoIzquierda("");
                    ticket.TextoIzquierda("Cliente:" + Reporte_Datos.Cliente);
                    ticket.TextoIzquierda("");
                    ticket.EncabezadoVenta();
                    ticket.lineasGuio();
                    var Cont = Detalles.Count();

                    for (var i = 0; i < Cont; i++)
                    {
                        ticket.AgregaArticulo(System.Convert.ToDecimal(Detalles[i].Cantidad.ToString()), Detalles[i].Descripcion.ToString(), System.Convert.ToDecimal(Detalles[i].Costo_Unitario.ToString()), System.Convert.ToDecimal(Detalles[i].Importe.ToString()));
                    }

                    ticket.lineasIgual();
                    //ticket.AgregarTotales("          Subtotal : $ ", System.Convert.ToDecimal(Reporte_Datos.Subtotal));
                    //ticket.AgregarTotales("          IVA  : $ ", System.Convert.ToDecimal(Reporte_Datos.Precio_IVA));
                    ticket.TextoIzquierda(" ");
                    ticket.AgregarTotales("          TOTAL       : $ ", System.Convert.ToDecimal(Reporte_Datos.Total_Vendido));
                    ticket.TextoIzquierda(" ");
                    ticket.TextoIzquierda("Este ticket forma parte de la factura");
                    ticket.TextoIzquierda("global del dia ");
                    ticket.TextoIzquierda(" ");
                    ticket.TextoCentro("GRACIAS POR SU COMPRA");
                    ticket.TextoCentro("VUELVA PRONTO");
                    ticket.TextoIzquierda("");
                    ticket.TextoIzquierda("");
                    ticket.TextoIzquierda("");
                    ticket.TextoIzquierda("");
                    ticket.TextoIzquierda("");
                    ticket.TextoIzquierda("");
                    ticket.TextoIzquierda("");
                    ticket.TextoIzquierda("");
                    ticket.TextoIzquierda("");
                    ticket.CortaTicket();
                    ticket.ImprimirTicket("" + Parametros.Rows[0]["Impresora"]);

                    if (Cajas.Count > 0)
                    {
                        CrearTicket ticket_caja = new CrearTicket();
                        //ticket.Imprimir_Logo();
                        ticket_caja.TextoIzquierda(" ");
                        ticket_caja.TextoCentro("JITOMATES");
                        ticket_caja.TextoCentro("'RIO GRANDE'");
                        if (Parametros.Rows.Count > 0)
                        {
                            //ticket_caja.TextoCentro("" + Parametros.Rows[0]["Domicilio"]);
                            //ticket_caja.TextoCentro("" + Parametros.Rows[0]["RFC"]);
                            //ticket_caja.TextoCentro("TEL." + Parametros.Rows[0]["Telefono"]);
                        }
                        ticket_caja.TextoIzquierda(" ");
                        ticket_caja.TextoDerecha("Folio: " + Reporte_Datos.Folio);
                        ticket_caja.TextoIzquierda(" ");
                        ticket_caja.TextoIzquierda("Fecha:" + Reporte_Datos.Fecha + "     " + Reporte_Datos.Dia);
                        ticket_caja.TextoIzquierda("");
                        ticket_caja.TextoIzquierda("Cliente:" + Reporte_Datos.Cliente);
                        ticket_caja.TextoIzquierda("");
                        ticket_caja.EncabezadoVenta_cajas();
                        ticket_caja.lineasGuio();
                        var Con = Cajas.Count();

                        for (var i = 0; i < Con; i++)
                        {
                            ticket_caja.AgregaArticulo_Caja(System.Convert.ToDecimal(Cajas[i].Cajas_Cantidad.ToString()), Cajas[i].Cajas_Descripcion.ToString());
                        }

                        ticket_caja.lineasIgual();
                        //ticket_caja.AgregarTotales("          Subtotal : $ ", System.Convert.ToDecimal(Reporte_Datos.Subtotal));
                        //ticket_caja.AgregarTotales("          IVA  : $ ", System.Convert.ToDecimal(Reporte_Datos.Precio_IVA));
                        ticket_caja.TextoIzquierda(" ");
                        ticket_caja.AgregarTotales("          IMPORTE QUE DEJA       : $ ", System.Convert.ToDecimal(Reporte_Datos.Importe_Cajas));
                        ticket_caja.TextoIzquierda(" ");
                        ticket_caja.TextoCentro("VUELVA PRONTO");
                        ticket_caja.TextoIzquierda("");
                        ticket_caja.TextoIzquierda("");
                        ticket_caja.TextoIzquierda("");
                        ticket_caja.TextoIzquierda("");
                        ticket_caja.TextoIzquierda("");
                        ticket_caja.CortaTicket();
                        ticket_caja.ImprimirTicket("" + Parametros.Rows[0]["Impresora"]);

                    }
                }
           
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
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String Pagare()
        {
            Respuesta Obj_Resp = new Respuesta();
            String Str_Respuesta;
            JsonSerializerSettings Configuracion_Json = new JsonSerializerSettings();
            Configuracion_Json.NullValueHandling = NullValueHandling.Ignore;
            Cls_Ctrl_Operaciones Obj_Negocio = new Cls_Ctrl_Operaciones();
            DataTable Dt_Parametros = new DataTable();
            Dt_Parametros = Obj_Negocio.Consultar_Datos_Fiscales();

            try
            {
                CrearTicket ticket = new CrearTicket();
                ticket.lineasIgual();
                ticket.TextoCentro("'Rio Grande'");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("Por este pagare yo");
                ticket.TextoIzquierda("___________________________________ me ");
                ticket.TextoIzquierda("comprometo a pagar incondicionalmente a");
                ticket.TextoIzquierda("la orden de Juan Manuel Hernandez ");
                ticket.TextoIzquierda("la cantidad de ________________________");
                ticket.TextoIzquierda("y me obligo a pagar en su totalidad. En");
                ticket.TextoIzquierda("la ciudad de Irapuato o en cualquier ");
                ticket.TextoIzquierda("lugar que se  me requiera.");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");
                ticket.TextoCentro("_____________________________________");
                ticket.TextoIzquierda(" ");
                ticket.TextoCentro("FIRMA");
                ticket.lineasIgual();
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");
                ticket.CortaTicket();

                ticket.ImprimirTicket("" + Dt_Parametros.Rows[0]["Impresora"]);
                Obj_Resp.Estatus = true;

            }
            catch (Exception ex)
            {

                //throw new Exception(ex.Message);
            }
            finally
            {
                Str_Respuesta = JsonConvert.SerializeObject(Obj_Resp, Formatting.Indented, Configuracion_Json);
            }
            return Str_Respuesta;
        }
    }
}
