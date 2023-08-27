using AutoMapper;

using MediatR;

using MilkRun.ApplicationCore.Contracts;
using MilkRun.ApplicationCore.Contracts.QueryParams;
using MilkRun.ApplicationCore.Contracts.ResponseModel;
using MilkRun.ApplicationCore.Models;
using MilkRun.Infrastructure.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MilkRun.Infrastructure.Features.Products.Queries
{
    public class GetProductsQuery : QueryParameter, IRequest<PagedResponse<IEnumerable<ProductViewModel>>>
    {
        [JsonPropertyName("brandId")]
        public Guid? BrandId { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }
    }
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PagedResponse<IEnumerable<ProductViewModel>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;


        public GetProductsQueryHandler(
            IProductRepository productRepository,
            IMapper mapper
         )
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<ProductViewModel>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            // query based on filter
            var entityPositions = await _productRepository.GetPagedProductsReponseAsync(request);
            var data = entityPositions.data;
            var mapResult = _mapper.Map<IEnumerable<ProductViewModel>>(data);
            // response wrapper
            return PagedResponse<IEnumerable<ProductViewModel>>.Success(mapResult, request.PageNumber, request.PageSize, entityPositions.totalRecords);
        }
    }
}
