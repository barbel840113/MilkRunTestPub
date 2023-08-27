using AutoMapper;

using Azure;

using MediatR;

using MilkRun.ApplicationCore.Contracts;
using MilkRun.ApplicationCore.Contracts.ResponseModel;
using MilkRun.ApplicationCore.Models;
using MilkRun.Infrastructure.Repositories;
namespace MilkRun.Infrastructure.Features.Products.Commands
{


    public class CreateProductCommand : IRequest<ApiResult<ProductViewModel>>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Guid BrandId { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ApiResult<ProductViewModel>>
    {
        private readonly IProductRepository _productRepositoryAsync;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(
            IProductRepository productRepository,
            IMapper mapper)
        {
            _productRepositoryAsync = productRepository ?? throw new ArgumentNullException(nameof(IProductRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(IMapper));
        }

        public async Task<ApiResult<ProductViewModel>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepositoryAsync.GetByQueryFirstAsync(x => x.Title == request.Title && x.BrandId == request.BrandId);
            if (product == null)
            {
                return ApiResult<ProductViewModel>.CreateBadResponse("Product already exists.");
            }

            // create new product
            var newProduct = new Product
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                BrandId = request.BrandId
            };

            // save new product
            var createdProduct = await _productRepositoryAsync.AddAsync(newProduct);
            var mapResult = _mapper.Map<ProductViewModel>(createdProduct);
            return ApiResult<ProductViewModel>.CreateSuccessResponse(mapResult);
        }
    }
}

