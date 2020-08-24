﻿#nullable enable
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastMember;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OfficeOpenXml;
using StoreSystem.Business.Abstract;
using StoreSystem.Entities.Concrete;
using StoreSystem.UI.Models;
using StoreSystem.UI.Utilities.Messages;

namespace StoreSystem.UI.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        private IFileService _fileService;
        public ProductController(IProductService productService, ICategoryService categoryService, IFileService fileService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _fileService = fileService;
        }
        [HttpGet]
        public IActionResult List(string productName)
        {
            var productListViewModel = new ProductListViewModel
            {
                Products = _productService.GetAll(productName)
            };
            return View(productListViewModel);
        }

        [HttpGet]
        public IActionResult List2(string productName)
        {
            var list = _productService.GetAll(productName);

            return Ok(list);
        }
        public ActionResult Add()
        {
            var model = new ProductAddViewModel
            {
                Product = new Product(),
                Categories = _categoryService.GetAll()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(Product product)
        {

            _productService.Add(product);
            TempData.Add("message", Messages.ProductAdded);
            return RedirectToAction("Add");//buraya return view(); dersek add viewini açmaya çalışacak
            //Böyle olunca category select listi dolu gelemeyecek o yüzden exception vericek 
            //category dolu gelmesi lazım o yüzden hata alırız.
        }

        public ActionResult add2()
        {
            _productService.Add(new Product{ProductName = "NAZIM",CategoryId = 2});
            return Ok();
        }

        //[HttpPost]
        //public ActionResult Add(Product product)
        //{

        //    _productService.Add(product);
        //    TempData.Add("message", Messages.ProductAdded);
        //    return RedirectToAction("Add");
        //    //buraya return view(); dersek add viewini açmaya çalışacak
        //    //Böyle olunca category select listi dolu gelemeyecek o yüzden exception vericek 
        //    //category dolu gelmesi lazım o yüzden hata alırız.

        //}


        public ActionResult Update(int productId)
        {
            var model = new ProductUpdateViewModel
            {
                Product = _productService.GetById(productId),
                Categories = _categoryService.GetAll()
            };

            return View(model);

        }

        [HttpPost]
        public ActionResult Update(Product product)
        {
            _productService.Update(product); 
            TempData.Add("message", Messages.ProductUpdated);
            return RedirectToAction("Update");
        }

        public ActionResult Delete(int productId)
        {
            _productService.Delete(productId);
            //TempData.Add("message", "Product was successfully deleted");
            return RedirectToAction("List");
        }

        public ActionResult Search(string? productName)
        {
            var productListViewModel = new ProductListViewModel
            {
                Products = _productService.GetByName(productName)
            };
            return View(productListViewModel);

        }

        public IActionResult Transaction(Product product)
        {
            _productService.TransactionalOperations(new Product
            {
                ProductName = "Çeto",
                CategoryId = 2,
                UnitPrice = 5
            });
            return Ok();
        }
        public IActionResult ExcelGetir()
        {
            var result = _fileService.ExcelGetir();

            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Guid.NewGuid() + "" + ".xlsx");
        }
        public IActionResult PdfGetir()
        {
            DataTable dataTable = new DataTable();

            dataTable.Load(ObjectReader.Create(_productService.DocumentsGetAll()));


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
            return File("/documents/" + fileName, "application/pdf", fileName);
        }

    }
}
