using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JPV_Portal.CORE;

namespace JPV_Portal.Modelo.Negocio
{
    public class Cls_Mdl_Cajas : TablaDB
    {
        public String Caja_ID            { get; set; }        
        public String Descripcion        { get; set; }
        public String Cantidad           { get; set; }

        public String Usuario_Modifico { get; set; }
        public String Usuario_Registro   { get; set; }
        public String Fecha_Registro     { get; set; }

        public MODO_DE_CAPTURA Modo_Captura { get; set; } //NO ENTIENDO COMO FUNCIONA

        public Cls_Mdl_Cajas() { }

        public Cls_Mdl_Cajas(String Caja_ID = "", String Descripcion = "", String Cantidad = "",
            String Usuario_Registro = "", String Fecha_Registro = "")
        {
            this.Caja_ID = Caja_ID;            
            this.Descripcion = Descripcion;
            this.Cantidad = Cantidad;                       
            this.Usuario_Registro = Usuario_Registro;
            this.Usuario_Modifico = Usuario_Modifico;
            this.Fecha_Registro = Fecha_Registro;
        }

        //********************************// IMPLEMENTACIONES DE LA INTERFAZ  //********************************// 

        /// <summary>
        /// Obtiene el nombre de la tabla.
        /// </summary>
        public String NombreTabla { get { return "Cat_Cajas"; } }
        /// <summary>
        /// Obtiene el id de la tabla.
        /// </summary>
        public String IDTabla { get { return "Caja_ID"; } }
        /// <summary>
        /// Obtiene el valor por el que se ordenaran los datos
        /// </summary>
        public String Orderby { get { return "Caja_ID"; } }
        /// <summary>
        /// Obtiene los parametros de operacion en la BD.
        /// </summary>
        /// <returns>Lista de Parámetros para operaciones en la BD.</returns>
        public List<ParametroBD> ObtenParametros()
        {
            //Creamos los parametros de BD.
            List<ParametroBD> parametrosBD = new List<ParametroBD>();
            if (!String.IsNullOrEmpty(Descripcion))                
                parametrosBD.Add(new ParametroBD("Descripcion", Descripcion));
            if(!String.IsNullOrEmpty(Cantidad))
                parametrosBD.Add(new ParametroBD("Cantidad", Cantidad));
            if (!String.IsNullOrEmpty(Usuario_Registro))
                parametrosBD.Add(new ParametroBD("Usuario_Creo", Usuario_Registro));
            else if (!String.IsNullOrEmpty(Usuario_Modifico))
            {
                parametrosBD.Add(new ParametroBD("Caja_ID", Caja_ID));
                parametrosBD.Add(new ParametroBD("Usuario_Modifico", Usuario_Modifico));
                parametrosBD.Add(new ParametroBD("Fecha_Modifico", Fecha_Registro));
            }
            //Retornamos la lista.
            return parametrosBD;
        }
    }
}