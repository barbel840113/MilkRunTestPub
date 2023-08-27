using AutoMapper;

using MediatR;

using Microsoft.Extensions.Configuration;

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
        private readonly IJsonRepository _jsonRepository;
        private readonly IMapper _mapper;
        private readonly string _useMockData;


        public GetProductsQueryHandler(
            IProductRepository productRepository,
            IMapper mapper,
            IConfiguration configuration,
            IJsonRepository jsonRepository
         )
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _useMockData = configuration["UseMockData"];
            _jsonRepository = jsonRepository;
        }

        public async Task<PagedResponse<IEnumerable<ProductViewModel>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            if(_useMockData == "true")
            {
                var mockData = await _jsonRepository.GetAllJsonData();
                var mockResult = _mapper.Map<IEnumerable<ProductViewModel>>(mockData);
                return PagedResponse<IEnumerable<ProductViewModel>>.Success(mockResult, request.PageNumber, request.PageSize, mockResult.Count());
            }
            // query based on filter
            var entityPositions = await _productRepository.GetPagedProductsReponseAsync(request);
            var data = entityPositions.data;
            var mapResult = _mapper.Map<IEnumerable<ProductViewModel>>(data);
            // response wrapper
            return PagedResponse<IEnumerable<ProductViewModel>>.Success(mapResult, request.PageNumber, request.PageSize, entityPositions.totalRecords);
        }
    }
}
