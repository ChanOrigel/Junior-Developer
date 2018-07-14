using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JPV_Portal.CORE;

namespace JPV_Portal.Modelo.Negocio
{
    public class Cls_Mdl_Ventas : TablaDB
    {
        public String Venta_ID { get; set; }
        public String Entrada_ID { get; set; }
        public String Caja_ID { get; set; }
        public String Importe_Cajas { get; set; }
        public String Producto { get; set; }
        public String Proveedor_Producto { get; set; }
        public String Fecha { get; set; }
        public String Cliente { get; set; }
        public String Total_Vendido { get; set; }
        public String Estatus { get; set; }
        public String Folio { get; set; }
        public String Venta_Detalle_ID { get; set; }
        public String Descripcion { get; set; }
        public String Cantidad { get; set; }
        public String Costo_Unitario { get; set; }
        public String Importe { get; set; }
        public String Dia { get; set; }
        public String Factura { get; set; }

        public String Cajas_Prestadas { get; set; }
        public String Cajas_Descripcion { get; set; }
        public String Cajas_Cantidad { get; set; }

        public String Usuario_Registro { get; set; }
        public String Fecha_Registro { get; set; }
        public String Usuario_Modifico { get; set; }
        public String Fecha_Modifico { get; set; }

        public MODO_DE_CAPTURA Modo_Captura { get; set; }

        public Cls_Mdl_Ventas() { }

        public Cls_Mdl_Ventas(String Venta_ID = "", String Cliente = "", String Total_Vendido = "",
            String Estatus = "", String Folio = "", String Factura = "", String Venta_Detalle_ID = "", String Descripcion = "", String Cantidad = "",
            String Costo_Unitario = "", String Importe = "", String Usuario_Modifico = "",
            String Fecha_Modifico = "", String Usuario_Registro = "", String Fecha_Registro = "")
        {
            this.Venta_ID = Venta_ID;
            this.Cliente = Cliente;
            this.Total_Vendido = Total_Vendido;
            this.Estatus = Estatus;
            this.Folio = Folio;
            this.Venta_Detalle_ID = Venta_Detalle_ID;
            this.Descripcion = Descripcion;
            this.Cantidad = Cantidad;
            this.Costo_Unitario = Costo_Unitario;
            this.Importe = Importe;
            this.Factura = Factura;

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
            if (!String.IsNullOrEmpty(Venta_ID))
                parametrosBD.Add(new ParametroBD("Venta_ID", Venta_ID));
            if (!String.IsNullOrEmpty(Cliente))
                parametrosBD.Add(new ParametroBD("Cliente", Cliente));
            parametrosBD.Add(new ParametroBD("Total_Vendido", Total_Vendido));
            parametrosBD.Add(new ParametroBD("Estatus", Estatus));
            parametrosBD.Add(new ParametroBD("Folio", Folio));
            if (!String.IsNullOrEmpty(Factura))
                parametrosBD.Add(new ParametroBD("Factura", Factura));

            //if (Modo_Captura.Equals(MODO_DE_CAPTURA.CAPTURA_ALTA))
            if (!String.IsNullOrEmpty(Usuario_Registro))
                parametrosBD.Add(new ParametroBD("Usuario_Creo", Usuario_Registro));
            else
            {
                parametrosBD.Add(new ParametroBD("Usuario_Modifico", Usuario_Modifico));
                //parametrosBD.Add(new ParametroBD("Fecha_Modifico", Fecha_Modifico));
            }
            //Retornamos la lista.
            return parametrosBD;
        }
    }
}