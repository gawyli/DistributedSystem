using DistributedSystem.Product.Core.ProductAggregate.Commands;
using DistributedSystem.Product.Core.ProductAggregate.Queries;
using DistributedSystem.Shared.Core.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DistributedSystem.Product.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IRepository _repository;
    private readonly IMediator _mediator;

    public ProductController(IRepository repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }


    [HttpGet("/product/list")]
    public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
    {
        var products = await _mediator.Send(new ListProducts.Query(), cancellationToken);

        return new JsonResult(products);
    }

    [HttpGet("/product/get/id")]
    public async Task<IActionResult> GetProduct(string id, CancellationToken cancellationToken)
    {
        var products = await _mediator.Send(new GetProductById.Query(id), cancellationToken);

        return new JsonResult(products);
    }

    [HttpGet("/product/getSaleOffer/productId")]
    [SwaggerOperation(
        Summary = "Get Product with Sale Offer")]
    public async Task<IActionResult> GetProductSaleOffer(string id, CancellationToken cancellationToken)
    {
        try
        {
            var products = await _mediator.Send(new GetProductDetails.Query(id), cancellationToken);

            return new JsonResult(products);
        }
        catch (Exception ex)
        {

            throw new ArgumentNullException(ex.Message);
        }
        
    }

    //[HttpGet]
    //public async Task<IActionResult> GetProductByName(string name, CancellationToken cancellationToken)
    //{
    //    var products = await _repository.ListAsync<DistributedSystem.Product.Core.ProductAggregate.Product>(cancellationToken);

    //    return new JsonResult(products);
    //}


    [HttpPost("/product/create")]
    [SwaggerOperation(
        Summary = "Creates a Product")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProduct.Command command, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _mediator.Send(command, cancellationToken);
        

        return new JsonResult(result);
    }

    [HttpPost("/product/update/id")]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProduct.Command command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return new JsonResult(result);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }

    }

    [HttpPost("/product/setSaleOffer/productId")]
    public async Task<IActionResult> SetProductSaleOffer([FromBody] CreateSaleOffer.Command command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return new JsonResult(result);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }

    }

}
