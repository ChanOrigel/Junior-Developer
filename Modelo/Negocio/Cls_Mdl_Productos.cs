using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JPV_Portal.CORE;

namespace JPV_Portal.Modelo.Negocio
{
    public class Cls_Mdl_Productos : TablaDB
    {
        public String Producto_ID { get; set; }
        public String Descripcion { get; set; }
        public String Cajas_Stock { get; set; }

        public String Usuario_Registro { get; set; }
        public String Fecha_Registro { get; set; }
        public String Usuario_Modifico { get; set; }
        public String Fecha_Modifico { get; set; }

        public MODO_DE_CAPTURA Modo_Captura { get; set; } 

        public Cls_Mdl_Productos() { } 

        public Cls_Mdl_Productos(String Producto_ID = "", String Descripcion = "", String Cajas_Stock = "", String Usuario_Modifico = "",
            String Fecha_Modifico = "", String Usuario_Registro = "", String Fecha_Registro = "")
        {
            this.Producto_ID = Producto_ID;
            this.Descripcion = Descripcion;
            this.Cajas_Stock = Cajas_Stock;
            this.Usuario_Modifico = Usuario_Modifico;
            this.Fecha_Modifico = Fecha_Modifico;
            this.Usuario_Registro = Usuario_Registro;
            this.Fecha_Registro = Fecha_Registro;
        }

        //********************************// IMPLEMENTACIONES DE LA INTERFAZ  //********************************// 

        /// <summary>
        /// Obtiene el nombre de la tabla.
        /// </summary>
        public String NombreTabla { get { return "Cat_Productos"; } }
        /// <summary>
        /// Obtiene el id de la tabla.
        /// </summary>
        public String IDTabla { get { return "Producto_ID"; } }
        /// <summary>
        /// Obtiene el valor por el que se ordenaran los datos
        /// </summary>
        public String Orderby { get { return "Producto_ID"; } }
        /// <summary>
        /// Obtiene los parametros de operacion en la BD.
        /// </summary>
        /// <returns>Lista de Parámetros para operaciones en la BD.</returns>
        public List<ParametroBD> ObtenParametros()
        {
            //Creamos los parametros de BD.
            List<ParametroBD> parametrosBD = new List<ParametroBD>();
            if (!String.IsNullOrEmpty(Producto_ID))
                parametrosBD.Add(new ParametroBD("Producto_ID", Producto_ID));
            parametrosBD.Add(new ParametroBD("Descripcion", Descripcion));
            parametrosBD.Add(new ParametroBD("Cajas_Stock", Cajas_Stock));

            //if (Modo_Captura.Equals(MODO_DE_CAPTURA.CAPTURA_ALTA))
            if (!String.IsNullOrEmpty(Usuario_Registro))
                parametrosBD.Add(new ParametroBD("Usuario_Creo", Usuario_Registro));
            else if (!String.IsNullOrEmpty(Usuario_Modifico))

            {
                parametrosBD.Add(new ParametroBD("Usuario_Modifico", Usuario_Modifico));
                //parametrosBD.Add(new ParametroBD("Fecha_Modifico", "Getdate()"));
            }
            //Retornamos la lista.
            return parametrosBD;
        }
    }
}