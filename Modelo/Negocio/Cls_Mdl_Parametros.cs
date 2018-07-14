using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JPV_Portal.CORE;

namespace JPV_Portal.Modelo.Negocio
{
    public class Cls_Mdl_Parametros : TablaDB
    {
        public String Parametro_ID { get; set; }
        public String Correo_Saliente { get; set; }
        public String Servidor_Correo { get; set; }
        public String Puerto_Correo { get; set; }
        public String Contrasenia_Correo { get; set; }
        public String Cifrar_Conexion { get; set; }
        public String Correo_Destino { get; set; }
        public String Impresora { get; set; }
        public String Correo_Empleado { get; set; }

        public String Usuario_Registro { get; set; }
        public String Fecha_Registro { get; set; }
        public String Usuario_Modifico { get; set; }
        public String Fecha_Modifico { get; set; }

        public MODO_DE_CAPTURA Modo_Captura { get; set; } //NO ENTIENDO COMO FUNCIONA

        public Cls_Mdl_Parametros() { } //PARA QUE SIRVE ESTO

        public Cls_Mdl_Parametros(
            String Parametro_ID = "", String Correo_Saliente = "", String Servidor_Correo = "", 
            String Puerto_Correo = "", String Contrasenia_Correo = "", String Cifrar_Conexion = "", String Correo_Empleado = "",
            String Correo_Destino = "", String Impresora = "", String Usuario_Modifico = "", 
            String Fecha_Modifico = "", String Usuario_Registro = "", String Fecha_Registro = "")
        {
            this.Parametro_ID = Parametro_ID;
            this.Correo_Saliente = Correo_Saliente;
            this.Servidor_Correo = Servidor_Correo;
            this.Puerto_Correo = Puerto_Correo;
            this.Contrasenia_Correo = Contrasenia_Correo;
            this.Cifrar_Conexion = Cifrar_Conexion;
            this.Correo_Destino = Correo_Destino;
            this.Impresora = Impresora;
            this.Correo_Empleado = Correo_Empleado;

            this.Usuario_Modifico = Usuario_Modifico;
            this.Fecha_Modifico = Fecha_Modifico;
            this.Usuario_Registro = Usuario_Registro;
            this.Fecha_Registro = Fecha_Registro;
        }

        //********************************// IMPLEMENTACIONES DE LA INTERFAZ  //********************************// 

        /// <summary>
        /// Obtiene el nombre de la tabla.
        /// </summary>
        public String NombreTabla { get { return "Cat_Parametros"; } }
        /// <summary>
        /// Obtiene el id de la tabla.
        /// </summary>
        public String IDTabla { get { return "Parametro_ID"; } }
        /// <summary>
        /// Obtiene el valor por el que se ordenaran los datos
        /// </summary>
        public String Orderby { get { return "Parametro_ID"; } }
        /// <summary>
        /// Obtiene los parametros de operacion en la BD.
        /// </summary>
        /// <returns>Lista de Parámetros para operaciones en la BD.</returns>
        public List<ParametroBD> ObtenParametros()
        {
            //Creamos los parametros de BD.
            List<ParametroBD> parametrosBD = new List<ParametroBD>();
            if (!String.IsNullOrEmpty(Parametro_ID))
                parametrosBD.Add(new ParametroBD("Parametro_ID", Parametro_ID));
            parametrosBD.Add(new ParametroBD("Correo_Saliente", Correo_Saliente));
            parametrosBD.Add(new ParametroBD("Servidor_Correo", Servidor_Correo));
            parametrosBD.Add(new ParametroBD("Puerto_Correo", Puerto_Correo));
            parametrosBD.Add(new ParametroBD("Contrasenia_Correo", Contrasenia_Correo));
            parametrosBD.Add(new ParametroBD("Cifrar_Conexion", Cifrar_Conexion));
            parametrosBD.Add(new ParametroBD("Correo_Destino", Correo_Destino));
            parametrosBD.Add(new ParametroBD("Impresora", Impresora));
            parametrosBD.Add(new ParametroBD("Correo_Empleado", Correo_Empleado));

            //if (Modo_Captura.Equals(MODO_DE_CAPTURA.CAPTURA_ALTA))
            if (!String.IsNullOrEmpty(Usuario_Registro))
                parametrosBD.Add(new ParametroBD("Usuario_Creo", Usuario_Registro));
            else if (!String.IsNullOrEmpty(Usuario_Modifico))

            {
                parametrosBD.Add(new ParametroBD("Usuario_Modifico", Usuario_Modifico));
            }
            //Retornamos la lista.
            return parametrosBD;
        }
    }
}