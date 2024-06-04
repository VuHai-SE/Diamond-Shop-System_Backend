﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace DAOs
{
    public class ProductCategoryDAO
    {
        private readonly DiamondStoreContext dbContext = null;

        public ProductCategoryDAO()
        {
            if (dbContext == null)
            {
                dbContext = new DiamondStoreContext();
            }
        }

        public List<TblProductCategory> GetProductCategories() 
            => dbContext.TblProductCategories.ToList();

        public TblProductCategory AddProductCategories(TblProductCategory productCategory)
        {
            dbContext.TblProductCategories.Add(productCategory);
            dbContext.SaveChanges();
            return productCategory;
        }

        public TblProductCategory GetProductCategory(string id)
            => dbContext.TblProductCategories.FirstOrDefault(m => m.CategoryId.Equals(id));

        public bool UpdateProductCategory(string id,  TblProductCategory productCategory)
        {
            return false;
        }

        public bool DeleteProductCategory(string id)
        {
            return false;
        }
    }
}
