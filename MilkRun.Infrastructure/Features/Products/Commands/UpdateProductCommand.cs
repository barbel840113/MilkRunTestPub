using MediatR;
using MilkRun.ApplicationCore.Contracts.ResponseModel;
using MilkRun.ApplicationCore.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MilkRun.ApplicationCore.Models;
using MilkRun.Infrastructure.Repositories;

namespace MilkRun.Infrastructure.Features.Products.Commands
{
    public class UpdateProductCommand : IRequest<ApiResult<ProductViewModel>>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public Guid? BrandId { get; set; }
        public Guid Id { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ApiResult<ProductViewModel>>
    {
        private readonly IProductRepository _productRepositoryAsync;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(
            IProductRepository productRepository,
            IMapper mapper)
        {
            _productRepositoryAsync = productRepository ?? throw new ArgumentNullException(nameof(IProductRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(IMapper));
        }

        public async Task<ApiResult<ProductViewModel>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepositoryAsync.GetByQueryFirstAsync(x => x.Id == request.Id);
            if (product == null)
            {
                return ApiResult<ProductViewModel>.CreateBadResponse("Product not found");
            }
            // update product
            if (string.IsNullOrEmpty(request.Title))
            {
                product.Title = request.Title;
            }

            if (string.IsNullOrEmpty(request.Description))
            {
                product.Description = request.Description;
            }

            if (request.Price.HasValue)
            {
                product.Price = request.Price.Value;
            }

            if (request.BrandId.HasValue)
            {
                product.BrandId = request.BrandId.Value;
            }

            await _productRepositoryAsync.UpdateAsync(product);
            var mapResult = _mapper.Map<ProductViewModel>(product);
            return ApiResult<ProductViewModel>.CreateSuccessResponse(mapResult);
        }
    }
}
