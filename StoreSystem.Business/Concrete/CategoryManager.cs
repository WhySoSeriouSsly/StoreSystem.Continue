using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoreSystem.Business.Abstract;
using StoreSystem.DataAcccesLayer.Abstract;
using StoreSystem.Entities.Concrete;

namespace StoreSystem.Business.Concrete
{
    public class CategoryManager:ICategoryService
    {
        private ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        public List<Category> GetAll()
        {
            return _categoryDal.GetList();
        }

        public List<Category> GetByName(string categoryName)
        {
            return _categoryDal.GetList(c=>c.CategoryName==categoryName||categoryName==null);
        }
    }
}
