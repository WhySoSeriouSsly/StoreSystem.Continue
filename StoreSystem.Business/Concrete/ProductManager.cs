using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using StoreSystem.Business.Abstract;
using StoreSystem.Business.ValidationRules;
using StoreSystem.Business.ValidationRules.FluentValidation;
using StoreSystem.Core.Aspects.Autofac.Caching;
using StoreSystem.Core.Aspects.Autofac.Performance;
using StoreSystem.Core.Aspects.Autofac.Transaction;
using StoreSystem.Core.Aspects.Autofac.Validation;
using StoreSystem.Core.Aspects.Postsharp.Caching;
using StoreSystem.Core.Aspects.Postsharp.Validation;
using StoreSystem.Core.CrossCuttingConcerns.Postsharp.Caching.Microsoft;
using StoreSystem.DataAcccesLayer.Abstract;
using StoreSystem.DataAcccesLayer.Concrete.EntityFramework;
using StoreSystem.Entities.Concrete;

namespace StoreSystem.Business.Concrete
{
    public class ProductManager : IProductService
    {
        //private readonly IValidator<Product> _productValidator;
        private IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public void Add(Product product)
        {
            _productDal.Add(product);

        }

        #region ManuelValidation

        //public void Add(Product product)
        //{

        //    ValidationTool.Validate(new ProductValidator(), product);
        //    _productDal.Add(product);
        //    ;
        //    // throw new ValidationException(validateResult.Errors);

        #endregion

        [PerformanceAspect(5)]
        [CacheRemoveAspect("IProductService.Get")]
        public void Delete(int productId)
        {
            _productDal.Delete(new Product { ProductId = productId });
        }
        // [CacheAspect(duration: 1)]
        // [PerformanceAspect(5)]
        public  List<Product> GetAll(string productName)
        {
            // Thread.Sleep(6000);
            return  _productDal.GetList(p => p.ProductName == productName || productName == null);
        }
        [PerformanceAspect(5)]
        public IEnumerable<Product> GetAllDesc()
        {
            // Thread.Sleep(6000);
            return _productDal.GetList().OrderByDescending(t => t.ProductId).Take(10);
        }
        [PerformanceAspect(5)]
        public List<Product> DocumentsGetAll()
        {
            return _productDal.GetList();
        }
        [PerformanceAspect(5)]
        public List<Product> GetByCategory(int categoryId)
        {
            return _productDal.GetList(filter: p => p.CategoryId == categoryId || categoryId == 0);
        }
        [TransactionScopeAspects]
        [PerformanceAspect(5)]
        public void TransactionalOperations(Product product)
        {
            _productDal.Add(product);
            _productDal.Update(product);
        }
        [PerformanceAspect(5)]
        public Product GetById(int productId)
        {
            return _productDal.Get(p => p.ProductId == productId);
        }
        [PerformanceAspect(5)]
        public List<Product> GetByName(string productName)
        {
            return _productDal.GetList(p => p.ProductName.Contains(productName) || productName == null);
        }


        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        [PerformanceAspect(5)]
        public void Update(Product product)
        {
            _productDal.Update(product);
        }

    }
}