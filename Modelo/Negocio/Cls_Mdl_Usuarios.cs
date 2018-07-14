using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JPV_Portal.CORE;

namespace JPV_Portal.Modelo.Negocio
{
    public class Cls_Mdl_Usuarios : TablaDB
    {
        public String Usuario_ID     { get; set; }
        public String Identificador  { get; set; }
        public String Nombre         { get; set; }
        public String Email          { get; set; }
        public String Password       { get; set; }
        public String Tipo           { get; set; }
        public String Estatus        { get; set; }

        public String Usuario_Registro   { get; set; }
        public String Fecha_Registro     { get; set; }

        public MODO_DE_CAPTURA Modo_Captura { get; set; }

        public Cls_Mdl_Usuarios() { }

        public Cls_Mdl_Usuarios(String Usuario_ID = "", String Nombre = "", String Email = "", String Password = "", String Estatus = "",
            String Tipo = "", String Usuario_Registro = "", String Fecha_Registro = "")
        {
            this.Usuario_ID = Usuario_ID;
            this.Nombre = Nombre;
            this.Email = Email;
            this.Password = Password;
            this.Estatus = Estatus;
            this.Tipo = Tipo;
            this.Usuario_Registro = Usuario_Registro;
            this.Fecha_Registro = Fecha_Registro;
        }

        //********************************// IMPLEMENTACIONES DE LA INTERFAZ  //********************************// 

        /// <summary>
        /// Obtiene el nombre de la tabla.
        /// </summary>
        public String NombreTabla { get { return "Cat_Usuarios"; } }
        /// <summary>
        /// Obtiene el id de la tabla.
        /// </summary>
        public String IDTabla { get { return "Usuario_ID"; } }
        /// <summary>
        /// Obtiene el valor por el que se ordenaran los datos
        /// </summary>
        public String Orderby { get { return "Usuario_ID"; } }
        /// <summary>
        /// Obtiene los parametros de operacion en la BD.
        /// </summary>
        /// <returns>Lista de Parámetros para operaciones en la BD.</returns>
        public List<ParametroBD> ObtenParametros()
        {
            //Creamos los parametros de BD.
            List<ParametroBD> parametrosBD = new List<ParametroBD>();
            parametrosBD.Add(new ParametroBD("Usuario_ID", Usuario_ID));
            if (!String.IsNullOrEmpty(Identificador))
                parametrosBD.Add(new ParametroBD("Identificador", Identificador));
            parametrosBD.Add(new ParametroBD("Nombre", Nombre));
            parametrosBD.Add(new ParametroBD("Email", Email));
            parametrosBD.Add(new ParametroBD("Password", Password));
            parametrosBD.Add(new ParametroBD("Tipo", Tipo));
            parametrosBD.Add(new ParametroBD("Estatus", Estatus));
            if (Modo_Captura.Equals(MODO_DE_CAPTURA.CAPTURA_ALTA))
                parametrosBD.Add(new ParametroBD("Usuario_Creo", Usuario_Registro));
            else
            {
                parametrosBD.Add(new ParametroBD("Usuario_Modifico", Usuario_Registro));
                parametrosBD.Add(new ParametroBD("Fecha_Modifico", Fecha_Registro));
            }
            //Retornamos la lista.
            return parametrosBD;
        }
    }
}