using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MilkRun.ApplicationCore.Contracts;
using MilkRun.ApplicationCore.Contracts.ResponseModel;
using MilkRun.Infrastructure.Features.Products.Commands;
using MilkRun.Infrastructure.Features.Products.Queries;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MilkRun.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/product")]
    public class ProductController : BaseApiController
    {
        [HttpGet("query")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<ProductViewModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Get([FromQuery] GetProductsQuery query)
        {
            var result = await Mediator.Send(query);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // create product
        [HttpPost("create")]
        [ProducesResponseType(typeof(ApiResult<ProductViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            var result = await Mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);           
        }
        // update product
        [HttpPut("update")]
        [ProducesResponseType(typeof(ApiResult<ProductViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand command)
        {
            var result = await Mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}