using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
       public  List<Product> _products;


        public InMemoryProductDal()
        {
            _products = new List<Product>
            
            {
            new Product { CategoryId = 1, ProductName = "Akıllı Telefon", ProductId = 101, UnitsInStock = 50, UnitPrice = 799.99M },
            new Product { CategoryId = 2, ProductName = "Laptop Bilgisayar", ProductId = 102, UnitsInStock = 30, UnitPrice = 1499.99M },
            new Product { CategoryId = 3, ProductName = "Tablet", ProductId = 103, UnitsInStock = 20, UnitPrice = 399.99M },
            new Product { CategoryId = 1, ProductName = "Kulaklık", ProductId = 104, UnitsInStock = 100, UnitPrice = 49.99M },
            new Product { CategoryId = 4, ProductName = "Televizyon", ProductId = 105, UnitsInStock = 10, UnitPrice = 999.99M }

            };


        }

        public void Add(Product product)
        {
            _products.Add(product);
         

        }

        public void Delete(Product product)
        {
          Product productToDelete = _products.SingleOrDefault(p=>p.CategoryId == product.ProductId); 
            _products.Remove(productToDelete);
        }

        public List<Product> GelAll()
        {
            return _products.ToList();
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategoryId(int categoryId)
        {
          return  _products.Where(p=>p.CategoryId==categoryId).ToList(); 
               
        }

        public void Update(Product product)
        {
            Product productToUpdate = _products.SingleOrDefault(p => p.CategoryId == product.ProductId);
            productToUpdate.ProductName= product.ProductName;   
            productToUpdate.UnitPrice= product.UnitPrice;
            productToUpdate.ProductId= product.ProductId;   
            productToUpdate.UnitsInStock= product.UnitsInStock; 

        }

        public List<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<ProductDetailDto> GetProductDetail()
        {
            throw new NotImplementedException();
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }
    }
}
