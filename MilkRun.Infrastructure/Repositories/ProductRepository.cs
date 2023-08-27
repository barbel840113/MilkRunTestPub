using LinqKit;
using Microsoft.IdentityModel.Tokens;

using MilkRun.ApplicationCore.Models;
using MilkRun.Infrastructure.DbContexts;
using MilkRun.Infrastructure.Features.Products.Queries;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkRun.Infrastructure.Repositories
{
    public interface IProductRepository : IGenericRepositoryAsync<Product>
    {
        Task<(IEnumerable<Product> data, int totalRecords)> GetPagedProductsReponseAsync(GetProductsQuery query);
    }

    public class ProductRepository : GenericRepositoryAsync<Product>, IProductRepository
    {
        public ProductRepository(MilkRunDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<(IEnumerable<Product> data, int totalRecords)> GetPagedProductsReponseAsync(GetProductsQuery query)
        {

            var predicate = PredicateBuilder.New<Product>();


            if (query.Title != null)
            {
                predicate = predicate.And(p => p.Title == query.Title);
            }

            if (query.BrandId != null)
            {
                predicate = predicate.And(p => p.BrandId == query.BrandId);
            }

            var orderBy = "Created ";
            if (!string.IsNullOrEmpty(query.OrderBy))
            {
                orderBy = query.OrderBy + " ";
            }

            if (!string.IsNullOrEmpty(query.OrderDir))
            {
                orderBy += query.OrderDir;
            }

            var countTask = await GetCountByQueryAsync(predicate);
            var result = await GetPagedReponseWithQueryOrderAsync(predicate, orderBy, query.PageNumber, query.PageSize);
            return (result, countTask);
        }
    }
}
