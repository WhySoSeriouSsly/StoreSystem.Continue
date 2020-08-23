using System;
using System.Collections.Generic;
using System.Text;
using StoreSystem.Entities.Concrete;

namespace StoreSystem.Business.Abstract
{
    public interface IProductService
    {
        List<Product> GetAll(string productName);
        List<Product> DocumentsGetAll();
        List<Product> GetByCategory(int categoryId);
        void Add(Product product);
        void Delete(int productId);
        void Update(Product product);
        void TransactionalOperations(Product product);
        Product GetById(int productId);
        List<Product> GetByName(string productName);
    }
}
