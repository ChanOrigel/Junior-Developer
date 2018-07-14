using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JPV_Portal.CORE;

namespace JPV_Portal.Modelo.Negocio
{
    public class Cls_Mdl_Salida_Mercancia : TablaDB
    {
        public String Entrada_ID { get; set; }
        public String Cliente_ID { get; set; }
        public String Cliente { get; set; }

        public String Proveedor_Producto { get; set; }
        public String Proveedor { get; set; }
        public String Producto { get; set; }
        public String Estatus { get; set; }
        public String Cantidad { get; set; }
        public String Chofer { get; set; }

        public String Total { get; set; }
        public String Costo_Unitario { get; set; }
        public String Importe { get; set; }

        public String Usuario_Registro { get; set; }
        public String Fecha_Registro { get; set; }
        public String Usuario_Modifico { get; set; }
        public String Fecha_Modifico { get; set; }

        public MODO_DE_CAPTURA Modo_Captura { get; set; }

        public Cls_Mdl_Salida_Mercancia() { }

        public Cls_Mdl_Salida_Mercancia(String Entrada_ID = "", String Cliente_ID = "", String Cliente = "",
            String Proveedor_Producto = "", String Producto = "", String Cantidad = "", String Total = "", String Estatus = "",
            String Costo_Unitario = "", String Importe = "",  String Usuario_Modifico = "",
            String Fecha_Modifico = "", String Usuario_Registro = "", String Fecha_Registro = "")
        {
            this.Entrada_ID = Entrada_ID;
            this.Cliente_ID = Cliente_ID;
            this.Cliente = Cliente;
            this.Proveedor_Producto = Proveedor_Producto;
            this.Producto = Producto;
            this.Cantidad = Cantidad;
            this.Total = Total;
            this.Estatus = Estatus;
            this.Costo_Unitario = Costo_Unitario;
            this.Importe = Importe;

            this.Usuario_Modifico = Usuario_Modifico;
            this.Fecha_Modifico = Fecha_Modifico;
            this.Usuario_Registro = Usuario_Registro;
            this.Fecha_Registro = Fecha_Registro;
        }

        //********************************// IMPLEMENTACIONES DE LA INTERFAZ  //********************************// 

        /// <summary>
        /// Obtiene el nombre de la tabla.
        /// </summary>
        public String NombreTabla { get { return "Ope_Ventas"; } }
        /// <summary>
        /// Obtiene el id de la tabla.
        /// </summary>
        public String IDTabla { get { return "Venta_ID"; } }
        /// <summary>
        /// Obtiene el valor por el que se ordenaran los datos
        /// </summary>
        public String Orderby { get { return "Venta_ID"; } }
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

            if (!String.IsNullOrEmpty(Cliente_ID))
                parametrosBD.Add(new ParametroBD("Cliente_ID", Cliente_ID));

            parametrosBD.Add(new ParametroBD("Proveedor_Producto", Proveedor_Producto));
            parametrosBD.Add(new ParametroBD("Total", Total));
            parametrosBD.Add(new ParametroBD("Estatus", Estatus));
            if (!String.IsNullOrEmpty(Importe))
                parametrosBD.Add(new ParametroBD("Importe", Importe));

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