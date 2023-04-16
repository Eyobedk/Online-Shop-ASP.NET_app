using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

        public ProductRepository()
        {
            products = cache["products"] as List<Product>;

            if (products == null)
            {
                products = new List<Product>();
            }
        }

        public void Commit() {
            cache["products"] = products;
        }

        public void Insert(Product product) {
            products.Add(product);
        }

        public void Update(Product product) {
            Product updated = products.Find(P => P.Id == product.Id);

            if (updated != null)
            {
                updated = product;
            }
            else {
                throw new Exception("no product found");
            }
        }

        public Product Find(string Id) {
            Product product = products.Find(P => P.Id == Id);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("no product found");
            }
        }

        public IQueryable<Product> Collection() {
            return products.AsQueryable();
        }

        public void Delete(String Id) {
            Product deleted = products.Find(P => P.Id == Id);

            if (deleted != null)
            {
                products.Remove(deleted);
            }
            else
            {
                throw new Exception("no product found");
            }
        }
    }
}