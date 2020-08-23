using System;
using System.Collections.Generic;
using System.Text;
using StoreSystem.DataAcccesLayer.Abstract;
using StoreSystem.DataAcccesLayer.Repository;
using StoreSystem.DataAcccesLayer.Repository.Contexts;
using StoreSystem.DataAcccesLayer.Repository.EntityFramework;
using StoreSystem.Entities.Concrete;

namespace StoreSystem.DataAcccesLayer.Concrete.EntityFramework
{
    public class EfProductDal:EfEntityRepositoryBase<Product,NortwindContext>,IProductDal
    {
    }
}
