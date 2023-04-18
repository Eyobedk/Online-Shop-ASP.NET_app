using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core;

namespace MyShop.DataAccess.InMemory
{
    public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;

        public InMemoryRepository()
        {
            this.className = typeof(T).Name;

            items = cache[this.className] as List<T>;

            if (items == null)
            {
                items = new List<T>();
            }

        }

        public void Commit()
        {
            cache[this.className] = items;
        }

        public void Insert(T item)
        {
            items.Add(item);
        }

        public void Update(T item)
        {
            T updated = items.Find(P => P.Id == item.Id);

            if (updated != null)
            {
                updated = item;
            }
            else
            {
                throw new Exception("no product found");
            }
        }

        public T Find(string Id)
        {
            T item = items.Find(P => P.Id == Id);

            if (item != null)
            {
                return item;
            }
            else
            {
                throw new Exception("no product found");
            }
        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

        public void Delete(string Id)
        {
            T deleted = items.Find(P => P.Id == Id);

            if (deleted != null)
            {
                items.Remove(deleted);
            }
            else
            {
                throw new Exception("no product found");
            }
        }

    }
}
