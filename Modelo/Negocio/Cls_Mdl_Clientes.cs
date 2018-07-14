using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JPV_Portal.CORE;

namespace JPV_Portal.Modelo.Negocio
{
    public class Cls_Mdl_Clientes : TablaDB
    {
        public String Cliente_ID { get; set; }
        public String Nombre { get; set; }
        public String Cajas_Pendientes { get; set; }
        public String Cuentas_Pendientes { get; set; }
        public String Bodega { get; set; }

        public String Usuario_Registro { get; set; }
        public String Fecha_Registro { get; set; }
        public String Usuario_Modifico { get; set; }
        public String Fecha_Modifico { get; set; }

        public MODO_DE_CAPTURA Modo_Captura { get; set; } //NO ENTIENDO COMO FUNCIONA

        public Cls_Mdl_Clientes() { } //PARA QUE SIRVE ESTO

        public Cls_Mdl_Clientes(String Cliente_ID = "", String Nombre = "", String Cajas_Pendientes = "", String Bodega = "", String Cuentas_Pendientes = "", String Usuario_Modifico = "",
            String Fecha_Modifico = "", String Usuario_Registro = "", String Fecha_Registro = "")
        {
            this.Cliente_ID = Cliente_ID;
            this.Nombre = Nombre;
            this.Cajas_Pendientes = Cajas_Pendientes;
            this.Cuentas_Pendientes = Cuentas_Pendientes;
            this.Bodega = Bodega;

            this.Usuario_Modifico = Usuario_Modifico;
            this.Fecha_Modifico = Fecha_Modifico;
            this.Usuario_Registro = Usuario_Registro;
            this.Fecha_Registro = Fecha_Registro;
        }

        //********************************// IMPLEMENTACIONES DE LA INTERFAZ  //********************************// 

        /// <summary>
        /// Obtiene el nombre de la tabla.
        /// </summary>
        public String NombreTabla { get { return "Cat_Clientes"; } }
        /// <summary>
        /// Obtiene el id de la tabla.
        /// </summary>
        public String IDTabla { get { return "Cliente_ID"; } }
        /// <summary>
        /// Obtiene el valor por el que se ordenaran los datos
        /// </summary>
        public String Orderby { get { return "Cliente_ID"; } }
        /// <summary>
        /// Obtiene los parametros de operacion en la BD.
        /// </summary>
        /// <returns>Lista de Parámetros para operaciones en la BD.</returns>
        public List<ParametroBD> ObtenParametros()
        {
            //Creamos los parametros de BD.
            List<ParametroBD> parametrosBD = new List<ParametroBD>();
            if (!String.IsNullOrEmpty(Cliente_ID))
                parametrosBD.Add(new ParametroBD("Cliente_ID", Cliente_ID));
            parametrosBD.Add(new ParametroBD("Nombre", Nombre));
            parametrosBD.Add(new ParametroBD("Cajas_Pendientes", Cajas_Pendientes));
            parametrosBD.Add(new ParametroBD("Cuentas_Pendientes", Cuentas_Pendientes));
            parametrosBD.Add(new ParametroBD("Bodega", Bodega));

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