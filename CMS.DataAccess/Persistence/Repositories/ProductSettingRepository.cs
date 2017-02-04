﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CMS.DataAccess.Core.Domain;
using CMS.DataAccess.Core.Extension;
using CMS.DataAccess.Core.Repositories;
using CMS.DataAccess.Models;
using MvcConnerstore.Collections;
using System.Data.Entity;
using System.Threading.Tasks;
using CMS.DataAccess.Core.Linqkit;
using Newtonsoft.Json;

namespace CMS.DataAccess.Persistence.Repositories
{
    public class ProductSettingRepository : BaseRepository<ProductSetting>, IProductSettingRepository
    {
        public ProductSettingRepository(DbContext context) : base(context)
        {
        }

        public void ConvertToModel(ref Product product, ProductCategoryRequest model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductSettingResponse> Paging(int pageIndex, int pageSize, out int totalRecord, Expression<Func<ProductSetting, bool>> predicate)
        {
            var productSettings = WorkContext.ProductSettings.ToList();

            if (!productSettings.Any())
            {
                totalRecord = 0;
                return new List<ProductSettingResponse>();
            }

            totalRecord = productSettings.Count();
            var productSettingResponses = productSettings.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList().Select(s => new ProductSettingResponse
            {
                Name = s.Name,
                Type = s.Type
            }).ToList();

            return productSettingResponses;
        }

        public WorkContext WorkContext
        {
            get { return Context as WorkContext; }
        }
    }
}
