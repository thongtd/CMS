using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Extension;
using CMS.DataAccess.Core.Repositories;
using CMS.DataAccess.Models;
using MvcConnerstore.Collections;
using System.Data.Entity;
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
            product.Click = model.Click;
            product.Title = model.Title;
            product.Description = model.Description;
            product.Keyword = model.Keyword;
            product.CultureCode = model.CultureCode;
            product.IsActive = model.IsActive;
            product.PinToTop = model.PinToTop;
            product.Price = model.Price;
            product.Discount = model.Discount;
            product.DiscountType = model.DiscountType;
        }

        public IEnumerable<ProductResponse> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<Product, bool>> predicate)
        {
            var products = WorkContext.Products
                .Include(s => s.ProductCategory).OrderByDescending(s => s.Id);

            if (products.Any())
            {
                var productResponses = products.Select(s => new ProductResponse
                {
                    ProductCategory = s.ProductCategory,
                    IsActive = s.IsActive,
                    ProductCategoryId = s.ProductCategoryId,
                    Name = s.Name,
                    BodyContent = s.BodyContent,
                    Click = s.Click,
                    ModeifiedDate = s.ModeifiedDate,
                    CreatedDate = s.CreatedDate,
                    CultureCode = s.CultureCode,
                    Description = s.Description,
                    Id = s.Id,
                    Title = s.Title,
                    Slug = s.Slug,
                    //Images = s.Images,
                    Keyword = s.Keyword,
                    Thumbnail = s.Thumbnail,
                    PinToTop = s.PinToTop,
                    SubContent = s.SubContent,
                    IdentityCode = s.IdentityCode,
                    Target = s.Target,
                    Tags = WorkContext.Tags.Where(t => s.IdentityCode == t.ObjectIdentityId).ToList(),
                    Price = s.Price,
                    Discount = s.Discount,
                    DiscountType = s.DiscountType
                }).ToList();

                totalRecord = productResponses.Count();
                productResponses = productResponses.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return new PagedList<ProductResponse>(productResponses, totalRecord);
            }

            totalRecord = 0;
            return new PagedList<ProductResponse>(null, 0);
        }

        public IEnumerable<ProductResponse> GetByTop(int top, Expression<Func<Product, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductResponse> GetTagByProductId(int blogId)
        {
            throw new NotImplementedException();
        }

        public ProductResponse GetBySlug(string slug)
        {
            var predicate = PredicateBuilder.Create<Product>(s => s.Slug.Equals(slug));

            var product = WorkContext.Products.SingleOrDefault(predicate);

            if (product == null)
            {
                return new ProductResponse();
            }
            return new ProductResponse
            {
                ProductCategory = product.ProductCategory,
                Name = product.Name,
                Id = product.Id,
                IsActive = product.IsActive,
                Description = product.Description,
                ProductCategoryId = product.ProductCategoryId,
                //Images = product.Images,
                BodyContent = product.BodyContent,
                Click = product.Click,
                CreatedDate = product.CreatedDate,
                CultureCode = product.CultureCode,
                Discount = product.Discount,
                DiscountType = product.DiscountType,
                IdentityCode = product.IdentityCode,
                Keyword = product.Keyword,
                ModeifiedDate = product.ModeifiedDate,
                PinToTop = product.PinToTop,
                Price = product.Price,
                Slug = product.Slug,
                SubContent = product.SubContent,
                Target = product.Target,
                Thumbnail = product.Thumbnail,
                Title = product.Title
            };
        }

        public void Add(ProductRequest model, string collectionTags)
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

                #region Add tags for blog
                if (!string.IsNullOrEmpty(collectionTags))
                {
                    string[] arrTags = collectionTags.Split(',');
                    var tags = uow.TagCategory.GetAll();
                    var tagIds = (from s in tags where arrTags.Contains(s.Name) select s).ToList();

                    foreach (var item in tagIds)
                    {
                        int tagId = int.Parse(item.Id.ToString());
                        var tag = new Tag()
                        {
                            ObjectName = Constants.ObjectName.Blog,
                            ObjectProperty = Constants.ObjectName.BlogIdenityCode,
                            ObjectIdentityId = identity,
                            TagCategoryId = tagId
                        };
                        uow.Tag.Add(tag);
                    }
                }
                #endregion

                uow.Complete();
            }
        }

        public void Update(ProductRequest model, string tags)
        {
            using (var uow = new UnitOfWork(new WorkContext()))
            {
                var product = uow.Product.Get(model.Id);

                ConvertToModel(ref product, model);
                product.ModeifiedDate = DateTime.UtcNow;

                var predicate = PredicateBuilder.Create<Tag>(s => s.ObjectIdentityId == product.IdentityCode && s.ObjectName == Constants.ObjectName.Blog
                    && s.ObjectProperty == Constants.ObjectName.BlogIdenityCode);
                var oldTags = uow.Tag.Find(predicate).ToList();

                uow.Tag.RemoveRange(oldTags);

                if (!string.IsNullOrEmpty(tags))
                {
                    string[] arrTags = tags.Split(',');
                    var tagCategories = uow.TagCategory.GetAll();

                    var tagIds = (from s in tagCategories where arrTags.Contains(s.Name) select s).ToList();

                    foreach (var item in tagIds)
                    {
                        int tagId = int.Parse(item.Id.ToString());
                        var tag = new Tag
                        {
                            ObjectName = Constants.ObjectName.Blog,
                            ObjectProperty = Constants.ObjectName.BlogIdenityCode,
                            ObjectIdentityId = product.IdentityCode,
                            TagCategoryId = tagId
                        };
                        uow.Tag.Add(tag);
                    }
                }

                uow.Complete();
            }
        }

        public WorkContext WorkContext
        {
            get { return Context as WorkContext; }
        }
    }
}