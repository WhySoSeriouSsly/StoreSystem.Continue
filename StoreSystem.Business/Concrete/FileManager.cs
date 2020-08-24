using System;
using System.Collections.Generic;
using System.Text;
using OfficeOpenXml;
using StoreSystem.Business.Abstract;
using StoreSystem.DataAcccesLayer.Abstract;

namespace StoreSystem.Business.Concrete
{
    public class FileManager:IFileService
    {
        private IProductDal _productDal;

        public FileManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        public byte[] ExcelGetir()
        {
            

            ExcelPackage excelPackage = new ExcelPackage();

            var excelBlank = excelPackage.Workbook.Worksheets.Add("ProductList");

            excelBlank.Cells["A1"].LoadFromCollection(_productDal.GetList(), true, OfficeOpenXml.Table.TableStyles.Light15);


            var bytes = excelPackage.GetAsByteArray();
            return bytes;
        }
    }
}
