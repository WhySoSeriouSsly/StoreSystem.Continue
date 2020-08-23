using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using StoreSystem.Entities.Dto;

namespace StoreSystem.Entities.Concrete
{
    public class Product:IEntity
    {
        //SİNGLE RESPONSİBLİTY,OPEN CLOSED PRİNCİPLE ,LİSKOV SUBSTUTİON ,İNTERFACE SEGRATİON,DEPENDENCY İNVERSİON
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }
    }
}
