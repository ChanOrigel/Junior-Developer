using System;
using OfficeOpenXml;
using System.IO;
using System.Data;
using OfficeOpenXml.Style;
using System.Globalization;

namespace JPV_Portal.ReportesExcel
{
    public class Rpt_Inventarios
    {
        FileInfo template;
        String ruta_nueva_archivo;
        int renglon = 7;
        int Renglon = 2;
        int aux_renglon;
        System.Data.DataTable Insumos;


        public Rpt_Inventarios(String ruta_plantilla, String ruta_nueva_archivo, System.Data.DataTable Lista_Insumos)
        {
            template = new FileInfo(ruta_plantilla);
            this.ruta_nueva_archivo = ruta_nueva_archivo;
            this.Insumos = Lista_Insumos;
        }

        public void Historial_Abonos()
        {

            using (ExcelPackage p = new ExcelPackage(template, true))
            {

                ExcelWorksheet ws = p.Workbook.Worksheets[1];

                aux_renglon = Renglon;

                foreach (DataRow Dr in Insumos.Rows)
                {
                    ws.Cells[Renglon, 1].Value = Dr["Folio"].ToString();
                    ws.Cells[Renglon, 2].Value = Dr["Cliente"].ToString();
                    ws.Cells[Renglon, 3].Value = Dr["Cantidad"].ToString();
                    ws.Cells[Renglon, 4].Value = Dr["Fecha_Creo"].ToString();
                    Renglon++;

                }

                ws.Cells[aux_renglon, 1, Renglon - 1, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                ws.Cells[aux_renglon, 1, Renglon - 1, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells[aux_renglon, 1, Renglon - 1, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                ws.Cells[aux_renglon, 1, Renglon - 1, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                // guarda los cambios
                Byte[] bin = p.GetAsByteArray();
                String file = ruta_nueva_archivo;
                File.WriteAllBytes(file, bin);
            }
        }

        public void Historial_Cajas()
        {

            using (ExcelPackage p = new ExcelPackage(template, true))
            {

                ExcelWorksheet ws = p.Workbook.Worksheets[1];

                aux_renglon = Renglon;

                foreach (DataRow Dr in Insumos.Rows)
                {
                    ws.Cells[Renglon, 1].Value = Dr["Tipo_Caja"].ToString();
                    ws.Cells[Renglon, 2].Value = Dr["Proveedor"].ToString();
                    ws.Cells[Renglon, 3].Value = Dr["Cantidad"].ToString();
                    ws.Cells[Renglon, 4].Value = Dr["Costo"].ToString();
                    ws.Cells[Renglon, 5].Value = Dr["Fecha_Creo"].ToString();
                    Renglon++;

                }

                ws.Cells[aux_renglon, 1, Renglon - 1, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                ws.Cells[aux_renglon, 1, Renglon - 1, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells[aux_renglon, 1, Renglon - 1, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                ws.Cells[aux_renglon, 1, Renglon - 1, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                // guarda los cambios
                Byte[] bin = p.GetAsByteArray();
                String file = ruta_nueva_archivo;
                File.WriteAllBytes(file, bin);
            }
        }

        public void Historial_Cajas_Entregadas()
        {

            using (ExcelPackage p = new ExcelPackage(template, true))
            {

                ExcelWorksheet ws = p.Workbook.Worksheets[1];

                aux_renglon = Renglon;

                foreach (DataRow Dr in Insumos.Rows)
                {
                    ws.Cells[Renglon, 1].Value = Dr["Folio"].ToString();
                    ws.Cells[Renglon, 2].Value = Dr["Tipo_Caja"].ToString();
                    ws.Cells[Renglon, 3].Value = Dr["Cliente"].ToString();
                    ws.Cells[Renglon, 4].Value = Dr["Cantidad"].ToString();
                    ws.Cells[Renglon, 5].Value = Dr["Fecha_Creo"].ToString();
                    ws.Cells[Renglon, 6].Value = Dr["Regreso_De_Deposito"].ToString();
                    Renglon++;

                }

                ws.Cells[aux_renglon, 1, Renglon - 1, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                ws.Cells[aux_renglon, 1, Renglon - 1, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells[aux_renglon, 1, Renglon - 1, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                ws.Cells[aux_renglon, 1, Renglon - 1, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                // guarda los cambios
                Byte[] bin = p.GetAsByteArray();
                String file = ruta_nueva_archivo;
                File.WriteAllBytes(file, bin);
            }
        }

        public void Ventas(System.Data.DataTable Abonos)
        {

            using (ExcelPackage p = new ExcelPackage(template, true))
            {

                ExcelWorksheet ws = p.Workbook.Worksheets[1];

                aux_renglon = renglon;
                Decimal canti = 0;
                Decimal vendido = 0;

                foreach (DataRow Dr in Insumos.Rows)
                {
                    ws.Cells[renglon, 1].Value = Dr["Folio"].ToString();
                    ws.Cells[renglon, 2].Value = Dr["Cantidad"].ToString();
                    ws.Cells[renglon, 3].Value = Dr["Descripcion"].ToString();
                    ws.Cells[renglon, 4].Value = Dr["Importe"].ToString();
                    ws.Cells[renglon, 5].Value = Dr["Cliente"].ToString();
                    ws.Cells[renglon, 6].Value = Dr["Estatus"].ToString();
                    ws.Cells[renglon, 7].Value = Dr["Fecha_Creo"].ToString();
                    ws.Cells[renglon, 8].Value = Dr["Factura"].ToString();
                    renglon++;

                    if(Dr["Estatus"].ToString()!="Cancelado")
                    {
                        canti = canti + System.Convert.ToDecimal(Dr["Cantidad"].ToString());
                        vendido = vendido + System.Convert.ToDecimal(Dr["Importe"].ToString());
                    }

                }
                ws.Cells[1, 4].Value = canti.ToString();
                ws.Cells[2, 4].Value = vendido.ToString();
                ws.Cells[3, 4].Value = Abonos.Rows[0]["Cantidad"].ToString();
                ws.Cells[4, 4].Value = System.Convert.ToDecimal(vendido.ToString())- System.Convert.ToDecimal(Abonos.Rows[0]["Cantidad"].ToString());

                ws.Cells[aux_renglon, 1, renglon - 1, 8].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                ws.Cells[aux_renglon, 1, renglon - 1, 8].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells[aux_renglon, 1, renglon - 1, 8].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                ws.Cells[aux_renglon, 1, renglon - 1, 8].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                // guarda los cambios
                Byte[] bin = p.GetAsByteArray();
                String file = ruta_nueva_archivo;
                File.WriteAllBytes(file, bin);
            }
        }

    }
}