using System;
using System.Collections.Generic;
using System.Text;
using StoreSystem.Entities.Concrete;

namespace StoreSystem.Business.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        List<Category> GetByName(string categoryName);

    }
}
