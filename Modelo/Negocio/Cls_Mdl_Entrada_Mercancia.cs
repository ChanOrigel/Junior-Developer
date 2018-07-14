using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JPV_Portal.CORE;


namespace JPV_Portal.Modelo.Negocio
{
    public class Cls_Mdl_Entrada_Mercancia : TablaDB
    {
        public String Entrada_ID { get; set; }
        public String Proveedor_ID { get; set; }
        public String Proveedor { get; set; }
        public String Producto { get; set; }
        public String Proveedor_Producto { get; set; }
        public String Toneladas { get; set; }
        public String Cajas { get; set; }
        public String Notas { get; set; }
        public String Estatus { get; set; }
        public String Cantidad_Descontar { get; set; }
        public String Chofer { get; set; }

        public String Fecha_Inicio { get; set; }
        public String Fecha_Fin { get; set; }
        public String Catalogo_Agregar_Quitar { get; set; }

        public String Usuario_Registro { get; set; }
        public String Fecha_Registro { get; set; }
        public String Usuario_Modifico { get; set; }
        public String Fecha_Modifico { get; set; }

        public MODO_DE_CAPTURA Modo_Captura { get; set; }

        public Cls_Mdl_Entrada_Mercancia() { }

        public Cls_Mdl_Entrada_Mercancia(String Proveedor_ID = "", String Proveedor = "", String Chofer = "", String Producto = "",
            String Proveedor_Producto = "", String Toneladas = "", String Cajas = "", String Notas = "", String Estatus = "",
            String Cantidad_Descontar = "",  String Usuario_Modifico = "",
            String Fecha_Modifico = "", String Usuario_Registro = "", String Fecha_Registro = "")
        {
            this.Proveedor_ID = Proveedor_ID;
            this.Proveedor = Proveedor;
            this.Producto = Producto;
            this.Proveedor_Producto = Proveedor_Producto;
            this.Toneladas = Toneladas;
            this.Cajas = Cajas;
            this.Notas = Notas;
            this.Estatus = Estatus;
            this.Cantidad_Descontar = Cantidad_Descontar;
            this.Chofer = Chofer;

            this.Usuario_Modifico = Usuario_Modifico;
            this.Fecha_Modifico = Fecha_Modifico;
            this.Usuario_Registro = Usuario_Registro;
            this.Fecha_Registro = Fecha_Registro;
        }

        //********************************// IMPLEMENTACIONES DE LA INTERFAZ  //********************************// 

        /// <summary>
        /// Obtiene el nombre de la tabla.
        /// </summary>
        public String NombreTabla { get { return "Ope_Entrada_Mercancia"; } }
        /// <summary>
        /// Obtiene el id de la tabla.
        /// </summary>
        public String IDTabla { get { return "Entrada_ID"; } }
        /// <summary>
        /// Obtiene el valor por el que se ordenaran los datos
        /// </summary>
        public String Orderby { get { return "Entrada_ID"; } }
        /// <summary>
        /// Obtiene los parametros de operacion en la BD.
        /// </summary>
        /// <returns>Lista de Parámetros para operaciones en la BD.</returns>
        public List<ParametroBD> ObtenParametros()
        {
            //Creamos los parametros de BD.
            List<ParametroBD> parametrosBD = new List<ParametroBD>();
            if (!String.IsNullOrEmpty(Entrada_ID))
                parametrosBD.Add(new ParametroBD("Entrada_ID", Entrada_ID));
            if (!String.IsNullOrEmpty(Proveedor_ID))
                parametrosBD.Add(new ParametroBD("Proveedor_ID", Proveedor_ID));
            parametrosBD.Add(new ParametroBD("Proveedor", Proveedor));
            parametrosBD.Add(new ParametroBD("Producto", Producto));
            parametrosBD.Add(new ParametroBD("Proveedor_Producto", Proveedor_Producto));
            if (!String.IsNullOrEmpty(Toneladas))
                parametrosBD.Add(new ParametroBD("Toneladas", Toneladas));
            parametrosBD.Add(new ParametroBD("Cajas", Cajas));
            if (!String.IsNullOrEmpty(Notas))
                parametrosBD.Add(new ParametroBD("Notas", Notas));
            if (!String.IsNullOrEmpty(Estatus))
                parametrosBD.Add(new ParametroBD("Estatus", Estatus));

            if (!String.IsNullOrEmpty(Cantidad_Descontar))
                parametrosBD.Add(new ParametroBD("Cantidad_Descontar", Cantidad_Descontar));

            if (!String.IsNullOrEmpty(Chofer))
                parametrosBD.Add(new ParametroBD("Chofer", Chofer));

            if (!String.IsNullOrEmpty(Fecha_Inicio))
                parametrosBD.Add(new ParametroBD("Fecha_Creo", Fecha_Inicio));
            if (!String.IsNullOrEmpty(Fecha_Fin))
                parametrosBD.Add(new ParametroBD("Fecha_Creo", Fecha_Fin));
           

            //if (Modo_Captura.Equals(MODO_DE_CAPTURA.CAPTURA_ALTA))
            if (!String.IsNullOrEmpty(Usuario_Registro))
                parametrosBD.Add(new ParametroBD("Usuario_Creo", Usuario_Registro));
            else if (!String.IsNullOrEmpty(Usuario_Modifico))

            {
                parametrosBD.Add(new ParametroBD("Usuario_Modifico", Usuario_Modifico));
                //parametrosBD.Add(new ParametroBD("Fecha_Modifico", Fecha_Modifico));
            }
            //Retornamos la lista.
            return parametrosBD;
        }
    }
}