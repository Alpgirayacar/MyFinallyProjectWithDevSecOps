using Business.Abstract;
using Business.BusinessAspects;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {

        IProductDal _productDal;
        ICategoryService _categoryService;  
     
        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {

            _categoryService= categoryService;
            
              
            _productDal = productDal;
        }

        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
       // [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            //business codes
          IResult result=  BusinessRules.Run(CheckIfProductNameExist(product.ProductName),
                CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckedIfCategoryLimitExeded());
            if(result != null)
            {
                return result;
            }
            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded);

         
         
         
            
         
        
          
        }


        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {

          
            //if (DateTime.Now.Hour == 16)
            //{
            //    return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            //}

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
        }

        [PerformanceAspect(5)]
        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        [CacheAspect]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            if (DateTime.Now.Hour == 23)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }


        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IPreductService.Get")]
        public IResult Update(Product product)
        {
            throw new NotImplementedException();
        }
        //IProductDal _productDal;

        //public ProductManager(IProductDal productDal)
        //{
        //    _productDal = productDal;   
        //}

        //public IResult Add(Product product)
        //{

        //    if(product.ProductName.Length<2)
        //    {
        //        return new ErrorResult( Messages.ProductNameInvalid);
        //    }
        //    _productDal.Add(product);
        //    return new SuccessResult(Messages.ProductAdded);
        //}

        //public IDataResult<List<Product>> GetAll()
        //{
        //    if(DateTime.Now.Hour==22)
        //    {
        //        return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
        //    }

        //    return  new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductListed );    
        //}



        //public IDataResult<List<Product>> GetAllByCategoryId(int categoryId)
        //{
        //    return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == categoryId));
        //}


        //IDataResult<List<Product>> IProductService.GetByUnitPrice(decimal min, decimal max)
        //{
        //    return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        //}



        //public IDataResult<Product> GetById(int productId)
        //{
        //    return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        //}



        //IDataResult<List<ProductDetailDto>> IProductService.GetProductDetails()
        //{
        //    return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        //}



        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 15)
            {
                return new ErrorResult(Messages.ProductOfCategoryError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExist(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlredyExist);
            }
            return new SuccessResult();
        }


        private IResult CheckedIfCategoryLimitExeded()
        {
            var result = _categoryService.GetAll();
            
          

            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExeded);
            }
            return new SuccessResult();

        }

        [TransactionScopeAspect]
       

        public IResult AddTransactionalTest(Product product)
        {
            Add(product);
            if(product.UnitPrice >= 10)
            {
                throw new Exception("");
            }
            return null;
        }
    }
}
