using System.Collections;
using System.Collections.Generic;
using BakeryStore.Models;

namespace BakeryStore.Data
{
    public interface IProductsService
    {
        public IList<Product> Products { get; set; }
    }
}