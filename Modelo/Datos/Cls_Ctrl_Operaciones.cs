using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using JPV_Portal.CORE;
using JPV_Portal.Modelo.Ayudante;
using JPV_Portal.Modelo.Negocio;
using SharpContent.ApplicationBlocks.Data;

namespace JPV_Portal.Modelo.Datos
{
    public class Cls_Ctrl_Operaciones
    {
        /// <summary>
        /// Auxiliar en la operacion de usuarios con la BD de SQLServer.
        /// </summary>
        private OPSQL Obj_OPSQL;
        /// <summary>
        /// Inicializa el controlador para capturar y registrar en la BD.
        /// </summary>
        public Cls_Ctrl_Operaciones()
        {
            //Inicializamos
            Obj_OPSQL = new OPSQL();
        }
        ///*******************************************************************************
        /// NOMBRE DE LA CLASE: InsertaActualiza
        /// DESCRIPCIÓN: METODO QUE INSERTA/ACTUALIZA/ELIMINA
        /// PARÁMETROS :     
        /// CREO       :  FRANCISCO JAVIER BECERRA TOLEDO
        /// FECHA_CREO : 
        /// MODIFICO          :
        /// FECHA_MODIFICO    :
        /// CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public Boolean MasterRegistro(TablaDB Elemento, MODO_DE_CAPTURA Captura, params FiltroBD[] filtros)
        {
            try
            {
                //Variable de resultado.
                Boolean resultado = false;
                //Retornamos el resultado.
                resultado = Obj_OPSQL.MasterObject(Elemento, Captura, filtros);
                //Retornamos el resultado.
                return resultado;
            }
            catch (Exception exDB)
            {
                //Cachamos la excepcion de tipo ejecucion de comandos y vemos que acciones tomar
                throw new Exception(exDB.Message);
            }
        }
        ///*******************************************************************************
        /// NOMBRE DE LA CLASE: InsertaActualiza
        /// DESCRIPCIÓN: METODO QUE INSERTA/ACTUALIZA 
        /// PARÁMETROS :     
        /// CREO       :  FRANCISCO JAVIER BECERRA TOLEDO
        /// FECHA_CREO : 
        /// MODIFICO          :
        /// FECHA_MODIFICO    :
        /// CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public int MasterCount(TablaDB Elemento, String Identificador)
        {
            String Sql = String.Empty;
            Object ID = 0;
            int Contador = 0;
            Sql = "SELECT COUNT(Identificador) FROM " + Elemento.NombreTabla + " WHERE Identificador ='" + Identificador + "'";
            try
            {
                ID = SqlHelper.ExecuteScalar(ConexionBD.BD, CommandType.Text, Sql);
                Contador = (int.Parse(ID.ToString()) + 1);
            }
            catch (Exception exDB)
            {
                //Cachamos la excepcion de tipo ejecucion de comandos y vemos que acciones tomar
                throw new Exception(exDB.Message);
            }

            return Contador;
        }

        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR A lOS USUARIOS REGISTRADOR
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Consulta_Usuario(Cls_Mdl_Usuarios Dato)
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "SELECT * FROM Cat_Usuarios";

                if (!String.IsNullOrEmpty(Dato.Email))
                    Sql += " WHERE Email = '" + Dato.Email + "'";

                if (!String.IsNullOrEmpty(Dato.Password))
                {
                    if (Sql.Contains("WHERE"))
                        Sql += " AND Password = '" + Dato.Password + "'";
                    else
                        Sql += " WHERE Password = '" + Dato.Password + "'";
                }

                if (!String.IsNullOrEmpty(Dato.Identificador))
                {
                    if (Sql.Contains("WHERE"))
                        Sql += " AND Identificador = '" + Dato.Identificador + "'";
                    else
                        Sql += " WHERE Identificador = '" + Dato.Identificador + "'";
                }
                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR A lOS CLIENTES REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Consulta_Cliente()
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "SELECT * FROM Cat_Clientes";

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR A lOS CLIENTES REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Consulta_Cliente_Salida()
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "SELECT * FROM Cat_Clientes where Bodega='S'";

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR A lOS CLIENTES REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Consulta_Clientes(String Dato)
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "SELECT * FROM Cat_Clientes";
                Sql += " where Nombre like '%" + Dato + "%'" + " and Bodega='N'";


                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR A lOS CLIENTES REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Cargar_Cajas()
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "SELECT * FROM Cat_Cajas";

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR A lOS CLIENTES REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Consultar_Datos_Fiscales()
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "SELECT * FROM Cat_Parametros";

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR LOS PRODUCTOS REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Consulta_Producto(Cls_Mdl_Productos Dato)
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "SELECT * FROM Cat_Productos";
                //Sql += " where Descripcion like '%" + Dato + "%'";

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR LOS PRODUCTOS REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Consulta_Productos(String Dato)
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "SELECT * FROM Cat_Productos";
                Sql += " where Descripcion like '%" + Dato + "%'";

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR LOS PARAMETROS REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Consulta_Parametros(Cls_Mdl_Parametros Dato)
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "SELECT * FROM Cat_Parametros";

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR LOS PARAMETROS REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Consulta_Proveedor(String Dato)
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "SELECT * FROM Cat_Proveedores";
                Sql += " where Nombre like '%" + Dato + "%'";

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR LOS PARAMETROS REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Consulta_Producto_Entrada(String Dato)
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "SELECT ";
                Sql += "convert(varchar, Ope_Entrada_Mercancia.Fecha_Creo, 103) as Fecha_Creo,  * ";
                Sql += " FROM Ope_Entrada_Mercancia where Estatus='Almacen'";
                Sql += " and Proveedor_Producto like '%" + Dato + "%'";
                Sql += " order by Ope_Entrada_Mercancia.Entrada_ID desc";

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR LOS PRODUCTOS REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Consulta_Entrada(Cls_Mdl_Entrada_Mercancia Datos, Cls_Mdl_Entrada_Mercancia Dato)
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "Select ";
                Sql += "convert(varchar, Ope_Entrada_Mercancia.Fecha_Creo, 103) as Fecha_Creo,  * ";
                Sql += " from Ope_Entrada_Mercancia";

                if (!String.IsNullOrEmpty(Dato.Fecha_Inicio))
                {
                    if (Sql.Contains("where"))
                        Sql += " and Ope_Entrada_Mercancia.Fecha_Creo  >= '" + Dato.Fecha_Inicio + " 00:00:00'";
                    else
                        Sql += " where Ope_Entrada_Mercancia.Fecha_Creo >= '" + Dato.Fecha_Inicio + " 00:00:00'";
                }

                if (!String.IsNullOrEmpty(Dato.Fecha_Fin))
                {
                    if (Sql.Contains("where"))
                        Sql += " and Ope_Entrada_Mercancia.Fecha_Creo <= '" + Dato.Fecha_Fin + " 23:59:59'";
                    else
                        Sql += " where Ope_Entrada_Mercancia.Fecha_Creo <= '" + Dato.Fecha_Fin + " 23:59:59'";
                }

                if (Sql.Contains("where"))
                    Sql += " and Estatus='Almacen'";
                else
                    Sql += " where Estatus='Almacen'";

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR LOS PRODUCTOS REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Consulta_Entrada_Historial(Cls_Mdl_Entrada_Mercancia Datos, Cls_Mdl_Entrada_Mercancia Dato)
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "Select ";
                Sql += "convert(varchar, Ope_Entrada_Mercancia.Fecha_Creo, 103) as Fecha_Creo,  * ";
                Sql += " from Ope_Entrada_Mercancia";

                if (!String.IsNullOrEmpty(Dato.Fecha_Inicio))
                {
                    if (Sql.Contains("where"))
                        Sql += " and Ope_Entrada_Mercancia.Fecha_Creo  >= '" + Dato.Fecha_Inicio + " 00:00:00'";
                    else
                        Sql += " where Ope_Entrada_Mercancia.Fecha_Creo >= '" + Dato.Fecha_Inicio + " 00:00:00'";
                }

                if (!String.IsNullOrEmpty(Dato.Fecha_Fin))
                {
                    if (Sql.Contains("where"))
                        Sql += " and Ope_Entrada_Mercancia.Fecha_Creo <= '" + Dato.Fecha_Fin + " 23:59:59'";
                    else
                        Sql += " where Ope_Entrada_Mercancia.Fecha_Creo <= '" + Dato.Fecha_Fin + " 23:59:59'";
                }

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR LOS PRODUCTOS REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public Boolean Guardar_Producto(Cls_Mdl_Entrada_Mercancia Dato, Cls_Mdl_Entrada_Mercancia Datos, String Estatus)
        {
            Boolean Transaccion = false;
            SqlTransaction Obj_Transaccion;
            SqlConnection Obj_Conexion = new SqlConnection(ConexionBD.BD);
            SqlCommand Obj_Comando = new SqlCommand();
            String Sql = String.Empty;

            int Cajas = 0;
            int Cantidad_Descontar = 0;
            int Vendidas = 0;
            String Estatus_ = String.Empty;
            try
            {

                Obj_Conexion.Open();
                Obj_Transaccion = Obj_Conexion.BeginTransaction();
                Obj_Comando.Transaction = Obj_Transaccion;
                Obj_Comando.Connection = Obj_Conexion;

                    SqlDataAdapter Da_Datos = new SqlDataAdapter(); //
                    DataTable Ds_Datos = new DataTable();

                    Sql = "Select *";
                    Sql += " from  Cat_Productos";
                    Sql += " Where  Cat_Productos.Descripcion = '" + Datos.Producto + "'";

                    Obj_Comando.CommandText = Sql;
                    Da_Datos = new SqlDataAdapter(Obj_Comando);
                    Da_Datos.Fill(Ds_Datos);

                if(Ds_Datos.Rows.Count > 0)
                { 
                    Decimal Cantidad = System.Convert.ToDecimal(Ds_Datos.Rows[0]["Cajas_Stock"]);
                    Decimal Add = 0;
                    if (Estatus=="Alta")
                         Add = System.Convert.ToDecimal(Datos.Cajas);
                    if (Estatus == "Modificar")
                        Add = System.Convert.ToDecimal(Datos.Catalogo_Agregar_Quitar);
                    Decimal Nueva_Cantidad = Cantidad + Add;

                    Sql = " UPDATE  Cat_Productos SET ";
                    Sql += " Cajas_Stock = " + (String.IsNullOrEmpty(Nueva_Cantidad.ToString()) ? "null, " : "'" + Nueva_Cantidad.ToString() + "' ");
                    Sql += " WHERE  Cat_Productos.Descripcion = '" + Datos.Producto + "'";

                    Obj_Comando.CommandText = Sql;
                    Obj_Comando.ExecuteNonQuery();
                }

                //**********************************PARTE DE MODIFICAR CANTIDAD*********************************
                SqlDataAdapter Da_Datos_Entrada = new SqlDataAdapter(); //
                DataTable Ds_Datos_Entrada = new DataTable();

                Sql = "Select *";
                Sql += " from  Ope_Entrada_Mercancia";
                Sql += " Where  Entrada_ID = '" + Datos.Entrada_ID + "'";

                Obj_Comando.CommandText = Sql;
                Da_Datos_Entrada = new SqlDataAdapter(Obj_Comando);
                Da_Datos_Entrada.Fill(Ds_Datos_Entrada);

                if (Ds_Datos_Entrada.Rows.Count > 0)
                { 
                    Cajas = System.Convert.ToInt16(Ds_Datos_Entrada.Rows[0]["Cajas"]);
                    Cantidad_Descontar = System.Convert.ToInt16(Ds_Datos_Entrada.Rows[0]["Cantidad_Descontar"]);
                    Vendidas = Cajas - Cantidad_Descontar;

                    Cantidad_Descontar = System.Convert.ToInt16(Datos.Cajas) - Vendidas;

                    if (Cantidad_Descontar <= 0)
                        Estatus_ = "Agotado";
                    else
                        Estatus_ = "Almacen";


                    Sql = " UPDATE  Ope_Entrada_Mercancia SET ";
                    Sql += " Proveedor_ID = " + (String.IsNullOrEmpty(Datos.Proveedor_ID) ? "null, " : "'" + Datos.Proveedor_ID + "', ");
                    Sql += " Proveedor = " + (String.IsNullOrEmpty(Datos.Proveedor) ? "null, " : "'" + Datos.Proveedor + "', ");
                    Sql += " Producto = " + (String.IsNullOrEmpty(Datos.Producto) ? "null, " : "'" + Datos.Producto + "', ");
                    Sql += " Proveedor_Producto = " + (String.IsNullOrEmpty(Datos.Proveedor_Producto) ? "null, " : "'" + Datos.Proveedor_Producto + "', ");
                    Sql += " Toneladas = " + (String.IsNullOrEmpty(Datos.Toneladas) ? "null, " : "'" + Datos.Toneladas + "', ");
                    Sql += " Cajas = " + (String.IsNullOrEmpty(Datos.Cajas) ? "null, " : "'" + Datos.Cajas + "', ");
                    Sql += " Cantidad_Descontar = " + (String.IsNullOrEmpty(Cantidad_Descontar.ToString()) ? "null, " : "'" + Cantidad_Descontar.ToString() + "', ");
                    Sql += " Notas = " + (String.IsNullOrEmpty(Datos.Notas) ? "null, " : "'" + Datos.Notas + "', ");
                    Sql += " Chofer = " + (String.IsNullOrEmpty(Datos.Chofer) ? "null, " : "'" + Datos.Chofer + "', ");
                    Sql += " Estatus = " + (String.IsNullOrEmpty(Estatus_.ToString()) ? "null, " : "'" + Estatus_.ToString() + "' ");
                    Sql += " WHERE  Ope_Entrada_Mercancia.Entrada_ID = '" + Datos.Entrada_ID + "'";

                    Obj_Comando.CommandText = Sql;
                    Obj_Comando.ExecuteNonQuery();
                }
                Obj_Transaccion.Commit();
                Obj_Conexion.Close();
                Transaccion = true;

            }
            catch (Exception Ex)
            {
                Transaccion = false;
                //Obj_Transaccion.Rollback();
                throw new Exception(Ex.Message);
            }
            finally
            {
                if (!Transaccion)
                {
                    Obj_Conexion.Close();
                }
            }
            return Transaccion;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR LOS PRODUCTOS REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public Boolean Guardar_Salida(Cls_Mdl_Salida_Mercancia Datos, List<Cls_Mdl_Salida_Mercancia> Lista)
        {
            Boolean Transaccion = false;
            SqlTransaction Obj_Transaccion;
            SqlConnection Obj_Conexion = new SqlConnection(ConexionBD.BD);
            SqlCommand Obj_Comando = new SqlCommand();
            String Sql = String.Empty;
            String Estatus = String.Empty;

            try
            {
                Obj_Conexion.Open();
                Obj_Transaccion = Obj_Conexion.BeginTransaction();
                Obj_Comando.Transaction = Obj_Transaccion;
                Obj_Comando.Connection = Obj_Conexion;

                Sql = "INSERT INTO Ope_Traspaso_Bodega (";
                Sql += " Nombre, ";
                Sql += " Cantidad_Cajas, ";
                Sql += " Estatus, ";
                Sql += " Usuario_Creo ";
                Sql += ") VALUES (";
                Sql += (String.IsNullOrEmpty(Datos.Cliente) ? "null, " : "'" + Datos.Cliente + "', ");
                Sql += (String.IsNullOrEmpty(Datos.Total) ? "null, " : "'" + Datos.Total + "', ");
                Sql += (String.IsNullOrEmpty(Datos.Estatus) ? "null, " : "'" + Datos.Estatus + "', ");
                Sql += (String.IsNullOrEmpty(Datos.Usuario_Registro) ? "null " : "'" + Datos.Usuario_Registro + "' ");
                Sql += ")";

                Obj_Comando.CommandText = Sql;
                Obj_Comando.ExecuteNonQuery();

                for (int i = 0; i < Lista.Count; i++)
                {

//*******************************************EDITAR CAJAS EN TABLA ENTRADA DE MERCANCÍA*********************************
                SqlDataAdapter Da_Datos_Entrada = new SqlDataAdapter(); //
                DataTable Ds_Datos_Entrada = new DataTable();


                Sql = "SELECT * FROM Ope_Entrada_Mercancia ";
                Sql += " where Entrada_ID = '" + Lista[i].Entrada_ID + "'" ;
                Sql += " and Estatus='Almacen'";

                Obj_Comando.CommandText = Sql;
                Da_Datos_Entrada = new SqlDataAdapter(Obj_Comando);
                Da_Datos_Entrada.Fill(Ds_Datos_Entrada);

                    Decimal Cantidad_a_Descontar = 0;

                if (Ds_Datos_Entrada.Rows.Count > 0) { 
                    Decimal Cantidad_Stock = System.Convert.ToDecimal(Ds_Datos_Entrada.Rows[0]["Cantidad_Descontar"]);
                    Cantidad_a_Descontar = System.Convert.ToDecimal(Lista[i].Cantidad);
                    var cantidad_nueva = Cantidad_Stock - Cantidad_a_Descontar;


                    if (cantidad_nueva > 0)
                        Estatus = "Almacen";
                    else
                        Estatus = "Agotado";

                    Sql = " UPDATE  Ope_Entrada_Mercancia SET ";
                    Sql += " Cantidad_Descontar = " + (String.IsNullOrEmpty(cantidad_nueva.ToString()) ? "null, " : "'" + cantidad_nueva.ToString() + "', ");
                    Sql += " Estatus = " + (String.IsNullOrEmpty(Estatus.ToString()) ? "null " : "'" + Estatus.ToString() + "' ");
                    Sql += " WHERE  Ope_Entrada_Mercancia.Entrada_ID = '" + Lista[i].Entrada_ID + "'";

                    Obj_Comando.CommandText = Sql;
                    Obj_Comando.ExecuteNonQuery();
                }
                //*************************************EDITAR CAJAS EN CATÁLOGO DE PRODUCTOS******************************************
                SqlDataAdapter Da_Datos_Cat = new SqlDataAdapter(); //
                DataTable Ds_Datos_Cat = new DataTable();


                Sql = "SELECT * FROM Cat_Productos ";
                Sql += " where Descripcion = '" + Lista[i].Producto + "'";

                Obj_Comando.CommandText = Sql;
                Da_Datos_Cat = new SqlDataAdapter(Obj_Comando);
                Da_Datos_Cat.Fill(Ds_Datos_Cat);

                    if(Ds_Datos_Cat.Rows.Count>0)
                    {
                        Decimal Cantidad_Cat = System.Convert.ToDecimal(Ds_Datos_Cat.Rows[0]["Cajas_Stock"]);
                        var cantidad_nueva_cat = Cantidad_Cat - Cantidad_a_Descontar;

                        Sql = " UPDATE  Cat_Productos SET ";
                        Sql += " Cajas_Stock = " + (String.IsNullOrEmpty(cantidad_nueva_cat.ToString()) ? "null, " : "'" + cantidad_nueva_cat.ToString() + "' ");
                        Sql += " WHERE  Cat_Productos.Descripcion = '" + Lista[i].Producto + "'";

                        Obj_Comando.CommandText = Sql;
                        Obj_Comando.ExecuteNonQuery();
                    }
                
                }

                Obj_Transaccion.Commit();
                Obj_Conexion.Close();
                Transaccion = true;

            }
            catch (Exception Ex)
            {
                Transaccion = false;
                //Obj_Transaccion.Rollback();
                throw new Exception(Ex.Message);
            }
            finally
            {
                if (!Transaccion)
                {
                    Obj_Conexion.Close();
                }
            }
            return Transaccion;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: GENERA UN FOLIO
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Nuevo_Folio()
        {
            String Sql = String.Empty;
            DataTable Dt_Registros = new DataTable();

            try
            {
                Sql = "SELECT TOP 1 * From Ope_Ventas";
                Sql += " Order by Venta_ID desc";

                Dt_Registros = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registros;
        }
        /////******************************************************************************* 
        /////NOMBRE DE LA FUNCIÓN:      Salvar_Lista
        /////DESCRIPCIÓN:       Guardar o actualizar los items de la tabla
        /////PARAMETROS:  
        /////CREO:      MARIA CHANTAL ORIGEL SEGURA
        /////FECHA_CREO:  
        /////MODIFICO: 
        /////FECHA_MODIFICO:
        /////CAUSA_MODIFICACIÓN:
        /////*******************************************************************************
        internal Boolean Guardar_Venta(Cls_Mdl_Ventas Datos, List<Cls_Mdl_Ventas> Lista, List<Cls_Mdl_Ventas> Cajas)
        {
            Boolean Transaccion = false;
            SqlTransaction Obj_Transaccion;
            SqlConnection Obj_Conexion = new SqlConnection(ConexionBD.BD);
            SqlCommand Obj_Comando = new SqlCommand();
            String Sql = String.Empty;
            String Estatus = String.Empty;

            Obj_Conexion.Open();
            Obj_Transaccion = Obj_Conexion.BeginTransaction();
            Obj_Comando.Transaction = Obj_Transaccion;
            Obj_Comando.Connection = Obj_Conexion;

            try
            {
                
                    Sql = "INSERT INTO Ope_Ventas (";
                    Sql += " Cliente, ";
                    Sql += " Total_Vendido, ";
                    Sql += " Estatus, ";
                    Sql += " Folio, ";
                    Sql += " Cajas_Prestadas, ";
                    //Sql += " Importe_Cajas, ";
                    Sql += " Factura, ";
                    Sql += " Usuario_Creo ";
                    Sql += ") VALUES (";
                    Sql += (String.IsNullOrEmpty(Datos.Cliente) ? "null, " : "'" + Datos.Cliente + "', ");
                    Sql += (String.IsNullOrEmpty(Datos.Total_Vendido) ? "null, " : "'" + Datos.Total_Vendido + "', ");
                    Sql += (String.IsNullOrEmpty(Datos.Estatus) ? "null, " : "'" + Datos.Estatus + "', ");
                    Sql += (String.IsNullOrEmpty(Datos.Folio) ? "null, " : "'" + Datos.Folio + "', ");
                    Sql += (String.IsNullOrEmpty(Datos.Cajas_Prestadas) ? "null, " : "'" + Datos.Cajas_Prestadas + "', ");
                    //Sql += (String.IsNullOrEmpty(Datos.Importe_Cajas) ? "null, " : "'" + Datos.Importe_Cajas + "', ");
                    Sql += (String.IsNullOrEmpty(Datos.Factura) ? "null, " : "'" + Datos.Factura + "', ");
                    Sql += (String.IsNullOrEmpty(Datos.Usuario_Registro) ? "null " : "'" + Datos.Usuario_Registro + "' ");
                    Sql += ")";

                    Obj_Comando.CommandText = Sql;
                    Obj_Comando.ExecuteNonQuery();

                SqlDataAdapter Da_Datos = new SqlDataAdapter(); //
                DataTable Ds_Datos = new DataTable();


                Sql = "SELECT Top 1 * FROM Ope_Ventas ";
                Sql += " order by Venta_ID desc";

                Obj_Comando.CommandText = Sql;
                Da_Datos = new SqlDataAdapter(Obj_Comando);
                Da_Datos.Fill(Ds_Datos);

                var Venta_ID = System.Convert.ToInt16(Ds_Datos.Rows[0]["Venta_ID"].ToString());

                for (int i = 0; i < Lista.Count; i++)
                {
                    Sql = "INSERT INTO  Ope_Ventas_Detalles (";
                    Sql += " Venta_ID, ";
                    Sql += " Descripcion, ";
                    Sql += " Cantidad, ";
                    Sql += " Costo_Unitario, ";
                    Sql += " Importe, ";
                    Sql += " Entrada_ID, ";
                    Sql += " Usuario_Creo ";
                    Sql += ") VALUES (";
                    Sql += (String.IsNullOrEmpty(Venta_ID.ToString()) ? "null, " : "'" + Venta_ID.ToString() + "', ");
                    Sql += (String.IsNullOrEmpty(Lista[i].Descripcion) ? "null, " : "'" + Lista[i].Descripcion + "', ");
                    Sql += (String.IsNullOrEmpty(Lista[i].Cantidad) ? "null, " : "'" + Lista[i].Cantidad + "', ");
                    Sql += (String.IsNullOrEmpty(Lista[i].Costo_Unitario) ? "null, " : "'" + Lista[i].Costo_Unitario + "', ");
                    Sql += (String.IsNullOrEmpty(Lista[i].Importe) ? "null, " : "'" + Lista[i].Importe + "', ");
                    Sql += (String.IsNullOrEmpty(Lista[i].Entrada_ID) ? "null, " : "'" + Lista[i].Entrada_ID + "', ");
                    Sql += (String.IsNullOrEmpty(Datos.Usuario_Registro) ? "null " : "'" + Datos.Usuario_Registro + "' ");
                    Sql += ")";

                    Obj_Comando.CommandText = Sql;
                    Obj_Comando.ExecuteNonQuery();

                }

                //******************SE AGREGA EL ADEUDO DE CAJAS  AL CLIENTE, Y SE DESCUENTA DEL CATÁLOGO DE CAJAS*********************
            
                SqlDataAdapter Da_Datos_Cliente = new SqlDataAdapter(); //
                DataTable Ds_Datos_Cliente = new DataTable();


                Sql = "SELECT * FROM Cat_Clientes where Nombre = '"+ Datos.Cliente + "'";

                Obj_Comando.CommandText = Sql;
                Da_Datos_Cliente = new SqlDataAdapter(Obj_Comando);
                Da_Datos_Cliente.Fill(Ds_Datos_Cliente);

                if(Ds_Datos_Cliente.Rows.Count > 0)
                { 
                    var Cliente_ID = System.Convert.ToInt16(Ds_Datos_Cliente.Rows[0]["Cliente_ID"].ToString());

                    Decimal Cajas_P = System.Convert.ToDecimal(Ds_Datos_Cliente.Rows[0]["Cajas_Pendientes"].ToString());
                    Decimal Cuentas_P = System.Convert.ToDecimal(Ds_Datos_Cliente.Rows[0]["Cuentas_Pendientes"].ToString());

                    Decimal caja = Cajas_P + System.Convert.ToDecimal(Datos.Cajas_Prestadas);
                    Decimal cuentas = Cuentas_P + 1;

                    Decimal Immporte_Cajas = 0;

                    if (!String.IsNullOrEmpty(Ds_Datos_Cliente.Rows[0]["Importe_Cajas"].ToString()))
                        Immporte_Cajas = System.Convert.ToDecimal(Ds_Datos_Cliente.Rows[0]["Importe_Cajas"].ToString()) + System.Convert.ToDecimal(Datos.Importe_Cajas);
                    else
                        Immporte_Cajas = System.Convert.ToDecimal(Datos.Importe_Cajas);

                    //PARTE DE ACTUALIZAR LOS DE CUENTAS PENDIENTES 

                    Sql = " Update Cat_Clientes SET ";
                    Sql += " Cajas_Pendientes = " + (String.IsNullOrEmpty(caja.ToString()) ? "null, " : "'" + caja.ToString() + "', ");
                    Sql += " Importe_Cajas = " + (String.IsNullOrEmpty(Immporte_Cajas.ToString()) ? "null " : "'" + Immporte_Cajas.ToString() + "', ");
                    Sql += " Cuentas_Pendientes = " + (String.IsNullOrEmpty(cuentas.ToString()) ? "null " : "'" + cuentas.ToString() + "' ");
                    Sql += " WHERE Cliente_ID = " + Cliente_ID.ToString();

                    Obj_Comando.CommandText = Sql;
                    Obj_Comando.ExecuteNonQuery();
                }
//******************SE DESCUENTA LA VENTA EN LA ENTRADA DE MERCANCÍA, Y EN CASO DE AGOTARSE, SE MODIFICA EL ESTATUS*********************


                SqlDataAdapter Da_Datos_Merca = new SqlDataAdapter(); //
                DataTable Ds_Datos_Merca = new DataTable();

                for (int i = 0; i < Lista.Count; i++)
                {

                    Sql = "SELECT * FROM Ope_Entrada_Mercancia where Entrada_ID = '" + Lista[i].Entrada_ID + "'";

                    Obj_Comando.CommandText = Sql;
                    Da_Datos_Merca = new SqlDataAdapter(Obj_Comando);
                    Da_Datos_Merca.Fill(Ds_Datos_Merca);

                    Decimal Cajas_Descontar = System.Convert.ToDecimal(Ds_Datos_Merca.Rows[i]["Cantidad_Descontar"].ToString());

                    Decimal cajas_restantes = Cajas_Descontar - System.Convert.ToDecimal(Lista[i].Cantidad);

                    String Estatus_ = "Almacen";

                    if(cajas_restantes >0)
                        Estatus_ = "Almacen";
                    else
                        Estatus_ = "Agotado";
                    //PARTE DE ACTUALIZAR LOS DE mercancia 

                    Sql = " Update Ope_Entrada_Mercancia SET ";
                    Sql += " Cantidad_Descontar = " + (String.IsNullOrEmpty(cajas_restantes.ToString()) ? "null, " : "'" + cajas_restantes.ToString() + "', ");
                    Sql += " Estatus = " + (String.IsNullOrEmpty(Estatus_.ToString()) ? "null " : "'" + Estatus_.ToString() + "' ");
                    Sql += " WHERE Entrada_ID = " + Lista[i].Entrada_ID.ToString();

                    Obj_Comando.CommandText = Sql;
                    Obj_Comando.ExecuteNonQuery();
                    
                }
                //***************DESCONTAR LAS CAJAS DEL CATALOGO  Y AGREGAR LAS CAJAS AL HISTORIAL********************************************************************

                if (!String.IsNullOrEmpty(Datos.Cliente))
                { 
                SqlDataAdapter Da_Datos_Historial = new SqlDataAdapter(); //
                DataTable Ds_Datos_Historial = new DataTable();

                
                int Cajas_Historial = 0;
                
                SqlDataAdapter Da_Datos_Caja = new SqlDataAdapter(); //
                DataTable Ds_Datos_Caja = new DataTable();

                for (int i = 0; i < Cajas.Count; i++)
                {
                    Sql = "SELECT * FROM Cat_Cajas where Descripcion = '" + Cajas[i].Cajas_Descripcion + "'";

                    Obj_Comando.CommandText = Sql;
                    Da_Datos_Caja = new SqlDataAdapter(Obj_Comando);
                    Da_Datos_Caja.Fill(Ds_Datos_Caja);

                    if (Ds_Datos_Caja.Rows.Count > 0)
                    { 
                        var Caja_ID = System.Convert.ToInt16(Ds_Datos_Caja.Rows[i]["Caja_ID"].ToString());

                        int Cajas_Descontar = System.Convert.ToInt16(Ds_Datos_Caja.Rows[i]["Cantidad"].ToString()) - System.Convert.ToInt16(Cajas[i].Cajas_Cantidad);

                        //PARTE DE ACTUALIZAR LOS DE CAJAS 

                        Sql = " Update Cat_Cajas SET ";
                        Sql += " Cantidad = " + (String.IsNullOrEmpty(Cajas_Descontar.ToString()) ? "null " : "'" + Cajas_Descontar.ToString() + "' ");
                        Sql += " WHERE Caja_ID = " + Caja_ID.ToString();

                        Obj_Comando.CommandText = Sql;
                        Obj_Comando.ExecuteNonQuery();
                    }

                    //**********************PARTE DE COLOCAR EL PAGO DEL IMPORTE DE LAS CAJAS******************************************
                    Sql = "SELECT * FROM Ope_Historial_Cajas where Cliente = '" + Datos.Cliente + "'";
                    Sql += " and Tipo_Caja = '" + Cajas[i].Cajas_Descripcion + "'";

                    Obj_Comando.CommandText = Sql;
                    Da_Datos_Historial = new SqlDataAdapter(Obj_Comando);
                    Da_Datos_Historial.Fill(Ds_Datos_Historial);

                    if (Ds_Datos_Historial.Rows.Count > 0)
                    {
                        if(Ds_Datos_Historial.Rows[i]["Tipo_Caja"].ToString()== Cajas[i].Cajas_Descripcion)
                        {
                            Cajas_Historial = System.Convert.ToInt16(Ds_Datos_Historial.Rows[i]["Cantidad"].ToString()) + System.Convert.ToInt16(Cajas[i].Cajas_Cantidad);
                          
                            Sql = " Update Ope_Historial_Cajas SET ";
                            Sql += " Cantidad = " + (String.IsNullOrEmpty(Cajas_Historial.ToString()) ? "null " : "'" + Cajas_Historial.ToString() + "' ");
                            Sql += " WHERE Caja_ID = " + Ds_Datos_Historial.Rows[i]["Caja_ID"].ToString();

                            Obj_Comando.CommandText = Sql;
                            Obj_Comando.ExecuteNonQuery();
                        }
                        else
                        {
                            Sql = "INSERT INTO  Ope_Historial_Cajas (";
                            Sql += " Tipo_Caja, ";
                            Sql += " Cantidad, ";
                            Sql += " Cliente, ";
                            Sql += " Estatus, ";
                            //Sql += " Importe_Cajas, ";
                            Sql += " Usuario_Creo ";
                            Sql += ") VALUES (";
                            Sql += (String.IsNullOrEmpty(Cajas[i].Cajas_Descripcion.ToString()) ? "null, " : "'" + Cajas[i].Cajas_Descripcion.ToString() + "', ");
                            Sql += (String.IsNullOrEmpty(Cajas[i].Cajas_Cantidad) ? "null, " : "'" + Cajas[i].Cajas_Cantidad + "', ");
                            Sql += (String.IsNullOrEmpty(Datos.Cliente) ? "null, " : "'" + Datos.Cliente + "', ");
                            Sql += (String.IsNullOrEmpty("Pendiente") ? "null, " : "'" + "Pendiente" + "', ");
                            //Sql += (String.IsNullOrEmpty(Datos.Importe_Cajas) ? "null, " : "'" + Datos.Importe_Cajas + "', ");
                            Sql += (String.IsNullOrEmpty(Datos.Usuario_Registro) ? "null " : "'" + Datos.Usuario_Registro + "' ");
                            Sql += ")";

                            Obj_Comando.CommandText = Sql;
                            Obj_Comando.ExecuteNonQuery();
                        }
                       
                    }
                    else
                    {
                        Sql = "INSERT INTO  Ope_Historial_Cajas (";
                        Sql += " Tipo_Caja, ";
                        Sql += " Cantidad, ";
                        Sql += " Cliente, ";
                        Sql += " Estatus, ";
                        //Sql += " Importe_Cajas, ";
                        Sql += " Usuario_Creo ";
                        Sql += ") VALUES (";
                        Sql += (String.IsNullOrEmpty(Cajas[i].Cajas_Descripcion.ToString()) ? "null, " : "'" + Cajas[i].Cajas_Descripcion.ToString() + "', ");
                        Sql += (String.IsNullOrEmpty(Cajas[i].Cajas_Cantidad) ? "null, " : "'" + Cajas[i].Cajas_Cantidad + "', ");
                        Sql += (String.IsNullOrEmpty(Datos.Cliente) ? "null, " : "'" + Datos.Cliente + "', ");
                        Sql += (String.IsNullOrEmpty("Pendiente") ? "null, " : "'" + "Pendiente" + "', ");
                        //Sql += (String.IsNullOrEmpty(Datos.Importe_Cajas) ? "null, " : "'" + Datos.Importe_Cajas + "', ");
                        Sql += (String.IsNullOrEmpty(Datos.Usuario_Registro) ? "null " : "'" + Datos.Usuario_Registro + "' ");
                        Sql += ")";

                        Obj_Comando.CommandText = Sql;
                        Obj_Comando.ExecuteNonQuery();
                    }

                }
                }
//*****************************SE AGREGA UN ABONO EN CERO PARA QUE PUEDA ENTRAR EN LA CONSULTA*******************
                Sql = "INSERT INTO  Ope_Historial_Abonos (";
                Sql += " Folio, ";
                Sql += " Cantidad, ";
                Sql += " Cliente, ";
                Sql += " Usuario_Creo ";
                Sql += ") VALUES (";
                Sql += (String.IsNullOrEmpty(Datos.Folio) ? "null, " : "'" + Datos.Folio + "', ");
                if(Datos.Estatus == "Pagado")
                    Sql += (String.IsNullOrEmpty(Datos.Total_Vendido) ? "null, " : "'" + Datos.Total_Vendido + "', ");
                else
                    Sql += (String.IsNullOrEmpty("0") ? "null, " : "'" + "0" + "', ");
                Sql += (String.IsNullOrEmpty(Datos.Cliente) ? "null, " : "'" + Datos.Cliente + "', ");
                Sql += (String.IsNullOrEmpty(Datos.Usuario_Registro) ? "null " : "'" + Datos.Usuario_Registro + "' ");
                Sql += ")";

                Obj_Comando.CommandText = Sql;
                Obj_Comando.ExecuteNonQuery();

                Obj_Transaccion.Commit();
                Obj_Conexion.Close();
                Transaccion = true;
            }
            catch (Exception Ex)
            {
                Transaccion = false;
                Obj_Transaccion.Rollback();
                throw new Exception(Ex.Message);
            }
            finally
            {
                if (!Transaccion)
                {
                    Obj_Conexion.Close();
                }
            }
            return Transaccion;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR A lOS CLIENTES REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Consultar_Cuentas_Pendientes(Cls_Mdl_A_Pagar Negocio)
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "select Venta_ID, Ope_Ventas.Cliente, Ope_Ventas.Folio, ";
                Sql += " convert(varchar, Ope_Ventas.Fecha_Creo, 103) as Fecha_Creo, ";
                Sql += " Total_Vendido - SUM(Ope_Historial_Abonos.Cantidad) as Total_Vendido ";
                Sql += " from Ope_Historial_Abonos inner join Ope_Ventas on ";
                Sql += " Ope_Historial_Abonos.Folio = Ope_Ventas.Folio";
                Sql += " where Ope_Historial_Abonos.Folio = Ope_Ventas.Folio";
                Sql += " and Estatus='Pendiente'";

                if (!String.IsNullOrEmpty(Negocio.Fecha_Inicio))
                {
                    if (Sql.Contains("where"))
                        Sql += " and Ope_Ventas.Fecha_Creo >= '" + Negocio.Fecha_Inicio + " 00:00:00'";
                    else
                        Sql += " where Ope_Ventas.Fecha_Creo >= '" + Negocio.Fecha_Inicio + " 00:00:00'";
                }

                if (!String.IsNullOrEmpty(Negocio.Fecha_Fin))
                {
                    if (Sql.Contains("where"))
                        Sql += " and Ope_Ventas.Fecha_Creo <= '" + Negocio.Fecha_Fin + " 23:59:59'";
                    else
                        Sql += " where Ope_Ventas.Fecha_Creo <= '" + Negocio.Fecha_Fin + " 23:59:59'";
                }
                if (!String.IsNullOrEmpty(Negocio.Folio))
                {
                    if (Sql.Contains("where"))
                        Sql += " and Ope_Ventas.Folio = '" + Negocio.Folio + "'";
                    else
                        Sql += " where Ope_Ventas.Folio = '" + Negocio.Folio + "'";
                }
                if (!String.IsNullOrEmpty(Negocio.Cliente))
                {
                    if (Sql.Contains("where"))
                        Sql += " and Ope_Ventas.Cliente = '" + Negocio.Cliente + "'";
                    else
                        Sql += " where Ope_Ventas.Cliente = '" + Negocio.Cliente + "'";
                }

                Sql += " group by Venta_ID, Ope_Ventas.Cliente, Ope_Ventas.Folio, Total_Vendido, Ope_Ventas.Fecha_Creo";
                Sql += " order by Ope_Ventas.Fecha_Creo desc";

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR A lOS CLIENTES REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public Boolean Modificar_Estatus(Cls_Mdl_A_Pagar Datos)
        {
            Boolean Transaccion = false;
            SqlTransaction Obj_Transaccion;
            SqlConnection Obj_Conexion = new SqlConnection(ConexionBD.BD);
            SqlCommand Obj_Comando = new SqlCommand();
            String Sql = String.Empty;
            String Estatus = String.Empty;

            Obj_Conexion.Open();
            Obj_Transaccion = Obj_Conexion.BeginTransaction();
            Obj_Comando.Transaction = Obj_Transaccion;
            Obj_Comando.Connection = Obj_Conexion;

            DataTable Dt_Registro = new DataTable();
            try
            {
                if(Datos.Estatus == "Abonar")
                {
                    Sql = "INSERT INTO  Ope_Historial_Abonos (";
                    Sql += " Folio, ";
                    Sql += " Cliente, ";
                    Sql += " Cantidad, ";
                    Sql += " Usuario_Creo ";
                    Sql += ") VALUES (";
                    Sql += (String.IsNullOrEmpty(Datos.Folio.ToString()) ? "null, " : "'" + Datos.Folio.ToString() + "', ");
                    Sql += (String.IsNullOrEmpty(Datos.Cliente) ? "null, " : "'" + Datos.Cliente + "', ");
                    Sql += (String.IsNullOrEmpty(Datos.Abonado) ? "null, " : "'" + Datos.Abonado + "', ");
                    Sql += (String.IsNullOrEmpty(Datos.Usuario_Registro) ? "null " : "'" + Datos.Usuario_Registro + "' ");
                    Sql += ")";

                    Obj_Comando.CommandText = Sql;
                    Obj_Comando.ExecuteNonQuery();

                    Decimal Restante = System.Convert.ToDecimal(Datos.Pagado) - System.Convert.ToDecimal(Datos.Abonado);

                    if(Restante > 1)
                        Estatus = "Pendiente";
                    else
                        Estatus = "Pagado";

                    //PARTE DE ACTUALIZAR LOS DE CUENTAS PENDIENTES 

                    Sql = " Update Ope_Ventas SET ";
                    Sql += " Estatus = " + (String.IsNullOrEmpty(Estatus.ToString()) ? "null " : "'" + Estatus.ToString() + "' ");
                    Sql += " WHERE Venta_ID = " + Datos.Venta_ID.ToString();

                    Obj_Comando.CommandText = Sql;
                    Obj_Comando.ExecuteNonQuery();

                    if(Estatus=="Pagado")
                    {
                        SqlDataAdapter Da_Datos = new SqlDataAdapter(); //
                        DataTable Ds_Datos = new DataTable();


                        Sql = "SELECT * FROM Cat_Clientes ";
                        Sql += " WHERE Nombre='" + Datos.Cliente + "'";

                        Obj_Comando.CommandText = Sql;
                        Da_Datos = new SqlDataAdapter(Obj_Comando);
                        Da_Datos.Fill(Ds_Datos);

                        Decimal Cuentas_Pend = 0;
                        if (Ds_Datos.Rows.Count > 0)
                        {
                            Cuentas_Pend = System.Convert.ToDecimal(Ds_Datos.Rows[0]["Cuentas_Pendientes"].ToString()) -1;

                            Sql = " Update Cat_Clientes SET ";
                            Sql += " Cuentas_Pendientes = " + (String.IsNullOrEmpty(Cuentas_Pend.ToString()) ? "null " : "'" + Cuentas_Pend.ToString() + "' ");
                            Sql += " WHERE Nombre = '" + Datos.Cliente + "'";

                            Obj_Comando.CommandText = Sql;
                            Obj_Comando.ExecuteNonQuery();
                        }
                    }

                }
                if (Datos.Estatus == "Pagado")
                {
                    Sql = "INSERT INTO  Ope_Historial_Abonos (";
                    Sql += " Folio, ";
                    Sql += " Cliente, ";
                    Sql += " Cantidad, ";
                    Sql += " Usuario_Creo ";
                    Sql += ") VALUES (";
                    Sql += (String.IsNullOrEmpty(Datos.Folio.ToString()) ? "null, " : "'" + Datos.Folio.ToString() + "', ");
                    Sql += (String.IsNullOrEmpty(Datos.Cliente) ? "null, " : "'" + Datos.Cliente + "', ");
                    Sql += (String.IsNullOrEmpty(Datos.Pagado) ? "null, " : "'" + Datos.Pagado + "', ");
                    Sql += (String.IsNullOrEmpty(Datos.Usuario_Registro) ? "null " : "'" + Datos.Usuario_Registro + "' ");
                    Sql += ")";

                    Obj_Comando.CommandText = Sql;
                    Obj_Comando.ExecuteNonQuery();

                    Estatus = "Pagado";

                    //PARTE DE ACTUALIZAR LOS DE CUENTAS PENDIENTES 

                    Sql = " Update Ope_Ventas SET ";
                    Sql += " Estatus = " + (String.IsNullOrEmpty(Estatus.ToString()) ? "null " : "'" + Estatus.ToString() + "' ");
                    Sql += " WHERE Venta_ID = " + Datos.Venta_ID.ToString();

                    Obj_Comando.CommandText = Sql;
                    Obj_Comando.ExecuteNonQuery();

                    SqlDataAdapter Da_Datos = new SqlDataAdapter(); //
                    DataTable Ds_Datos = new DataTable();


                    Sql = "SELECT * FROM Cat_Clientes ";
                    Sql += " WHERE Nombre='" + Datos.Cliente + "'";

                    Obj_Comando.CommandText = Sql;
                    Da_Datos = new SqlDataAdapter(Obj_Comando);
                    Da_Datos.Fill(Ds_Datos);

                    Decimal Cuentas_Pend = 0;
                    if (Ds_Datos.Rows.Count > 0)
                    {
                        Cuentas_Pend = System.Convert.ToDecimal(Ds_Datos.Rows[0]["Cuentas_Pendientes"].ToString()) - 1;

                        Sql = " Update Cat_Clientes SET ";
                        Sql += " Cuentas_Pendientes = " + (String.IsNullOrEmpty(Cuentas_Pend.ToString()) ? "null " : "'" + Cuentas_Pend.ToString() + "' ");
                        Sql += " WHERE Nombre = '" + Datos.Cliente + "'";

                        Obj_Comando.CommandText = Sql;
                        Obj_Comando.ExecuteNonQuery();
                    }

                }
                if (Datos.Estatus == "Cancelado")
                {
                    //**** SE COLOCA EL ESTATUS DE CANCELADO EN OPE VENTAS*****
                    Sql = " Update Ope_Ventas SET ";
                    Sql += " Estatus = " + (String.IsNullOrEmpty(Datos.Estatus.ToString()) ? "null " : "'" + Datos.Estatus.ToString() + "' ");
                    Sql += " WHERE Venta_ID = " + Datos.Venta_ID.ToString();

                    Obj_Comando.CommandText = Sql;
                    Obj_Comando.ExecuteNonQuery();

                    //**SE CONSULTAN LOS DETALLES DE LA VENTA, PORQUE EN CADA DETALLE HAY UNA ENTRADA_ID, QUE ES
                    //**DONDE SE VA A VOLVER A SUMAR LA CANTIDAD DE JITOMATE QUE SE RESTO EN LA VENTA
                    SqlDataAdapter Da_Datos = new SqlDataAdapter(); //
                    DataTable Ds_Datos = new DataTable();

                    Sql = "SELECT * FROM Ope_Ventas_Detalles ";
                    Sql += " WHERE Venta_ID='" + Datos.Venta_ID + "'";

                    Obj_Comando.CommandText = Sql;
                    Da_Datos = new SqlDataAdapter(Obj_Comando);
                    Da_Datos.Fill(Ds_Datos);

                    for(int i=0; i< Ds_Datos.Rows.Count;i++)
                    {
                        var Entrada_ID = System.Convert.ToInt16(Ds_Datos.Rows[i]["Entrada_ID"].ToString());
                        var Canti = System.Convert.ToDecimal(Ds_Datos.Rows[i]["Cantidad"].ToString());

                        SqlDataAdapter Da_Datos_Productos = new SqlDataAdapter(); //
                        DataTable Ds_Datos_Productos = new DataTable();

                        //**SE CONSULTAN DE LA TABLA ENTRADA DE MERCANCIA LOS PRODUCTOS, TAMBIEN SE MODIFICA EL ESTATUS
                        Sql = "SELECT * FROM Ope_Entrada_Mercancia ";
                        Sql += " WHERE Entrada_ID='" + Entrada_ID.ToString() + "'";

                        Obj_Comando.CommandText = Sql;
                        Da_Datos_Productos = new SqlDataAdapter(Obj_Comando);
                        Da_Datos_Productos.Fill(Ds_Datos_Productos);

                        var Cantidad = System.Convert.ToDecimal(Ds_Datos_Productos.Rows[0]["Cantidad_Descontar"].ToString())+ Canti;

                        if (Cantidad > 0)
                            Estatus = "Almacen";
                        else
                            Estatus = "Agotado";

                        Sql = " Update Ope_Entrada_Mercancia SET ";
                        Sql += " Estatus = " + (String.IsNullOrEmpty(Estatus.ToString()) ? "null, " : "'" + Estatus.ToString() + "', ");
                        Sql += " Cantidad_Descontar = " + (String.IsNullOrEmpty(Cantidad.ToString()) ? "null " : "'" + Cantidad.ToString() + "' ");
                        Sql += " WHERE Entrada_ID='" + Entrada_ID.ToString() + "'";

                        Obj_Comando.CommandText = Sql;
                        Obj_Comando.ExecuteNonQuery();

                        //SE AGREGARÁN AL CATÁLOGO DE CAJAS LAS QUE SUPUESTAMENTE SE LLEVÓ EL CLIENTE
                        SqlDataAdapter Da_Datos_Historial_Cajas = new SqlDataAdapter(); //
                        DataTable Ds_DatosHistorial_Cajas = new DataTable();

                        Sql = "select SUM(Cantidad) AS Cantidad from Ope_Historial_Cajas ";
                        Sql += " WHERE Folio='" + Datos.Folio + "'";

                        Obj_Comando.CommandText = Sql;
                        Da_Datos_Historial_Cajas = new SqlDataAdapter(Obj_Comando);
                        Da_Datos_Historial_Cajas.Fill(Ds_DatosHistorial_Cajas);
                        //SE VA A TRAER TODAS LAS CAJAS QUE SON DE ESE FOLIO, SERVIRÁ PARA RESTARLO DEL CAT DE CLIENTES


                        //ESTA CONSULTA ES PARA VER CUANTAS CAJAS DEBE EL CLIENTE
                        SqlDataAdapter Da_Datos_CLIENTE = new SqlDataAdapter(); //
                        DataTable Ds_Datos_CLIENTE = new DataTable();

                        Sql = "select * from Cat_Clientes";
                        Sql += " WHERE Nombre='" + Datos.Cliente + "'";

                        Obj_Comando.CommandText = Sql;
                        Da_Datos_CLIENTE = new SqlDataAdapter(Obj_Comando);
                        Da_Datos_CLIENTE.Fill(Ds_Datos_CLIENTE);

                        if (Ds_Datos_CLIENTE.Rows.Count > 0)
                        {
                            var Canti_Pendi = System.Convert.ToDecimal(Ds_Datos_CLIENTE.Rows[0]["Cajas_Pendientes"].ToString()) - System.Convert.ToInt16(Ds_DatosHistorial_Cajas.Rows[0]["Cantidad"].ToString());
                            var Deudas_Pendi = System.Convert.ToDecimal(Ds_Datos_CLIENTE.Rows[0]["Cuentas_Pendientes"].ToString()) - 1;
                            //SE VA A HACER EL UPDATE DE LAS CAJAS QUE DEBE EL CLIENTE, RESTANDO LAS QUE SE CANCELARON
                            Sql = " Update Cat_Clientes SET ";
                            Sql += " Cajas_Pendientes = " + (String.IsNullOrEmpty(Canti_Pendi.ToString()) ? "null, " : "'" + Canti_Pendi.ToString() + "', ");
                            Sql += " Cuentas_Pendientes = " + (String.IsNullOrEmpty(Deudas_Pendi.ToString()) ? "null " : "'" + Deudas_Pendi.ToString() + "' ");
                            Sql += " WHERE Nombre='" + Datos.Cliente + "'";

                            Obj_Comando.CommandText = Sql;
                            Obj_Comando.ExecuteNonQuery();
                        }

                        //EN ESTE UPDATE SOLO COLOCAREMOS CON ESTATUS DE CANCELADO A LAS CAJAS DE ESE FOLIO
                        Sql = " Update Ope_Historial_Cajas SET ";
                        Sql += " Estatus = " + (String.IsNullOrEmpty("Cancelado") ? "null " : "'" + "Cancelado" + "' ");
                        Sql += " WHERE Folio='" + Datos.Folio + "'";

                        Obj_Comando.CommandText = Sql;
                        Obj_Comando.ExecuteNonQuery();

                        //**SE TOMARÁ TODAS LAS CAJAS QUE SE AGREGARON CON LA VENTA, Y SE SUMARÁN EN EL CAT CAJAS
                        SqlDataAdapter Da_Datos_Cajas = new SqlDataAdapter(); //
                        DataTable Ds_Datos_Cajas = new DataTable();

                        Sql = "select * from Ope_Historial_Cajas ";
                        Sql += " WHERE Folio='" + Datos.Folio + "'";

                        Obj_Comando.CommandText = Sql;
                        Da_Datos_Cajas = new SqlDataAdapter(Obj_Comando);
                        Da_Datos_Cajas.Fill(Ds_Datos_Cajas);

                        if(Ds_Datos_Cajas.Rows.Count > 0)
                        {
                            for(i = 0; i< Ds_Datos_Cajas.Rows.Count;i++)
                            {
                                SqlDataAdapter Da_DatosCajas = new SqlDataAdapter(); //
                                DataTable Ds_DatosCajas = new DataTable();
                                Sql = "select * from Cat_Cajas ";
                                Sql += " WHERE Descripcion='" + Ds_Datos_Cajas.Rows[i]["Tipo_Caja"].ToString() + "'";
                                Obj_Comando.CommandText = Sql;
                                Da_DatosCajas = new SqlDataAdapter(Obj_Comando);
                                Da_DatosCajas.Fill(Ds_DatosCajas);

                                if(Ds_DatosCajas.Rows.Count > 0)
                                { 
                                var cajas = System.Convert.ToInt16(Ds_DatosCajas.Rows[i]["Cantidad"].ToString())+ System.Convert.ToInt16(Ds_Datos_Cajas.Rows[i]["Cantidad"].ToString());

                                Sql = " Update Cat_Cajas SET ";
                                Sql += " Cantidad = " + (String.IsNullOrEmpty(cajas.ToString()) ? "null " : "'" + cajas.ToString() + "' ");
                                Sql += " WHERE Descripcion='" + Ds_Datos_Cajas.Rows[i]["Tipo_Caja"].ToString() + "'";

                                Obj_Comando.CommandText = Sql;
                                Obj_Comando.ExecuteNonQuery();
                                }
                            }

                        }

                    }
                    }
                Obj_Transaccion.Commit();
                Obj_Conexion.Close();
                Transaccion = true;
            }
            catch (Exception Ex)
            {
                Transaccion = false;
                Obj_Transaccion.Rollback();
                throw new Exception(Ex.Message);
            }
            finally
            {
                if (!Transaccion)
                {
                    Obj_Conexion.Close();
                }
            }
            return Transaccion;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR A lOS CLIENTES REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Consultar_Cajas(Cls_Mdl_A_Pagar Negocio)
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "select SUM(Cantidad) as Cantidad from Ope_Historial_Cajas  ";
                Sql += " where Cliente='" + Negocio.Cliente + "'";
                Sql += " and Estatus='Pendiente' ";

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR A lOS CLIENTES REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Consultar_Tabla_Cajas(Cls_Mdl_A_Pagar Negocio)
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "select  ";
                Sql += " convert(varchar, Ope_Historial_Cajas.Fecha_Creo, 103) as Fecha_Creo, *";
                Sql += " from Ope_Historial_Cajas ";
                Sql += " where Estatus='Pendiente'";

                if (!String.IsNullOrEmpty(Negocio.Fecha_Inicio))
                {
                    if (Sql.Contains("where"))
                        Sql += " and Ope_Historial_Cajas.Fecha_Creo >= '" + Negocio.Fecha_Inicio + " 00:00:00'";
                    else
                        Sql += " where Ope_Historial_Cajas.Fecha_Creo >= '" + Negocio.Fecha_Inicio + " 00:00:00'";
                }

                if (!String.IsNullOrEmpty(Negocio.Fecha_Fin))
                {
                    if (Sql.Contains("where"))
                        Sql += " and Ope_Historial_Cajas.Fecha_Creo <= '" + Negocio.Fecha_Fin + " 23:59:59'";
                    else
                        Sql += " where Ope_Historial_Cajas.Fecha_Creo <= '" + Negocio.Fecha_Fin + " 23:59:59'";
                }
                if (!String.IsNullOrEmpty(Negocio.Folio))
                {
                    if (Sql.Contains("where"))
                        Sql += " and Ope_Historial_Cajas.Folio = '" + Negocio.Folio + "'";
                    else
                        Sql += " where Ope_Historial_Cajas.Folio = '" + Negocio.Folio + "'";
                }
                if (!String.IsNullOrEmpty(Negocio.Cliente))
                {
                    if (Sql.Contains("where"))
                        Sql += " and Ope_Historial_Cajas.Cliente = '" + Negocio.Cliente + "'";
                    else
                        Sql += " where Ope_Historial_Cajas.Cliente = '" + Negocio.Cliente + "'";
                }
                Sql += " order by Ope_Historial_Cajas.Fecha_Creo";

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR A lOS CLIENTES REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Consultar_Deposito(Cls_Mdl_A_Pagar Negocio)
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "select * from Cat_Clientes  ";
                Sql += " where Nombre='" + Negocio.Cliente + "'";

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR A lOS CLIENTES REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Cajas_Pendientes(Cls_Mdl_A_Pagar Negocio)
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "select * from Ope_Historial_Cajas  ";
                Sql += " where Cliente ='" + Negocio.Cliente + "'";

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR A lOS CLIENTES REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public Boolean Recepcion_Cajas(Cls_Mdl_A_Pagar Datos)
        {
            Boolean Transaccion = false;
            SqlTransaction Obj_Transaccion;
            SqlConnection Obj_Conexion = new SqlConnection(ConexionBD.BD);
            SqlCommand Obj_Comando = new SqlCommand();
            String Sql = String.Empty;
            String Estatus = String.Empty;

            Obj_Conexion.Open();
            Obj_Transaccion = Obj_Conexion.BeginTransaction();
            Obj_Comando.Transaction = Obj_Transaccion;
            Obj_Comando.Connection = Obj_Conexion;

            DataTable Dt_Registro = new DataTable();
            try
            {
                SqlDataAdapter Da_Datos_Historial_Cajas = new SqlDataAdapter(); //
                DataTable Ds_DatosHistorial_Cajas = new DataTable();


                Sql = "SELECT * FROM Ope_Historial_Cajas ";
                Sql += " WHERE Cliente='" + Datos.Cliente + "'";

                Obj_Comando.CommandText = Sql;
                Da_Datos_Historial_Cajas = new SqlDataAdapter(Obj_Comando);
                Da_Datos_Historial_Cajas.Fill(Ds_DatosHistorial_Cajas);


                //*********************************SE SUMAN LAS CAJAS RECIBIDAS, PARA AUMENTAR EL INVENTARIO EN  CAT CAJAS************

                SqlDataAdapter Da_Datos = new SqlDataAdapter(); //
                DataTable Ds_Datos = new DataTable();


                Sql = "SELECT * FROM Cat_Cajas ";
                Sql += " WHERE Descripcion='" + Datos.Descripcion + "'";

                Obj_Comando.CommandText = Sql;
                Da_Datos = new SqlDataAdapter(Obj_Comando);
                Da_Datos.Fill(Ds_Datos);

                var Cantidad = 0;
                if (Ds_Datos.Rows.Count > 0)
                {
                    Cantidad = System.Convert.ToInt16(Ds_Datos.Rows[0]["Cantidad"].ToString()) + System.Convert.ToInt16(Datos.Cantidad);

                    Sql = " Update Cat_Cajas SET ";
                    Sql += " Cantidad = " + (String.IsNullOrEmpty(Cantidad.ToString()) ? "null " : "'" + Cantidad.ToString() + "' ");
                    Sql += " WHERE Descripcion = '" + Datos.Descripcion + "'";

                    Obj_Comando.CommandText = Sql;
                    Obj_Comando.ExecuteNonQuery();
                }

                //*********************************************************************************************************************

                //***********************************SE RESTAN LAS CAJAS DEL CATALOGO DE CLIENTES************************************** 

                SqlDataAdapter Da_Datos_Cliente = new SqlDataAdapter(); //
                DataTable Ds_Datos_Cliente = new DataTable();


                Sql = "SELECT * FROM Cat_Clientes ";
                Sql += " WHERE Nombre='" + Datos.Cliente + "'";

                Obj_Comando.CommandText = Sql;
                Da_Datos_Cliente = new SqlDataAdapter(Obj_Comando);
                Da_Datos_Cliente.Fill(Ds_Datos_Cliente);

                Decimal Canti = 0;
                if (Ds_Datos_Cliente.Rows.Count > 0)
                {
                    Canti = System.Convert.ToDecimal(Ds_Datos_Cliente.Rows[0]["Cajas_Pendientes"].ToString()) - System.Convert.ToDecimal(Datos.Cantidad);
                    var depo = System.Convert.ToDecimal(Ds_Datos_Cliente.Rows[0]["Importe_Cajas"].ToString()) - System.Convert.ToDecimal(Datos.Importe);

                    Sql = " Update Cat_Clientes SET ";
                    Sql += " Importe_Cajas = " + (String.IsNullOrEmpty(depo.ToString()) ? "null, " : "'" + depo.ToString() + "', ");
                    Sql += " Cajas_Pendientes = " + (String.IsNullOrEmpty(Canti.ToString()) ? "null " : "'" + Canti.ToString() + "' ");
                    Sql += " WHERE Nombre = '" + Datos.Cliente + "'";

                    Obj_Comando.CommandText = Sql;
                    Obj_Comando.ExecuteNonQuery();
                }

                //***********************************SE RESTAN LAS CAJAS DEL HISTORIAL DE CAJAS************************************** 

                var Cantid = System.Convert.ToDecimal(Datos.Pagado) - System.Convert.ToDecimal(Datos.Cantidad);

                Sql = " Update Ope_Historial_Cajas SET ";
                Sql += " Estatus = " + (String.IsNullOrEmpty(Datos.Estatus) ? "null, " : "'" + Datos.Estatus + "', ");
                Sql += " Cantidad = " + (String.IsNullOrEmpty(Cantid.ToString()) ? "null " : "'" + Cantid.ToString() + "' ");
                Sql += " WHERE Caja_ID = '" + Datos.Caja_ID + "'";

                Obj_Comando.CommandText = Sql;
                Obj_Comando.ExecuteNonQuery();
                //*********************************************************************************************************************
                //***********************************SE RESTA EL IMPORTE ENTREGADO DE LA TABLA VENTAS************************************** 
                //SqlDataAdapter Da_Datos_importe = new SqlDataAdapter(); //
                //DataTable Ds_Datos_importe = new DataTable();

                //Sql = "SELECT * FROM Ope_Ventas ";
                //Sql += " WHERE Folio='" + Datos.Folio + "'";

                //Obj_Comando.CommandText = Sql;
                //Da_Datos_importe = new SqlDataAdapter(Obj_Comando);
                //Da_Datos_importe.Fill(Ds_Datos_importe);

                //var Importe = System.Convert.ToDecimal(Ds_Datos_importe.Rows[0]["Importe_Cajas"].ToString()) - System.Convert.ToDecimal(Datos.Importe);

                //Sql = " Update Ope_Ventas SET ";
                //Sql += " Importe_Cajas = " + (String.IsNullOrEmpty(Importe.ToString()) ? "null " : "'" + Importe.ToString() + "' ");
                //Sql += " WHERE Folio = '" + Datos.Folio + "'";

                //Obj_Comando.CommandText = Sql;
                //Obj_Comando.ExecuteNonQuery();

                //*************************SE COLOCAN LAS CAJAS RECIBIDAS EN EL HISTORIAL*****************************************
                Sql = "INSERT INTO  Ope_Mostrar_Entrega_Cajas (";
                //Sql += " Folio, ";
                Sql += " Cliente, ";
                Sql += " Tipo_Caja, ";
                Sql += " Cantidad, ";
                Sql += " Regreso_De_Deposito, ";
                Sql += " Usuario_Creo ";
                Sql += ") VALUES (";
                //Sql += (String.IsNullOrEmpty(Datos.Folio.ToString()) ? "null, " : "'" + Datos.Folio.ToString() + "', ");
                Sql += (String.IsNullOrEmpty(Datos.Cliente) ? "null, " : "'" + Datos.Cliente + "', ");
                Sql += (String.IsNullOrEmpty(Datos.Descripcion) ? "null, " : "'" + Datos.Descripcion + "', ");
                Sql += (String.IsNullOrEmpty(Datos.Cantidad) ? "null, " : "'" + Datos.Cantidad + "', ");
                Sql += (String.IsNullOrEmpty(Datos.Importe) ? "null, " : "'" + Datos.Importe + "', ");
                Sql += (String.IsNullOrEmpty(Datos.Usuario_Registro) ? "null " : "'" + Datos.Usuario_Registro + "' ");
                Sql += ")";

                Obj_Comando.CommandText = Sql;
                Obj_Comando.ExecuteNonQuery();


                Obj_Transaccion.Commit();
                Obj_Conexion.Close();
                Transaccion = true;
            }
            catch (Exception Ex)
            {
                Transaccion = false;
                Obj_Transaccion.Rollback();
                throw new Exception(Ex.Message);
            }
            finally
            {
                if (!Transaccion)
                {
                    Obj_Conexion.Close();
                }
            }
            return Transaccion;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR A lOS CLIENTES REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Historial_Abonos(Cls_Mdl_A_Pagar Negocio)
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "select ";
                Sql += " convert(varchar, Ope_Historial_Abonos.Fecha_Creo, 103) as Fecha_Creo, *";
                Sql += " from Ope_Historial_Abonos";
                Sql += " where Cantidad > 0";

                if (!String.IsNullOrEmpty(Negocio.Fecha_Inicio))
                {
                    if (Sql.Contains("where"))
                        Sql += " and Ope_Historial_Abonos.Fecha_Creo >= '" + Negocio.Fecha_Inicio + " 00:00:00'";
                    else
                        Sql += " where Ope_Historial_Abonos.Fecha_Creo >= '" + Negocio.Fecha_Inicio + " 00:00:00'";
                }

                if (!String.IsNullOrEmpty(Negocio.Fecha_Fin))
                {
                    if (Sql.Contains("where"))
                        Sql += " and Ope_Historial_Abonos.Fecha_Creo <= '" + Negocio.Fecha_Fin + " 23:59:59'";
                    else
                        Sql += " where Ope_Historial_Abonos.Fecha_Creo <= '" + Negocio.Fecha_Fin + " 23:59:59'";
                }
                if (!String.IsNullOrEmpty(Negocio.Folio))
                {
                    if (Sql.Contains("where"))
                        Sql += " and Ope_Historial_Abonos.Folio = '" + Negocio.Folio + "'";
                    else
                        Sql += " where Ope_Historial_Abonos.Folio = '" + Negocio.Folio + "'";
                }
                if (!String.IsNullOrEmpty(Negocio.Cliente))
                {
                    if (Sql.Contains("where"))
                        Sql += " and Ope_Historial_Abonos.Cliente = '" + Negocio.Cliente + "'";
                    else
                        Sql += " where Ope_Historial_Abonos.Cliente = '" + Negocio.Cliente + "'";
                }
                Sql += " order by Ope_Historial_Abonos.Fecha_Creo desc";

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR A lOS CLIENTES REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Ventas_Fechas(Cls_Mdl_A_Pagar Negocio)
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "select Ope_Ventas.Venta_ID, Descripcion, Cantidad, Importe, Cliente, Estatus, Folio, Factura, Ope_Ventas.Fecha_Creo";
                //Sql += " convert(varchar, Ope_Ventas.Fecha_Creo, 103) as Fecha_Creo ";
                Sql += " from Ope_Ventas_Detalles inner join Ope_Ventas on Ope_Ventas_Detalles.Venta_ID=Ope_Ventas.Venta_ID ";

                if (!String.IsNullOrEmpty(Negocio.Fecha_Inicio))
                {
                    if (Sql.Contains("where"))
                        Sql += " and Ope_Ventas.Fecha_Creo >= '" + Negocio.Fecha_Inicio + " 00:00:00'";
                    else
                        Sql += " where Ope_Ventas.Fecha_Creo >= '" + Negocio.Fecha_Inicio + " 00:00:00'";
                }

                if (!String.IsNullOrEmpty(Negocio.Fecha_Fin))
                {
                    if (Sql.Contains("where"))
                        Sql += " and Ope_Ventas.Fecha_Creo <= '" + Negocio.Fecha_Fin + " 23:59:59'";
                    else
                        Sql += " where Ope_Ventas.Fecha_Creo <= '" + Negocio.Fecha_Fin + " 23:59:59'";
                }
                
                Sql += " order by Ope_Ventas.Fecha_Creo desc";

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR A lOS CLIENTES REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Abonos_Fechas(Cls_Mdl_A_Pagar Negocio)
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "select SUM(Cantidad) as Cantidad from Ope_Historial_Abonos ";

                if (!String.IsNullOrEmpty(Negocio.Fecha_Inicio))
                {
                    if (Sql.Contains("where"))
                        Sql += " and Ope_Historial_Abonos.Fecha_Creo >= '" + Negocio.Fecha_Inicio + " 00:00:00'";
                    else
                        Sql += " where Ope_Historial_Abonos.Fecha_Creo >= '" + Negocio.Fecha_Inicio + " 00:00:00'";
                }

                if (!String.IsNullOrEmpty(Negocio.Fecha_Fin))
                {
                    if (Sql.Contains("where"))
                        Sql += " and Ope_Historial_Abonos.Fecha_Creo <= '" + Negocio.Fecha_Fin + " 23:59:59'";
                    else
                        Sql += " where Ope_Historial_Abonos.Fecha_Creo <= '" + Negocio.Fecha_Fin + " 23:59:59'";
                }

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Cajas
        ///DESCRIPCIÓN: CONSULTAR A LAS CAJAS REGISTRADAS
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Consulta_Cajas(Cls_Mdl_Cajas Dato)
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "SELECT * FROM Cat_Cajas";

                if (!String.IsNullOrEmpty(Dato.Descripcion))
                    Sql += " WHERE Descripcion LIKE '%" + Dato.Descripcion + "%'";

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Proveedores
        ///DESCRIPCIÓN: CONSULTAR A lOS PROVEEDOR REGISTRADOR
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Consulta_Proveedores(Cls_Mdl_Proveedor Dato)
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "SELECT * FROM Cat_Proveedores";
                if (!String.IsNullOrEmpty(Dato.Nombre))
                    Sql += " WHERE Nombre LIKE '%" + Dato.Nombre + "%'";
                if (!String.IsNullOrEmpty(Dato.Tipo_Proveedor))
                {
                    if (Sql.Contains("WHERE"))
                        Sql += " AND Tipo = '" + Dato.Tipo_Proveedor + "'";
                    else
                        Sql += " WHERE Tipo = '" + Dato.Tipo_Proveedor + "'";
                }
                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR A lOS CLIENTES REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public Boolean Guardar_Entrada_Cajas(Cls_Mdl_A_Pagar Datos)
        {
            Boolean Transaccion = false;
            SqlTransaction Obj_Transaccion;
            SqlConnection Obj_Conexion = new SqlConnection(ConexionBD.BD);
            SqlCommand Obj_Comando = new SqlCommand();
            String Sql = String.Empty;
            String Estatus = String.Empty;

            Obj_Conexion.Open();
            Obj_Transaccion = Obj_Conexion.BeginTransaction();
            Obj_Comando.Transaction = Obj_Transaccion;
            Obj_Comando.Connection = Obj_Conexion;

            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "INSERT INTO  Ope_Historial_Compra_Cajas (";
                Sql += " Proveedor, ";
                Sql += " Tipo_Caja, ";
                Sql += " Cantidad, ";
                Sql += " Costo, ";
                Sql += " Usuario_Creo ";
                Sql += ") VALUES (";
                Sql += (String.IsNullOrEmpty(Datos.Proveedor.ToString()) ? "null, " : "'" + Datos.Proveedor.ToString() + "', ");
                Sql += (String.IsNullOrEmpty(Datos.Cajas) ? "null, " : "'" + Datos.Cajas + "', ");
                Sql += (String.IsNullOrEmpty(Datos.Cantidad) ? "null, " : "'" + Datos.Cantidad + "', ");
                Sql += (String.IsNullOrEmpty(Datos.Costo) ? "null, " : "'" + Datos.Costo + "', ");
                Sql += (String.IsNullOrEmpty(Datos.Usuario_Registro) ? "null " : "'" + Datos.Usuario_Registro + "' ");
                Sql += ")";

                Obj_Comando.CommandText = Sql;
                Obj_Comando.ExecuteNonQuery();

                //PARTE DE ACTUALIZAR LA CANTIDAD DEL CATALOGO 
                SqlDataAdapter Da_Datos = new SqlDataAdapter(); //
                DataTable Ds_Datos = new DataTable();

                Sql = "SELECT * FROM Cat_Cajas ";
                Sql += " WHERE Descripcion='" + Datos.Cajas + "'";

                Obj_Comando.CommandText = Sql;
                Da_Datos = new SqlDataAdapter(Obj_Comando);
                Da_Datos.Fill(Ds_Datos);

                var cajas = System.Convert.ToInt16(Ds_Datos.Rows[0]["Cantidad"].ToString()) + System.Convert.ToInt16(Datos.Cantidad.ToString());

                Sql = " Update Cat_Cajas SET ";
                Sql += " Cantidad = " + (String.IsNullOrEmpty(cajas.ToString()) ? "null " : "'" + cajas.ToString() + "' ");
                Sql += " WHERE Descripcion='" + Datos.Cajas + "'";

                Obj_Comando.CommandText = Sql;
                Obj_Comando.ExecuteNonQuery();

             
                Obj_Transaccion.Commit();
                Obj_Conexion.Close();
                Transaccion = true;
            }
            catch (Exception Ex)
            {
                Transaccion = false;
                Obj_Transaccion.Rollback();
                throw new Exception(Ex.Message);
            }
            finally
            {
                if (!Transaccion)
                {
                    Obj_Conexion.Close();
                }
            }
            return Transaccion;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR A lOS CLIENTES REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Historial_Cajas()
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "select ";
                Sql += " convert(varchar, Ope_Historial_Compra_Cajas.Fecha_Creo, 103) as Fecha_Creo, *";
                Sql += " from Ope_Historial_Compra_Cajas";

                Sql += " order by Ope_Historial_Compra_Cajas.Fecha_Creo desc";

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR A lOS CLIENTES REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Historial_Cajas_Entregadas(Cls_Mdl_A_Pagar Negocio)
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "select * from Ope_Mostrar_Entrega_Cajas ";

                if (!String.IsNullOrEmpty(Negocio.Fecha_Inicio))
                {
                    if (Sql.Contains("where"))
                        Sql += " and Ope_Mostrar_Entrega_Cajas.Fecha_Creo >= '" + Negocio.Fecha_Inicio + " 00:00:00'";
                    else
                        Sql += " where Ope_Mostrar_Entrega_Cajas.Fecha_Creo >= '" + Negocio.Fecha_Inicio + " 00:00:00'";
                }

                if (!String.IsNullOrEmpty(Negocio.Fecha_Fin))
                {
                    if (Sql.Contains("where"))
                        Sql += " and Ope_Mostrar_Entrega_Cajas.Fecha_Creo <= '" + Negocio.Fecha_Fin + " 23:59:59'";
                    else
                        Sql += " where Ope_Mostrar_Entrega_Cajas.Fecha_Creo <= '" + Negocio.Fecha_Fin + " 23:59:59'";
                }

                if (!String.IsNullOrEmpty(Negocio.Folio))
                {
                    if (Sql.Contains("where"))
                        Sql += " and Ope_Mostrar_Entrega_Cajas.Folio = '" + Negocio.Folio + "'";
                    else
                        Sql += " where Ope_Mostrar_Entrega_Cajas.Folio = '" + Negocio.Folio + "'";
                }
                if (!String.IsNullOrEmpty(Negocio.Cliente))
                {
                    if (Sql.Contains("where"))
                        Sql += " and Ope_Mostrar_Entrega_Cajas.Cliente = '" + Negocio.Cliente + "'";
                    else
                        Sql += " where Ope_Mostrar_Entrega_Cajas.Cliente = '" + Negocio.Cliente + "'";
                }
                Sql += " order by Ope_Mostrar_Entrega_Cajas.Fecha_Creo desc";

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Usuario
        ///DESCRIPCIÓN: CONSULTAR A lOS CLIENTES REGISTRADOS
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public Boolean Pagar_Cajas_Bodegas(Cls_Mdl_A_Pagar Datos)
        {
            Boolean Transaccion = false;
            SqlTransaction Obj_Transaccion;
            SqlConnection Obj_Conexion = new SqlConnection(ConexionBD.BD);
            SqlCommand Obj_Comando = new SqlCommand();
            String Sql = String.Empty;
            String Estatus = String.Empty;

            Obj_Conexion.Open();
            Obj_Transaccion = Obj_Conexion.BeginTransaction();
            Obj_Comando.Transaction = Obj_Transaccion;
            Obj_Comando.Connection = Obj_Conexion;

            DataTable Dt_Registro = new DataTable();
            try
            {
              
                    Sql = "INSERT INTO  Ope_Historial_Abonos (";
                    Sql += " Folio, ";
                    Sql += " Cliente, ";
                    Sql += " Cantidad, ";
                    Sql += " Usuario_Creo ";
                    Sql += ") VALUES (";
                    Sql += (String.IsNullOrEmpty("0") ? "null, " : "'" + "0" + "', ");
                    Sql += (String.IsNullOrEmpty(Datos.Cliente) ? "null, " : "'" + Datos.Cliente + "', ");
                    Sql += (String.IsNullOrEmpty(Datos.Importe) ? "null, " : "'" + Datos.Importe + "', ");
                    Sql += (String.IsNullOrEmpty(Datos.Usuario_Registro) ? "null " : "'" + Datos.Usuario_Registro + "' ");
                    Sql += ")";

                    Obj_Comando.CommandText = Sql;
                    Obj_Comando.ExecuteNonQuery();

                    //PARTE DE ACTUALIZAR LOS DE CUENTAS PENDIENTES 

                    Sql = " Update Ope_Traspaso_Bodega SET ";
                    Sql += " Cantidad_Cajas = " + (String.IsNullOrEmpty(Datos.Cantidad.ToString()) ? "null, " : "'" + Datos.Cantidad.ToString() + "', ");
                    Sql += " Estatus = " + (String.IsNullOrEmpty(Datos.Estatus.ToString()) ? "null " : "'" + Datos.Estatus.ToString() + "' ");
                    Sql += " WHERE Bodega_ID = " + Datos.Bodega_ID.ToString();

                    Obj_Comando.CommandText = Sql;
                    Obj_Comando.ExecuteNonQuery();

                Obj_Transaccion.Commit();
                Obj_Conexion.Close();
                Transaccion = true;
            }
            catch (Exception Ex)
            {
                Transaccion = false;
                Obj_Transaccion.Rollback();
                throw new Exception(Ex.Message);
            }
            finally
            {
                if (!Transaccion)
                {
                    Obj_Conexion.Close();
                }
            }
            return Transaccion;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: Consulta_Cajas
        ///DESCRIPCIÓN: CONSULTAR A LAS CAJAS REGISTRADAS
        ///PARAMETROS:  
        ///CREO:       FRANCISCO JAVIER BECERRA TOLEDO
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        public DataTable Consultar_Tabla_Bodegas(Cls_Mdl_A_Pagar Dato)
        {
            String Sql = String.Empty;
            DataTable Dt_Registro = new DataTable();
            try
            {
                Sql = "SELECT ";
                Sql += " convert(varchar, Ope_Traspaso_Bodega.Fecha_Creo, 103) as Fecha_Creo, *";
                Sql += " FROM Ope_Traspaso_Bodega";
                Sql += " WHERE Cantidad_Cajas > 0";

                if (!String.IsNullOrEmpty(Dato.Fecha_Inicio))
                {
                    if (Sql.Contains("where"))
                        Sql += " and Ope_Traspaso_Bodega.Fecha_Creo >= '" + Dato.Fecha_Inicio + " 00:00:00'";
                    else
                        Sql += " where Ope_Traspaso_Bodega.Fecha_Creo >= '" + Dato.Fecha_Inicio + " 00:00:00'";
                }

                if (!String.IsNullOrEmpty(Dato.Fecha_Fin))
                {
                    if (Sql.Contains("where"))
                        Sql += " and Ope_Traspaso_Bodega.Fecha_Creo <= '" + Dato.Fecha_Fin + " 23:59:59'";
                    else
                        Sql += " where Ope_Traspaso_Bodega.Fecha_Creo <= '" + Dato.Fecha_Fin + " 23:59:59'";
                }
               
                if (!String.IsNullOrEmpty(Dato.Cliente))
                {
                    if (Sql.Contains("where"))
                        Sql += " and Ope_Traspaso_Bodega.Cliente = '" + Dato.Cliente + "'";
                    else
                        Sql += " where Ope_Traspaso_Bodega.Cliente = '" + Dato.Cliente + "'";
                }

                Dt_Registro = SqlHelper.ExecuteDataset(ConexionBD.BD, CommandType.Text, Sql).Tables[0];
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return Dt_Registro;
        }
    }
}