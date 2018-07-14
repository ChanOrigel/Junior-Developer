using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JPV_Portal.CORE;

namespace JPV_Portal.Modelo.Negocio
{
    public class Cls_Mdl_A_Pagar : TablaDB
    {
        public String Abono_ID { get; set; }
        public String Caja_ID { get; set; }
        public String Bodega_ID { get; set; }
        public String Folio { get; set; }
        public String Cliente { get; set; }
        public String Cantidad { get; set; }
        public String Estatus { get; set; }
        public String Venta_ID { get; set; }
        public String Pagado { get; set; }
        public String Abonado { get; set; }
        public String Fecha_Inicio { get; set; }
        public String Fecha_Fin { get; set; }
        public String Descripcion { get; set; }
        public String Importe { get; set; }
        public String Flag { get; set; }
        public String Cajas { get; set; }
        public String Proveedor { get; set; }
        public String Costo { get; set; }

        public String Restante { get; set; }

        public String Usuario_Registro { get; set; }
        public String Fecha_Registro { get; set; }
        public String Usuario_Modifico { get; set; }
        public String Fecha_Modifico { get; set; }

        public MODO_DE_CAPTURA Modo_Captura { get; set; } //NO ENTIENDO COMO FUNCIONA

        public Cls_Mdl_A_Pagar() { } //PARA QUE SIRVE ESTO

        public Cls_Mdl_A_Pagar(String Abono_ID = "", String Folio = "", String Cliente = "", String Cantidad = "",
            String Usuario_Registro = "", String Fecha_Registro = "")
        {
            this.Abono_ID = Abono_ID;
            this.Folio = Folio;
            this.Cliente = Cliente;
            this.Cantidad = Cantidad;
            this.Usuario_Modifico = Usuario_Modifico;
            this.Fecha_Modifico = Fecha_Modifico;
            this.Usuario_Registro = Usuario_Registro;
            this.Fecha_Registro = Fecha_Registro;
        }
        //********************************// IMPLEMENTACIONES DE LA INTERFAZ  //********************************// 

        /// <summary>
        /// Obtiene el nombre de la tabla.
        /// </summary>
        public String NombreTabla { get { return "Ope_Historial_Abonos"; } }
        /// <summary>
        /// Obtiene el id de la tabla.
        /// </summary>
        public String IDTabla { get { return "Abono_ID"; } }
        /// <summary>
        /// Obtiene el valor por el que se ordenaran los datos
        /// </summary>
        public String Orderby { get { return "Abono_ID"; } }
        /// <summary>
        /// Obtiene los parametros de operacion en la BD.
        /// </summary>
        /// <returns>Lista de Parámetros para operaciones en la BD.</returns>
        public List<ParametroBD> ObtenParametros()
        {
            //Creamos los parametros de BD.
            List<ParametroBD> parametrosBD = new List<ParametroBD>();
            if (!String.IsNullOrEmpty(Abono_ID))
                parametrosBD.Add(new ParametroBD("Abono_ID", Abono_ID));
            parametrosBD.Add(new ParametroBD("Folio", Folio));
            parametrosBD.Add(new ParametroBD("Cliente", Cliente));
            parametrosBD.Add(new ParametroBD("Cantidad", Cantidad));

            //if (Modo_Captura.Equals(MODO_DE_CAPTURA.CAPTURA_ALTA))
            if (!String.IsNullOrEmpty(Usuario_Registro))
                parametrosBD.Add(new ParametroBD("Usuario_Creo", Usuario_Registro));
            else
            {
                parametrosBD.Add(new ParametroBD("Usuario_Modifico", Usuario_Modifico));
                //parametrosBD.Add(new ParametroBD("Fecha_Modifico", "Getdate()"));
            }
            //Retornamos la lista.
            return parametrosBD;
        }
    }
}