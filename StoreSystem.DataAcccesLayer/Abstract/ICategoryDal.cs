using System;
using System.Collections.Generic;
using System.Text;
using StoreSystem.DataAcccesLayer.Repository;
using StoreSystem.Entities.Concrete;

namespace StoreSystem.DataAcccesLayer.Abstract
{
    public interface ICategoryDal : IEntityRepository<Category>
    {
    }
}
