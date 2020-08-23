using System.Collections.Generic;
using StoreSystem.Entities.Concrete;

namespace StoreSystem.UI.Models
{
    public class ProductAddViewModel
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
    }
}