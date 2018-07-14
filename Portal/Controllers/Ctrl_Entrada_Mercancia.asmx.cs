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
using JPV_Portal.Modelo.Datos;
using JPV_Portal.Modelo.Negocio;
using JPV_Portal.Modelo.Ayudante;
using JPV_Portal.CORE;

namespace JPV_Portal.Portal.Controllers
{
    /// <summary>
    /// Descripción breve de Ctrl_Entrada_Mercancia
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class Ctrl_Entrada_Mercancia : System.Web.Services.WebService
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
                        if (Dt_Registros.Rows.Count > 0)
                            Obj_Respuesta.Registros = JsonConvert.SerializeObject(Dt_Registros, Formatting.None);

                        Obj_Respuesta.Mensaje = "ok";
                        Obj_Respuesta.Estatus = true;
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
        //[WebMethod(EnableSession = true)]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public String Consultar()
        //{
        //    Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
        //    Respuesta Obj_Respuesta = new Respuesta();
        //    Cls_Mdl_Entrada_Mercancia Obj_Capturado = new Cls_Mdl_Entrada_Mercancia();
        //    String Json_Resultado = String.Empty;
        //    DataTable Dt_Registros = new DataTable();
        //    try
        //    {
        //        Dt_Registros = Controlador.Consulta_Entrada(new Cls_Mdl_Entrada_Mercancia(), Obj_Capturado);
        //        if (Dt_Registros != null)
        //        {
        //            Obj_Respuesta.Mensaje = "ok";
        //            Obj_Respuesta.Estatus = true;

        //            var result = from Fila in Dt_Registros.AsEnumerable()
        //                         select new[] {
        //                             Fila.Field<String>("Proveedor").Trim(),
        //                             //String.IsNullOrEmpty(("Toneladas")) ? "null, " : Fila.Field<Decimal>("Toneladas").ToString(),
        //                             Fila.Field<Decimal>("Toneladas").ToString() ==null ? " ": Fila.Field<Decimal>("Toneladas").ToString(),
        //                             Fila.Field<String>("Estatus").Trim(),
        //                             Fila.Field<String>("Producto").Trim(),
        //                             Fila.Field<Decimal>("Cantidad_Descontar").ToString(),
        //                             Fila.Field<String>("Notas")==null ? " ": Fila.Field<String>("Notas").ToString(),
        //                             //String.IsNullOrEmpty(Fila.Field<>("Notas")) ? "null, " : Fila.Field<String>("Notas").ToString(),
        //                             Fila.Field<String>("Fecha_Creo").Trim(),
        //                             Fila.Field<int>("Entrada_ID").ToString(),
        //                             //String.IsNullOrEmpty(Fila.Field<>("Cantidad_Descontar").ToString()) ? "null, " : Fila.Field<Decimal>("Cantidad_Descontar").ToString(),
        //                             Fila.Field<Decimal>("Cantidad_Descontar").ToString() ==null ? " ": Fila.Field<Decimal>("Cantidad_Descontar").ToString(),
        //                         };
        //            Obj_Respuesta.data = JsonConvert.SerializeObject(result, Formatting.None);
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        Obj_Respuesta.Estatus = false;
        //        Obj_Respuesta.Mensaje = "Consultar Usuario[" + Ex.Message + "]";
        //    }
        //    finally
        //    {
        //        Json_Resultado = JsonMapper.ToJson(Obj_Respuesta);
        //    }
        //    return Json_Resultado;
        //}
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
                Dt_Registros = Controlador.Consulta_Entrada_Historial(new Cls_Mdl_Entrada_Mercancia(), Obj_Capturado);
                if (Dt_Registros != null)
                {
                    if (Dt_Registros.Rows.Count > 0)
                        Obj_Respuesta.Registros = JsonConvert.SerializeObject(Dt_Registros, Newtonsoft.Json.Formatting.None);

                    Obj_Respuesta.Mensaje = "ok";
                    Obj_Respuesta.Estatus = true;
                }
                //if (Dt_Registros != null)
                //{
                //    Obj_Respuesta.Mensaje = "ok";
                //    Obj_Respuesta.Estatus = true;

                //    var result = from Fila in Dt_Registros.AsEnumerable()
                //                 select new[] {
                //                     Fila.Field<String>("Proveedor").Trim(),
                //                     Fila.Field<String>("Producto").ToString(),
                //                     Fila.Field<Decimal>("Cajas").ToString(),
                //                     Fila.Field<Decimal>("Toneladas").ToString(),
                //                     Fila.Field<String>("Notas")==null ? " ": Fila.Field<String>("Notas").ToString(),
                //                     Fila.Field<String>("Fecha_Creo").Trim(),
                //                     Fila.Field<String>("Estatus").ToString()
                //                 };
                //    Obj_Respuesta.data = JsonConvert.SerializeObject(result, Formatting.None);
                //}
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
        public void Cargar_Cmb_Proveedor()
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
                Dt_Registros = Controlador.Consulta_Proveedor(nvc["q"].ToString().Trim());
                if (!String.IsNullOrEmpty(nvc["q"]))
                    Str_Q = nvc["q"].ToString().Trim();

                var Datos = from Fila in Dt_Registros.AsEnumerable()
                            orderby Fila.Field<int>("Proveedor_ID") ascending
                            select new Cls_Select2
                            {
                                id = Fila.Field<int>("Proveedor_ID"),
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
            String Json_Resultado = String.Empty;
            List<Cls_Select2> Lista_Select = new List<Cls_Select2>();
            DataTable Dt_Registros = new DataTable();
            try
            {
                String Str_Q = String.Empty;
                NameValueCollection nvc = Context.Request.Form;
                Dt_Registros = Controlador.Consulta_Productos(nvc["q"].ToString().Trim());
                if (!String.IsNullOrEmpty(nvc["q"]))
                    Str_Q = nvc["q"].ToString().Trim();

                var Datos = from Fila in Dt_Registros.AsEnumerable()
                            orderby Fila.Field<int>("Producto_ID") ascending
                            select new Cls_Select2
                            {
                                id = Fila.Field<int>("Producto_ID"),
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
            Cls_Mdl_Entrada_Mercancia Obj_Capturado = new Cls_Mdl_Entrada_Mercancia();
            JsonSerializerSettings Configuracion_Json = new JsonSerializerSettings();
            Respuesta Obj_Respuesta = new Respuesta();
            String Str_Respuesta = String.Empty;
            try
            {
                String Estatus = "Alta";
                Obj_Capturado = JsonConvert.DeserializeObject<Cls_Mdl_Entrada_Mercancia>(Datos);
                Obj_Capturado.Usuario_Registro = Sessiones.Nombre;
                if (Controlador.MasterRegistro(Obj_Capturado, CORE.MODO_DE_CAPTURA.CAPTURA_ALTA))
                {
                   if(Controlador.Guardar_Producto(new Cls_Mdl_Entrada_Mercancia(), Obj_Capturado, Estatus))
                    {
                        Obj_Respuesta.Estatus = true;
                        Obj_Respuesta.Mensaje = "Registro exitoso.";
                    }
                }
            }
            catch (Exception ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "Guardar Cliente[" + ex.Message + "]";
            }
            finally
            {
                Obj_Respuesta.Estatus = true;
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
        public String Guardar_Excel(String Datos)
        {
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            List<Cls_Mdl_Entrada_Mercancia> Lista_Productos = new List<Cls_Mdl_Entrada_Mercancia>();
            Cls_Mdl_Entrada_Mercancia Obj_Capturado = new Cls_Mdl_Entrada_Mercancia();
            JsonSerializerSettings Configuracion_Json = new JsonSerializerSettings();
            Respuesta Obj_Respuesta = new Respuesta();
            String Str_Respuesta = String.Empty;
            try
            {
                Lista_Productos = JsonConvert.DeserializeObject<List<Cls_Mdl_Entrada_Mercancia>>(Datos);

                foreach(Cls_Mdl_Entrada_Mercancia tabla in Lista_Productos)
                {
                    tabla.Usuario_Registro = Sessiones.Nombre;
                }
                List<TablaDB>Lista=Lista_Productos.ToList <TablaDB>();

                if (Controlador.MasterRegistro(Obj_Capturado, CORE.MODO_DE_CAPTURA.CAPTURA_ALTA, new FiltroBD(Lista, MODO_DE_CAPTURA.CAPTURA_ALTA)))
                {
                        Obj_Respuesta.Estatus = true;
                        Obj_Respuesta.Mensaje = "Registro exitoso.";
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
            Cls_Mdl_Entrada_Mercancia Obj_Capturado = new Cls_Mdl_Entrada_Mercancia();
            JsonSerializerSettings Configuracion_Json = new JsonSerializerSettings();
            Respuesta Obj_Respuesta = new Respuesta();
            String Str_Respuesta = String.Empty;
            try
            {
                String Estatus = "Modificar";
                Obj_Capturado = JsonConvert.DeserializeObject<Cls_Mdl_Entrada_Mercancia>(Datos);
                Obj_Capturado.Usuario_Modifico = Sessiones.Nombre;

                //if (Controlador.MasterRegistro(Obj_Capturado, CORE.MODO_DE_CAPTURA.CAPTURA_ACTUALIZA))
                //{
                    if(Sessiones.Tipo_Usuario=="Administrador")
                    { 
                        if (Controlador.Guardar_Producto(new Cls_Mdl_Entrada_Mercancia(), Obj_Capturado, Estatus))
                        {
                            Obj_Respuesta.Estatus = true;
                            Obj_Respuesta.Mensaje = "Registro exitoso.";
                        }
                    }
                    else
                    {
                        Obj_Respuesta.Estatus = true;
                        Obj_Respuesta.Mensaje = "Registro exitoso.";
                    }
                //}
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
        ///NOMBRE DE LA FUNCIÓN: Leer_Excel
        ///DESCRIPCIÓN:     FUNCION PARA LEER EXCEL
        ///PARAMETROS:      RUTA
        ///CREO:            MARÍA CHANTAL ORIGEL SEGURA
        ///FECHA_CREO:      24 OCTUBRE 2017
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///******************************************************************************* 
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String Leer_Excel(String ruta)
        {
            Respuesta Obj_Respuesta = new Respuesta();
            JsonSerializerSettings Configuracion_Json = new JsonSerializerSettings();
            String Str_Respuesta;
            DataTable Dt_Excel = new DataTable();
            Configuracion_Json.NullValueHandling = NullValueHandling.Ignore;
            Obj_Respuesta.Registros = "{}";

            try
            {
                //Leemos el excel y lo pasamos a un DataTable 
                Dt_Excel = Leer_Excel_Ruta(Server.MapPath("~/Temporal/" + ruta.Trim().ToUpper()));

                //Funcion que limpia el excel de registros vacios                              
                Obj_Respuesta = Validar_Excel(Obj_Respuesta, Dt_Excel);
            }
            catch (Exception ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "Leer Excel No. Material[" + ex.Message + "]";
            }
            finally
            {
                Str_Respuesta = JsonConvert.SerializeObject(Obj_Respuesta, Formatting.Indented, Configuracion_Json);
            }
            return Str_Respuesta;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Leer_Excel_Ruta
        ///DESCRIPCIÓN:     FUNCION PARA LEER EXCEL DEL ARCHIVO
        ///PARAMETROS:      RUTA
        ///CREO:            MARÍA CHANTAL ORIGEL SEGURA
        ///FECHA_CREO:      24 OCTUBRE 2017
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///******************************************************************************* 
        public DataTable Leer_Excel_Ruta(String Path)
        {
            //Para empezar definimos la conexión OleDb a nuestro fichero Excel.
            OleDbConnection Conexion = new OleDbConnection();
            OleDbCommand Comando = new OleDbCommand();
            OleDbDataAdapter Adaptador = new OleDbDataAdapter();
            DataTable Dt_Registros = new DataTable();
            String Query = String.Empty;

            try
            {
                Conexion.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" +
                    "Data Source=" + Path + ";" +
                    "Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";

                Conexion.Open();

                Comando.CommandText = "SELECT * FROM [Sheet1$]";

                Comando.Connection = Conexion;
                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(Dt_Registros);
            }
            catch (Exception Ex)
            {
                throw new Exception("Error al leer un archivo de excel. Error: " + Ex.Message + "]");

            }
            finally
            {
                Conexion.Close();
            }
            return Dt_Registros;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Validar_Excel
        ///DESCRIPCIÓN:     FUNCION PARA VALIDAR EL EXCEL DEL ARCHIVO
        ///PARAMETROS:      RUTA, DT_EXCEL
        ///CREO:            MARÍA CHANTAL ORIGEL SEGURA
        ///FECHA_CREO:      24 OCTUBRE 2017
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///******************************************************************************* 
        public Respuesta Validar_Excel(Respuesta Obj_Respuesta, DataTable Dt_Excel)
        {
            Cls_Mdl_Entrada_Mercancia Obj_Negocio = new Cls_Mdl_Entrada_Mercancia();

            DataTable Dt_Tabla = new DataTable();
            DataTable Dt_Temporal = new DataTable();
            String Str_Msj = String.Empty;
            DataRow Dr_Fila = null;
            Boolean Bool_Valido = true;

            try
            {
                Dt_Tabla = Crear_Tabla_Materiales();

                foreach (DataRow dr in Dt_Excel.Rows)
                {
                    Bool_Valido = true;

                    if (Bool_Valido)
                    {
                        Dr_Fila = Dt_Tabla.NewRow();
                        Dr_Fila["Proveedor"] = dr["Proveedor"].ToString().Replace("\"", " in").Trim();
                        Dr_Fila["Producto"] = dr["Producto"].ToString().Replace("\"", " in").Trim();
                        Dr_Fila["Cajas"] = dr["Cantidad"].ToString().Replace("\"", " in").Trim();
                        Dr_Fila["Chofer"] = dr["Chofer"].ToString().Replace("\"", " in").Trim();
                        Dr_Fila["Cantidad_Descontar"] = dr["Cantidad"].ToString().Replace("\"", " in").Trim();
                        Dr_Fila["Proveedor_Producto"] = Dr_Fila["Proveedor"] + " " + Dr_Fila["Producto"];
                        Dr_Fila["Usuario_Creo"] = Sessiones.Nombre; ;
                        Dr_Fila["Estatus"] = "Almacen";
                        Dr_Fila["Toneladas"] = 0;

                        Dt_Tabla.Rows.Add(Dr_Fila);

                    }
                }
                if (Dt_Tabla.Rows.Count > 0)
                {
                    var result = from Fila in Dt_Tabla.AsEnumerable()
                             select new[] {
                                     Fila.Field<String>("Proveedor").Trim(),
                                     Fila.Field<String>("Producto").Trim(),
                                     Fila.Field<String>("Cajas").ToString(),
                                     Fila.Field<String>("Chofer").ToString(),
                                     Fila.Field<String>("Cantidad_Descontar").ToString(),
                                     Fila.Field<String>("Proveedor_Producto").ToString(),
                                     Fila.Field<String>("Usuario_Creo").ToString(),
                                     Fila.Field<String>("Estatus").ToString(),
                                     Fila.Field<String>("Toneladas").ToString(),
                                 };
                    Obj_Respuesta.Registros = JsonConvert.SerializeObject(result, Formatting.None);
                    Obj_Respuesta.Tabla_Registros = JsonConvert.SerializeObject(Dt_Tabla, Formatting.None);

                }

                if (!String.IsNullOrEmpty(Str_Msj))
                    Str_Msj = "No se pudieron cargar todos los registros debido a las siguientes observaciones:  <br/>" + Str_Msj;

                Obj_Respuesta.Mensaje = Str_Msj;
                Obj_Respuesta.Estatus = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Obj_Respuesta;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consultar_Materiales_Datos
        ///DESCRIPCIÓN:     FUNCION QUE CREA TABLA PARA VALIDAR EXCEL
        ///PARAMETROS:      N/A
        ///CREO:            MARÍA CHANTAL ORIGEL SEGURA
        ///FECHA_CREO:      24 OCTUBRE 2017
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************  
        public DataTable Crear_Tabla_Materiales()
        {
            DataTable Dt_Tabla = new DataTable();
            Dt_Tabla.Columns.Add("Proveedor", typeof(string));
            Dt_Tabla.Columns.Add("Producto", typeof(string));
            Dt_Tabla.Columns.Add("Cajas", typeof(string));
            Dt_Tabla.Columns.Add("Chofer", typeof(string));
            Dt_Tabla.Columns.Add("Cantidad_Descontar", typeof(string));
            Dt_Tabla.Columns.Add("Proveedor_Producto", typeof(string));
            Dt_Tabla.Columns.Add("Usuario_Creo", typeof(string));
            Dt_Tabla.Columns.Add("Estatus", typeof(string));
            Dt_Tabla.Columns.Add("Toneladas", typeof(string));

            return Dt_Tabla;
        }
    }
}
