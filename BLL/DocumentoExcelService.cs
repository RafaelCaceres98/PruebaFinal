using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructura;
using Entity;


namespace BLL
{
  public  class DocumentoExcelService
    {
        DocumentoExcel documentoexcel;

        public DocumentoExcelService()
        {
            documentoexcel = new DocumentoExcel();
        }

        public void CrearDocumento() {
            documentoexcel.CrearDocumento();
        
        }

        public void LlenarDocumento(IList<Persona>personas) {
            documentoexcel.LlenarDocumento(personas);
        
        }
    }
}
