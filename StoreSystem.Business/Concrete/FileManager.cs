using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using FastMember;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using StoreSystem.Business.Abstract;
using StoreSystem.DataAcccesLayer.Abstract;
using static System.Reflection.Metadata.Document;

namespace StoreSystem.Business.Concrete
{
    public class FileManager:IFileService
    {
        private IProductDal _productDal;

        public FileManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        public byte[] ExcelFileGet()
        {
            

            ExcelPackage excelPackage = new ExcelPackage();

            var excelBlank = excelPackage.Workbook.Worksheets.Add("ProductList");

            excelBlank.Cells["A1"].LoadFromCollection(_productDal.GetList(), true, OfficeOpenXml.Table.TableStyles.Light15);


            var bytes = excelPackage.GetAsByteArray();
            return bytes;
        }

        public string PdfFileGet()
        {
            DataTable dataTable = new DataTable();

            dataTable.Load(ObjectReader.Create(_productDal.GetList()));


            string fileName = Guid.NewGuid() + ".pdf";

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/documents/" + fileName);

            var stream = new FileStream(path, FileMode.Create);



            Document document = new Document(PageSize.A4, 25f, 25f, 25f, 25f);

            PdfWriter.GetInstance(document, stream);

            document.Open();


            PdfPTable pdfPTable = new PdfPTable(dataTable.Columns.Count);

            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                pdfPTable.AddCell(dataTable.Columns[i].ColumnName);
            }


            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    pdfPTable.AddCell(dataTable.Rows[i][j].ToString());
                }
            }



            document.Add(pdfPTable);

            document.Close();
            return fileName;
        }
    }
}
