using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JPV_Portal.CORE;

namespace JPV_Portal.Modelo.Negocio
{
    public class Cls_Mdl_Proveedor : TablaDB
    {
        public String Proveedor_ID       { get; set; }        
        public String Nombre             { get; set; }
        public String Origen             { get; set; }
        public String Telefono           { get; set; }
        public String Tipo_Proveedor     { get; set; }

        public String Usuario_Registro   { get; set; }
        public String Fecha_Registro     { get; set; }

        public MODO_DE_CAPTURA Modo_Captura { get; set; } //NO ENTIENDO COMO FUNCIONA

        public Cls_Mdl_Proveedor() { }

        public Cls_Mdl_Proveedor(String Proveedor_ID = "", String Nombre = "", String Origen = "",
            String Telefono = "", String Tipo_Proveedor = "", String Usuario_Registro = "", String Fecha_Registro = "")
        {
            this.Proveedor_ID = Proveedor_ID;            
            this.Nombre = Nombre;
            this.Origen = Origen;
            this.Telefono = Telefono;
            this.Tipo_Proveedor = Tipo_Proveedor;
            this.Usuario_Registro = Usuario_Registro;
            this.Fecha_Registro = Fecha_Registro;
        }

        //********************************// IMPLEMENTACIONES DE LA INTERFAZ  //********************************// 

        /// <summary>
        /// Obtiene el nombre de la tabla.
        /// </summary>
        public String NombreTabla { get { return "Cat_Proveedores"; } }
        /// <summary>
        /// Obtiene el id de la tabla.
        /// </summary>
        public String IDTabla { get { return "Proveedor_ID"; } }
        /// <summary>
        /// Obtiene el valor por el que se ordenaran los datos
        /// </summary>
        public String Orderby { get { return "Proveedor_ID"; } }
        /// <summary>
        /// Obtiene los parametros de operacion en la BD.
        /// </summary>
        /// <returns>Lista de Parámetros para operaciones en la BD.</returns>
        public List<ParametroBD> ObtenParametros()
        {
            //Creamos los parametros de BD.
            List<ParametroBD> parametrosBD = new List<ParametroBD>();
            parametrosBD.Add(new ParametroBD("Nombre", Nombre));
            if (!String.IsNullOrEmpty(Proveedor_ID))
                parametrosBD.Add(new ParametroBD("Proveedor_ID", Proveedor_ID));
            if (!String.IsNullOrEmpty(Origen))
                parametrosBD.Add(new ParametroBD("Origen", Origen));
            if (!String.IsNullOrEmpty(Telefono))
                parametrosBD.Add(new ParametroBD("Telefono", Telefono));
            parametrosBD.Add(new ParametroBD("Tipo_Proveedor", Tipo_Proveedor));
            if (!String.IsNullOrEmpty(Usuario_Registro))
                parametrosBD.Add(new ParametroBD("Usuario_Creo", Usuario_Registro));
            if (!String.IsNullOrEmpty(Proveedor_ID))
                parametrosBD.Add(new ParametroBD("Proveedor_ID", Proveedor_ID));
            if (!String.IsNullOrEmpty(Usuario_Registro))
                parametrosBD.Add(new ParametroBD("Usuario_Modifico", Usuario_Registro));
            if (!String.IsNullOrEmpty(Fecha_Registro))
                parametrosBD.Add(new ParametroBD("Fecha_Modifico", Fecha_Registro));
            return parametrosBD;
        }
    }
}