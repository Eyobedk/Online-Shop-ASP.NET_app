using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategory;

        public ProductCategoryRepository()
        {
            productCategory = cache["productCategory"] as List<ProductCategory>;

            if (productCategory == null)
            {
                productCategory = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            cache["productCategory"] = productCategory;
        }

        public void Insert(ProductCategory category)
        {
            productCategory.Add(category);
        }

        public void Update(ProductCategory category)
        {
            ProductCategory updated = productCategory.Find(P => P.Id == category.Id);

            if (updated != null)
            {
                updated = category;
            }
            else
            {
                throw new Exception("no product found");
            }
        }

        public ProductCategory Find(string Id)
        {
            ProductCategory category = productCategory.Find(P => P.Id == Id);

            if (category != null)
            {
                return category;
            }
            else
            {
                throw new Exception("no product found");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategory.AsQueryable();
        }

        public void Delete(string Id)
        {
            ProductCategory deleted = productCategory.Find(P => P.Id == Id);

            if (deleted != null)
            {
                productCategory.Remove(deleted);
            }
            else
            {
                throw new Exception("no product found");
            }
        }
    }
}
