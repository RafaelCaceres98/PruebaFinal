using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloseDXML.Excel;
using Entity;

namespace Infraestructura
{
    XLWorkbook xLWorkbook;
    IXLWorksheet xLWorksheet;

   public  class DocumentoExcel
    {
        XLWorkbook xLWorkbook;
        IXLWorksheet xLWorksheet;
        

        public DocumentoExcel()
        {
            xLWorkbook = new XLWorkbook();
        }


        public void CrearDocumento() {
            xLWorsheet = xLWorkbook.Worksheet.Add("Clientes Registrados");
            xLWorkvook.SaveAs(@"C:\Users\Estudiante\ReporteExcel.xlsx");

        }

        public void LlenarDocumento(IList<Persona> personas)
        {
            xLWorsheet.Cell("A1")value = personas;
            xLWorkvook.SaveAs(@"C:\Users\Estudiante\ReporteExcel.xlsx");

        }
    }
}
