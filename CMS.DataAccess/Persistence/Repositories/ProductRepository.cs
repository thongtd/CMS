﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Extension;
using CMS.DataAccess.Core.Repositories;
using CMS.DataAccess.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using CMS.DataAccess.Core.Linqkit;
using Newtonsoft.Json;

namespace CMS.DataAccess.Persistence.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(WorkContext context) : base(context)
        {
        }

        public void ConvertToModel(ref Product product, ProductRequest model)
        {
            product.Name = model.Name;
            product.Slug = string.IsNullOrEmpty(model.Slug) ? model.Name.NameToSlug() : model.Slug;
            product.ProductCategoryId = model.ProductCategoryId;
            product.Thumbnail = model.Thumbnail;
            product.Images = model.Images.Any() ? JsonConvert.SerializeObject(model.Images) : null;
            product.CreatedDate = model.CreatedDate;
            product.ModeifiedDate = DateTime.UtcNow;
            product.SubContent = model.SubContent;
            product.BodyContent = model.BodyContent;
            product.Target = model.Target;
            product.View = model.Click;
            product.Title = model.Title;
            product.Description = model.Description;
            product.Keyword = model.Keyword;
            product.CultureCode = model.CultureCode;
            product.IsActive = model.IsActive;
            product.PinToTop = model.PinToTop;
            product.Price = model.Price;
            product.Discount = model.Discount;
            product.DiscountIsPercent = model.DiscountIsPercent;
            product.NumberOfProduct = model.NumberOfProduct;
            product.Unit = model.Unit;
            product.Color = string.Join(";", model.Color);
            product.Size = string.Join(";", model.Size);
        }

        public IEnumerable<ProductResponse> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<Product, bool>> predicate)
        {
            var products = WorkContext.Products.Include(s => s.ProductCategory).OrderByDescending(s => s.Id).ToList();

            if (!products.Any())
            {
                totalRecord = 0;
                return new List<ProductResponse>();
            }

            totalRecord = products.Count();
            var productResponses = products.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList().Select(s => new ProductResponse
            {
                ProductCategory = s.ProductCategory,
                IsActive = s.IsActive,
                ProductCategoryId = s.ProductCategoryId,
                Name = s.Name,
                BodyContent = s.BodyContent,
                View = s.View,
                ModeifiedDate = s.ModeifiedDate,
                CreatedDate = s.CreatedDate,
                CultureCode = s.CultureCode,
                Description = s.Description,
                Id = s.Id,
                Title = s.Title,
                Slug = s.Slug,
                Images = s.Images != null ? JsonConvert.DeserializeObject<IList<string>>(s.Images) : null,
                Keyword = s.Keyword,
                Thumbnail = s.Thumbnail,
                PinToTop = s.PinToTop,
                SubContent = s.SubContent,
                IdentityCode = s.IdentityCode,
                Target = s.Target,
                Tags = WorkContext.Tags.Where(t => s.IdentityCode == t.ObjectIdentityId).ToList(),
                Price = s.Price,
                Discount = s.Discount,
                DiscountIsPercent = s.DiscountIsPercent
            }).ToList();

            return productResponses;
        }

        public IEnumerable<ProductResponse> GetByTop(int top, Expression<Func<Product, bool>> predicate)
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var products = uow.Product.Find(predicate).Take(top).ToList();

                return products.Any() ? products.Select(ConvertToProductResponse).ToList() : new List<ProductResponse>();
            }
        }

        public ProductResponse GetTagByProductId(int id)
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var product = uow.Product.Get(id);

                return ConvertToProductResponse(product);
            }
        }

        public ProductResponse GetBySlug(string slug)
        {
            var predicate = PredicateBuilder.Create<Product>(s => s.Slug.Equals(slug));

            var product = WorkContext.Products.SingleOrDefault(predicate);

            if (product == null)
            {
                return new ProductResponse();
            }

            return ConvertToProductResponse(product);
        }

        public async Task Add(ProductRequest model)
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var identity = Guid.NewGuid();

                var product = (Product)model;
                product.CreatedDate = DateTime.UtcNow;

                var productCategory = uow.ProductCategory.Get(model.ProductCategoryId);

                product.CultureCode = productCategory.CultureCode;

                product.IdentityCode = identity;
                uow.Product.Add(product);
                
                if (!string.IsNullOrEmpty(model.TagClouds))
                {
                    var arrTags = model.TagClouds.Split(',');
                    await uow.Tag.AddTagToObject(arrTags, Constants.ObjectName.Product, identity, false);
                }

                uow.Complete();
            }
        }

        public async Task Update(ProductRequest model)
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var product = uow.Product.Get(model.Id);

                ConvertToModel(ref product, model);
                product.ModeifiedDate = DateTime.UtcNow;

                if (!string.IsNullOrEmpty(model.TagClouds))
                {
                    var arrTags = model.TagClouds.Split(',');

                    var taskRemove = uow.Tag.RemoveOldTags(Constants.ObjectName.Product, product.IdentityCode);
                    await Task.WhenAll(taskRemove);

                    await uow.Tag.AddTagToObject(arrTags, Constants.ObjectName.Product, product.IdentityCode, true);
                }

                uow.Complete();
            }
        }

        public WorkContext WorkContext
        {
            get { return Context as WorkContext; }
        }

        private ProductResponse ConvertToProductResponse(Product product)
        {
            if (product == null)
            {
                return new ProductResponse();
            }

            return new ProductResponse
            {
                Name = product.Name,
                BodyContent = product.BodyContent,
                CreatedDate = product.CreatedDate,
                CultureCode = product.CultureCode,
                Description = product.Description,
                Discount = product.Discount,
                DiscountIsPercent = product.DiscountIsPercent,
                Id = product.Id,
                IdentityCode = product.IdentityCode,
                Images = product.Images != null ? JsonConvert.DeserializeObject<IList<string>>(product.Images) : null,
                IsActive = product.IsActive,
                Keyword = product.Keyword,
                ModeifiedDate = product.ModeifiedDate,
                NumberOfProduct = product.NumberOfProduct,
                PinToTop = product.PinToTop,
                Price = product.Price,
                ProductCategory = product.ProductCategory,
                ProductCategoryId = product.ProductCategoryId,
                SellingOfProduct = product.SellingOfProduct,
                Slug = product.Slug,
                SubContent = product.SubContent,
                Target = product.Target,
                Thumbnail = product.Thumbnail,
                Title = product.Title,
                View = product.View,
                Tags = WorkContext.Tags.Where(t => product.IdentityCode == t.ObjectIdentityId).ToList()
            };
        }
    }
}